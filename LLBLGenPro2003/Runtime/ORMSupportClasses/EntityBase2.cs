/////////////////////////////////////////////////////////////
// LLBLGen Pro ORM Support Classes Library
// (c) 2002-2003 Solutions Design. All rights reserved.
// http://www.llblgen.com
// http://www.sd.nl/llblgen
// 
// THIS IS NOT OPEN SOURCE SOFTWARE OF ANY KIND.
// SOURCECODE DISTRIBUTION IS NOT ALLOWED IN ANY WAY.
/////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Reflection;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// General Entity Base class, which is used to inherit the Entity classes from. Used in the Adapter template set.
	/// This entity does not have any persistence info on board.
	/// </summary>
	[Serializable]
	public abstract class EntityBase2 : IEntity2, ITransactionalElement, ISerializable
	{
		#region Class Member Declarations
		private IEntityFields2		_fields;
		private string				_name;
		private bool				_isNew, _isDeleted, _backupIsDeleted, _backupIsNew, _editCycleInProgress;
		private IValidator			_validator;
		private Hashtable			_relatedEntitySyncInfos;		// EntitySyncInfo objects stored in an Hashtable per ObjectID
		private Hashtable			_field2RelatedEntity;			// Fieldname (mapped on relation) - ObjectID combinations. Used to find back entries in _relatedEntitySyncInfos
		private Guid				_objectID;
		private IConcurrencyPredicateFactory	_concurrencyPredicateFactoryToUse;
		private IEntityValidator	_entityValidatorToUse;

		[NonSerialized]
		private IEntityFields2		 _backupFields;
		[NonSerialized]
		private bool				_pendingChangedEvent;
		[NonSerialized]
		private	ITransaction		_containingTransaction;
		[NonSerialized]
		private IEntityCollection2	_parentCollection;		// databinding related
		[NonSerialized]
		private	bool				_isNewViaDataBinding;	// databinding related
		[NonSerialized]
		private	bool				_isDeserializing;
		#endregion

		#region Event Handler Declarations
		/// <summary>
		/// Event handler declaration for the event that is fired each time the one of values of this entity are changed.
		/// The event does not contain the value / field which is changed, it only signals subscribers the entity is changed
		/// and the subscriber should act accordingly, f.e. fire a ListChanged event.
		/// </summary>
		public event EventHandler EntityContentsChanged;
		/// <summary>
		/// Event handler declaration for the event that is fired each time this entity is persisted. Related entities can subscribe to
		/// this event to start housekeeping actions, like syncing internal FK fields with the PK fields of this entity.
		/// </summary>
		public event EventHandler AfterSave;

		#endregion


		/// <summary>
		/// CTor
		/// </summary>
		public EntityBase2()
		{
			InitClass("");
		}


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="name">The full name for this entity, which is important for the DAO to find back persistence info for this entity.</param>
		public EntityBase2(string name)
		{
			InitClass(name);
		}


		/// <summary>
		/// Private CTor for deserialization
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected EntityBase2(SerializationInfo info, StreamingContext context)
		{
			try
			{
				_isDeserializing = true;
				_name=info.GetString("_name");
				_fields = (IEntityFields2)info.GetValue("_fields", typeof(IEntityFields2));
				_backupFields = null;
				_containingTransaction = null;
				_isNew = info.GetBoolean("_isNew");
				_backupIsNew = false;
				_isDeleted = info.GetBoolean("_isDeleted");
				_backupIsDeleted = false;
				_validator = (IValidator)info.GetValue("_validator", typeof(IValidator));
				_editCycleInProgress = false;
				_parentCollection=null;
				_isNewViaDataBinding=false;
				_objectID = (Guid)info.GetValue("_objectID", typeof(Guid));
				_relatedEntitySyncInfos = (Hashtable)info.GetValue("_relatedEntitySyncInfos", typeof(Hashtable));
				_field2RelatedEntity = (Hashtable)info.GetValue("_field2RelatedEntity", typeof(Hashtable));
				_concurrencyPredicateFactoryToUse = (IConcurrencyPredicateFactory)info.GetValue("_concurrencyPredicateFactoryToUse", typeof(IConcurrencyPredicateFactory));
				_entityValidatorToUse = (IEntityValidator)info.GetValue("_entityValidatorToUse", typeof(IEntityValidator));
			}
			finally
			{
				_isDeserializing = false;
			}
		}


		/// <summary>
		/// Method which will fire the AfterSave event to signal that this entity is persisted and refetched succesfully.
		/// </summary>
		public void FlagAsSaved()
		{
			if(AfterSave!=null)
			{
				// fire the AfterSave event.
				AfterSave(this, new EventArgs());
			}
		}


		/// <summary>
		/// Overrides the GetHashCode routine. It will calculate a hashcode for this entity using the eXclusive OR of the 
		/// hashcodes of the primary key fields in this entity. That hashcode is returned. If no primary key fields are present,
		/// the hashcode of the base class is returned, which will not be unique.
		/// </summary>
		/// <returns>Hashcode for this entity object, based on its primary key field values</returns>
		public override int GetHashCode()
		{
			return ((EntityFields2)_fields).GetHashCode();
		}


		/// <summary>
		/// Converts this entity to XML, recursively. Uses the LLBLGenProEntityName for the rootnode name
		/// </summary>
		/// <param name="entityXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		public void WriteXml(out string entityXml)
		{
			WriteXml(_name, out entityXml);
		}


		/// <summary>
		/// Converts this entity to XML, recursively. Uses the LLBLGenProEntityName for the rootnode name
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		public void WriteXml(XmlDocument parentDocument, out XmlNode entityNode)
		{
			WriteXml(_name, parentDocument, out entityNode);
		}


		/// <summary>
		/// Converts this entity to XML, recursively. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="entityXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		public void WriteXml(string rootNodeName, out string entityXml)
		{
			XmlNode entityNode = null;
			WriteXml(rootNodeName, new XmlDocument(), out entityNode);
			entityXml = entityNode.OuterXml;
		}


		/// <summary>
		/// Converts this entity to XML, recursively. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		public virtual void WriteXml(string rootNodeName, XmlDocument parentDocument, out XmlNode entityNode)
		{
			Entity2Xml(rootNodeName, parentDocument, new Hashtable(), out entityNode);
		}


		/// <summary>
		/// Produces the actual XML for this entity, recursively. Because it recurses through referenced entities, it keeps track of which objects are processed
		/// so cyclic references are not resulting in cyclic recursion and thus a crash. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="processedObjectIDs">Hashtable with ObjectIDs of all the objects already processed. If this entity's ObjectID is in the
		/// hashtable's key list, a ProcessedObjectReference tag is emitted and the routine simply returns. </param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		internal void Entity2Xml(string rootNodeName, XmlDocument parentDocument, Hashtable processedObjectIDs, out XmlNode entityNode)
		{
			XmlHelper nodeCreator = new XmlHelper();

			if(processedObjectIDs.ContainsKey(_objectID))
			{
				// already processed. Simply create a ProcessedObjectReference with the ObjectID and return.
				entityNode = parentDocument.CreateNode(XmlNodeType.Element, "ProcessedObjectReference", "");
				nodeCreator.AddAttribute(entityNode, "ObjectID", _objectID.ToString());

				// done.
				return;
			}
			entityNode = parentDocument.CreateNode(XmlNodeType.Element, rootNodeName, "");
			// add ourselves to the processedObjectIDs
			processedObjectIDs.Add(_objectID, null);

			// add PK fields as attributes.
			for(int i=0;i<_fields.PrimaryKeyFields.Count;i++)
			{
				IEntityField2 primaryKeyField = (IEntityField2)(_fields.PrimaryKeyFields[i]);
				nodeCreator.AddAttribute(entityNode, primaryKeyField.Name, primaryKeyField.CurrentValue.ToString());
			}

			// add assembly as attribute
			nodeCreator.AddAttribute(entityNode, "Assembly", this.GetType().Assembly.FullName);
			nodeCreator.AddAttribute(entityNode, "Type", this.GetType().FullName);

			// create hashtable with names of all fields inside this entity
			Hashtable fieldNames = new Hashtable(_fields.Count);
			for (int i = 0; i < _fields.Count; i++)
			{
				fieldNames.Add(_fields[i].Name, null);
			}

			// get properties of this IEntity2 implementing object
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);
			for (int i = 0; i < properties.Count; i++)
			{
				if(properties[i].Attributes.Contains(new XmlIgnoreAttribute()))
				{
					// ignore this property
					continue;
				}

				// check if this property is part of the Fields collection. If so, skip it, because it is handled by the Fields property
				if(fieldNames.ContainsKey(properties[i].Name))
				{
					// field, continue
					continue;
				}

				if(properties[i].PropertyType.IsInterface)
				{
					// check for Fields
					if(properties[i].PropertyType.Equals(typeof(IEntityFields2)))
					{
						// .Fields property
						XmlNode entityFieldsNode = nodeCreator.AddNode(entityNode, "Fields");

						// add the fields
						_fields.WriteXml(parentDocument, entityFieldsNode);
						// done with this property
						continue;
					}

					// check for Validator
					if(properties[i].PropertyType.Equals(typeof(IValidator)))
					{
						// .Validator property
						XmlNode validatorNode = nodeCreator.AddNode(entityNode, "Validator");
						IValidator validator = properties[i].GetValue(this) as IValidator;
						if(validator==null)
						{
							nodeCreator.AddAttribute(validatorNode, "Assembly", "Unknown");
						}
						else
						{
							nodeCreator.AddAttribute(validatorNode, "Assembly", validator.GetType().Assembly.FullName);
							nodeCreator.AddAttribute(validatorNode, "Type", validator.GetType().FullName);
						}
						// done with this property
						continue;
					}

					// check for EntityValidator
					if(properties[i].PropertyType.Equals(typeof(IEntityValidator)))
					{
						// .EntityValidator property
						XmlNode entityValidatorNode = nodeCreator.AddNode(entityNode, "EntityValidator");
						IEntityValidator entityValidator = properties[i].GetValue(this) as IEntityValidator;
						if(entityValidator==null)
						{
							nodeCreator.AddAttribute(entityValidatorNode, "Assembly", "Unknown");
						}
						else
						{
							nodeCreator.AddAttribute(entityValidatorNode, "Assembly", entityValidator.GetType().Assembly.FullName);
							nodeCreator.AddAttribute(entityValidatorNode, "Type", entityValidator.GetType().FullName);
						}
						// done with this property
						continue;
					}

					// check for ConcurrencyPredicateFactory
					if(properties[i].PropertyType.Equals(typeof(IConcurrencyPredicateFactory)))
					{
						// .ConcurrencyPredicateFactory property
						XmlNode concurrencyPredicateFactoryNode = nodeCreator.AddNode(entityNode, "ConcurrencyPredicateFactory");
						IConcurrencyPredicateFactory cpFactory = properties[i].GetValue(this) as IConcurrencyPredicateFactory;
						if(cpFactory==null)
						{
							nodeCreator.AddAttribute(concurrencyPredicateFactoryNode, "Assembly", "Unknown");
						}
						else
						{
							nodeCreator.AddAttribute(concurrencyPredicateFactoryNode, "Assembly", cpFactory.GetType().Assembly.FullName);
							nodeCreator.AddAttribute(concurrencyPredicateFactoryNode, "Type", cpFactory.GetType().FullName);
						}
						// done with this property
						continue;
					}
				}

				// get all interfaces of the type of this property
				Type[] propertyInterfaces = properties[i].PropertyType.GetInterfaces();

				bool propertyHandled = false;
				for(int j=0;j<propertyInterfaces.Length;j++)
				{
					// Use waterfall method, check using Equals.
					if(propertyInterfaces[j].Equals(typeof(IEntity2)))
					{
						// a related Entity property which references an entity related to this entity. 
						XmlNode propertyNode = nodeCreator.AddNode(entityNode, "EntityReference");
						nodeCreator.AddAttribute(propertyNode, "PropertyName", properties[i].Name);

						XmlNode refEntityNode = null;
						EntityBase2 referencedEntity = properties[i].GetValue(this) as EntityBase2;
						if(referencedEntity!=null)
						{
							referencedEntity.Entity2Xml(referencedEntity.LLBLGenProEntityName, parentDocument, processedObjectIDs, out refEntityNode);
							propertyNode.AppendChild(refEntityNode);
						}

						propertyHandled = true;
						break;
					}

					if(propertyInterfaces[j].Equals(typeof(IEntityCollection2)))
					{
						// a related entity collection property which references an EntityCollectionBase2 derived class with entities related to this entity.
						XmlNode propertyNode = nodeCreator.AddNode(entityNode, "EntityCollectionReference");
						nodeCreator.AddAttribute(propertyNode, "PropertyName", properties[i].Name);
						
						XmlNode refEntityCollectionNode = null;
						EntityCollectionBase2 referencedEntityCollection = properties[i].GetValue(this) as EntityCollectionBase2;
						if(referencedEntityCollection!=null)
						{
							referencedEntityCollection.EntityCollection2Xml(properties[i].Name, parentDocument, processedObjectIDs, out refEntityCollectionNode);
							propertyNode.AppendChild(refEntityCollectionNode);
						}

						propertyHandled = true;
						break;
					}
				}

				if(propertyHandled)
				{
					continue;
				}

				// Normal not yet handled property. Add it.
				XmlNode childNode = nodeCreator.AddNode(entityNode, properties[i].Name);
				string valueTypeName = properties[i].PropertyType.UnderlyingSystemType.FullName.ToString();
				nodeCreator.AddAttribute(childNode, "Type", valueTypeName);

				string valueAsString = String.Empty;
				switch(valueTypeName)
				{
					case "System.String":
					case "System.Byte":
					case "System.Int32":
					case "System.Int16":
					case "System.Int64":
					case "System.Decimal":
					case "System.Double":
					case "System.Single":
					case "System.Boolean":
					case "System.Guid":
						valueAsString = properties[i].GetValue(this).ToString();
						break;
					case "System.DateTime":
						valueAsString = ((DateTime)properties[i].GetValue(this)).Ticks.ToString();
						break;
					case "System.Byte[]":
						// special case, base64encode it
						valueAsString = Convert.ToBase64String((byte[])properties[i].GetValue(this));
						break;
					default:
						object value = properties[i].GetValue(this);
						if(value!=null)
						{
							valueAsString = value.ToString();
						}
						else
						{
							valueAsString = "";
						}
						break;
				}
				childNode.AppendChild(parentDocument.CreateTextNode(valueAsString));
			}

			// add info nodes
			XmlNode isDirtyNode = nodeCreator.AddNode(entityNode, "IsDirty", _fields.IsDirty.ToString());
			nodeCreator.AddAttribute(isDirtyNode, "Type", "System.Boolean");

			XmlNode entityStateNode = nodeCreator.AddNode(entityNode, "EntityState", _fields.State.ToString());
			nodeCreator.AddAttribute(entityStateNode, "Type", _fields.State.GetType().FullName.ToString());
		}


		/// <summary>
		/// Will fill the entity and its containing members (recursively) with the data stored in the Xml string passed in. The string xmlData has to
		/// be filled with Xml in the format written by IEntity2.WriteXml() and the Xml has to be compatible with the structure of this entity.
		/// </summary>
		/// <param name="xmlData">string with Xml data which should be read into this entity and its members. This string has to be in the
		/// correct format and should be loadable into a new XmlDocument without problems</param>
		public void ReadXml(string xmlData)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xmlData);
			ReadXml(doc.DocumentElement);
		}


		/// <summary>
		/// Will fill the entity and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntity2.WriteXml() and the Xml has to be compatible with the structure of this entity.
		/// </summary>
		/// <param name="node">XmlNode with Xml data which should be read into this entity and its members. Node's root element is the root element
		/// of the entity's Xml data</param>
		public virtual void ReadXml(XmlNode node)
		{
			ArrayList nodeEntityReferences = new ArrayList();
			Hashtable processedObjectIDs = new Hashtable();
			Xml2Entity(node, processedObjectIDs, nodeEntityReferences);

			// walk all references found and set them to the correct objects.
			XmlHelper.SetReadReferences(nodeEntityReferences, processedObjectIDs);
		}


		/// <summary>
		/// Performs the actual conversion from Xml to entity data. 
		/// </summary>
		/// <param name="node">current node which points to an entity node.</param>
		/// <param name="processedObjectIDs">ObjectID's of all entities instantiated</param>
		/// <param name="nodeEntityReferences">Arraylist with all the references to entity objects we probably do not yet have instantiated. This list
		/// is traversed after the xml tree has been processed. (not done by this routine, but by the caller)</param>
		internal void Xml2Entity(XmlNode node, Hashtable processedObjectIDs, ArrayList nodeEntityReferences)
		{
			try
			{
				_isDeserializing = true;
				XmlHelper typeConverter = new XmlHelper();

				// get this instance's properties.
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);

				// first walk the subnodes and process only the direct childs, skipping entity collections and entity references.
				foreach(XmlNode currentElement in node.ChildNodes)
				{
					switch(currentElement.Name)
					{
						// filter out special cases, the rest is handled by the default section.
						case "EntityCollectionReference":
							// create a new instance.
							XmlNode entityCollectionNode = currentElement.FirstChild;
							// get a reference to the collection object.
							EntityCollectionBase2 referencedEntityCollection = (EntityCollectionBase2)properties[currentElement.Attributes["PropertyName"].Value].GetValue(this);
							bool readonlySave = referencedEntityCollection.IsReadOnly;
							referencedEntityCollection.IsReadOnly = false;
							try
							{
								referencedEntityCollection.Xml2EntityCollection(entityCollectionNode, processedObjectIDs, nodeEntityReferences);
							}
							finally
							{
								referencedEntityCollection.IsReadOnly = readonlySave;
							}
							break;
						case "EntityReference":
							// first test if this node is empty
							if(currentElement.ChildNodes.Count<=0)
							{
								// is empty
								continue;
							}

							// if this node contains an entity reference, add a new entity reference to the arraylist. If it contains
							// a full object in Xml, deserialize that object.
							// find 'ProcessedObjectReference' subnode, if present.
							XmlNode referenceNode = currentElement.SelectSingleNode("ProcessedObjectReference");
							if(referenceNode!=null)
							{
								// reference node found. Add reference.
								NodeEntityReference newReference = new NodeEntityReference();
								newReference.ObjectID = new Guid(referenceNode.Attributes["ObjectID"].Value);
								newReference.PropertyHoldingInstance = this;
								newReference.IsCollectionAdd=false;
								newReference.ReferencingProperty = properties[currentElement.Attributes["PropertyName"].Value];
								nodeEntityReferences.Add(newReference);
								// done with this node.
								continue;
							}
							// not a reference, instantiate the full node if a full node is present
							XmlNode entityNode = currentElement.FirstChild;

							// get type and assembly for entity instance.
							string entityAssemblyName = entityNode.Attributes["Assembly"].Value;
							string entityTypeName = entityNode.Attributes["Type"].Value;
							// load assembly
							Assembly entityAssembly = Assembly.Load(entityAssemblyName);
							// create instance
							EntityBase2 referencedEntity = (EntityBase2)entityAssembly.CreateInstance(entityTypeName);
							referencedEntity.IsDeserializing=true;
							try
							{
								// convert this entity's xml into data inside this entity
								referencedEntity.Xml2Entity(entityNode, processedObjectIDs, nodeEntityReferences);
								properties[currentElement.Attributes["PropertyName"].Value].SetValue(this, referencedEntity);
							}
							finally
							{
								referencedEntity.IsDeserializing=false;
							}
							break;
						case "Validator":
							string validatorAssemblyName = currentElement.Attributes["Assembly"].Value;
							if(validatorAssemblyName=="Unknown")
							{
								// no validator set nor serialized
								continue;
							}
							Assembly validatorAssembly = Assembly.Load(validatorAssemblyName);
							string validatorClassType = currentElement.Attributes["Type"].Value;
							this.Validator = (IValidator)validatorAssembly.CreateInstance(validatorClassType);
							break;
						case "EntityValidator":
							string entityValidatorAssemblyName = currentElement.Attributes["Assembly"].Value;
							if(entityValidatorAssemblyName=="Unknown")
							{
								// no entity validator set nor serialized
								continue;
							}
							Assembly entityValidatorAssembly = Assembly.Load(entityValidatorAssemblyName);
							string entityValidatorClassType = currentElement.Attributes["Type"].Value;
							this.EntityValidatorToUse = (IEntityValidator)entityValidatorAssembly.CreateInstance(entityValidatorClassType);
							break;
						case "ConcurrencyPredicateFactory":
							string cpFactoryAssemblyName = currentElement.Attributes["Assembly"].Value;
							if(cpFactoryAssemblyName=="Unknown")
							{
								// no factory object set nor serialized
								continue;
							}
							Assembly cpFactoryAssembly = Assembly.Load(cpFactoryAssemblyName);
							string cpFactoryClassType = currentElement.Attributes["Type"].Value;
							this.ConcurrencyPredicateFactoryToUse = (IConcurrencyPredicateFactory)cpFactoryAssembly.CreateInstance(cpFactoryClassType);
							break;
						case "Fields":
							// get all child nodes, 1 node per field
							XmlNodeList fieldNodes = currentElement.ChildNodes;
							for (int i = 0; i < fieldNodes.Count; i++)
							{
								XmlNode currentValueNode = fieldNodes[i].SelectSingleNode("CurrentValue");
								string currentValueTypeName = currentValueNode.Attributes["Type"].Value;
								string currentValueAsString = currentValueNode.InnerText;
								this.Fields[fieldNodes[i].Name].ForcedCurrentValueWrite(typeConverter.XmlValueToObject(currentValueTypeName, currentValueAsString));
								XmlNode isNullNode = fieldNodes[i].SelectSingleNode("IsNull");
								this.Fields[fieldNodes[i].Name].IsNull = (bool)typeConverter.XmlValueToObject("System.Boolean", isNullNode.InnerText);
								XmlNode isChangedNode = fieldNodes[i].SelectSingleNode("IsChanged");
								((EntityField2)this.Fields[fieldNodes[i].Name]).ForcedChangedWrite((bool)typeConverter.XmlValueToObject("System.Boolean", isChangedNode.InnerText));
							}
							break;
						case "IsDirty":
							this.Fields.IsDirty = Convert.ToBoolean(currentElement.InnerText);
							break;
						case "EntityState":
							string entityState = currentElement.InnerText;
							switch(entityState)
							{
								case "Deleted":
									this.Fields.State = EntityState.Deleted;
									break;
								case "Fetched":
									this.Fields.State = EntityState.Deleted;
									break;
								case "New":
									this.Fields.State = EntityState.New;
									break;
								case "OutOfSync":
									this.Fields.State = EntityState.OutOfSync;
									break;
							}
							break;
						default:
							// custom property, not a field.
							string elementTypeName = currentElement.Attributes["Type"].Value;
							string xmlValue = currentElement.InnerText;
							properties[currentElement.Name].SetValue(this, typeConverter.XmlValueToObject(elementTypeName, xmlValue));
							break;
					}
				}

				// add the ObjectID of this object, which is now read from XML, to the hashtable
				processedObjectIDs.Add(_objectID, this);
			}
			finally
			{
				_isDeserializing = false;
			}
		}


		/// <summary>
		/// Routine which will flag all subscribers of the EntityContentsChanged event that this entity's contents is changed.
		/// </summary>
		public void FlagMeAsChanged()
		{
			if(EntityContentsChanged!=null)
			{
				EntityContentsChanged(this, new EventArgs());
			}
		}


		/// <summary>
		/// Will reject (and thus roll back) all changes made to the entity's EntityFields. It rolls back to the initial version. 
		/// </summary>
		public void RejectChanges()
		{
			if(_fields!=null)
			{
				_fields.RejectChanges();
			}
		}


		/// <summary>
		/// When the <see cref="ITransaction"/> in which this IEntity2 participates is commited, this IEntity2 can succesfully finish actions performed by this
		/// IEntity2. This method is called by <see cref="ITransaction"/>, you should not call it by yourself. When this IEntity2 doesn't participate in a
		/// transaction it finishes the actions itself, calling this method is not needed.
		/// </summary>
		public void TransactionCommit()
		{
			_isNew = false;
			_fields.AcceptChanges();
			_fields.State = EntityState.OutOfSync;
			_backupFields = null;
		}


		/// <summary>
		/// When the <see cref="ITransaction"/> in which this IEntity2 participates is rolled back, this IEntity2 has to roll back its internal variables.
		/// This method is called by <see cref="ITransaction"/>, you should not call it by yourself. 
		/// </summary>
		public void TransactionRollback()
		{
			_fields = _backupFields;
			_isDeleted = _backupIsDeleted;
			_isNew = _backupIsNew;
			_backupFields = null;
		}


		/// <summary>
		/// Compares passed in object with the given object. This is a compare of PK fields. These have to be the same in VALUES. 
		/// When the values are not the same, or the type is not the same as the current type, false is returned, true otherwise.
		/// When this doesn't have any PK fields, all fields are compared. null values are considered as the same value. 
		/// </summary>
		/// <param name="obj">IEntity2 implementing object of the same type as this which will be compared to the PK values of this. </param>
		/// <returns>True when the PK values of this are the same as the PK values of obj, or when this doesn't have any PK fields, all fields
		/// have the same value as obj's fields. False otherwise.</returns>
		/// <remarks>If this entity or the passed in entity is new, no values are compared, but the physical objects are compared (object.Equals()),
		/// because new entities can look the same, value wise due to identity fields which are all 0, however which are physical different entities 
		/// (object wise)</remarks>
		public override bool Equals(object obj)
		{
			IEntity2 passedIn = obj as IEntity2;
			if(passedIn==null)
			{
				// not the same type, not identical.
				return false;
			}

			if((_fields==null)||(passedIn.Fields==null))
			{
				return false;
			}

			// new entities are always different. If one of the two (this, or passed in object) is new, they have to be tested using reference testing.
			// if that fails, they're not the same. New entities can't be compared using field values
			if(_isNew || passedIn.IsNew)
			{
				// one or both is new, use instance compare.
				return (this==passedIn);
			}

			return ((EntityFields2)_fields).Equals(passedIn.Fields);
		}


		/// <summary>
		/// ISerializable member. Does custom serialization so event handlers do not get serialized.
		/// </summary>
		/// <param name="info">See ISerializable</param>
		/// <param name="context">See ISerialilzable</param>
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_name", _name);
			info.AddValue("_fields", _fields);
			info.AddValue("_isNew", _isNew);
			info.AddValue("_isDeleted", _isDeleted);
			info.AddValue("_validator", _validator);
			info.AddValue("_objectID", _objectID);
			info.AddValue("_relatedEntitySyncInfos", _relatedEntitySyncInfos);
			info.AddValue("_field2RelatedEntity", _field2RelatedEntity);
			info.AddValue("_concurrencyPredicateFactoryToUse", _concurrencyPredicateFactoryToUse);
			info.AddValue("_entityValidatorToUse", _entityValidatorToUse);
		}


		/// <summary>
		/// Creates the requested predicate of the type specified. If no IConcurrencyPredicateFactory instance is stored in this entity instance, null
		/// is returned.
		/// </summary>
		/// <param name="predicateTypeToCreate">The type of predicate to create</param>
		/// <returns>A ready to use predicate to use in the query to execute, or null/Nothing if no IConcurrencyPredicateFactory instance is present, 
		/// in which case the predicate is ignored</returns>
		public virtual IPredicateExpression GetConcurrencyPredicate(ConcurrencyPredicateType predicateTypeToCreate)
		{
			if(_concurrencyPredicateFactoryToUse!=null)
			{
				return _concurrencyPredicateFactoryToUse.CreatePredicate(predicateTypeToCreate, this);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Gets the current value of the EntityField2 with the index fieldIndex.
		/// </summary>
		/// <param name="fieldIndex">Index of EntityField2 to get the current value of</param>
		/// <returns>The current value of the EntityField2 specified</returns>
		/// <exception cref="ORMEntityOutOfSyncException">When the entity is out of sync and needs to be refetched first.</exception>
		/// <exception cref="ORMEntityIsDeletedException">When the entity is marked as deleted.</exception>
		/// <exception cref="ArgumentOutOfRangeException">When fieldIndex is smaller than 0 or bigger than the amount of fields in the fields collection.</exception>
		public object GetCurrentFieldValue(int fieldIndex)
		{
			if(_fields.State==EntityState.OutOfSync)
			{
				throw new ORMEntityOutOfSyncException("The entity is out of sync with its data in the database. Refetch this entity before using this in-memory instance.");
			}

			if(_isDeleted)
			{
				throw new ORMEntityIsDeletedException("This entity is deleted from the database and can't be used in logic.");
			}

			if((fieldIndex<0)||(fieldIndex>=_fields.Count))
			{
				throw new ArgumentOutOfRangeException("fieldIndex", fieldIndex, "The field index passed is not a valid index in the fields collection of this entity.");
			}

			if(_fields!=null)
			{
				return _fields[fieldIndex].CurrentValue;
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Sets the EntityField with the name fieldName to the new value value. Marks also the entityfields as dirty. Will refetch the complete entity's fields
		/// from the database if necessary (i.e. the entity is outofsync.).
		/// </summary>
		/// <param name="fieldName">Name of EntityField to set the new value of</param>
		/// <param name="value">Value to set</param>
		/// <returns>true if the value is actually set, false otherwise.</returns>
		/// <exception cref="ORMValueTypeMismatchException">The value specified is not of the same IEntityField.DataType as the field.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The value specified has a size that is larger than the maximum size defined for the related column in the databas</exception>
		public  bool SetNewFieldValue(string fieldName, object value)
		{
			return SetNewFieldValue(_fields[fieldName].FieldIndex, value);
		}


		/// <summary>
		/// Sets the EntityField2 on index fieldIndex to the new value value. Marks also the entityfields2 as dirty. 
		/// </summary>
		/// <param name="fieldIndex">Index of EntityField2 to set the new value of</param>
		/// <param name="value">Value to set</param>
		/// <returns>true if the value is actually set, false otherwise.</returns>
		/// <exception cref="ArgumentOutOfRangeException">When fieldIndex is smaller than 0 or bigger than the amount of fields in the fields collection.</exception>
		public  bool SetNewFieldValue(int fieldIndex, object value)
		{
			if((fieldIndex<0)||(fieldIndex>=_fields.Count))
			{
				throw new ArgumentOutOfRangeException("fieldIndex", fieldIndex, "The field index passed is not a valid index in the fields collection of this entity.");
			}

			bool valueIsSet = false;
			if(_fields!=null)
			{
				IEntityField2 fieldToSet = _fields[fieldIndex];
				if(ValidateValue(fieldToSet, value, fieldIndex))
				{
					if(_editCycleInProgress)
					{
						// do not control the editing of the field's value with the field's edit cycle routines.
						fieldToSet.CurrentValue = value;
						_fields.IsChangedInThisEditCycle = true;
						_fields.IsDirty=true;
						valueIsSet = true;
					}
					else
					{
						try
						{
							fieldToSet.BeginEdit();
							fieldToSet.CurrentValue = value;
							fieldToSet.EndEdit();
							_fields.IsDirty=true;
							valueIsSet = true;
						}
						catch
						{
							fieldToSet.CancelEdit();
							throw;
						}
					}
				}
			}

			if(valueIsSet)
			{
				if((!_editCycleInProgress)||(_editCycleInProgress && !_isNew)||(_editCycleInProgress && _parentCollection==null))
				{
					// fire the EntityContentsChanged event, if there are subscribers. 
					FlagMeAsChanged();
				}
				else
				{
					// edit cycle in progress, hold the signal till endedit is called
					_pendingChangedEvent = true;
				}
			}

			return valueIsSet;
		}


		/// <summary>
		/// Validates the entity by calling a set IEntityValidator implementing object's Validate() method. If no IEntityValidator object is set
		/// true is returned.
		/// </summary>
		/// <returns>The result of IEntityValidator.Validate(this).</returns>
		/// <remarks>Called by save logic.</remarks>
		/// <exception cref="ORMEntityValidationException">If validation fails</exception>
		public bool Validate()
		{
			if(_entityValidatorToUse!=null)
			{
				return _entityValidatorToUse.Validate(this);
			}
			else
			{
				return true;
			}
		}


		/// <summary>
		/// Sets the internal parameter related to the fieldname passed to the instance relatedEntity. 
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		public abstract void SetRelatedEntity(IEntity2 relatedEntity, string fieldName);

		/// <summary>
		/// Unsets the internal parameter related to the fieldname passed to the instance relatedEntity. Reverses the actions taken by SetRelatedEntity() 
		/// </summary>
		/// <param name="relatedEntity">Instance to unset as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		public abstract void UnsetRelatedEntity(IEntity2 relatedEntity, string fieldName);

		/// <summary>
		/// Gets a collection of related entities referenced by this entity which depend on this entity (this entity is the PK side of their FK fields). These
		/// entities will have to be persisted after this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity2 objects, referenced by this entity</returns>
		public abstract IEntityCollection2 GetDependingRelatedEntities();
		/// <summary>
		/// Gets a collection of related entities referenced by this entity which this entity depends on (this entity is the FK side of their PK fields). These
		/// entities will have to be persisted before this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity2 objects, referenced by this entity</returns>
		public abstract IEntityCollection2 GetDependentRelatedEntities();


		/// <summary>
		/// Gets an ArrayList of all entity collections stored as member variables in this entity. The contents of the ArrayList is
		/// used by the DataAccessAdapter to perform recursive saves. Only 1:n related collections are returned.
		/// </summary>
		/// <returns>Collection with 0 or more IEntityCollection2 objects, referenced by this entity</returns>
		public abstract ArrayList GetMemberEntityCollections();


		/// <summary>
		/// Synchronizes the PK values of the dataSupplier with the related FK values of this entity. 
		/// </summary>
		/// <param name="relation">the entity relation object which defines the relation between the dataSupplier entity and this entity.</param>
		/// <param name="dataSupplier">the entity related to this entity and which has the PK values for one or more FK fields in this entity.</param>
		protected virtual void SyncFKFields(IEntityRelation relation, IEntity2 dataSupplier)
		{
			if(dataSupplier==null)
			{
				// nothing to sync
				return;
			}

			// walk the fields in the relation and store data from the dataSupplier into the fields of this entity.
			for (int i = 0; i < relation.AmountFields; i++)
			{
				if(! (_fields[relation.GetFKEntityFieldCore(i).Name].IsPrimaryKey && !_isNew) )
				{
					// we're syncing a field which is not a PK or a PK field and we're new
					string fkFieldName = relation.GetFKEntityFieldCore(i).Name;
					string pkFieldName = relation.GetPKEntityFieldCore(i).Name;
					bool setValue = true;
					if(_fields[fkFieldName].CurrentValue!=null)
					{
						setValue = !_fields[fkFieldName].CurrentValue.Equals(dataSupplier.Fields[pkFieldName].CurrentValue);
					}
					if(setValue)
					{
						_fields[fkFieldName].CurrentValue = dataSupplier.Fields[pkFieldName].CurrentValue;
						// set IsDirty flag
						_fields.IsDirty=true;
					}
				}
			}
		}


		/// <summary>
		/// Will retrieve all stored entity synchronization information for the passed in entity. If no information is
		/// stored, an empty hashtable is returned. All sync info is stored by fieldname
		/// </summary>
		/// <param name="relatedEntity">related entity to retrieve the sync info for</param>
		/// <returns>Hashtable with the sync info, stored per fieldname, set for this entity</returns>
		protected virtual Hashtable GetEntitySyncInformation(IEntity2 relatedEntity)
		{
			if(_relatedEntitySyncInfos.ContainsKey(relatedEntity.ObjectID))
			{
				return (Hashtable)_relatedEntitySyncInfos[relatedEntity.ObjectID];
			}
			else
			{
				// not found.
				return new Hashtable();
			}
		}


		/// <summary>
		/// Will unset (remove) the passed in information as Entity sync information. If there is no sync information stored for the related entity
		/// after this info has been removed, the complete hashentry is removed.
		/// </summary>
		/// <param name="fieldName">Name of field of this entity mapped onto passed in relation</param>
		/// <param name="relatedEntity">related entity set as value for field with name fieldName</param>
		/// <param name="relation">EntityRelation object which is the relation between this entity and the passed in relatedEntity</param>
		protected virtual void UnsetEntitySyncInformation(string fieldName, IEntity2 relatedEntity, IEntityRelation relation)
		{
			Hashtable entitySyncInfos = null;

			if(!_field2RelatedEntity.ContainsKey(fieldName))
			{
				// no, nothing to unset
				return;
			}

			Guid setRelatedEntityObjectID = (Guid)_field2RelatedEntity[fieldName];

			if( setRelatedEntityObjectID == relatedEntity.ObjectID)
			{
				// this entity is set as the related entity. remove sync info
				entitySyncInfos = (Hashtable)_relatedEntitySyncInfos[setRelatedEntityObjectID];
				// remove the sync info for the field
				entitySyncInfos.Remove(fieldName);
				// remove fieldname - objectid
				_field2RelatedEntity.Remove(fieldName);
			}
		}


		/// <summary>
		/// Will set the passed in information as Entity sync information. If there is no sync information stored yet for the related entity
		/// then a new entry is created, otherwise it's info is added to the sync information of this entity, if it isn't already present.
		/// If there is already sync information for this field stored for another related entity, that information is removed.
		/// </summary>
		/// <param name="fieldName">Name of field of this entity mapped onto passed in relation</param>
		/// <param name="relatedEntity">related entity set as value for field with name fieldName</param>
		/// <param name="relation">EntityRelation object which is the relation between this entity and the passed in relatedEntity</param>
		protected virtual void SetEntitySyncInformation(string fieldName, IEntity2 relatedEntity, IEntityRelation relation)
		{
			Hashtable entitySyncInfos = null;

			// first check if there is already sync info for this fieldname
			if(_field2RelatedEntity.ContainsKey(fieldName))
			{
				// yes. Check if it's about the passed in entity
				Guid setRelatedEntityObjectID = (Guid)_field2RelatedEntity[fieldName];
				if(relatedEntity==null)
				{
					// no, remove the sync info for this entity for the field-relation combination
					entitySyncInfos = (Hashtable)_relatedEntitySyncInfos[setRelatedEntityObjectID];
					// remove the sync info for the field
					entitySyncInfos.Remove(fieldName);
					// remove fieldname - objectid
					_field2RelatedEntity.Remove(fieldName);
					// done
					return;
				}

				if( setRelatedEntityObjectID != relatedEntity.ObjectID)
				{
					// no, remove the sync info for this entity for the field-relation combination
					entitySyncInfos = (Hashtable)_relatedEntitySyncInfos[setRelatedEntityObjectID];
					// remove the sync info for the field
					entitySyncInfos.Remove(fieldName);
					// remove fieldname - objectid
					_field2RelatedEntity.Remove(fieldName);
					// continue with routine as normal. 
				}
				else
				{
					// already stored. simply return
					return;
				}
			}

			// check if there is already a bucket for sync infos for this entity
			if(_relatedEntitySyncInfos.ContainsKey(relatedEntity.ObjectID))
			{
				// yes.
				entitySyncInfos = (Hashtable)_relatedEntitySyncInfos[relatedEntity.ObjectID];
			}
			else
			{
				// no
				entitySyncInfos = new Hashtable();
				_relatedEntitySyncInfos.Add(relatedEntity.ObjectID, entitySyncInfos);
			}

			// there is no sync info for this field present, add the combi first
			_field2RelatedEntity.Add(fieldName, relatedEntity.ObjectID);

			// add sync info
			EntitySyncInfo newSyncInfo = new EntitySyncInfo();
			newSyncInfo.DataSupplyingEntity = relatedEntity;
			newSyncInfo.Relation = relation;
			entitySyncInfos.Add(fieldName, newSyncInfo);
		}


		/// <summary>
		/// Initializes the class' internals
		/// </summary>
		/// <param name="name"></param>
		private void InitClass(string name)
		{
			_name = name;
			_fields = null;
			_backupFields = null;
			_containingTransaction = null;
			_isNew = false;
			_isDeleted = false;
			_backupIsNew = false;
			_backupIsDeleted = false;
			_validator = null;
			_editCycleInProgress = false;
			_parentCollection=null;
			_isNewViaDataBinding=false;
			_pendingChangedEvent = false;
			_objectID = Guid.NewGuid();
			_isDeserializing = false;
			_relatedEntitySyncInfos = new Hashtable();
			_field2RelatedEntity = new Hashtable();
			_concurrencyPredicateFactoryToUse = null;
			_entityValidatorToUse = null;
		}


		/// <summary>
		/// Validates the input variable if it is a valid value for the target table field related to the passed in EntityField2 fieldToValidate.
		/// Primary keys can't be updated when the entity isn't a new entity. If value is null/nothing, true is returned.
		/// </summary>
		/// <param name="fieldToValidate">EntityField2 which is the destination of the value to validate</param>
		/// <param name="value">Value to validate</param>
		/// <param name="fieldIndex">The index of ValueDestination in the EntityFields2 array.</param>
		/// <returns>true if the value is valid, false otherwise</returns>
		/// <exception cref="ORMValueTypeMismatchException">The value specified is not of the same IEntityField2.DataType as ValueDestination field.</exception>
		private bool ValidateValue(IEntityField2 fieldToValidate, object value, int fieldIndex)
		{
			// Primary key fields can't be updated when this entity isn't a new entity
			if(fieldToValidate.IsPrimaryKey && !_isNew)
			{
				// can't update a primary key
				throw new ORMFieldIsReadonlyException("The field '" + fieldToValidate.Name + "' is part of a Primary Key and can't be updated after it is created in the persistent storage.");
			}

			// first filter out NULL values which are specified for not nullable fields
			if(value==null)
			{
				return true;
			}

			Type typeOfValue = value.GetType();
			if((typeOfValue!=fieldToValidate.DataType)&&(fieldToValidate.DataType!=typeof(object)))
			{
				throw new ORMValueTypeMismatchException("The value " + value.ToString() + " is of type '" + typeOfValue.ToString() + "' while the field is of type '" + fieldToValidate.DataType.ToString() + "'");
			}

			// value can be valid. 
			// use if - then - else tree to check. This is faster than the stringbased compare of type names
			bool wasSuccesful = true;
			string exceptionMessage = "";
			if(fieldToValidate.DataType == typeof(System.String))
			{
				// check length
				string valueAsString = (string)value;
				wasSuccesful = ((valueAsString.Length >= 0) && (valueAsString.Length <= fieldToValidate.MaxLength));
				exceptionMessage = "The value specified will cause an overflow error in the database. Value length: " + valueAsString.Length +". Column max. length: " + fieldToValidate.MaxLength;
			}
			if(fieldToValidate.DataType == typeof(System.Byte[]))
			{
				// check size
				Byte[] valueAsByteArray = (Byte[])value;
				wasSuccesful = ((valueAsByteArray.Length >= 0) && (valueAsByteArray.Length <= fieldToValidate.MaxLength));
				exceptionMessage = "The value specified will cause an overflow error in the database. Value length: " + valueAsByteArray.Length +". Column max. length: " + fieldToValidate.MaxLength;
			}
			// all other types are not causing overflows, since these types are checked by the CLR.

			// If already not valid, throw exception.
			if(!wasSuccesful)
			{
				// throw exception
				throw new ArgumentOutOfRangeException(fieldToValidate.Name, exceptionMessage);
			}

			// perform custom validation. 
			return (ValidateValueCustom(fieldIndex, value));
		}


		/// <summary>
		/// Method which will validate, using custom code supplied this class, the field with index fieldIndex if it should accept
		/// the specified value. This routine is only called when standard checks already succeeded, so value isn't null, and value does match the
		/// destination column definition of the EntityField2 related to fieldIndex.
		/// </summary>
		/// <param name="fieldIndex">Index of field to validate</param>
		/// <param name="value">value to validate</param>
		/// <returns>True if the validation succeeded, false otherwise.</returns>
		private bool ValidateValueCustom(int fieldIndex, object value)
		{
			if(_validator!=null)
			{
				return _validator.Validate(fieldIndex, value);
			}
			else
			{
				return true;
			}
		}


		/// <summary>
		/// Called when this Entity2 is added to a transaction object. This routine make sure all data currently inside the entity can be
		/// recovered when the transaction is rolled back.
		/// </summary>
		private void TransactionStart()
		{
			// back up vital info
			_backupIsNew = _isNew;
			_backupIsDeleted = _isDeleted;

			// copy EntityFields, if present.
			if(_fields!=null)
			{
				// create copy of efsfields.
				_backupFields = (EntityFields2)((EntityFields2)_fields).Clone();
			}
		}


		/// <summary>
		/// IEditableObject method. Used by databinding.
		/// A succesful edit has been performed. All new values have to be moved to the current value slots.
		/// </summary>
		public void EndEdit()
		{
			if(_fields!=null)
			{
				if(_editCycleInProgress)
				{
					_fields.EndEdit();
					_editCycleInProgress = false;

					if(_isNewViaDataBinding)
					{
						_isNewViaDataBinding=false;
					}

					// check if there is a changed event pending
					if(_pendingChangedEvent)
					{
						// yes. Mark it as changed
						FlagMeAsChanged();
						_pendingChangedEvent=false;
					}
				}
			}
		}

		/// <summary>
		/// IEditableObject method. Used by databinding.
		/// </summary>
		public void CancelEdit()
		{
			if(_fields!=null)
			{
				if(_editCycleInProgress)
				{
					_fields.CancelEdit();
					_editCycleInProgress = false;

					if(_isNewViaDataBinding)
					{
						// remove from parent
						_parentCollection.Remove(this);
					}

					_pendingChangedEvent = false;
				}
			}
		}

		/// <summary>
		/// IEditableObject method. Used by databinding.
		/// </summary>
		public void BeginEdit()
		{
			if(_fields!=null)
			{
				if(!_editCycleInProgress)
				{
					_fields.BeginEdit();
					_editCycleInProgress = true;
				}
			}
		}


		#region Class Property Declarations
		/// <summary>
		/// Returns the full name for this entity, which is important for the DAO to find back persistence info for this entity.
		/// </summary>
		/// <example>CustomerEntity</example>
		[System.ComponentModel.Browsable(false)]
		public string LLBLGenProEntityName
		{
			get {return _name;}
		}

		/// <summary>
		/// Gets / sets isAddedViaDataBinding. Databinding related.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		internal bool IsNewViaDataBinding
		{
			get
			{
				return _isNewViaDataBinding;
			}
			set
			{
				_isNewViaDataBinding = value;
			}
		}

		/// <summary>
		/// Gets / sets parentCollection. databinding related.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		internal IEntityCollection2 ParentCollection
		{
			get
			{
				return _parentCollection;
			}
			set
			{
				_parentCollection = value;
			}
		}


		/// <summary>
		/// The validator object used to validate values for fields. This is a custom validator called after the build-in validations succeed.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public IValidator Validator
		{
			get { return _validator; }
			set { _validator = value; }
		}

		
		/// <summary>
		/// The internal presentation of the data, which is an EntityFields object, which implements <see cref="IEntityFields"/>.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public virtual IEntityFields2 Fields
		{
			get { return _fields; }
			set 
			{ 
				if(value==null)
				{
					throw new ArgumentNullException("Fields", "Fields can't be set to null");
				}

				if(_fields != null)
				{
					if(value.Count != _fields.Count)
					{
						// different EntityFields object
						throw new ArgumentException("The EntityFields2 object specified has a different layout than expected");
					}
				}

				_fields = value; 
			}
		}

		/// <summary>
		/// Marker for the entity object if the object is new and should be inserted when saved (true) or read from the
		/// database (false).
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public virtual bool IsNew
		{
			get { return _isNew; }
			set { _isNew = value; }
		}

		/// <summary>
		/// The <see cref="ITransaction"/> this ITransactionalElement implementing object is participating in. Only valid if
		/// <see cref="ParticipatesInTransaction"/> is true. If set to null, the ITransactionalElement is no longer participating
		/// in a transaction.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		public virtual ITransaction Transaction
		{
			get
			{
				return _containingTransaction;
			}
			set
			{
				if((value!=null)&&(_containingTransaction!=null))
				{
					// already is in a transaction. 
					throw new ArgumentException("This object already participates in the transaction '" + _containingTransaction.Name + 
						"' and can't be added to another transaction during a running transaction. ");
				}

				_containingTransaction = value;
				if(_fields!=null)
				{
					TransactionStart();
				}
			}
		}

		/// <summary>
		/// Flag to check if the ITransactionalElement implementing object is participating in a transaction or not.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		public virtual bool ParticipatesInTransaction
		{
			get
			{
				return (_containingTransaction!=null);
			}
		}
		
		/// <summary>
		/// Gets / sets the unique Object ID which is created at runtime when the entity is instantiated. Can be used for external caches.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public virtual Guid ObjectID 
		{
			get {return _objectID;}
			set {_objectID = value;}
		}

		/// <summary>
		/// Returns true if this entity instance is in the middle of a deserialization process, for example during a ReadXml() call.
		/// For internal use only. 
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		public bool IsDeserializing 
		{
			get { return _isDeserializing; }
			set { _isDeserializing=value;}
		}

		/// <summary>
		/// Gets / sets the IConcurrencyPredicateFactory to use for <see cref="GetConcurrencyPredicate"/>.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public IConcurrencyPredicateFactory ConcurrencyPredicateFactoryToUse
		{
			get
			{
				return _concurrencyPredicateFactoryToUse;
			}
			set
			{
				_concurrencyPredicateFactoryToUse = value;
			}
		}

		/// <summary>
		/// The validator object used to validate the complete entity. Call <see cref="Validate"/> to use this validator.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public IEntityValidator EntityValidatorToUse
		{
			get
			{
				return _entityValidatorToUse;
			}
			set
			{
				_entityValidatorToUse = value;
			}
		}

		/// <summary>
		/// Marker for the entity object if the object is 'dirty' (changed, true) or not (false). Affects/reads .Fields.IsDirty.
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		public bool IsDirty
		{
			get
			{
				if(_isDeserializing || (_fields==null))
				{
					return false;
				}
				return _fields.IsDirty;
			}
			set
			{
				if(_fields!=null)
				{
					_fields.IsDirty = value;
				}
			}
		}

		/// <summary>
		/// List of IEntityField2 references which form the primary key. Reads/Affects .Fields.PrimaryKeyFields
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		[XmlIgnore]
		public ArrayList PrimaryKeyFields 
		{
			get 
			{
				if(_fields!=null)
				{
					return _fields.PrimaryKeyFields;
				}
				else
				{
					return new ArrayList();
				}
			}
		}
		#endregion
	}
}
