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
using System.Xml;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Class which forms the EntityFields2 type. An EntityFields type is a collection of IEntityField objects which forms the total amount of fields 
	/// for a given entity.
	/// SelfServicing specific
	/// </summary>
	[Serializable]
	public class EntityFields : IEntityFields, ICloneable
	{
		#region Class Member Declarations
		private IEntityField[]	_entityFields;					// the basic store for the entity fields
		private Hashtable		_entityFieldReferences;			// the references store for retrieval via the name of the entity fields
		private ArrayList		_primaryKeyFields;				// the list of primary key entity field references.
		private bool			_isDirty, _wasDirtyWhenEditStarted, _isChangedInThisEditCycle;
		private EntityState		_state;
		#endregion
		

		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="amount">The initial amount of fields in this EntityFields collection</param>
		public EntityFields(int amount)
		{
			_entityFieldReferences = new Hashtable(amount);
			_entityFields = new EntityField[amount]; 
			_primaryKeyFields = new ArrayList(amount);
			_isDirty = false;
			_wasDirtyWhenEditStarted = false;
			_isChangedInThisEditCycle = false;
			_state = EntityState.New;
		}


		/// <summary>
		/// Overrides the GetHashCode routine. It will calculate a hashcode for this set of entity fields using the eXclusive OR of the 
		/// hashcodes of the primary key fields in this set of entity fields. That hashcode is returned. If no primary key fields are present,
		/// the hashcode of the base class is returned, which will not be unique.
		/// </summary>
		/// <returns>Hashcode for this entity object, based on its primary key field values</returns>
		public override int GetHashCode()
		{
			if(_primaryKeyFields.Count<=0)
			{
				return base.GetHashCode();
			}

			int hashToReturn = ((EntityField)_primaryKeyFields[0]).GetHashCode();

			// calculate hash value
			for(int i=1;i<_primaryKeyFields.Count;i++)
			{
				EntityField field = (EntityField)_primaryKeyFields[i];

				hashToReturn ^= field.GetHashCode();
			}

			return hashToReturn;
		}


		/// <summary>
		/// Compares passed in object with the given object. This is a compare of PK fields. These have to be the same in VALUES. 
		/// When the values are not the same, or the type is not the same as the current type, false is returned, true otherwise.
		/// When this doesn't have any PK fields, all fields are compared. null values are considered as the same value. 
		/// </summary>
		/// <param name="obj">IEntityFields implementing object of the same type as this which will be compared to the PK values of this. </param>
		/// <returns>True when the PK values of this are the same as the PK values of obj, or when this doesn't have any PK fields, all fields
		/// have the same value as obj's fields. False otherwise.</returns>
		public override bool Equals(object obj)
		{
			IEntityFields passedIn = obj as IEntityFields;
			if(passedIn==null)
			{
				// not equal
				return false;
			}

			if((this.Count!=passedIn.Count)||(_primaryKeyFields.Count!=passedIn.PrimaryKeyFields.Count))
			{
				// not equal
				return false;
			}

			bool theSame=true;
			if(_primaryKeyFields.Count>0)
			{
				for (int i = 0; i < _primaryKeyFields.Count; i++)
				{
					theSame&=(((IEntityField)_primaryKeyFields[i]).CurrentValue.Equals( ((IEntityField)passedIn.PrimaryKeyFields[i]).CurrentValue) );
					if(!theSame)
					{
						// already not the same. quit
						break;
					}
				}
			}
			else
			{
				// measure all fields
				for (int i = 0; i < _entityFields.Length; i++)
				{
					theSame&=(_entityFields[i].CurrentValue==passedIn[i].CurrentValue);
					if(!theSame)
					{
						// already not the same. quit
						break;
					}
				}
			}

			return theSame;
		}


		/// <summary>
		/// Converts this EntityFields object to a set of XmlNodes with all the fields as individual nodes.
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the nodes this method will create. This document is required
		/// to create the new nodes for the fields</param>
		/// <param name="parentNode">the node the fields will have to be added to.</param>
		public void WriteXml(XmlDocument parentDocument, XmlNode parentNode)
		{
			for(int i=0;i<_entityFields.Length;i++)
			{
				EntityField field = (EntityField)_entityFields[i];
				XmlNode entityFieldNode = null;
				field.WriteXml(parentDocument, out entityFieldNode);
				parentNode.AppendChild(entityFieldNode);
			}
		}


		/// <summary>
		/// Returns the complete list of IEntityField objects as an array of IFieldPersistenceInfo objects. IEntityField objects implement
		/// IFieldPersistenceInfo.
		/// </summary>
		/// <returns>Array of IFieldPersistenceInfo objects</returns>
		public IFieldPersistenceInfo[] GetAsPersistenceInfoArray()
		{
			return (IFieldPersistenceInfo[])_entityFields;
		}


		/// <summary>
		/// Returns the complete list of IEntityField objects as an array of IEntityFieldCore objects. IEntityField objects implement
		/// IEntityFieldCore
		/// </summary>
		/// <returns>Array of IEntityFieldCore objects</returns>
		public IEntityFieldCore[] GetAsEntityFieldCoreArray()
		{
			return (IEntityFieldCore[])_entityFields;
		}


		/// <summary>
		/// All changes to all <see cref="IEntityField"/> objects in this collection are accepted. 
		/// </summary>
		public void AcceptChanges()
		{
			for(int i=0;i<_entityFields.Length;i++)
			{
				if(_entityFields[i]!=null)
				{
					_entityFields[i].AcceptChange();
				}
			}

			_isDirty = false;
		}

		
		/// <summary>
		/// Per field, the last change made is rejected.
		/// </summary>
		public void RejectChanges()
		{
			for(int i=0;i<_entityFields.Length;i++)
			{
				if(_entityFields[i]!=null)
				{
					_entityFields[i].RejectChange();
				}
			}

			_isDirty = false;
		}


		/// <summary>
		/// Clones this instance and its contents using a deep copy.
		/// </summary>
		/// <returns>an exact, deep copy of this EntityFields object and its contents.</returns>
		public virtual object Clone()
		{
			EntityFields fieldsToReturn = (EntityFields)this.MemberwiseClone();
			fieldsToReturn.SetupCloning();

			for(int i=0;i<fieldsToReturn.Count;i++)
			{
				fieldsToReturn[i] = (EntityField)(((EntityField)this[i]).Clone());
			}

			return fieldsToReturn;
		}

		
		/// <summary>
		/// Prepares this object to be filled with objects. It is called after the MemberwiseClone call and resets all references
		/// to objects and creates new objects or sets them to null. 
		/// </summary>
		internal void SetupCloning()
		{
			_entityFieldReferences = new Hashtable(_entityFieldReferences.Count);
			_primaryKeyFields = new ArrayList(_primaryKeyFields.Count);
			_entityFields = new EntityField[_entityFields.Length]; 
		}


		/// <summary>
		/// IEditableObject method. Used by databinding.
		/// A succesful edit has been performed. All new values have to be moved to the current value slots.
		/// </summary>
		public void EndEdit()
		{
			if(_isChangedInThisEditCycle)
			{
				for(int i=0;i<_entityFields.Length;i++)
				{
					_entityFields[i].EndEdit();
				}
				_isChangedInThisEditCycle = false;
			}
		}


		/// <summary>
		/// IEditableObject method. Used by databinding.
		/// Doesn't reset isDirty. 
		/// </summary>
		public void CancelEdit()
		{
			if(_isChangedInThisEditCycle)
			{
				for(int i=0;i<_entityFields.Length;i++)
				{
					_entityFields[i].CancelEdit();
				}

				_isDirty = _wasDirtyWhenEditStarted;
			}
		}


		/// <summary>
		/// IEditableObject method. Used by databinding.
		/// </summary>
		public void BeginEdit()
		{
			for(int i=0;i<_entityFields.Length;i++)
			{
				_entityFields[i].BeginEdit();
			}
			_wasDirtyWhenEditStarted = _isDirty;
			_isChangedInThisEditCycle = false;
		}


		#region Class Property Declarations
		/// <summary>
		/// The amount of IEntityFields allocated in the EntityFields object.
		/// </summary>
		public int Count 
		{
			get {return _entityFields.Length;}
		}

		/// <summary>
		/// Gets the flag if the contents of the EntityFields object is 'dirty', which means that one or more fields are changed. 
		/// <see cref="AcceptChanges"/> and <see cref="RejectChanges"/> reset this flag.
		/// </summary>
		public bool IsDirty 
		{
			get {return _isDirty; } 
			set {_isDirty = value; }
		}

		/// <summary>
		/// List of IEntityField references which form the primary key
		/// </summary>
		public ArrayList PrimaryKeyFields
		{
			get { return _primaryKeyFields; }
		}

		/// <summary>
		/// Gets / sets the EntityField on the specified Index. 
		/// </summary>
		/// <exception cref="System.IndexOutOfRangeException">When the index specified is not found in the internal datastorage.</exception>
		/// <exception cref="System.ArgumentNullException">When the passed in <see cref="IEntityField"/> is null</exception>
		/// <exception cref="System.ArgumentException">When the passed in <see cref="IEntityField"/> is already added. Fields have to be unique.</exception>
		public IEntityField this [int index] 
		{
			get
			{
				if((index < 0) || (index >= _entityFields.Length))
				{
					throw new IndexOutOfRangeException("The specified index is not in the range of known indexes");
				}

				return _entityFields[index];
			}

			set
			{
				if(index<0)
				{
					throw new IndexOutOfRangeException("The index on which the object should be placed cannot be smaller than 0.");
				}

				if(index >= _entityFields.Length)
				{
					throw new IndexOutOfRangeException("The index on which the object should be placed is outside the specified range of indexes.");
				}

				if(value==null)
				{
					throw new ArgumentNullException("Item", "Item cannot be null");
				}

				// Add the field to the sorted list, for fast retrieval using different mechanisms.
				_entityFieldReferences.Add(value.Name, value);
				_entityFields[index] = value;
				if (value.IsPrimaryKey)
				{
					// add it to the primary key set too
					_primaryKeyFields.Add(value);
				}
			}
		}

		/// <summary>
		/// Gets the EntityField with the specified name.
		/// </summary>
		/// <exception cref="System.ArgumentException">When the specified name is the empty string or contains only spaces or is not found.</exception>
		/// <remarks>This property is read-only. If you want to set a value, use the int indexer</remarks>
		public IEntityField this [string name] 
		{
			get
			{
				if(name.Length <= 0)
				{
					// Names of zero length are rejected
					throw new ArgumentException("Name cannot be of zero length.");
				}
				if(name.Trim().Length <= 0)
				{
					throw new ArgumentException("Name has to contain other characters than just spaces.");
				}
				if(!_entityFieldReferences.ContainsKey(name))
				{
					// invalid name.
					throw new ArgumentException("Name supplied is not part of the set of fields contained in this EntityFields collection", name);
				}

				return (IEntityField)(_entityFieldReferences[name]);
			}
		}

		/// <summary>
		/// The state of the EntityFields object, the heart and soul of every EntityObject.
		/// </summary>
		public EntityState State 
		{
			get { return _state; } 
			set { _state = value; }
		}

		/// <summary>
		/// Flag to signal if the entity fields have changed during an edit cycle which is controlled outside this IEntityFields object. If set to
		/// true, EndEdit will succeed, otherwise EndEdit will ignore any changes, since these are made in a previous edit cycle which is already
		/// ended.
		/// </summary>
		public bool IsChangedInThisEditCycle 
		{
			get {return _isChangedInThisEditCycle;} 
			set {_isChangedInThisEditCycle = value;}
		}

		#endregion

	}
}
