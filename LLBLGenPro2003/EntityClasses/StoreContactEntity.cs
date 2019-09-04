///////////////////////////////////////////////////////////////
// This is generated code. If you modify this code, be aware
// of the fact that when you re-generate the code, your changes
// are lost. If you want to keep your changes, make this file read-only
// when you have finished your changes, however it is recommended that
// you inherit from this class to extend the functionality of this generated
// class or you modify / extend the templates used to generate this code.
//
// Do not try to run this code on another version of the database than the database
// which was used to generate this code. This means that when you used f.e. SqlServer 2000
// to generate this code, it is likely that you will not be able to use that code on
// SqlServer 7 due to SQL syntax mismatches. Most code is generic code which will work
// with any database, but some code relies on a specific database type/vendor/version used. 
// This code is located in the DaoClasses which target a specific specified database. Also all
// classes target a specific specified Dynamic Query Engine (DQE) in the using/imports
// directives. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 1.0.2003.1
// Code is generated on: woensdag 4 september 2019 12:09:45
// Code is generated using templates: C# template set for SqlServer
// Templates vendor: Solutions Design.
// Templates version: 1.0.2003.3.061104
//////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Collections;
using System.Runtime.Serialization;
using System.Data;

using LLBL2003.AdventureWorks2008;
using LLBL2003.AdventureWorks2008.FactoryClasses;
using LLBL2003.AdventureWorks2008.CollectionClasses;
using LLBL2003.AdventureWorks2008.DaoClasses;
using LLBL2003.AdventureWorks2008.RelationClasses;
using LLBL2003.AdventureWorks2008.ValidatorClasses;
using LLBL2003.AdventureWorks2008.HelperClasses;

using SD.LLBLGen.Pro.ORMSupportClasses2003;

namespace LLBL2003.AdventureWorks2008.EntityClasses
{
	/// <summary>
	/// Entity class which represents the entity 'StoreContact' in the 'AdventureWorks' database.
	/// </summary>
	[Serializable]
	public class StoreContactEntity : EntityBase, ISerializable
	{
		#region Class Member Declarations


		private ContactEntity _contact;
		private bool	_alwaysFetchContact, _alreadyFetchedContact;
		private ContactTypeEntity _contactType;
		private bool	_alwaysFetchContactType, _alreadyFetchedContactType;
		private StoreEntity _store;
		private bool	_alwaysFetchStore, _alreadyFetchedStore;

		#endregion

		#region DataBinding Change Event Handler Declarations

		/// <summary>
		/// Event which is thrown when CustomerID changes value. Databinding related.
		/// </summary>
		public event EventHandler CustomerIDChanged;

		/// <summary>
		/// Event which is thrown when ContactID changes value. Databinding related.
		/// </summary>
		public event EventHandler ContactIDChanged;

		/// <summary>
		/// Event which is thrown when ContactTypeID changes value. Databinding related.
		/// </summary>
		public event EventHandler ContactTypeIDChanged;

		/// <summary>
		/// Event which is thrown when Rowguid changes value. Databinding related.
		/// </summary>
		public event EventHandler RowguidChanged;

		/// <summary>
		/// Event which is thrown when ModifiedDate changes value. Databinding related.
		/// </summary>
		public event EventHandler ModifiedDateChanged;
		#endregion

		/// <summary>
		/// CTor
		/// </summary>
		public StoreContactEntity()
		{
			InitClassEmpty(new PropertyDescriptorFactory(), new StoreContactEntityFactory());
		}

	
		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="customerID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="contactID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		public StoreContactEntity(System.Int32 customerID, System.Int32 contactID)
		{
			InitClassFetch(customerID, contactID, new StoreContactValidator(), new PropertyDescriptorFactory(), new StoreContactEntityFactory());
		}


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="customerID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="contactID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="validator">The custom validator object for this StoreContactEntity</param>
		public StoreContactEntity(System.Int32 customerID, System.Int32 contactID, StoreContactValidator validator)
		{
			InitClassFetch(customerID, contactID, validator, new PropertyDescriptorFactory(), new StoreContactEntityFactory());
		}


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="customerID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="contactID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="validator">The custom validator object for this StoreContactEntity</param>
		/// <param name="propertyDescriptorFactoryToUse">PropertyDescriptor factory to use in GetItemProperties method of contained collections. Complex databinding related.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		public StoreContactEntity(System.Int32 customerID, System.Int32 contactID, StoreContactValidator validator, IPropertyDescriptorFactory propertyDescriptorFactoryToUse, IEntityFactory entityFactoryToUse)
		{
			InitClassFetch(customerID, contactID, validator, propertyDescriptorFactoryToUse, entityFactoryToUse);
		}
	

		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="propertyDescriptorFactoryToUse">PropertyDescriptor factory to use in GetItemProperties method of contained collections. Complex databinding related.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		public StoreContactEntity(IPropertyDescriptorFactory propertyDescriptorFactoryToUse, IEntityFactory entityFactoryToUse)
		{
			InitClassEmpty(propertyDescriptorFactoryToUse, entityFactoryToUse);
		}


		/// <summary>
		/// Private CTor for deserialization
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		private StoreContactEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{


			_contact = (ContactEntity)info.GetValue("_contact", typeof(ContactEntity));
			if(_contact!=null)
			{
				// rewire event handler.
				_contact.AfterSave+=new EventHandler(OnEntityAfterSave);
			}
			_alwaysFetchContact = info.GetBoolean("_alwaysFetchContact");
			_alreadyFetchedContact = info.GetBoolean("_alreadyFetchedContact");
			_contactType = (ContactTypeEntity)info.GetValue("_contactType", typeof(ContactTypeEntity));
			if(_contactType!=null)
			{
				// rewire event handler.
				_contactType.AfterSave+=new EventHandler(OnEntityAfterSave);
			}
			_alwaysFetchContactType = info.GetBoolean("_alwaysFetchContactType");
			_alreadyFetchedContactType = info.GetBoolean("_alreadyFetchedContactType");
			_store = (StoreEntity)info.GetValue("_store", typeof(StoreEntity));
			if(_store!=null)
			{
				// rewire event handler.
				_store.AfterSave+=new EventHandler(OnEntityAfterSave);
			}
			_alwaysFetchStore = info.GetBoolean("_alwaysFetchStore");
			_alreadyFetchedStore = info.GetBoolean("_alreadyFetchedStore");

		}
		
		
		/// <summary>
		/// Will perform post-ReadXml actions as well as the normal ReadXml() actions, performed by the base class.
		/// </summary>
		/// <param name="node">XmlNode with Xml data which should be read into this entity and its members. Node's root element is the root element
		/// of the entity's Xml data</param>
		public override void ReadXml(System.Xml.XmlNode node)
		{
			base.ReadXml (node);

			// walk the contained data objects to see if they're fetched. If so, set the flags to true.


			_alreadyFetchedContact = (_contact != null);
			_alreadyFetchedContactType = (_contactType != null);
			_alreadyFetchedStore = (_store != null);

		}

		
		/// <summary>
		/// Saves the Entity class to the persistent storage. It updates or inserts the entity, which depends if the entity was originally read from the 
		/// database. If the entity is new, an insert is done and the updateRestriction is ignored. If the entity is not new, the updateRestriction
		/// predicate is used to create an additional where clause (it will be added with AND) for the update query. This predicate can be used for
		/// concurrency checks, like checks on timestamp column values.
		/// </summary>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query. Will be ignored when the entity is
		/// <param name="recurse">When true, it will save all dirty objects referenced (directly or indirectly) by this entity also.</param>
		/// <returns>true if Save succeeded, false otherwise</returns>
		/// <remarks>Do not call this routine directly, use the overloaded version in a derived class as this version doesn't construct a
		/// local transaction during recursive save, this is done in the overloaded version in a derived class.</remarks>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		public override bool Save(IPredicate updateRestriction, bool recurse)
		{
			bool transactionStartedInThisScope = false;
			Transaction transactionManager = null;

			if(recurse)
			{
				if(!base.ParticipatesInTransaction)
				{
					// Start local transaction
					transactionManager = new Transaction(IsolationLevel.ReadCommitted, "SaveRecursively");
					// Add ourselves
					transactionManager.Add(this);
					transactionStartedInThisScope=true;
				}
			}
			try
			{
				// perform the save action(s)
				bool result = base.Save(updateRestriction, recurse);

				// only commit a transaction which is started here.
				if(transactionStartedInThisScope)
				{
					transactionManager.Commit();
				}

				return result;
			}
			catch
			{
				// exception caught. roll back a transaction started here.
				if(transactionStartedInThisScope)
				{
					transactionManager.Rollback();
				}
				// bubble exception 
				throw;
			}
			finally
			{
				if(transactionStartedInThisScope)
				{
					transactionManager.Dispose();
				}
			}
		}


		/// <summary>
		/// ISerializable member. Does custom serialization so event handlers do not get serialized.
		/// Serializes members of this entity class and uses the base class' implementation to serialize
		/// the rest.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{


			info.AddValue("_contact", _contact);
			info.AddValue("_alwaysFetchContact", _alwaysFetchContact);
			info.AddValue("_alreadyFetchedContact", _alreadyFetchedContact);
			info.AddValue("_contactType", _contactType);
			info.AddValue("_alwaysFetchContactType", _alwaysFetchContactType);
			info.AddValue("_alreadyFetchedContactType", _alreadyFetchedContactType);
			info.AddValue("_store", _store);
			info.AddValue("_alwaysFetchStore", _alwaysFetchStore);
			info.AddValue("_alreadyFetchedStore", _alreadyFetchedStore);

			base.GetObjectData(info, context);
		}


		/// <summary>
		/// Sets the internal parameter related to the fieldname passed to the instance relatedEntity. 
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		public override void SetRelatedEntity(IEntity relatedEntity, string fieldName)
		{
			switch(fieldName)
			{
				case "Contact":
					SetupSyncContact(relatedEntity);
					break;
				case "ContactType":
					SetupSyncContactType(relatedEntity);
					break;
				case "Store":
					SetupSyncStore(relatedEntity);
					break;


				default:
					// do nothing
					break;
			}
		}
		
		
		/// <summary>
		/// Unsets the internal parameter related to the fieldname passed to the instance relatedEntity. Reverses the actions taken by SetRelatedEntity() 
		/// </summary>
		/// <param name="relatedEntity">Instance to unset as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		public override void UnsetRelatedEntity(IEntity relatedEntity, string fieldName)
		{
			switch(fieldName)
			{
				case "Contact":
					DesetupSyncContact(false);
					break;
				case "ContactType":
					DesetupSyncContactType(false);
					break;
				case "Store":
					DesetupSyncStore(false);
					break;


				default:
					// do nothing
					break;
			}
		}


		/// <summary>
		/// Gets a collection of related entities referenced by this entity which depend on this entity (this entity is the PK side of their FK fields). These
		/// entities will have to be persisted after this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity objects, referenced by this entity</returns>
		public override ArrayList GetDependingRelatedEntities()
		{
			ArrayList toReturn = new ArrayList();


			return toReturn;
		}
		
		
		/// <summary>
		/// Gets a collection of related entities referenced by this entity which this entity depends on (this entity is the FK side of their PK fields). These
		/// entities will have to be persisted before this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity objects, referenced by this entity</returns>
		public override ArrayList GetDependentRelatedEntities()
		{
			ArrayList toReturn = new ArrayList();

			// each related entity which is dirty, is added to the collection, and will be saved first.
			if(_contact!=null)
			{
				toReturn.Add(_contact);
			}
			if(_contactType!=null)
			{
				toReturn.Add(_contactType);
			}
			if(_store!=null)
			{
				toReturn.Add(_store);
			}

			return toReturn;
		}
		
		
		/// <summary>
		/// Gets an ArrayList of all entity collections stored as member variables in this entity. The contents of the ArrayList is
		/// used by the DataAccessAdapter to perform recursive saves. Only 1:n related collections are returned.
		/// </summary>
		/// <returns>Collection with 0 or more IEntityCollection objects, referenced by this entity</returns>
		public override ArrayList GetMemberEntityCollections()
		{
			ArrayList toReturn = new ArrayList();


		
			return toReturn;
		}


		/// <summary>
		/// Create an xml representation of this entity class
		/// </summary>
		/// <returns></returns>
		[Obsolete("ToXml is obsolete. use WriteXml instead.", false)]
		public override System.Xml.XmlNode ToXml()
		{
			return base.ToXml("StoreContactEntity");
		}


		/// <summary>
		/// Returns a unique hashcode for this entity which is unique for this type. It is based on the primary key values
		/// of this entity. If no primary key values are available, the hashcode of the object is returned.
		/// </summary>
		/// <returns>unique hashcode based on the values of the primary key values of this entity</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

	
	
		/// <summary>
		/// Fetches the contents of this entity from the persistent storage using the primary key.
		/// </summary>
		/// <param name="customerID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="contactID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <returns>True if succeeded, false otherwise.</returns>
		public bool FetchUsingPK(System.Int32 customerID, System.Int32 contactID)
		{
			return Fetch(customerID, contactID);
		}


		/// <summary>
		/// Refetches the Entity from the persistent storage. Refetch is used to re-load an Entity which is marked "Out-of-sync", due to a save action. 
		/// Refetching an empty Entity has no effect. 
		/// </summary>
		/// <returns>true if Refetch succeeded, false otherwise</returns>
		/// <exception cref="System.ApplicationException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		public override bool Refetch()
		{
			return Fetch(this.CustomerID, this.ContactID);
		}


		/// <summary>
		/// Deletes the Entity from the persistent storage. This method succeeds also when the Entity is not present.
		/// </summary>
		/// <param name="deleteRestriction">Predicate expression, meant for concurrency checks in a delete query. Overrules the predicate returned
		/// by a set ConcurrencyPredicateFactory object.</param>
		/// <returns>true if Delete succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the delete process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		public override bool Delete(IPredicate deleteRestriction)
		{
			StoreContactDAO dao = DAOFactory.CreateStoreContactDAO();
			bool wasSuccesful = dao.DeleteStoreContact(base.Fields, base.Transaction, deleteRestriction);
			if(wasSuccesful)
			{
				base.Delete(deleteRestriction);
			}
			return wasSuccesful;
		}
	

		/// <summary>
		/// Returns true if the original value for the field with the fieldIndex passed in, read from the persistent storage was NULL, false otherwise.
		/// Should not be used for testing if the current value is NULL, because the current value can't be set to NULL.
		/// If the current value for the field with the index fieldIndex is set to <i>null</i>, you can simply test
		/// against <i>null</i> to see if the <i>current value</i> of the field is null. 
		/// </summary>
		/// <param name="fieldIndex">Index of the field to test if that field was NULL in the persistent storage</param>
		/// <returns>true if the field with the passed in index was NULL in the persistent storage, false otherwise</returns>
		public bool TestOriginalFieldValueForNull(StoreContactFieldIndex fieldIndex)
		{
			return base.Fields[(int)fieldIndex].IsNull;
		}

	
	
	
		/// <summary>
		/// Retrieves the related entity of type 'ContactEntity', using a relation of type 'n:1'
		/// </summary>
		/// <returns>A fetched entity of type 'ContactEntity' which is related to this entity.</returns>
		public virtual ContactEntity GetSingleContact()
		{
			return GetSingleContact(false);
		}


		/// <summary>
		/// Retrieves the related entity of type 'ContactEntity', using a relation of type 'n:1'
		/// </summary>
		/// <param name="forceFetch">if true, it will discard any changes currently in the currently loaded related entity and will refetch the entity from 
		/// the persistent storage</param>
		/// <returns>A fetched entity of type 'ContactEntity' which is related to this entity.</returns>
		public virtual ContactEntity GetSingleContact(bool forceFetch)
		{
 			if( ( !_alreadyFetchedContact || forceFetch || _alwaysFetchContact) && !base.IsSerializing && !base.IsDeserializing )
			{
				// refetch it.
				ContactEntity newEntity = new ContactEntity();
				if(base.ParticipatesInTransaction)
				{
					base.Transaction.Add(newEntity);
				}
				newEntity.FetchUsingPK(this.ContactID);
				this.Contact = newEntity;

				_alreadyFetchedContact = true;
			}
			return _contact;
		}
	
		/// <summary>
		/// Retrieves the related entity of type 'ContactTypeEntity', using a relation of type 'n:1'
		/// </summary>
		/// <returns>A fetched entity of type 'ContactTypeEntity' which is related to this entity.</returns>
		public virtual ContactTypeEntity GetSingleContactType()
		{
			return GetSingleContactType(false);
		}


		/// <summary>
		/// Retrieves the related entity of type 'ContactTypeEntity', using a relation of type 'n:1'
		/// </summary>
		/// <param name="forceFetch">if true, it will discard any changes currently in the currently loaded related entity and will refetch the entity from 
		/// the persistent storage</param>
		/// <returns>A fetched entity of type 'ContactTypeEntity' which is related to this entity.</returns>
		public virtual ContactTypeEntity GetSingleContactType(bool forceFetch)
		{
 			if( ( !_alreadyFetchedContactType || forceFetch || _alwaysFetchContactType) && !base.IsSerializing && !base.IsDeserializing )
			{
				// refetch it.
				ContactTypeEntity newEntity = new ContactTypeEntity();
				if(base.ParticipatesInTransaction)
				{
					base.Transaction.Add(newEntity);
				}
				newEntity.FetchUsingPK(this.ContactTypeID);
				this.ContactType = newEntity;

				_alreadyFetchedContactType = true;
			}
			return _contactType;
		}
	
		/// <summary>
		/// Retrieves the related entity of type 'StoreEntity', using a relation of type 'n:1'
		/// </summary>
		/// <returns>A fetched entity of type 'StoreEntity' which is related to this entity.</returns>
		public virtual StoreEntity GetSingleStore()
		{
			return GetSingleStore(false);
		}


		/// <summary>
		/// Retrieves the related entity of type 'StoreEntity', using a relation of type 'n:1'
		/// </summary>
		/// <param name="forceFetch">if true, it will discard any changes currently in the currently loaded related entity and will refetch the entity from 
		/// the persistent storage</param>
		/// <returns>A fetched entity of type 'StoreEntity' which is related to this entity.</returns>
		public virtual StoreEntity GetSingleStore(bool forceFetch)
		{
 			if( ( !_alreadyFetchedStore || forceFetch || _alwaysFetchStore) && !base.IsSerializing && !base.IsDeserializing )
			{
				// refetch it.
				StoreEntity newEntity = new StoreEntity();
				if(base.ParticipatesInTransaction)
				{
					base.Transaction.Add(newEntity);
				}
				newEntity.FetchUsingPK(this.CustomerID);
				this.Store = newEntity;

				_alreadyFetchedStore = true;
			}
			return _store;
		}
	
	
	
		#region Data binding change event raising methods
	
		/// <summary>
		/// Event thrower for the CustomerIDChanged event, which is thrown when CustomerID changes value.
		/// Databinding related.
		/// </summary>
		protected virtual void OnCustomerIDChanged()
		{
			if(CustomerIDChanged!=null)
			{
				CustomerIDChanged(this, new EventArgs());
			}
		}
	
		/// <summary>
		/// Event thrower for the ContactIDChanged event, which is thrown when ContactID changes value.
		/// Databinding related.
		/// </summary>
		protected virtual void OnContactIDChanged()
		{
			if(ContactIDChanged!=null)
			{
				ContactIDChanged(this, new EventArgs());
			}
		}
	
		/// <summary>
		/// Event thrower for the ContactTypeIDChanged event, which is thrown when ContactTypeID changes value.
		/// Databinding related.
		/// </summary>
		protected virtual void OnContactTypeIDChanged()
		{
			if(ContactTypeIDChanged!=null)
			{
				ContactTypeIDChanged(this, new EventArgs());
			}
		}
	
		/// <summary>
		/// Event thrower for the RowguidChanged event, which is thrown when Rowguid changes value.
		/// Databinding related.
		/// </summary>
		protected virtual void OnRowguidChanged()
		{
			if(RowguidChanged!=null)
			{
				RowguidChanged(this, new EventArgs());
			}
		}
	
		/// <summary>
		/// Event thrower for the ModifiedDateChanged event, which is thrown when ModifiedDate changes value.
		/// Databinding related.
		/// </summary>
		protected virtual void OnModifiedDateChanged()
		{
			if(ModifiedDateChanged!=null)
			{
				ModifiedDateChanged(this, new EventArgs());
			}
		}
	
		#endregion


		/// <summary>
		/// Event handler which is called by a related entity after that entity is persisted.
		/// </summary>
		/// <param name="sender">IEntity instance</param>
		/// <param name="e"></param>
		protected virtual void OnEntityAfterSave(object sender, EventArgs e)
		{
			IEntity persistedEntity = (IEntity)sender;

			ArrayList entitySyncInfos = new ArrayList(((Hashtable)base.GetEntitySyncInformation(persistedEntity)).Values);
			for (int i = 0; i < entitySyncInfos.Count; i++)
			{
				EntitySyncInfoSS currentSyncInfo = (EntitySyncInfoSS)entitySyncInfos[i];
				base.SyncFKFields(currentSyncInfo.Relation, currentSyncInfo.DataSupplyingEntity);
			}
		}


		/// <summary>
		/// Performs the insert action of a new Entity to the persistent storage.
		/// </summary>
		/// <returns>true if succeeded, false otherwise</returns>
		protected override bool InsertEntity()
		{
			StoreContactDAO dao = DAOFactory.CreateStoreContactDAO();
			return dao.AddStoreContact(base.Fields, base.Transaction);
		}

	
		/// <summary>
		/// Performs the update action of an existing Entity to the persistent storage.
		/// </summary>
		/// <returns>true if succeeded, false otherwise</returns>
		protected override bool UpdateEntity()
		{
			StoreContactDAO dao = DAOFactory.CreateStoreContactDAO();
			return dao.UpdateStoreContact(base.Fields, base.Transaction);
		}
		
		
		/// <summary>
		/// Performs the update action of an existing Entity to the persistent storage.
		/// </summary>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query</param>
		/// <returns>true if succeeded, false otherwise</returns>
		protected override bool UpdateEntity(IPredicate updateRestriction)
		{
			StoreContactDAO dao = DAOFactory.CreateStoreContactDAO();
			return dao.UpdateStoreContact(base.Fields, base.Transaction, updateRestriction);
		}
	
	
		/// <summary>
		/// Initializes the class with empty data, as if it is a new Entity.
		/// </summary>
		/// <param name="propertyDescriptorFactoryToUse">PropertyDescriptor factory to use in GetItemProperties method of contained collections. Complex databinding related.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		private void InitClassEmpty(IPropertyDescriptorFactory propertyDescriptorFactoryToUse, IEntityFactory entityFactoryToUse)
		{
			InitClassMembers(propertyDescriptorFactoryToUse, entityFactoryToUse);
	
			base.Fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.StoreContactEntity);
			base.IsNew=true;
			base.EntityFactoryToUse = entityFactoryToUse;
			base.Validator = new StoreContactValidator();
		}

	
		/// <summary>
		/// Initializes the the entity and fetches the data related to the entity in this entity.
		/// </summary>
		/// <param name="customerID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="contactID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="propertyDescriptorFactoryToUse">PropertyDescriptor factory to use in GetItemProperties method of contained collections. Complex databinding related.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="validator">The validator object for this StoreContactEntity</param>
		private void InitClassFetch(System.Int32 customerID, System.Int32 contactID, StoreContactValidator validator, IPropertyDescriptorFactory propertyDescriptorFactoryToUse, IEntityFactory entityFactoryToUse)
		{
			InitClassMembers(propertyDescriptorFactoryToUse, entityFactoryToUse);
			base.Fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.StoreContactEntity);
			bool wasSuccesful = Fetch(customerID, contactID);
			base.IsNew = !wasSuccesful;
			base.Validator = validator;
			base.EntityFactoryToUse = entityFactoryToUse;
		}
	

		/// <summary>
		/// Initializes the class members
		/// </summary>
		/// <param name="propertyDescriptorFactoryToUse">PropertyDescriptor factory to use in GetItemProperties method of contained collections. Complex databinding related.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		private void InitClassMembers(IPropertyDescriptorFactory propertyDescriptorFactoryToUse, IEntityFactory entityFactoryToUse)
		{


			_contact = null;
			_alwaysFetchContact = false;
			_alreadyFetchedContact = false;
			_contactType = null;
			_alwaysFetchContactType = false;
			_alreadyFetchedContactType = false;
			_store = null;
			_alwaysFetchStore = false;
			_alreadyFetchedStore = false;

		}


		/// <summary>
		/// Removes the sync logic for member _contact
		/// </summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		private void DesetupSyncContact(bool signalRelatedEntity)
		{
			if(_contact != null)
			{
				// disconnect the entity from this entity
				_contact.AfterSave-=new EventHandler(OnEntityAfterSave);
				if(signalRelatedEntity)
				{
					_contact.UnsetRelatedEntity(this, "StoreContact");
				}
				base.UnsetEntitySyncInformation("Contact", _contact, StoreContactEntity.Relations.ContactEntityUsingContactID);
				_contact = null;
			}
		}
		
		
		/// <summary>
		/// setups the sync logic for member _contact
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncContact(IEntity relatedEntity)
		{
			DesetupSyncContact(true);
			
			if(relatedEntity!=null)
			{
				_contact = (ContactEntity)relatedEntity;
				_contact.AfterSave+=new EventHandler(OnEntityAfterSave);
				base.SetEntitySyncInformation("Contact", _contact, StoreContactEntity.Relations.ContactEntityUsingContactID);
				if(!_contact.IsNew)
				{
					// sync now
					base.SyncFKFields(StoreContactEntity.Relations.ContactEntityUsingContactID, _contact);
				}
			}

			_alreadyFetchedContact = (relatedEntity != null);
		}

		/// <summary>
		/// Removes the sync logic for member _contactType
		/// </summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		private void DesetupSyncContactType(bool signalRelatedEntity)
		{
			if(_contactType != null)
			{
				// disconnect the entity from this entity
				_contactType.AfterSave-=new EventHandler(OnEntityAfterSave);
				if(signalRelatedEntity)
				{
					_contactType.UnsetRelatedEntity(this, "StoreContact");
				}
				base.UnsetEntitySyncInformation("ContactType", _contactType, StoreContactEntity.Relations.ContactTypeEntityUsingContactTypeID);
				_contactType = null;
			}
		}
		
		
		/// <summary>
		/// setups the sync logic for member _contactType
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncContactType(IEntity relatedEntity)
		{
			DesetupSyncContactType(true);
			
			if(relatedEntity!=null)
			{
				_contactType = (ContactTypeEntity)relatedEntity;
				_contactType.AfterSave+=new EventHandler(OnEntityAfterSave);
				base.SetEntitySyncInformation("ContactType", _contactType, StoreContactEntity.Relations.ContactTypeEntityUsingContactTypeID);
				if(!_contactType.IsNew)
				{
					// sync now
					base.SyncFKFields(StoreContactEntity.Relations.ContactTypeEntityUsingContactTypeID, _contactType);
				}
			}

			_alreadyFetchedContactType = (relatedEntity != null);
		}

		/// <summary>
		/// Removes the sync logic for member _store
		/// </summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		private void DesetupSyncStore(bool signalRelatedEntity)
		{
			if(_store != null)
			{
				// disconnect the entity from this entity
				_store.AfterSave-=new EventHandler(OnEntityAfterSave);
				if(signalRelatedEntity)
				{
					_store.UnsetRelatedEntity(this, "StoreContact");
				}
				base.UnsetEntitySyncInformation("Store", _store, StoreContactEntity.Relations.StoreEntityUsingCustomerID);
				_store = null;
			}
		}
		
		
		/// <summary>
		/// setups the sync logic for member _store
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncStore(IEntity relatedEntity)
		{
			DesetupSyncStore(true);
			
			if(relatedEntity!=null)
			{
				_store = (StoreEntity)relatedEntity;
				_store.AfterSave+=new EventHandler(OnEntityAfterSave);
				base.SetEntitySyncInformation("Store", _store, StoreContactEntity.Relations.StoreEntityUsingCustomerID);
				if(!_store.IsNew)
				{
					// sync now
					base.SyncFKFields(StoreContactEntity.Relations.StoreEntityUsingCustomerID, _store);
				}
			}

			_alreadyFetchedStore = (relatedEntity != null);
		}



	
		/// <summary>
		/// Fetches the entity from the persistent storage. Fetch simply reads the entity into an EntityFields object. 
		/// </summary>
		/// <param name="customerID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <param name="contactID">PK value for StoreContact which data should be fetched into this StoreContact object</param>
		/// <returns>True if succeeded, false otherwise.</returns>
		private bool Fetch(System.Int32 customerID, System.Int32 contactID)
		{
			StoreContactDAO dao = DAOFactory.CreateStoreContactDAO();

			// Load EntityFields of StoreContact
			dao.FetchStoreContact(base.Fields, base.Transaction, customerID, contactID);

			bool fetchResult = false;
			if(base.Fields.State == EntityState.Fetched)
			{
				base.IsNew = false;
				fetchResult = true;
			}

			return fetchResult;
		}
	
	
		#region Class Property Declarations
		/// <summary>
		/// The relations object holding all relations of this entity with other entity classes.
		/// </summary>
		public static StoreContactRelations Relations
		{
			get	{ return new StoreContactRelations(); }
		}

	
		/// <summary>
		/// The CustomerID property of the Entity StoreContact
		/// </summary>
		public virtual System.Int32 CustomerID
		{
			get
			{
				object valueToReturn = base.GetCurrentFieldValue((int)StoreContactFieldIndex.CustomerID);
				if(valueToReturn == null)
				{
					throw new ORMFieldIsNullException("Cannot get the value for CustomerID because it is set to NULL.");
				}
				return (System.Int32)valueToReturn;
			}
			set
			{
				if(base.SetNewFieldValue((int)StoreContactFieldIndex.CustomerID, value))
				{
					OnCustomerIDChanged();
				}
			}
		}
	
		/// <summary>
		/// The ContactID property of the Entity StoreContact
		/// </summary>
		public virtual System.Int32 ContactID
		{
			get
			{
				object valueToReturn = base.GetCurrentFieldValue((int)StoreContactFieldIndex.ContactID);
				if(valueToReturn == null)
				{
					throw new ORMFieldIsNullException("Cannot get the value for ContactID because it is set to NULL.");
				}
				return (System.Int32)valueToReturn;
			}
			set
			{
				if(base.SetNewFieldValue((int)StoreContactFieldIndex.ContactID, value))
				{
					OnContactIDChanged();
				}
			}
		}
	
		/// <summary>
		/// The ContactTypeID property of the Entity StoreContact
		/// </summary>
		public virtual System.Int32 ContactTypeID
		{
			get
			{
				object valueToReturn = base.GetCurrentFieldValue((int)StoreContactFieldIndex.ContactTypeID);
				if(valueToReturn == null)
				{
					throw new ORMFieldIsNullException("Cannot get the value for ContactTypeID because it is set to NULL.");
				}
				return (System.Int32)valueToReturn;
			}
			set
			{
				if(base.SetNewFieldValue((int)StoreContactFieldIndex.ContactTypeID, value))
				{
					OnContactTypeIDChanged();
				}
			}
		}
	
		/// <summary>
		/// The Rowguid property of the Entity StoreContact
		/// </summary>
		public virtual System.Guid Rowguid
		{
			get
			{
				object valueToReturn = base.GetCurrentFieldValue((int)StoreContactFieldIndex.Rowguid);
				if(valueToReturn == null)
				{
					throw new ORMFieldIsNullException("Cannot get the value for Rowguid because it is set to NULL.");
				}
				return (System.Guid)valueToReturn;
			}
			set
			{
				if(base.SetNewFieldValue((int)StoreContactFieldIndex.Rowguid, value))
				{
					OnRowguidChanged();
				}
			}
		}
	
		/// <summary>
		/// The ModifiedDate property of the Entity StoreContact
		/// </summary>
		public virtual System.DateTime ModifiedDate
		{
			get
			{
				object valueToReturn = base.GetCurrentFieldValue((int)StoreContactFieldIndex.ModifiedDate);
				if(valueToReturn == null)
				{
					throw new ORMFieldIsNullException("Cannot get the value for ModifiedDate because it is set to NULL.");
				}
				return (System.DateTime)valueToReturn;
			}
			set
			{
				if(base.SetNewFieldValue((int)StoreContactFieldIndex.ModifiedDate, value))
				{
					OnModifiedDateChanged();
				}
			}
		}
	
	
	
	
		/// <summary>
		/// Gets / sets related entity of type 'ContactEntity'. This property is not visible in databinded grids.
		/// Setting this property to a new object will make the load-on-demand feature to stop fetching data from the database, until you set this
		/// property to null. Setting this property to an entity will make sure that FK-PK relations are synchronized when appropriate.
		/// </summary>
		/// <remarks>This property is added for conveniance, however it is recommeded to use the method 'GetSingleContact()', because 
		/// this property is rather expensive and a method tells the user to cache the result when it has to be used more than once in the
		/// same scope. The property is marked non-browsable to make it hidden in binded controls, f.e. datagrids.</remarks>
		[Browsable(false)]
		public virtual ContactEntity Contact
		{
			get
			{
				return GetSingleContact(false);
			}
			set
			{
				if(base.IsDeserializing)
				{
					SetupSyncContact(value);
				}
				else
				{
					if(value==null)
					{
						if(_contact != null)
						{
							// signal the related entity to unset the sync info. Because this will boil down to the collection object, this class
							// get notified by the collection class, which will then clean up the sync info from this object to the related object.
							_contact.UnsetRelatedEntity(this, "StoreContact");
						}
					}
					else
					{
						// just call SetRelatedEntity, as it will bubble down to the collection which will call our SetRelatedEntity, which
						// will do the actual set action
						((IEntity)value).SetRelatedEntity(this, "StoreContact");
					}
				}
			}
		}

		/// <summary>
		/// Gets / sets the lazy loading flag for Contact. When set to true, Contact is always refetched from the 
		/// persistent storage. When set to false, the data is only fetched the first time Contact is accessed. You can always execute
		/// a forced fetch by calling GetSingleContact(true).
		/// </summary>
		[Browsable(false)]
		public bool AlwaysFetchContact
		{
			get
			{
				return _alwaysFetchContact;
			}
			set
			{
				_alwaysFetchContact = value;
			}	
		}
	
		/// <summary>
		/// Gets / sets related entity of type 'ContactTypeEntity'. This property is not visible in databinded grids.
		/// Setting this property to a new object will make the load-on-demand feature to stop fetching data from the database, until you set this
		/// property to null. Setting this property to an entity will make sure that FK-PK relations are synchronized when appropriate.
		/// </summary>
		/// <remarks>This property is added for conveniance, however it is recommeded to use the method 'GetSingleContactType()', because 
		/// this property is rather expensive and a method tells the user to cache the result when it has to be used more than once in the
		/// same scope. The property is marked non-browsable to make it hidden in binded controls, f.e. datagrids.</remarks>
		[Browsable(false)]
		public virtual ContactTypeEntity ContactType
		{
			get
			{
				return GetSingleContactType(false);
			}
			set
			{
				if(base.IsDeserializing)
				{
					SetupSyncContactType(value);
				}
				else
				{
					if(value==null)
					{
						if(_contactType != null)
						{
							// signal the related entity to unset the sync info. Because this will boil down to the collection object, this class
							// get notified by the collection class, which will then clean up the sync info from this object to the related object.
							_contactType.UnsetRelatedEntity(this, "StoreContact");
						}
					}
					else
					{
						// just call SetRelatedEntity, as it will bubble down to the collection which will call our SetRelatedEntity, which
						// will do the actual set action
						((IEntity)value).SetRelatedEntity(this, "StoreContact");
					}
				}
			}
		}

		/// <summary>
		/// Gets / sets the lazy loading flag for ContactType. When set to true, ContactType is always refetched from the 
		/// persistent storage. When set to false, the data is only fetched the first time ContactType is accessed. You can always execute
		/// a forced fetch by calling GetSingleContactType(true).
		/// </summary>
		[Browsable(false)]
		public bool AlwaysFetchContactType
		{
			get
			{
				return _alwaysFetchContactType;
			}
			set
			{
				_alwaysFetchContactType = value;
			}	
		}
	
		/// <summary>
		/// Gets / sets related entity of type 'StoreEntity'. This property is not visible in databinded grids.
		/// Setting this property to a new object will make the load-on-demand feature to stop fetching data from the database, until you set this
		/// property to null. Setting this property to an entity will make sure that FK-PK relations are synchronized when appropriate.
		/// </summary>
		/// <remarks>This property is added for conveniance, however it is recommeded to use the method 'GetSingleStore()', because 
		/// this property is rather expensive and a method tells the user to cache the result when it has to be used more than once in the
		/// same scope. The property is marked non-browsable to make it hidden in binded controls, f.e. datagrids.</remarks>
		[Browsable(false)]
		public virtual StoreEntity Store
		{
			get
			{
				return GetSingleStore(false);
			}
			set
			{
				if(base.IsDeserializing)
				{
					SetupSyncStore(value);
				}
				else
				{
					if(value==null)
					{
						if(_store != null)
						{
							// signal the related entity to unset the sync info. Because this will boil down to the collection object, this class
							// get notified by the collection class, which will then clean up the sync info from this object to the related object.
							_store.UnsetRelatedEntity(this, "StoreContact");
						}
					}
					else
					{
						// just call SetRelatedEntity, as it will bubble down to the collection which will call our SetRelatedEntity, which
						// will do the actual set action
						((IEntity)value).SetRelatedEntity(this, "StoreContact");
					}
				}
			}
		}

		/// <summary>
		/// Gets / sets the lazy loading flag for Store. When set to true, Store is always refetched from the 
		/// persistent storage. When set to false, the data is only fetched the first time Store is accessed. You can always execute
		/// a forced fetch by calling GetSingleStore(true).
		/// </summary>
		[Browsable(false)]
		public bool AlwaysFetchStore
		{
			get
			{
				return _alwaysFetchStore;
			}
			set
			{
				_alwaysFetchStore = value;
			}	
		}
	
	
		#endregion
	}
}
