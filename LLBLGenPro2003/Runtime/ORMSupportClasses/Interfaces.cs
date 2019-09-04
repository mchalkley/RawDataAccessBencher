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
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using System.Collections;
using System.Xml;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	#region SelfServicing specific interfaces
	/// <summary>
	/// Interface for the EntityField type. An EntityField is the unit which is used to hold the value for a given property of an entity.
	/// SelfServicing specific.
	/// </summary>
	public interface IEntityField : IEntityFieldCore, IFieldPersistenceInfo
	{
		/// <summary>
		/// Converts this EntityField to an XmlNode. 
		/// </summary>
		/// <returns>This EntityField in XmlNode format</returns>
		XmlNode ToXml();
		/// <summary>
		/// Converts this EntityField2 to an XmlNode. 
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will return. This document is required
		/// to create the new node object</param>
		/// <param name="entityFieldNode">The output parameter which will represent this EntityField2 as XmlNode</param>
		void WriteXml(XmlDocument parentDocument, out XmlNode entityFieldNode);
		
		/// <summary>
		/// If set to true, in the constructor, no changes can be made to this field. 
		/// </summary>
		bool IsReadOnly {get;}
	}


	/// <summary>
	/// Interface for the EntityFields type. An EntityFields type is a collection of IEntityField objects which forms the total amount of fields for a given entity.
	/// SelfServicing specific
	/// </summary>
	public interface IEntityFields : IEditableObject
	{
		/// <summary>
		/// All changes to all <see cref="IEntityField"/> objects in this collection are applied. 
		/// </summary>
		void AcceptChanges();
		/// <summary>
		/// Per field, the last change made is rejected.
		/// </summary>
		void RejectChanges();
		/// <summary>
		/// Overrides the GetHashCode routine. It will calculate a hashcode for this set of entity fields using the eXclusive OR of the 
		/// hashcodes of the primary key fields in this set of entity fields. That hashcode is returned. If no primary key fields are present,
		/// the hashcode of the base class is returned, which will not be unique.
		/// </summary>
		/// <returns>Hashcode for this entity object, based on its primary key field values</returns>
		int GetHashCode();
		/// <summary>
		/// Returns the complete list of IEntityField objects as an array of IFieldPersistenceInfo objects. IEntityField objects implement
		/// IFieldPersistenceInfo.
		/// </summary>
		/// <returns>Array of IFieldPersistenceInfo objects</returns>
		IFieldPersistenceInfo[] GetAsPersistenceInfoArray();
		/// <summary>
		/// Returns the complete list of IEntityField objects as an array of IEntityFieldCore objects. IEntityField objects implement
		/// IEntityFieldCore
		/// </summary>
		/// <returns>Array of IEntityFieldCore objects</returns>
		IEntityFieldCore[] GetAsEntityFieldCoreArray();
		/// <summary>
		/// Converts this EntityFields object to a set of XmlNodes with all the fields as individual nodes.
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the nodes this method will create. This document is required
		/// to create the new nodes for the fields</param>
		/// <param name="parentNode">the node the fields will have to be added to.</param>
		void WriteXml(XmlDocument parentDocument, XmlNode parentNode);

		/// <summary>
		/// The amount of IEntityFields allocated in the EntityFields object.
		/// </summary>
		int	Count {get;}
		/// <summary>
		/// Gets the flag if the contents of the EntityFields object is 'dirty', which means that one or more fields are changed. 
		/// <see cref="AcceptChanges"/> and <see cref="RejectChanges"/> reset this flag.
		/// </summary>
		bool IsDirty {get; set;}
		/// <summary>
		/// Gets / sets the EntityField on the specified Index. 
		/// </summary>
		/// <exception cref="System.IndexOutOfRangeException">When the index specified is not found in the internal datastorage.</exception>
		/// <exception cref="System.ArgumentNullException">When the passed in <see cref="IEntityField"/> is null</exception>
		/// <exception cref="System.ArgumentException">When the passed in <see cref="IEntityField"/> is already added. Fields have to be unique.</exception>
		IEntityField this [int index] {get; set;} 
		/// <summary>
		/// Gets the EntityField with the specified name.
		/// </summary>
		/// <exception cref="System.ArgumentException">When the specified name is the empty string or contains only spaces</exception>
		/// <remarks>This property is read-only. If you want to set a value, use the int indexer</remarks>
		IEntityField this [string name] {get;}
		/// <summary>
		/// List of IEntityField references which form the primary key
		/// </summary>
		ArrayList PrimaryKeyFields {get;}
		/// <summary>
		/// The state of the EntityFields object, the heart and soul of every EntityObject.
		/// </summary>
		EntityState State {get; set;}
		/// <summary>
		/// Flag to signal if the entity fields have changed during an edit cycle which is controlled outside this IEntityFields object. If set to
		/// true, EndEdit will succeed, otherwise EndEdit will ignore any changes, since these are made in a previous edit cycle which is already
		/// ended.
		/// </summary>
		bool IsChangedInThisEditCycle {get; set;}
	}


	/// <summary>
	/// Interface used for all Entity classes, it's the interface implemented by the abstract base class which is used to derive every entity class from
	/// SelfServicing specific
	/// </summary>
	public interface IEntity : IEntityCore
	{
		/// <summary>
		/// Sets the internal parameter related to the fieldname passed to the instance relatedEntity. 
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		void SetRelatedEntity(IEntity relatedEntity, string fieldName);
		/// <summary>
		/// Unsets the internal parameter related to the fieldname passed to the instance relatedEntity. Reverses the actions taken by SetRelatedEntity() 
		/// </summary>
		/// <param name="relatedEntity">Instance to unset as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		void UnsetRelatedEntity(IEntity relatedEntity, string fieldName);
		/// <summary>
		/// Gets a collection of related entities referenced by this entity which depend on this entity (this entity is the PK side of their FK fields). These
		/// entities will have to be persisted after this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity objects, referenced by this entity</returns>
		ArrayList GetDependingRelatedEntities();
		/// <summary>
		/// Gets a collection of related entities referenced by this entity which this entity depends on (this entity is the FK side of their PK fields). These
		/// entities will have to be persisted before this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity objects, referenced by this entity</returns>
		ArrayList GetDependentRelatedEntities();
		/// <summary>
		/// Gets an ArrayList of all entity collections stored as member variables in this entity. The contents of the ArrayList is
		/// used by the Save logic to perform recursive saves. Only 1:n related collections are returned.
		/// </summary>
		/// <returns>Collection with 0 or more IEntityCollection objects, referenced by this entity</returns>
		ArrayList GetMemberEntityCollections();
		/// <summary>
		/// Saves the Entity class to the persistent storage. It updates or inserts the entity, which depends if the entity was originally read from the 
		/// database. Will not recursively save internal dirty entities. 
		/// Uses, if applicable, the ConcurrencyPredicateFactory to supply the predicate to limit save activity.
		/// </summary>
		/// <returns>true if Save succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Save();
		/// <summary>
		/// Saves the Entity class to the persistent storage. It updates or inserts the entity, which depends if the entity was originally read from the 
		/// database.
		/// Uses, if applicable, the ConcurrencyPredicateFactory to supply the predicate to limit save activity.
		/// </summary>
		/// <param name="recurse">When true, it will save all dirty objects referenced (directly or indirectly) by this entity also.</param>
		/// <returns>true if Save succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Save(bool recurse);
		/// <summary>
		/// Saves the Entity class to the persistent storage. It updates or inserts the entity, which depends if the entity was originally read from the 
		/// database. If the entity is new, an insert is done and the updateRestriction is ignored. If the entity is not new, the updateRestriction
		/// predicate is used to create an additional where clause (it will be added with AND) for the update query. This predicate can be used for
		/// concurrency checks, like checks on timestamp column values. Will not recursively save internal dirty entities. 
		/// </summary>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query. Will be ignored when the entity is
		/// new. Overrules an optional set ConcurrencyPredicateFactory.</param>
		/// <returns>true if Save succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Save(IPredicate updateRestriction);
		/// <summary>
		/// Saves the Entity class to the persistent storage. It updates or inserts the entity, which depends if the entity was originally read from the 
		/// database. If the entity is new, an insert is done and the updateRestriction is ignored. If the entity is not new, the updateRestriction
		/// predicate is used to create an additional where clause (it will be added with AND) for the update query. This predicate can be used for
		/// concurrency checks, like checks on timestamp column values.
		/// </summary>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query. Will be ignored when the entity is
		/// <param name="recurse">When true, it will save all dirty objects referenced (directly or indirectly) by this entity also.</param>
		/// new. Overrules an optional set ConcurrencyPredicateFactory.</param>
		/// <returns>true if Save succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Save(IPredicate updateRestriction, bool recurse);
		/// <summary>
		/// Deletes the Entity from the persistent storage. This method succeeds also when the Entity is not present.
		/// Uses, if applicable, the ConcurrencyPredicateFactory to supply the predicate to limit delete activity.
		/// </summary>
		/// <returns>true if Delete succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the delete process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Delete();
		/// <summary>
		/// Deletes the Entity from the persistent storage. This method succeeds also when the Entity is not present.
		/// </summary>
		/// <param name="deleteRestriction">Predicate expression, meant for concurrency checks in a delete query. Overrules the predicate returned
		/// by a set ConcurrencyPredicateFactory object.</param>
		/// <returns>true if Delete succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the delete process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Delete(IPredicate deleteRestriction);
		/// <summary>
		/// Refetches the Entity from the persistent storage. Refetch is used to re-load an Entity which is marked "Out-of-sync", due to a save action. 
		/// Refetching an empty Entity has no effect.
		/// </summary>
		/// <returns>true if Refetch succeeded, false otherwise</returns>
		/// <exception cref="ORMQueryExecutionException">When an exception is caught during the save process. The caught exception is set as the
		/// inner exception. Encapsulation of database-related exceptions is necessary since these exceptions do not have a common exception framework
		/// implemented.</exception>
		bool Refetch();
		/// <summary>
		/// Converts the EntityFields inside this entity into an EntityFields node with inner nodes for each field.
		/// </summary>
		/// <returns>XmlNode containing the EntityFields in xml format</returns>
		XmlNode ToXml();
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively. 
		/// </summary>
		/// <param name="entityXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(out string entityXml);
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively. 
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		void WriteXml(XmlDocument parentDocument, out XmlNode entityNode);
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively.
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="entityXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(string rootNodeName, out string entityXml);
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively.
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		void WriteXml(string rootNodeName, XmlDocument parentDocument, out XmlNode entityNode);
		/// <summary>
		/// Will fill the entity and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntity.WriteXml() and the Xml has to be compatible with the structure of this entity.
		/// </summary>
		/// <param name="node">XmlNode with Xml data which should be read into this entity and its members. Node's root element is the root element
		/// of the entity's Xml data</param>
		void ReadXml(XmlNode node);
		/// <summary>
		/// Will fill the entity and its containing members (recursively) with the data stored in the Xml string passed in. The string xmlData has to
		/// be filled with Xml in the format written by IEntity.WriteXml() and the Xml has to be compatible with the structure of this entity.
		/// </summary>
		/// <param name="xmlData">string with Xml data which should be read into this entity and its members. This string has to be in the
		/// correct format and should be loadable into a new XmlDocument without problems</param>
		void ReadXml(string xmlData);

		/// <summary>
		/// The internal presentation of the data, which is an EntityFields object, which implements <see cref="IEntityFields"/>.
		/// </summary>
		IEntityFields Fields {get; set;}
		/// <summary>
		/// The EntityFactory to use when creating entity objects during a GetSingle*() call.
		/// </summary>
		IEntityFactory EntityFactoryToUse {get; set;}
		/// <summary>
		/// Returns true if this entity instance is in the middle of a serialization process, for example during a WriteXml() call.
		/// For internal use only. 
		/// </summary>
		bool IsSerializing {get; set;}

	}



	/// <summary>
	/// Interface for the definition of a Transaction class which is used to control a serie of actions on multiple entities or entity collection classes.
	/// SelfServicing specific
	/// </summary>
	public interface ITransaction
	{
		/// <summary>
		/// Commits the transaction in action. It will end all database activity, since commiting a transaction is finalizing it. After
		/// calling Commit or Rollback, the ITransaction implementing class will reset itself. When used in combination of COM+, it will
		/// call ContextUtil.SetComplete() to commit the current COM+ transaction.
		/// </summary>
		void Commit();
		/// <summary>
		/// Rolls back the transaction in action. It will end all database activity, since commiting a transaction is finalizing it. After
		/// calling Commit or Rollback, the ITransaction implementing class will reset itself. When used in combination of COM+, it will
		/// call ContextUtil.SetAbort() to abort (rollback) the current COM+ transaction.
		/// </summary>
		void Rollback();
		/// <summary>
		/// Adds the passed in object as a participant of this transaction. 
		/// </summary>
		/// <param name="participant">The ITransactionalElement implementing object which actions have to be included in this transaction</param>
		void Add(ITransactionalElement participant);
		/// <summary>
		/// Removes the passed in object from the transaction.
		/// </summary>
		/// <param name="participant">The ITransactionalElement implementing object which should be removed from this transaction</param>
		void Remove(ITransactionalElement participant);
		/// <summary>
		/// Increases the recursion counter with 1. If the counter reaches 0, the objectID's in the _entitiesInTransaction collection are removed.
		/// For internal use only.
		/// </summary>
		void SaveInRecursionStarted();
		/// <summary>
		/// Decreases the recursion counter with 1. If the counter reaches 0, the objectID's in the _entitiesInTransaction collection are removed.
		/// For internal use only.
		/// </summary>
		void SaveInRecursionEnded();
		/// <summary>
		/// Gets the isolation level the transaction should use. Only settable with the constructor.
		/// </summary>
		IsolationLevel TransactionIsolationLevel {get;}
		/// <summary>
		/// Gets the name of the transaction. Only settable with the constructor.
		/// </summary>
		string Name {get; }
		/// <summary>
		/// Gets the connection string used for the connection with the database. Only settable with the constructor.
		/// </summary>
		string ConnectionString {get; }
		/// <summary>
		/// The connection object to use with this transaction. 
		/// </summary>
		IDbConnection ConnectionToUse {get; }
		/// <summary>
		/// The physical transaction object used over <see cref="ConnectionToUse"/>.
		/// </summary>
		IDbTransaction PhysicalTransaction {get; }
		/// <summary>
		/// ArrayList of GUID's of the entities currently participating in this transaction. This collection is
		/// used to keep track of which entities already have been added during a recursive save.
		/// </summary>
		ArrayList EntitiesInTransaction {get;}
	}


	/// <summary>
	/// Interface which is necessary for the Transaction class. Every class which has to be controlled by a Transaction object
	/// has to implement this interface. Examples are: an Entity class and an Entity Collection Class.
	/// SelfServicing specific
	/// </summary>
	public interface ITransactionalElement
	{
		/// <summary>
		/// The <see cref="ITransaction"/> this ITransactionalElement implementing object is participating in. Only valid if
		/// <see cref="ParticipatesInTransaction"/> is true. If set to null, the ITransactionalElement is no longer participating
		/// in a transaction.
		/// </summary>
		ITransaction Transaction {get; set;}
		/// <summary>
		/// Flag to check if the ITransactionalElement implementing object is participating in a transaction or not.
		/// </summary>
		bool ParticipatesInTransaction {get; }
		/// <summary>
		/// When the <see cref="ITransaction"/> in which this element participates is commited, this element can succesfully finish actions performed by this
		/// element. This method is called by <see cref="ITransaction"/>, you should not call it by yourself. When this element doesn't participate in a
		/// transaction it finishes the actions itself, calling this method is not needed.
		/// </summary>
		void TransactionCommit();
		/// <summary>
		/// When the <see cref="ITransaction"/> in which this element participates is rolled back, this element has to roll back its internal variables.
		/// This method is called by <see cref="ITransaction"/>, you should not call it by yourself. 
		/// </summary>
		void TransactionRollback();
	}


	/// <summary>
	/// Interface for Data Access Objects (DAO). Every IEntity implementation has one specific Dao object
	/// SelfServicing specific.
	/// </summary>
	public interface IDao
	{
		/// <summary>
		/// Executes the passed in action query and, if not null, runs it inside the passed in transaction.
		/// </summary>
		/// <param name="queryToExecute">ActionQuery to execute.</param>
		/// <param name="containingTransaction">A containing transaction if caller is added to a transaction, or null of not.</param>
		/// <returns>execution result, which is the amount of rows affected (if applicable)</returns>
		int ExecuteActionQuery(IActionQuery queryToExecute, ITransaction containingTransaction);
		/// <summary>
		/// Wires the passed in transaction to the command object of the passed in query. If no transaction is passed in, nothing is wired.
		/// </summary>
		/// <param name="queryToWire">Query to wire up with the passed in transaction</param>
		/// <param name="activeTransaction">transaction to wire to the query</param>
		void WireTransaction(IQuery queryToWire, ITransaction activeTransaction);
		/// <summary>
		/// Executes the passed in retrieval query and, if not null, runs it inside the passed in transaction. Used to read 1 row.
		/// </summary>
		/// <param name="queryToExecute">Retrieval query to execute</param>
		/// <param name="containingTransaction">A containing transaction if caller is added to a transaction, or null of not.</param>
		/// <param name="fieldsToFill">The IEntityFields object to store the fetched data in</param>
		void ExecuteSingleRowRetrievalQuery(IRetrievalQuery queryToExecute, ITransaction containingTransaction, IEntityFields fieldsToFill);
		/// <summary>
		/// Executes the passed in retrieval query and, if not null, runs it inside the passed in transaction. Used to read 1 row.
		/// </summary>
		/// <param name="queryToExecute">Retrieval query to execute</param>
		/// <param name="containingTransaction">A containing transaction if caller is added to a transaction, or null of not.</param>
		/// <param name="entityFactory">the factory object which can produce the entities this method has to fill.</param>
		/// <param name="collectionToFill">Collection to fill with the retrieved rows.</param>
		/// <param name="allowDuplicates">Flag to signal if duplicates in the datastream should be loaded into the collection (true) or not (false)</param>
		/// <param name="validatorToUse">Validator object to use when creating a new entity. Can be null.</param>
		void ExecuteMultiRowRetrievalQuery(IRetrievalQuery queryToExecute, ITransaction containingTransaction, IEntityFactory entityFactory, 
			IEntityCollection collectionToFill, bool allowDuplicates, IValidator validatorToUse);

		/// <summary>
		/// Class which will supply the default value for a specified .NET type. Necessary for rowfetchers when a NULL field is found.
		/// </summary>
		ITypeDefaultValue TypeDefaultValueSupplier {get; set;}
	}


	/// <summary>
	/// Interface for the EntityCollection type. The collection defines typed basic collection behavior. 
	/// Selfservicing specific
	/// </summary>
	public interface IEntityCollection
	{
		/// <summary>
		/// Event which is fired if Remove or RemoveAt(index) is called and the remove is not yet executed.
		/// 'sender' is the object that will be removed from the list.
		/// </summary>
		event EventHandler BeforeRemove;

		/// <summary>
		/// Adds an IEntity object to the list.
		/// </summary>
		/// <param name="entityToAdd">Entity to add</param>
		/// <returns>Index in list</returns>
		int Add(IEntity entityToAdd);
		/// <summary>
		/// Inserts an IEntity on position Index
		/// </summary>
		/// <param name="index">Index where to insert the Object Entity</param>
		/// <param name="entityToAdd">Entity to insert</param>
		void Insert(int index, IEntity entityToAdd);
		/// <summary>
		/// Remove given IEntity from the list.
		/// </summary>
		/// <param name="entityToRemove">Entity object to remove from list.</param>
		void Remove(IEntity entityToRemove);
		/// <summary>
		/// Returns true if the list contains the given IEntity Object
		/// </summary>
		/// <param name="entityToFind">Entity object to check.</param>
		/// <returns>true if Entity exists in list.</returns>
		bool Contains(IEntity entityToFind);
		/// <summary>
		/// Returns index in the list of given IEntity object.
		/// </summary>
		/// <param name="entityToFind">Entity Object to check</param>
		/// <returns>index in list.</returns>
		int IndexOf(IEntity entityToFind);
		/// <summary>
		/// copy the complete list of IEntity objects to an array of IEntity objects.
		/// </summary>
		/// <param name="destination">Array of IEntity Objects wherein the contents of the list will be copied.</param>
		/// <param name="index">Start index to copy from</param>
		void CopyTo(IEntity[] destination, int index);
		/// <summary>
		/// Sets the entity information of the entity object containing this collection. Call this method only from
		/// entity classes which contain IEntityCollection members, like 'Customer' which contains 'Orders' entity collection.
		/// </summary>
		/// <param name="containingEntity">The entity containing this entity collection as a member variable</param>
		/// <param name="fieldName">The field the containing entity has mapped onto the relation which delivers the entities contained
		/// in this collection</param>
		void SetContainingEntityInfo(IEntity containingEntity, string fieldName);
		/// <summary>
		/// Saves all new/dirty Entities in the IEntityCollection in the persistent storage. If this IEntityCollection is added
		/// to a transaction, the save processes will be done in that transaction, if the entity isn't already added to another transaction.
		/// If the entity is already in another transaction, it will use that transaction. If no transaction is present, the saves are done in a
		/// new Transaction (which is created in an inherited method.). Will not recursively save entities inside the collection.
		/// </summary>
		/// <returns>Amount of entities inserted</returns>
		/// <remarks>All exceptions will be bubbled upwards so transaction code can anticipate on exceptions.</remarks>
		int SaveMulti();
		/// <summary>
		/// Saves all new/dirty Entities in the IEntityCollection in the persistent storage. If this IEntityCollection is added
		/// to a transaction, the save processes will be done in that transaction, if the entity isn't already added to another transaction.
		/// If the entity is already in another transaction, it will use that transaction. If no transaction is present, the saves are done in a
		/// new Transaction (which is created in an inherited method.)
		/// </summary>
		/// <param name="recurse">If true, will recursively save the entities inside the collection</param>
		/// <returns>Amount of entities inserted</returns>
		/// <remarks>All exceptions will be bubbled upwards so transaction code can anticipate on exceptions.</remarks>
		int SaveMulti(bool recurse);
		/// <summary>
		/// Deletes all Entities in the IEntityCollection from the persistent storage. If this IEntityCollection is added
		/// to a transaction, the delete processes will be done in that transaction, if the entity isn't already added to another transaction.
		/// If the entity is already in another transaction, it will use that transaction. If no transaction is present, the deletes are done in a
		/// new Transaction (which is created in an inherited method.)
		/// Deleted entities are marked deleted and are removed from the collection.
		/// </summary>
		/// <returns>Amount of entities deleted</returns>
		/// <remarks>All exceptions will be bubbled upwards so transaction code can anticipate on exceptions.</remarks>
		int DeleteMulti();
		/// <summary>
		/// Converts this entity collection to XML, recursively. Uses "EntityCollection" for the rootnode name
		/// </summary>
		/// <param name="entityCollectionXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(out string entityCollectionXml);
		/// <summary>
		/// Converts this entity collection to XML. Uses "EntityCollection" for the rootnode name
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityCollectionNode">The XmlNode representing this complete entitycollection object, including containing data.</param>
		void WriteXml(XmlDocument parentDocument, out XmlNode entityCollectionNode);
		/// <summary>
		/// Converts this entity collection to XML. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity collection.</param>
		/// <param name="entityCollectionXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(string rootNodeName, out string entityCollectionXml);
		/// <summary>
		/// Converts this entity collection to XML. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity collection.</param>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityCollectionNode">The XmlNode representing this complete entitycollection object, including containing data.</param>
		void WriteXml(string rootNodeName, XmlDocument parentDocument, out XmlNode entityCollectionNode);
		/// <summary>
		/// Will fill the entity collection and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntityCollection.WriteXml() and the Xml has to be compatible with the structure of this entity collection.
		/// </summary>
		/// <param name="node">XmlNode with Xml data which should be read into this entity and its members. Node's root element is the root element
		/// of the entity collection's Xml data</param>
		void ReadXml(XmlNode node);
		/// <summary>
		/// Will fill the entity collection and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntityCollection.WriteXml() and the Xml has to be compatible with the structure of this entity collection.
		/// </summary>
		/// <param name="xmlData">string with Xml data which should be read into this entity collection and its members. This string has to be in the
		/// correct format and should be loadable into a new XmlDocument without problems</param>
		void ReadXml(string xmlData);

		/// <summary>
		/// The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.
		/// </summary>
		long MaxNumberOfItemsToReturn {get; set;}
		/// <summary>
		/// The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.
		/// </summary>
		ISortExpression SortClauses {get; set;}
		/// <summary>
		/// Returns true if this collection contains dirty objects. If this collection contains dirty objects, an 
		/// already filled collection should not be refreshed until a save is performed. This property is calculated in real time
		/// and can be time consuming when the collection contains a lot of objects. Use this property only in cases when the value
		/// of this property is used to do a refetch or not.
		/// </summary>
		bool ContainsDirtyContents {get;}
		/// <summary>
		/// The EntityFactory to use when creating entity objects during a GetMulti() call or other logic which requires the creation of new entities.
		/// </summary>
		IEntityFactory EntityFactoryToUse {get; set;}
		/// <summary>
		/// Gets / sets the validator object to use when creating entity objects using the entity factory. Ignored when null.
		/// </summary>
		IValidator ValidatorToUse {get; set;}
		/// <summary>
		/// Surpresses the removal of all contents of the collection in a GetMulti*() call. Used by code in related entities to prevent the removal
		/// of objects when collection properties are accessed.
		/// </summary>
		bool SuppressClearInGetMulti {get; set;}
	}


	/// <summary>
	/// Interface for EntityFactory objects used by several methods which have to create entity objects on the fly.
	/// SelfServicing specific
	/// </summary>
	public interface IEntityFactory
	{
		/// <summary>
		/// Creates a new <see cref="IEntity"/> instance 
		/// </summary>
		/// <returns>the new IEntity instance</returns>
		IEntity Create();
	}


	/// <summary>
	/// Interface for the GroupByCollection class which is used to collect EntityFields which are used for the 
	/// GROUP BY clause in a retrieval query. When a group by collection is specified in a retrieval query, all
	/// fields in the resultset have to be in this collection.
	/// SelfServicing specific
	/// </summary>
	public interface IGroupByCollection
	{
		/// <summary>
		/// Adds the passed in IEntityField instance to the list. IEntityFields can be added just once.
		/// </summary>
		/// <param name="fieldToAdd">IEntityField instance to add</param>
		/// <returns>Index of added field in the list.</returns>
		int Add(IEntityField fieldToAdd);
		/// <summary>
		/// Removes the passed in IEntityField instance. Finds the object to remove using value compare.
		/// </summary>
		/// <param name="fieldToRemove">IEntityField instance to remove</param>
		void Remove(IEntityField fieldToRemove);
		/// <summary>
		/// Checks if the field is in the list. Does a value compare, not an object reference compare. 
		/// </summary>
		/// <param name="fieldToCheck">IEntityField to check for presencee.</param>
		/// <returns>true if a similar field is found in the collection, false otherwise.</returns>
		bool Contains(IEntityField fieldToCheck);

		/// <summary>
		/// Indexer in the collection.
		/// </summary>
		IEntityField this [int index] {get; set;}
		/// <summary>
		/// The amount of items currently stored in the IGroupByCollection
		/// </summary>
		int Count {get;}
	}


	/// <summary>
	/// interface for the factory which creates different sets of property descriptor sets. Required for complex databinding. 
	/// Selfservicing specific.
	/// </summary>
	public interface IPropertyDescriptorFactory
	{
		/// <summary>
		/// Creates a new propertydescriptorcollection using the specialized methods of the types stored INSIDE the type specified.
		/// </summary>
		/// <param name="typeOfCollection">type which contains other types which properties we're interested in.</param>
		/// <returns>filled propertydescriptorcollection of type inside the type specified.</returns>
		PropertyDescriptorCollection GetItemProperties(Type typeOfCollection);
		/// <summary>
		/// Constructs the actual property descriptor collection.
		/// </summary>
		/// <param name="entityToCheck">entity instance which properties should be included in the collection</param>
		/// <param name="typeOfEntity">full type of the entity</param>
		/// <returns>filled in property descriptor collection</returns>
		PropertyDescriptorCollection GetPropertyDescriptors(IEntity entityToCheck, Type typeOfEntity);
	}


	/// <summary>
	/// Interface for TypedView classes. 
	/// Selfservicing specific.
	/// </summary>
	public interface ITypedView
	{
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query. 
		/// Will use no sort filter, no select filter, will allow duplicate rows and will not limit the amount of rows returned
		/// </summary>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill();
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query. 
		/// Will not use a filter, will allow duplicate rows.
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses);
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query. 
		/// Will not use a filter.
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="allowDuplicates">Flag to allow duplicate rows (true) or not (false)</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates);
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query, using the specified filter
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="allowDuplicates">Flag to allow duplicate rows (true) or not (false)</param>
		/// <param name="selectFilter">Predicate expression to filter on the rows inserted in this TypedView object.</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates, IPredicate selectFilter);
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query, using the specified filter
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="allowDuplicates">Flag to allow duplicate rows (true) or not (false)</param>
		/// <param name="selectFilter">Predicate expression to filter on the rows inserted in this TypedView object.</param>
		/// <param name="transactionToUse">The transaction object to use. Can be null. If specified, the connection object of the transaction is
		/// used to fill the TypedView, which avoids deadlocks on SqlServer.</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates, IPredicate selectFilter, ITransaction transactionToUse);

		/// <summary>
		/// Returns the amount of rows in this typed view.
		/// </summary>
		int Count {get; }
	}


	/// <summary>
	/// Interface for TypedList classes. ITypedList is already defined in .NET, that's why it is suffixed with Lgp.
	/// Selfservicing specific.
	/// </summary>
	public interface ITypedListLgp : ITypedListCore
	{
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query. 
		/// Will use no sort filter, no select filter, will allow duplicate rows and will not limit the amount of rows returned
		/// </summary>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill();
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query. 
		/// Will not use a filter, will allow duplicate rows.
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses);
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query. 
		/// Will not use a filter.
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="allowDuplicates">Flag to allow duplicate rows (true) or not (false)</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates);
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query, using the specified filter
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="allowDuplicates">Flag to allow duplicate rows (true) or not (false)</param>
		/// <param name="selectFilter">Predicate which is used to filter the rows to insert in this Typed List instance</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates, IPredicate selectFilter);
		/// <summary>
		/// Fills itself with data. it builds a dynamic query and loads itself with that query, using the specified filter
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. specifying 0 means all rows are returned</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="allowDuplicates">Flag to allow duplicate rows (true) or not (false)</param>
		/// <param name="selectFilter">Predicate which is used to filter the rows to insert in this Typed List instance</param>
		/// <param name="transactionToUse">The transaction object to use. Can be null. If specified, the connection object of the transaction is
		/// used to fill the TypedView, which avoids deadlocks on SqlServer.</param>
		/// <returns>true if fill succeeded, false otherwise</returns>
		bool Fill(long maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates, IPredicate selectFilter, ITransaction transactionToUse);
	}

	#endregion


	#region Adapter specific interfaces
	/// <summary>
	/// Interface for the EntityField2 type. An EntityField2 is the unit which is used to hold the value for a given property of an entity.
	/// Adapter specific.
	/// </summary>
	public interface IEntityField2 : IEntityFieldCore
	{
		/// <summary>
		/// Converts this EntityField2 to an XmlNode. 
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will return. This document is required
		/// to create the new node object</param>
		/// <param name="entityFieldNode">The output parameter which will represent this EntityField2 as XmlNode</param>
		void WriteXml(XmlDocument parentDocument, out XmlNode entityFieldNode);

		/// <summary>
		/// The maximum length of the value of the entityfield (string/binary data). Is ignored for entityfields which hold non-string and non-binary values.
		/// Value initially set for this field is the length of the database column this field is mapped on.
		/// </summary>
		int MaxLength {get; set;}
		/// <summary>
		/// The scale of the value for this field. 
		/// Value initially set for this field is the scale of the database column this field is mapped on.
		/// </summary>
		byte Scale {get; set;}
		/// <summary>
		/// The precision of the value for this field.
		/// Value initially set for this field is the precision of the database column this field is mapped on.
		/// </summary>
		byte Precision {get; set;}
		/// <summary>
		/// Name of the containing object this field belongs to (entity or typed view). This name is required to retrieve persistence information.
		/// Set via constructor.
		/// </summary>
		string ContainingObjectName {get;}
	}


	/// <summary>
	/// Interface for the EntityFields2 type. An EntityFields2 type is a collection of IEntityField2 objects which forms the total amount of 
	/// fields for a given entity.
	/// Adapter specific
	/// </summary>
	public interface IEntityFields2 : IEditableObject
	{
		/// <summary>
		/// All changes to all <see cref="IEntityField"/> objects in this collection are applied. 
		/// </summary>
		void AcceptChanges();
		/// <summary>
		/// Per field, the last change made is rejected.
		/// </summary>
		void RejectChanges();
		/// <summary>
		/// Overrides the GetHashCode routine. It will calculate a hashcode for this set of entity fields using the eXclusive OR of the 
		/// hashcodes of the primary key fields in this set of entity fields. That hashcode is returned. If no primary key fields are present,
		/// the hashcode of the base class is returned, which will not be unique.
		/// </summary>
		/// <returns>Hashcode for this entity object, based on its primary key field values</returns>
		int GetHashCode();
		/// <summary>
		/// Returns the complete list of IEntityField objects as an array of IEntityFieldCore objects. IEntityField objects implement
		/// IEntityFieldCore
		/// </summary>
		/// <returns>Array of IEntityFieldCore objects</returns>
		IEntityFieldCore[] GetAsEntityFieldCoreArray();
		/// <summary>
		/// Converts this EntityFields2 object to a set of XmlNodes with all the fields as individual nodes.
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the nodes this method will create. This document is required
		/// to create the new nodes for the fields</param>
		/// <param name="parentNode">the node the fields will have to be added to.</param>
		void WriteXml(XmlDocument parentDocument, XmlNode parentNode);

		/// <summary>
		/// The amount of IEntityFields2 allocated in the EntityFields object.
		/// </summary>
		int	Count {get;}
		/// <summary>
		/// Gets / sets the flag if the contents of the EntityFields2 object is 'dirty', which means that one or more fields are changed. 
		/// <see cref="AcceptChanges"/> and <see cref="RejectChanges"/> reset this flag.
		/// </summary>
		bool IsDirty {get; set;}
		/// <summary>
		/// Gets / sets the EntityField2 on the specified Index. 
		/// </summary>
		/// <exception cref="System.IndexOutOfRangeException">When the index specified is not found in the internal datastorage.</exception>
		/// <exception cref="System.ArgumentNullException">When the passed in <see cref="IEntityField2"/> is null</exception>
		/// <exception cref="System.ArgumentException">When the passed in <see cref="IEntityField2"/> is already added. Fields have to be unique.</exception>
		IEntityField2 this [int index] {get; set;} 
		/// <summary>
		/// Gets the EntityField with the specified name.
		/// </summary>
		/// <exception cref="System.ArgumentException">When the specified name is the empty string or contains only spaces</exception>
		/// <remarks>This property is read-only. If you want to set a value, use the int indexer</remarks>
		IEntityField2 this [string name] {get;}
		/// <summary>
		/// List of IEntityField2 references which form the 'primary key', or uniquely identifying set of values for this set of fields, thus for the entity
		/// holding these fields.
		/// </summary>
		ArrayList PrimaryKeyFields {get;}
		/// <summary>
		/// The state of the EntityFields object, the heart and soul of every EntityObject.
		/// </summary>
		EntityState State {get; set;}
		/// <summary>
		/// Flag to signal if the entity fields have changed during an edit cycle which is controlled outside this IEntityFields2 object. If set to
		/// true, EndEdit will succeed, otherwise EndEdit will ignore any changes, since these are made in a previous edit cycle which is already
		/// ended.
		/// </summary>
		bool IsChangedInThisEditCycle {get; set;}
	}


	/// <summary>
	/// Interface used for all Entity2 classes 
	/// Adapter specific
	/// </summary>
	public interface IEntity2 : IEntityCore
	{
		/// <summary>
		/// Sets the internal parameter related to the fieldname passed to the instance relatedEntity. 
		/// </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		void SetRelatedEntity(IEntity2 relatedEntity, string fieldName);
		/// <summary>
		/// Unsets the internal parameter related to the fieldname passed to the instance relatedEntity. Reverses the actions taken by SetRelatedEntity() 
		/// </summary>
		/// <param name="relatedEntity">Instance to unset as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		void UnsetRelatedEntity(IEntity2 relatedEntity, string fieldName);
		/// <summary>
		/// Gets a collection of related entities referenced by this entity which depend on this entity (this entity is the PK side of their FK fields). These
		/// entities will have to be persisted after this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity2 objects, referenced by this entity</returns>
		IEntityCollection2 GetDependingRelatedEntities();
		/// <summary>
		/// Gets a collection of related entities referenced by this entity which this entity depends on (this entity is the FK side of their PK fields). These
		/// entities will have to be persisted before this entity during a recursive save.
		/// </summary>
		/// <returns>Collection with 0 or more IEntity2 objects, referenced by this entity</returns>
		IEntityCollection2 GetDependentRelatedEntities();
		/// <summary>
		/// Gets an ArrayList of all entity collections stored as member variables in this entity. The contents of the ArrayList is
		/// used by the DataAccessAdapter to perform recursive saves. Only 1:n related collections are returned.
		/// </summary>
		/// <returns>Collection with 0 or more IEntityCollection2 objects, referenced by this entity</returns>
		ArrayList GetMemberEntityCollections();
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively. Uses the LLBLGenProEntityName for the rootnode name
		/// </summary>
		/// <param name="entityXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(out string entityXml);
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively. Uses the LLBLGenProEntityName for the rootnode name
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		void WriteXml(XmlDocument parentDocument, out XmlNode entityNode);
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively.
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="entityXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(string rootNodeName, out string entityXml);
		/// <summary>
		/// Converts the data inside inside this entity into XML, recursively.
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity.</param>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityNode">The XmlNode representing this complete entity object, including containing data.</param>
		void WriteXml(string rootNodeName, XmlDocument parentDocument, out XmlNode entityNode);
		/// <summary>
		/// Will fill the entity and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntity2.WriteXml() and the Xml has to be compatible with the structure of this entity.
		/// </summary>
		/// <param name="node">XmlNode with Xml data which should be read into this entity and its members. Node's root element is the root element
		/// of the entity's Xml data</param>
		void ReadXml(XmlNode node);
		/// <summary>
		/// Will fill the entity and its containing members (recursively) with the data stored in the Xml string passed in. The string xmlData has to
		/// be filled with Xml in the format written by IEntity2.WriteXml() and the Xml has to be compatible with the structure of this entity.
		/// </summary>
		/// <param name="xmlData">string with Xml data which should be read into this entity and its members. This string has to be in the
		/// correct format and should be loadable into a new XmlDocument without problems</param>
		void ReadXml(string xmlData);
		/// <summary>
		/// The internal presentation of the data, which is an EntityFields2 object, which implements <see cref="IEntityFields2"/>.
		/// </summary>
		IEntityFields2 Fields {get; set;}
		/// <summary>
		/// Returns the full name for this entity, which is important for the DAO to find back persistence info for this entity.
		/// </summary>
		/// <example>CustomerEntity</example>
		string LLBLGenProEntityName {get;}
	}


	/// <summary>
	/// Interface for EntityFactory2 objects used by several methods which have to create entity objects on the fly.
	/// Factories have to add a valid validator object to the entities.
	/// Adapter specific
	/// </summary>
	public interface IEntityFactory2
	{
		/// <summary>
		/// Creates a new <see cref="IEntity2"/> instance 
		/// </summary>
		/// <returns>the new IEntity2 instance</returns>
		IEntity2 Create();
		/// <summary>
		/// Creates a new <see cref="IEntity2"/> instance but uses a special constructor which will set the Fields object of the new
		/// IEntity2 instance to the passed in fields object. Implement this method to support multi-type in single table inheritance.
		/// </summary>
		/// <param name="fields">Populated IEntityFields2 object for the new entity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		IEntity2 Create(IEntityFields2 fields);
		/// <summary>
		/// Creates, using the generated EntityFieldsFactory, the IEntityFields2 object for the entity to create. This method is used
		/// by internal code to create the fields object to store fetched data. 
		/// </summary>
		/// <returns>Empty IEntityFields2 object.</returns>
		IEntityFields2 CreateFields();
	}


	/// <summary>
	/// Interface for Data Access Adapter (DAA) objects. Instances of this interface are used as 'adapters' to work with databases. 
	/// Adapter specific.
	/// </summary>
	public interface IDataAccessAdapter
	{
		/// <summary>
		/// Executes the passed in action query and, if not null, runs it inside the passed in transaction.
		/// </summary>
		/// <param name="queryToExecute">ActionQuery to execute.</param>
		/// <returns>execution result, which is the amount of rows affected (if applicable)</returns>
		int ExecuteActionQuery(IActionQuery queryToExecute);
		/// <summary>
		/// Executes the passed in retrieval query and, if not null, runs it inside the passed in transaction. Used to read 1 row.
		/// It sets the connection object of the command object of query object passed in to the connection object of this class.
		/// </summary>
		/// <param name="queryToExecute">Retrieval query to execute</param>
		/// <param name="fieldsToFill">The IEntityFields2 object to store the fetched data in</param>
		/// <param name="fieldsPersistenceInfo">The IFieldPersistenceInfo objects for the fieldsToFill fields</param>
		void ExecuteSingleRowRetrievalQuery(IRetrievalQuery queryToExecute, IEntityFields2 fieldsToFill, IFieldPersistenceInfo[] fieldsPersistenceInfo);
		/// <summary>
		/// Executes the passed in retrieval query and, if not null, runs it inside the passed in transaction. Used to read 1 or more rows.
		/// It sets the connection object of the command object of query object passed in to the connection object of this class.
		/// </summary>
		/// <param name="queryToExecute">Retrieval query to execute</param>
		/// <param name="entityFactory">the factory object which can produce the entities this method has to fill.</param>
		/// <param name="collectionToFill">Collection to fill with the retrieved rows.</param>
		/// <param name="fieldsPersistenceInfo">The persistence information for the fields of the entity created by entityFactory</param>
		/// <param name="allowDuplicates">Flag to signal if duplicates in the datastream should be loaded into the collection (true) or not (false)</param>
		/// <param name="validatorToUse">Validator object to use when creating a new entity. Can be null.</param>
		void ExecuteMultiRowRetrievalQuery(IRetrievalQuery queryToExecute, IEntityFactory2 entityFactory, 
			IEntityCollection2 collectionToFill, IFieldPersistenceInfo[] fieldsPersistenceInfo, bool allowDuplicates, IValidator validatorToUse);
		/// <summary>
		/// Executes the passed in retrieval query and returns the results as a datatable using the passed in data-adapter. 
		/// It sets the connection object of the command object of query object passed in to the connection object of this class.
		/// </summary>
		/// <param name="queryToExecute">Retrieval query to execute</param>
		/// <param name="dataAdapterToUse">The dataadapter to use to fill the datatable.</param>
		/// <returns>DataTable with the rows requested</returns>
		DataTable ExecuteMultiRowDataTableRetrievalQuery(IRetrievalQuery queryToExecute, DbDataAdapter dataAdapterToUse);
		/// <summary>
		/// Executes the passed in retrieval query and returns the results in thedatatable specified using the passed in data-adapter. 
		/// It sets the connection object of the command object of query object passed in to the connection object of this class.
		/// </summary>
		/// <param name="queryToExecute">Retrieval query to execute</param>
		/// <param name="dataAdapterToUse">The dataadapter to use to fill the datatable.</param>
		/// <param name="tableToFill">DataTable to fill</param>
		/// <returns>true if succeeded, false otherwise</returns>
		bool ExecuteMultiRowDataTableRetrievalQuery(IRetrievalQuery queryToExecute, DbDataAdapter dataAdapterToUse, DataTable tableToFill);
		/// <summary>
		/// Wires an active transaction to the command object of the passed in query. If no transaction is in progress, nothing is wired.
		/// </summary>
		/// <param name="queryToWire">Query to wire up with the passed in transaction</param>
		void WireTransaction(IQuery queryToWire);
		/// <summary>
		/// Starts a new transaction. All database activity after this call will be ran in this transaction and all objects will participate
		/// in this transaction until its committed or rolled back. 
		/// If there is a transaction in progress, an exception is thrown.
		/// Will create and open a new connection if a transaction is not open and/or available.
		/// </summary>
		/// <param name="isolationLevelToUse">The isolation level to use for this transaction</param>
		/// <param name="name">The name for this transaction</param>
		/// <exception cref="InvalidOperationException">If a transaction is already in progress.</exception>
		void StartTransaction(IsolationLevel isolationLevelToUse, string name);
		/// <summary>
		/// Commits the transaction in action. It will end all database activity, since commiting a transaction is finalizing it. After
		/// calling Commit or Rollback, the ITransaction implementing class will reset itself.
		/// </summary>
		/// <remarks>Will close the active connection unless KeepConnectionOpen has been set to true</remarks>
		void Commit();
		/// <summary>
		/// Rolls back the transaction in action. It will end all database activity, since commiting a transaction is finalizing it. After
		/// calling Commit or Rollback, the ITransaction implementing class will reset itself. 
		/// </summary>
		/// <remarks>Will close the active connection unless KeepConnectionOpen has been set to true</remarks>
		void Rollback();
		/// <summary>
		/// Opens the active connection object. If the connection is already open, nothing is done.
		/// If no connection object is present, a new one is created
		/// </summary>
		void OpenConnection();
		/// <summary>
		/// Closes the active connection. If no connection is available or the connection is closed, nothing is done.
		/// </summary>
		void CloseConnection();
		/// <summary>
		/// Saves the passed in entity to the persistent storage. Will <i>not</i> refetch the entity after this save.
		/// The entity will stay out-of-sync. If the entity is new, it will be inserted, if the entity is existent, the changed
		/// entity fields will be changed in the database. Will do a recursive save.
		/// </summary>
		/// <param name="entityToSave">The entity to save</param>
		/// <returns>true if the save was succesful, false otherwise.</returns>
		/// <remarks>Will use a current transaction if a transaction is in progress</remarks>
		bool SaveEntity(IEntity2 entityToSave);
		/// <summary>
		/// Saves the passed in entity to the persistent storage. If the entity is new, it will be inserted, if the entity is existent, the changed
		/// entity fields will be changed in the database. Will do a recursive save.
		/// Will pass the concurrency predicate returned by GetConcurrencyPredicate(ConcurrencyPredicateType.Save) as update restriction.
		/// </summary>
		/// <param name="entityToSave">The entity to save</param>
		/// <param name="refetchAfterSave">When true, it will refetch the entity from the persistent storage so it will be up-to-date
		/// after the save action.</param>
		/// <returns>true if the save was succesful, false otherwise.</returns>
		/// <remarks>Will use a current transaction if a transaction is in progress</remarks>
		bool SaveEntity(IEntity2 entityToSave, bool refetchAfterSave);
		/// <summary>
		/// Saves the passed in entity to the persistent storage. If the entity is new, it will be inserted, if the entity is existent, the changed
		/// entity fields will be changed in the database. Will do a recursive save.
		/// Will pass the concurrency predicate returned by GetConcurrencyPredicate(ConcurrencyPredicateType.Save) as update restriction.
		/// </summary>
		/// <param name="entityToSave">The entity to save</param>
		/// <param name="refetchAfterSave">When true, it will refetch the entity from the persistent storage so it will be up-to-date
		/// after the save action.</param>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query. Will be ignored when the entity is new.</param>
		/// <returns>true if the save was succesful, false otherwise.</returns>
		/// <remarks>Will use a current transaction if a transaction is in progress</remarks>
		bool SaveEntity(IEntity2 entityToSave, bool refetchAfterSave, IPredicateExpression updateRestriction);
		/// <summary>
		/// Saves the passed in entity to the persistent storage. If the entity is new, it will be inserted, if the entity is existent, the changed
		/// entity fields will be changed in the database.
		/// </summary>
		/// <param name="entityToSave">The entity to save</param>
		/// <param name="refetchAfterSave">When true, it will refetch the entity from the persistent storage so it will be up-to-date
		/// after the save action.</param>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query. Will be ignored when the entity is new.</param>
		/// <param name="recurse">When true, it will save all dirty objects referenced (directly or indirectly) by entityToSave also.</param>
		/// <returns>true if the save was succesful, false otherwise.</returns>
		/// <remarks>Will use a current transaction if a transaction is in progress</remarks>
		bool SaveEntity(IEntity2 entityToSave, bool refetchAfterSave, IPredicateExpression updateRestriction, bool recurse);
		/// <summary>
		/// Fetches an entity from the persistent storage into the passed in Entity2 object using a primary key filter. The primary key fields of
		/// the entity passed in have to have the primary key values. (Example: CustomerID has to have a value, when you want to fetch a CustomerEntity
		/// from the persistent storage into the passed in object)
		/// </summary>
		/// <param name="entityToFetch">The entity object in which the fetched entity data will be stored. The primary key fields have to have a value.</param>
		/// <remarks>Will use a current transaction if a transaction is in progress, so MVCC or other concurrency scheme used by the database can be
		/// utilized</remarks>
		/// <returns>true if the Fetch was succesful, false otherwise</returns>
		bool FetchEntity(IEntity2 entityToFetch);
		/// <summary>
		/// Fetches an entity from the persistent storage into the object specified using the filter specified. 
		/// Use the entity's uniqueconstraint filter construction methods to construct the required uniqueConstraintFilter for the 
		/// unique constraint you want to use.
		/// </summary>
		/// <param name="entityToFetch">The entity object in which the fetched entity data will be stored.</param>
		/// <param name="uniqueConstraintFilter">The filter which should filter on fields with a unique constraint.</param>
		/// <returns>true if the Fetch was succesful, false otherwise</returns>
		bool FetchEntityUsingUniqueConstraint(IEntity2 entityToFetch, IPredicateExpression uniqueConstraintFilter);
		/// <summary>
		/// Fetches a new entity using the filter/relation combination filter passed in via <i>filterBucket</i> and the new entity is created using the
		/// passed in entity factory. Use this method when fetching a related entity using a current entity (for example, fetch the related Customer entity
		/// of an existing Order entity)
		/// </summary>
		/// <param name="entityFactoryToUse">The factory which will be used to create a new entity object which will be fetched</param>
		/// <param name="filterBucket">the completely filled in IRelationPredicateBucket object which will be used as a filter for the fetch. The fetch
		/// will only load the first entity loaded, even if the filter results into more entities being fetched</param>
		/// <returns>The new entity fetched.</returns>
		IEntity2 FetchNewEntity(IEntityFactory2 entityFactoryToUse, IRelationPredicateBucket filterBucket);
		/// <summary>
		/// Deletes the specified entity from the persistent storage. The entity is not usable after this call, the state is set to
		/// OutOfSync.
		/// Will use the current transaction if a transaction is in progress.
		/// </summary>
		/// <param name="entityToDelete">The entity instance to delete from the persistent storage</param>
		/// <returns>true if the delete was succesful, otherwise false.</returns>
		bool DeleteEntity(IEntity2 entityToDelete);
		/// <summary>
		/// Deletes the specified entity from the persistent storage. The entity is not usable after this call, the state is set to
		/// OutOfSync.
		/// Will use the current transaction if a transaction is in progress.
		/// </summary>
		/// <param name="entityToDelete">The entity instance to delete from the persistent storage</param>
		/// <param name="deleteRestriction">Predicate expression, meant for concurrency checks in a delete query</param>
		/// <returns>true if the delete was succesful, otherwise false.</returns>
		bool DeleteEntity(IEntity2 entityToDelete, IPredicateExpression deleteRestriction);
		/// <summary>
		/// Fetches one or more entities which match the filter information in the filterBucket into the EntityCollection passed.
		/// The entity collection object has to contain an entity factory object which will be the factory for the entity instances
		/// to be fetched.
		/// This overload returns all found entities and doesn't apply sorting
		/// </summary>
		/// <param name="collectionToFill">EntityCollection object containing an entity factory which has to be filled</param>
		/// <param name="filterBucket">filter information for retrieving the entities. If null, all entities are returned of the type created by
		/// the factory in the passed in EntityCollection instance.</param>
		void FetchEntityCollection(IEntityCollection2 collectionToFill, IRelationPredicateBucket filterBucket);
		/// <summary>
		/// Fetches one or more entities which match the filter information in the filterBucket into the EntityCollection passed.
		/// The entity collection object has to contain an entity factory object which will be the factory for the entity instances
		/// to be fetched.
		/// This overload returns all found entities and doesn't apply sorting
		/// </summary>
		/// <param name="collectionToFill">EntityCollection object containing an entity factory which has to be filled</param>
		/// <param name="filterBucket">filter information for retrieving the entities. If null, all entities are returned of the type created by
		/// the factory in the passed in EntityCollection instance.</param>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of entities to return. If 0, all entities matching the filter are returned</param>
		void FetchEntityCollection(IEntityCollection2 collectionToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn);
		/// <summary>
		/// Fetches one or more entities which match the filter information in the filterBucket into the EntityCollection passed.
		/// The entity collection object has to contain an entity factory object which will be the factory for the entity instances
		/// to be fetched.
		/// This overload returns all found entities and doesn't apply sorting
		/// </summary>
		/// <param name="collectionToFill">EntityCollection object containing an entity factory which has to be filled</param>
		/// <param name="filterBucket">filter information for retrieving the entities. If null, all entities are returned of the type created by
		/// the factory in the passed in EntityCollection instance.</param>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of entities to return. If 0, all entities matching the filter are returned</param>
		/// <param name="sortClauses">SortClause expression which is applied to the query executed, sorting the fetch result.</param>
		/// <exception cref="ArgumentException">If the passed in collectionToFill doesn't contain an entity factory.</exception>
		void FetchEntityCollection(IEntityCollection2 collectionToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, ISortExpression sortClauses);
		/// <summary>
		/// Saves all dirty objects inside the collection passed to the persistent storage. It will do this inside a transaction if a transaction
		/// is not yet available. Will not refetch saved entities and will not recursively save the entities.
		/// </summary>
		/// <param name="collectionToSave">EntityCollection with one or more dirty entities which have to be persisted</param>
		/// <returns>the amount of persisted entities</returns>
		int SaveEntityCollection(IEntityCollection2 collectionToSave);
		/// <summary>
		/// Saves all dirty objects inside the collection passed to the persistent storage. It will do this inside a transaction if a transaction
		/// is not yet available.
		/// </summary>
		/// <param name="collectionToSave">EntityCollection with one or more dirty entities which have to be persisted</param>
		/// <param name="refetchSavedEntitiesAfterSave">Refetches a saved entity from the database, so the entity will not be 'out of sync'</param>
		/// <param name="recurse">When true, it will save all dirty objects referenced (directly or indirectly) by the entities inside collectionToSave also.</param>
		/// <returns>the amount of persisted entities</returns>
		int SaveEntityCollection(IEntityCollection2 collectionToSave, bool refetchSavedEntitiesAfterSave, bool recurse);
		/// <summary>
		/// Deletes all dirty objects inside the collection passed from the persistent storage. It will do this inside a transaction if a transaction
		/// is not yet available. Entities which are physically deleted from the persistent storage are marked with the state 'Deleted' but are not
		/// removed from the collection.
		/// If the passed in entity has a concurrency predicate factory object, the returned predicate expression is used to restrict the delete process.		
		/// </summary>
		/// <param name="collectionToDelete">EntityCollection with one or more dirty entities which have to be persisted</param>
		/// <returns>the amount of physically deleted entities</returns>
		int DeleteEntityCollection(IEntityCollection2 collectionToDelete);
		/// <summary>
		/// Deletes all entities of the name passed in as <i>entityName</i> (e.g. "CustomerEntity") from the persistent storage if they match the filter
		/// supplied in <i>filterBucket</i>.
		/// </summary>
		/// <param name="entityName">The name of the entity to retrieve persistence information. For example "CustomerEntity". This name can be
		/// retrieved from an existing entity's Name property.</param>
		/// <param name="filterBucket">filter information to filter out the entities to delete</param>
		/// <returns>the amount of physically deleted entities</returns>
		int DeleteEntitiesDirectly(string entityName, IRelationPredicateBucket filterBucket);
		/// <summary>
		/// Updates all entities of the same type of the entity <i>entityWithNewValues</i> directly in the persistent storage if they match the filter
		/// supplied in <i>filterBucket</i>. Only the fields changed in entityWithNewValues are updated for these fields. 
		/// </summary>
		/// <param name="entityWithNewValues">Entity object which contains the new values for the entities of the same type and which match the filter
		/// in filterBucket. Only fields which are changed are updated.</param>
		/// <param name="filterBucket">filter information to filter out the entities to update.</param>
		/// <returns>the amount of physically updated entities</returns>
		int UpdateEntitiesDirectly(IEntity2 entityWithNewValues, IRelationPredicateBucket filterBucket);
		/// <summary>
		/// Fetches the fields passed in fieldCollectionToFetch from the persistent storage using the relations and filter information stored in
		/// filterBucket into the DataTable object passed in. Use this routine to fill a typed list object. Doesn't apply any sorting, doesn't limit
		/// the resultset on the amount of rows to return, does allow duplicates.
		/// For TypedView filling, use the method FetchTypedView()
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields to fetch into the datatable. The fields
		/// inside this object are used to construct the selection resultset.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data. 
		/// Use the TypedList's method GetRelationInfo() to retrieve this bucket.</param>
		void FetchTypedList(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket);
		/// <summary>
		/// Fetches the fields passed in fieldCollectionToFetch from the persistent storage using the relations and filter information stored in
		/// filterBucket into the DataTable object passed in. Use this routine to fill a typed list object. Doesn't apply any sorting, doesn't limit
		/// the resultset on the amount of rows to return.
		/// For TypedView filling, use the method FetchTypedView()
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields to fetch into the datatable. The fields
		/// inside this object are used to construct the selection resultset.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data. 
		/// Use the TypedList's method GetRelationInfo() to retrieve this bucket.</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		void FetchTypedList(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket, bool allowDuplicates);
		/// <summary>
		/// Fetches the fields passed in fieldCollectionToFetch from the persistent storage using the relations and filter information stored in
		/// filterBucket into the DataTable object passed in. Use this routine to fill a typed list object. Doesn't apply any sorting.
		/// For TypedView filling, use the method FetchTypedView()
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields to fetch into the datatable. The fields
		/// inside this object are used to construct the selection resultset.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data. 
		/// Use the TypedList's method GetRelationInfo() to retrieve this bucket.</param>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. If 0, all rows matching the filter are returned</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		void FetchTypedList(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, bool allowDuplicates);
		/// <summary>
		/// Fetches the fields passed in fieldCollectionToFetch from the persistent storage using the relations and filter information stored in
		/// filterBucket into the DataTable object passed in. Use this routine to fill a typed list object. 
		/// For TypedView filling, use the method FetchTypedView()
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields to fetch into the datatable. The fields
		/// inside this object are used to construct the selection resultset. Use the typed list's method GetFieldsInfo() to retrieve
		/// this IEntityField2 information</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data. 
		/// Use the TypedList's method GetRelationInfo() to retrieve this bucket.</param>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. If 0, all rows matching the filter are returned</param>
		/// <param name="sortClauses">SortClause expression which is applied to the query executed, sorting the fetch result.</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		void FetchTypedList(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates);
		/// <summary>
		/// Fetches the Typed View fields passed in fieldCollectionToFetch from the persistent storage using the filter information stored in
		/// filterBucket into the DataTable object passed in. Doesn't apply any sorting, doesn't limit the amount of rows returned by the query, doesn't
		/// apply any filtering, allows duplicate rows.
		/// Use this routine to fill a TypedView object.
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields of the typed view to fetch into the datatable.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		void FetchTypedView(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill);
		/// <summary>
		/// Fetches the Typed View fields passed in fieldCollectionToFetch from the persistent storage using the filter information stored in
		/// filterBucket into the DataTable object passed in. Doesn't apply any sorting, doesn't limit the amount of rows returned by the query, doesn't
		/// apply any filtering.
		/// Use this routine to fill a TypedView object.
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields of the typed view to fetch into the datatable.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		void FetchTypedView(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, bool allowDuplicates);
		/// <summary>
		/// Fetches the Typed View fields passed in fieldCollectionToFetch from the persistent storage using the filter information stored in
		/// filterBucket into the DataTable object passed in. Doesn't apply any sorting, doesn't limit the amount of rows returned by the query.
		/// Use this routine to fill a TypedView object.
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields of the typed view to fetch into the datatable.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data. 
		/// Use the TypedList's method GetRelationInfo() to retrieve this bucket.</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		void FetchTypedView(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket, bool allowDuplicates);
		/// <summary>
		/// Fetches the Typed View fields passed in fieldCollectionToFetch from the persistent storage using the filter information stored in
		/// filterBucket into the DataTable object passed in. Doesn't apply any sorting
		/// Use this routine to fill a TypedView object.
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields of the typed view to fetch into the datatable.</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data. 
		/// Use the TypedList's method GetRelationInfo() to retrieve this bucket.</param>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. If 0, all rows matching the filter are returned</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		void FetchTypedView(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, bool allowDuplicates);
		/// <summary>
		/// Fetches the Typed View fields passed in fieldCollectionToFetch from the persistent storage using the filter information stored in
		/// filterBucket into the DataTable object passed in. Use this routine to fill a TypedView object.
		/// </summary>
		/// <param name="fieldCollectionToFetch">IEntityField2 collection which contains the fields of the typed view to fetch into the datatable.
		/// Use the Typed View's method GetFieldsInfo() to get this IEntityField2 field collection</param>
		/// <param name="dataTableToFill">The datatable object to fill with the data for the fields in fieldCollectionToFetch</param>
		/// <param name="filterBucket">filter information (relations and predicate expressions) for retrieving the data.</param>
		/// <param name="maxNumberOfItemsToReturn">The maximum amount of rows to return. If 0, all rows matching the filter are returned</param>
		/// <param name="sortClauses">SortClause expression which is applied to the query executed, sorting the fetch result.</param>
		/// <param name="allowDuplicates">When true, it will not filter out duplicate rows, otherwise it will DISTINCT duplicate rows.</param>
		/// <remarks>To fill a DataTable with entity rows, use this method as well, by passing the Fields collection of an entity of the same type
		/// as the one you want to fetch as fieldCollectionToFetch.</remarks>
		void FetchTypedView(IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, ISortExpression sortClauses, bool allowDuplicates);
		/// <summary>
		/// Called right before the actual Save action is executed.
		/// </summary>
		/// <param name="saveQuery">the ActionQuery object which will be executed</param>
		/// <param name="entityToSave">the entity which will be saved by saveQuery</param>
		void OnSaveEntity(IActionQuery saveQuery, IEntity2 entityToSave);
		/// <summary>
		/// Called right after the actual Save action was executed.
		/// </summary>
		/// <param name="saveQuery">the ActionQuery object which will be executed</param>
		/// <param name="entityToSave">the entity which is saved by saveQuery</param>
		void OnSaveEntityComplete(IActionQuery saveQuery, IEntity2 entityToSave);
		/// <summary>
		/// Called at the start of the SaveEntityCollection() method
		/// </summary>
		/// <param name="entityCollectionToSave">the entity collection to save</param>
		void OnSaveEntityCollection(IEntityCollection2 entityCollectionToSave);
		/// <summary>
		/// Called at the end of the SaveEntityCollection() method
		/// </summary>
		/// <param name="entityCollectionToSave">the entity collection which was saved</param>
		void OnSaveEntityCollectionComplete(IEntityCollection2 entityCollectionToSave);
		/// <summary>
		/// Called right before the actual delete action is executed
		/// </summary>
		/// <param name="deleteQuery">the ActionQuery object which will be executed</param>
		/// <param name="entityToDelete">the entity which will be deleted by deleteQuery</param>
		void OnDeleteEntity(IActionQuery deleteQuery, IEntity2 entityToDelete);
		/// <summary>
		/// Called right before the actual delete action is executed
		/// </summary>
		/// <param name="deleteQuery">the ActionQuery object which will be executed</param>
		/// <param name="entityToDelete">the entity which was deleted by deleteQuery</param>
		void OnDeleteEntityComplete(IActionQuery deleteQuery, IEntity2 entityToDelete);
		/// <summary>
		/// Called at the start of the DeleteEntityCollection method
		/// </summary>
		/// <param name="entityCollectionToDelete">the entity collection to delete</param>
		void OnDeleteEntityCollection(IEntityCollection2 entityCollectionToDelete);
		/// <summary>
		/// Called right before the actual delete query is executed
		/// </summary>
		/// <param name="deleteQuery">The ActionQuery to execute</param>
		void OnDeleteEntitiesDirectly(IActionQuery deleteQuery);
		/// <summary>
		/// Called right after the actual delete query is executed
		/// </summary>
		/// <param name="deleteQuery">The ActionQuery to execute</param>
		void OnDeleteEntitiesDirectlyComplete(IActionQuery deleteQuery);
		/// <summary>
		/// Called at the end of the DeleteEntityCollection method
		/// </summary>
		/// <param name="entityCollectionToDelete">the entity collection which was delete</param>
		void OnDeleteEntityCollectionComplete(IEntityCollection2 entityCollectionToDelete);
		/// <summary>
		/// Called right before the actual fetch is executed.
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery which will be executed</param>
		/// <param name="fieldsToFetch">the IEntityFields2 object which will be filled by selectQuery</param>
		void OnFetchEntity(IRetrievalQuery selectQuery, IEntityFields2 fieldsToFetch);
		/// <summary>
		/// Called right after the actual fetch is executed.
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery which was executed</param>
		/// <param name="fieldsToFetch">the IEntityFields2 object which was filled by selectQuery</param>
		void OnFetchEntityComplete(IRetrievalQuery selectQuery, IEntityFields2 fieldsToFetch);
		/// <summary>
		/// Called right before the actual fetch is executed
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery which will be executed</param>
		/// <param name="entityCollectionToFetch">the entity collection to fill</param>
		void OnFetchEntityCollection(IRetrievalQuery selectQuery, IEntityCollection2 entityCollectionToFetch);
		/// <summary>
		/// Called right after the actual fetch is executed
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery which was executed</param>
		/// <param name="entityCollectionToFetch">the entity collection which was filled</param>
		void OnFetchEntityCollectionComplete(IRetrievalQuery selectQuery, IEntityCollection2 entityCollectionToFetch);
		/// <summary>
		/// Called right before the actual fetch is executed
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery object to execute</param>
		/// <param name="fieldCollectionToFetch">the fieldslist used to construct the query</param>
		/// <param name="dataTableToFill">the datatable object to fill</param>
		void OnFetchTypedList(IRetrievalQuery selectQuery, IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill);
		/// <summary>
		/// Called right after the actual fetch is executed
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery object which was executed</param>
		/// <param name="fieldCollectionToFetch">the fieldslist used to construct the query</param>
		/// <param name="dataTableToFill">the datatable object which was filled</param>
		void OnFetchTypedListComplete(IRetrievalQuery selectQuery, IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill);
		/// <summary>
		/// Called right before the actual fetch is executed
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery object to execute</param>
		/// <param name="fieldCollectionToFetch">the fieldslist used to construct the query</param>
		/// <param name="dataTableToFill">the datatable object to fill</param>
		void OnFetchTypedView(IRetrievalQuery selectQuery, IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill);
		/// <summary>
		/// Called right after the actual fetch is executed
		/// </summary>
		/// <param name="selectQuery">the RetrievalQuery object which was executed</param>
		/// <param name="fieldCollectionToFetch">the fieldslist used to construct the query</param>
		/// <param name="dataTableToFill">the datatable object which was filled</param>
		void OnFetchTypedViewComplete(IRetrievalQuery selectQuery, IEntityFields2 fieldCollectionToFetch, DataTable dataTableToFill);
		/// <summary>
		/// Called right before the actual update query is executed
		/// </summary>
		/// <param name="updateQuery">The ActionQuery to execute</param>
		void OnUpdateEntitiesDirectly(IActionQuery updateQuery);
		/// <summary>
		/// Called right after the actual update query is executed
		/// </summary>
		/// <param name="updateQuery">The ActionQuery to execute</param>
		void OnUpdateEntitiesDirectlyComplete(IActionQuery updateQuery);

		/// <summary>
		/// Class which will supply the default value for a specified .NET type. Necessary for rowfetchers when a NULL field is found.
		/// </summary>
		ITypeDefaultValue TypeDefaultValueSupplier {get; set;}
		/// <summary>
		/// Gets IsTransactionInProgress. True when there is a transaction in progress.
		/// </summary>
		bool IsTransactionInProgress {get;}
		/// <summary>
		/// Gets / sets the isolation level a transaction should use. 
		/// Setting this during a transaction in progress has no effect on the current running transaction.
		/// </summary>
		IsolationLevel TransactionIsolationLevel {get; set;}
		/// <summary>
		/// Gets the name of the transaction. Setting this during a transaction in progress has no effect on the current running transaction.
		/// </summary>
		string TransactionName {get; set;}
		/// <summary>
		/// Gets / sets the connection string to use for the connection with the database.
		/// </summary>
		string ConnectionString {get; set;}
		/// <summary>
		/// Gets / sets KeepConnectionOpen, a flag used to keep open connections after a database action has finished.
		/// </summary>
		bool KeepConnectionOpen {get; set;}
		/// <summary>
		/// Gets / sets the timeout value to use with the command object(s) created by the adapter.
		/// Default is 30 seconds
		/// Set this prior to calling a method which executes database logic.
		/// </summary>
		int CommandTimeOut {get; set;}
	}


	/// <summary>
	/// Interface for the EntityCollection2 type. The collection defines typed basic collection behavior. 
	/// Adapter specific
	/// </summary>
	public interface IEntityCollection2
	{
		/// <summary>
		/// Event which is fired if Remove or RemoveAt(index) is called and the remove is not yet executed.
		/// 'sender' is the object that will be removed from the list.
		/// </summary>
		event EventHandler BeforeRemove;

		/// <summary>
		/// Adds an IEntity2 object to the list.
		/// </summary>
		/// <param name="entityToAdd">Entity2 to add</param>
		/// <returns>Index in list</returns>
		int Add(IEntity2 entityToAdd);
		/// <summary>
		/// Inserts an IEntity2 on position Index
		/// </summary>
		/// <param name="index">Index where to insert the Object Entity</param>
		/// <param name="entityToAdd">Entity2 to insert</param>
		void Insert(int index, IEntity2 entityToAdd);
		/// <summary>
		/// Remove given IEntity2 instance from the list.
		/// </summary>
		/// <param name="entityToRemove">Entity2 object to remove from list.</param>
		void Remove(IEntity2 entityToRemove);
		/// <summary>
		/// Returns true if the list contains the given IEntity2 Object
		/// </summary>
		/// <param name="entityToFind">Entity2 object to check.</param>
		/// <returns>true if Entity2 exists in list.</returns>
		bool Contains(IEntity2 entityToFind);
		/// <summary>
		/// Returns index in the list of given IEntity2 object.
		/// </summary>
		/// <param name="entityToFind">Entity2 Object to check</param>
		/// <returns>index in list.</returns>
		int IndexOf(IEntity2 entityToFind);
		/// <summary>
		/// copy the complete list of IEntity2 objects to an array of IEntity objects.
		/// </summary>
		/// <param name="destination">Array of IEntity2 Objects wherein the contents of the list will be copied.</param>
		/// <param name="index">Start index to copy from</param>
		void CopyTo(IEntity2[] destination, int index);
		/// <summary>
		/// Constructs the actual property descriptor collection.
		/// </summary>
		/// <param name="entityToCheck">entity instance which properties should be included in the collection</param>
		/// <param name="typeOfEntity">full type of the entity</param>
		/// <returns>filled in property descriptor collection</returns>
		PropertyDescriptorCollection GetPropertyDescriptors(IEntity2 entityToCheck, Type typeOfEntity);
		/// <summary>
		/// Sets the entity information of the entity object containing this collection. Call this method only from
		/// entity classes which contain EntityCollection members, like 'Customer' which contains 'Orders' entity collection.
		/// </summary>
		/// <param name="containingEntity">The entity containing this entity collection as a member variable</param>
		/// <param name="fieldName">The field the containing entity has mapped onto the relation which delivers the entities contained
		/// in this collection</param>
		void SetContainingEntityInfo(IEntity2 containingEntity, string fieldName);
		/// <summary>
		/// Converts this entity collection to XML, recursively. Uses "EntityCollection" for the rootnode name
		/// </summary>
		/// <param name="entityCollectionXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(out string entityCollectionXml);
		/// <summary>
		/// Converts this entity collection to XML. Uses "EntityCollection" for the rootnode name
		/// </summary>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityCollectionNode">The XmlNode representing this complete entitycollection object, including containing data.</param>
		void WriteXml(XmlDocument parentDocument, out XmlNode entityCollectionNode);
		/// <summary>
		/// Converts this entity collection to XML. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity collection.</param>
		/// <param name="entityCollectionXml">The complete outer XML as string, representing this complete entity object, including containing data.</param>
		void WriteXml(string rootNodeName, out string entityCollectionXml);
		/// <summary>
		/// Converts this entity collection to XML. 
		/// </summary>
		/// <param name="rootNodeName">name of root element to use when building a complete XML representation of this entity collection.</param>
		/// <param name="parentDocument">the XmlDocument which will contain the node this method will create. This document is required
		/// to create the new node object</param>
		/// <param name="entityCollectionNode">The XmlNode representing this complete entitycollection object, including containing data.</param>
		void WriteXml(string rootNodeName, XmlDocument parentDocument, out XmlNode entityCollectionNode);
		/// <summary>
		/// Will fill the entity collection and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntityCollection2.WriteXml() and the Xml has to be compatible with the structure of this entity collection.
		/// </summary>
		/// <param name="node">XmlNode with Xml data which should be read into this entity and its members. Node's root element is the root element
		/// of the entity collection's Xml data</param>
		void ReadXml(XmlNode node);
		/// <summary>
		/// Will fill the entity collection and its containing members (recursively) with the data stored in the XmlNode passed in. The XmlNode has to
		/// be filled with Xml in the format written by IEntityCollection2.WriteXml() and the Xml has to be compatible with the structure of this entity collection.
		/// </summary>
		/// <param name="xmlData">string with Xml data which should be read into this entity collection and its members. This string has to be in the
		/// correct format and should be loadable into a new XmlDocument without problems</param>
		void ReadXml(string xmlData);
		
		/// <summary>
		/// Returns true if this collection contains dirty objects. If this collection contains dirty objects, an 
		/// already filled collection should not be refreshed until a save is performed. 
		/// Set operation is for XML Deserialization.
		/// </summary>
		bool ContainsDirtyContents {get;set;}
		/// <summary>
		/// The EntityFactory2 to use when creating entity objects when bound to a control and AddNew is enabled.
		/// </summary>
		IEntityFactory2 EntityFactoryToUse {get; set;}
		/// <summary>
		/// Gets / sets the validator object to use when creating entity objects using the entity factory. Ignored when null.
		/// </summary>
		IValidator ValidatorToUse {get; set;}
		/// <summary>
		/// Simple indexer. 
		/// </summary>
		IEntity2 this [int index] {get; set;}
		/// <summary>
		/// The amount of IEntity2 elements in this entity collection
		/// </summary>
		int Count {get;}
		/// <summary>
		/// Get / set the readonly flag for this collection. If set to true, it will affect IBindingList parameters as well.
		/// </summary>
		bool IsReadOnly {get; set;}
		/// <summary>
		/// Returns the collection of entities which are flagged as dirty. 
		/// </summary>
		ArrayList DirtyEntities {get;}
		/// <summary>
		/// When set to true, an entity passed to Add() or Insert() will be tested if it's already present. If so, the index is returned and the
		/// object is not added again. If set to false (default: true) this check is not performed. Setting this property to true can slow down fetch logic.
		/// DataAccessAdapter's fetch logic sets this property to false during a multi-entity fetch.
		/// </summary>
		bool DoNotPerformAddIfPresent {get; set;}
	}


	/// <summary>
	/// Interface for TypedView classes. 
	/// Adapter specific.
	/// </summary>
	public interface ITypedView2
	{
		/// <summary>
		/// Gets the IEntityFields2 collection of fields of this typed view. Use this method in combination with the FetchTypedView() methods in 
		/// DataAccessAdapter.
		/// </summary>
		/// <returns>Ready to use IEntityFields2 collection object.</returns>
		IEntityFields2 GetFieldsInfo();

		/// <summary>
		/// Returns the amount of rows in this typed view.
		/// </summary>
		int Count {get;}
	}


	/// <summary>
	/// Interface for TypedList classes. ITypedList is already defined in .NET, that's why it is suffixed with Lgp.
	/// Adapter specific.
	/// </summary>
	public interface ITypedListLgp2 : ITypedListCore
	{
		/// <summary>
		/// Gets the IEntityFields2 collection of fields of this typed view. Use this method in combination with the FetchTypedView() methods in 
		/// DataAccessAdapter.
		/// </summary>
		/// <returns>Ready to use IEntityFields2 collection object.</returns>
		IEntityFields2 GetFieldsInfo();
		/// <summary>
		/// Gets the IRelationPredicateBucket object which contains the relation information for this Typed List. Use this method in combination with the 
		/// FetchTypedList() methods in DataAccessAdapter.
		/// </summary>
		/// <returns>Ready to use IRelationPredicateBucket object.</returns>
		IRelationPredicateBucket GetRelationInfo();
	}
	#endregion


	#region Generic interfaces
	/// <summary>
	/// Interface for ConcurrencyPredicateFactory objects which can be provided by the developer to produce at runtime predicate objects which will
	/// be added to the update query or delete query. The predicate is returned by the IEntity2 method GetConcurrencyPredicate. Especially useful
	/// in recursive saves.
	/// </summary>
	/// <remarks>This interface is changed, due to a design error. One implementation can be used for both template groups.</remarks>
	public interface IConcurrencyPredicateFactory
	{
		/// <summary>
		/// Creates the requested predicate of the type specified
		/// </summary>
		/// <param name="predicateTypeToCreate">The type of predicate to create</param>
		/// <param name="containingEntity">the entity object containing this IConcurrencyPredicateFactory instance.</param>
		/// <returns>A ready to use predicate to use in the query to execute. Can be null/Nothing, in which case the predicate is ignored</returns>
		/// <remarks>Use this method instead of the original version, to prevent incomplete instantiation from XML.</remarks>
		IPredicateExpression CreatePredicate(ConcurrencyPredicateType predicateTypeToCreate, object containingEntity);
	}


	/// <summary>
	/// Interface for implementing an entity validator. The validate method is called prior to a save action or when IEntity(2).Validate is called. 
	/// It's up to the implementor what happens, it is recommended to throw an ORMEntityValidationException or derived exception from that exception when
	/// the validation fails, so recursive saved entities which are validated on the fly in the recursion will fail the transaction correctly when the validation
	/// fails. Recursive saves ignore the return value of the Validate method.
	/// </summary>
	public interface IEntityValidator
	{
		/// <summary>
		/// Validates the entity passed in.
		/// </summary>
		/// <param name="containingEntity">the entity object containing this IEntityValidator instance.</param>
		/// <returns>true if validation succeeded, false otherwise</returns>
		bool Validate(object containingEntity);
	}


	/// <summary>
	/// Interface definition for a bucket class which contains both a predicate expression and a relation collection which are related to each other 
	/// (the predicate expression works in combination with the relation collection's contents). Used in adapter's context however can also be used
	/// in other situations, for example custom templates.
	/// </summary>
	public interface IRelationPredicateBucket
	{
		/// <summary>
		/// The relation collection with EntityRelation objects which is used in combination with the PredicateExpression in this bucket
		/// </summary>
		IRelationCollection Relations {get;}
		/// <summary>
		/// The predicate expression to use in combination with the Relations in this bucket.
		/// </summary>
		IPredicateExpression PredicateExpression {get;}
	}


	/// <summary>
	/// Interface definition which defines the core IEntityField set. Is implemented by other interfaces like IEntityField and IEntityField2.
	/// Generic.
	/// </summary>
	public interface IEntityFieldCore : IEditableObject
	{
		/// <summary>
		/// Accepts <see cref="CurrentValue"/> as the current value, discarding a saved original value.
		/// </summary>
		void AcceptChange();
		/// <summary>
		/// Rejects the <see cref="CurrentValue"/>, the <see cref="CurrentValue"/> will be discarded rolled back to the stored original value
		/// and <see cref="IsChanged"/> will report false.
		/// </summary>
		void RejectChange();
		/// <summary>
		/// Overwrites the current value with the value passed. This bypasses value checking and field properties like readonly. 
		/// Used by internal code only. Do not call this from your code.
		/// </summary>
		/// <param name="value">Value to store as the current value</param>
		void ForcedCurrentValueWrite(object value);
		/// <summary>
		/// Overrides the GetHashCode() method. It will return the hashcode of the value of the field as the hashcode. 
		/// </summary>
		/// <returns>hashcode of the value of the field.</returns>
		int GetHashCode();

		/// <summary>
		/// The name of the field. Name cannot be of zero length nor can they consist of solely spaces. Leading and trailing spaces are trimmed.
		/// </summary>
		/// <exception cref="ArgumentException">The value specified for Name is invalid.</exception>
		string Name {get; set;}
		/// <summary>
		/// The alias to use for this field. Only used when this field object is part of a typed list. 
		/// Adapter: returns the alias set in the designer
		/// SelfServicing: returns Name
		/// </summary>
		string Alias {get; set;}
		/// <summary>
		/// Gets the current value for this field and sets the new value for this field, by overwriting current value. The value in 
		/// currentValue is discarded, versioning control has to save the original value of currentValue before this property is called. 
		/// </summary>
		/// <remarks>
		/// Calling this property directly will not trigger versioning control,
		/// thus calling this property directly is not recommended. Call <see cref="EntityBase.SetNewFieldValue"/> instead.
		/// Type of the new value has to be the same as <see cref="DataType"/>, which is set in the
		/// constructor. If this field is set to readonly, an exception is raised. 
		/// </remarks>
		/// <exception cref="ORMFieldIsReadonlyException">The field is set to readonly and can't be changed.</exception>
		/// <exception cref="ORMValueTypeMismatchException">The value specified is not of the same <see cref="DataType"/> as this field.</exception>
		object CurrentValue {get;set;}
		/// <summary>
		/// The <see cref="System.Type"/> of the values of this field.
		/// </summary>
		System.Type	DataType {get;}
		/// <summary>
		/// If set to true, in the constructor, this field will end up in the PrimaryKey field list of the containing IEntityFields object.
		/// </summary>
		bool IsPrimaryKey {get;}
		/// <summary>
		/// If the value of this field is changed, this property is set to true. Set when <see cref="CurrentValue"/> receives a valid value. Set to 
		/// false when <see cref="AcceptChange"/> is called succesfully.
		/// </summary>
		bool IsChanged {get; set;}
		/// <summary>
		/// If the original value in the column for this entityfield is DBNull (NULL), this parameter should be set to true, otherwise to false.
		/// In BL Logic, it's impractical to work with NULL values, so these are converted to handable values. The developer can still determine if
		/// the original value was DBNull by checking this field. Using NULL values is not recommended. 
		/// If <see cref="IFieldPersistenceInfo.SourceColumnIsNullable"/> is false, IsNull is undefined.
		/// </summary>
		bool IsNull {get; set;}
		/// <summary>
		/// Gets the field index related to this IEntityField, so the field can be used to retrieve the field index.
		/// </summary>
		int FieldIndex {get;}
	}


	/// <summary>
	/// Interface used for as a base for all Entity classes
	/// Generic specific
	/// </summary>
	public interface IEntityCore : IEditableObject
	{
		/// <summary>
		/// Event handler declaration for the event that is fired each time the one of values of this entity are changed.
		/// The event does not contain the value / field which is changed, it only signals subscribers the entity is changed
		/// and the subscriber should act accordingly, f.e. fire a ListChanged event.
		/// </summary>
		event EventHandler EntityContentsChanged;
		/// <summary>
		/// Event handler declaration for the event that is fired each time this entity is persisted. Related entities can subscribe to
		/// this event to start housekeeping actions, like syncing internal FK fields with the PK fields of this entity.
		/// </summary>
		event EventHandler AfterSave;

		/// <summary>
		/// Method which will fire the AfterSave event to signal that this entity is persisted and refetched succesfully.
		/// </summary>
		void FlagAsSaved();
		/// <summary>
		/// Will reject (and thus roll back) all changes made to the entity's EntityFields. It rolls back to the initial version. 
		/// </summary>
		void RejectChanges();
		/// <summary>
		/// Sets the EntityField with the name fieldName to the new value value. Marks also the entityfields as dirty. Will refetch the complete entity's fields
		/// from the database if necessary (i.e. the entity is outofsync.).
		/// </summary>
		/// <param name="fieldName">Name of EntityField to set the new value of</param>
		/// <param name="value">Value to set</param>
		/// <returns>true if the value is actually set, false otherwise.</returns>
		/// <exception cref="ORMValueTypeMismatchException">The value specified is not of the same IEntityField.DataType as the field.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The value specified has a size that is larger than the maximum size defined for the related column in the databas</exception>
		bool SetNewFieldValue(string fieldName, object value);
		/// <summary>
		/// Sets the EntityField on index fieldIndex to the new value value. Marks also the entityfields as dirty. Will refetch the complete entity's fields
		/// from the database if necessary (i.e. the entity is outofsync.).
		/// </summary>
		/// <param name="fieldIndex">Index of EntityField to set the new value of</param>
		/// <param name="value">Value to set</param>
		/// <returns>true if the value is actually set, false otherwise.</returns>
		bool SetNewFieldValue(int fieldIndex, object value);
		/// <summary>
		/// Gets the current value of the EntityField with the index fieldIndex. Will refetch the complete entity's fields
		/// from the database if necessary (i.e. the entity is outofsync.).
		/// </summary>
		/// <param name="fieldIndex">Index of EntityField to get the current value of</param>
		/// <returns>The current value of the EntityField specified</returns>
		/// <exception cref="ORMEntityIsDeletedException">When the entity is marked as deleted.</exception>
		/// <exception cref="ArgumentOutOfRangeException">When fieldIndex is smaller than 0 or bigger than the amount of fields in the fields collection.</exception>
		object GetCurrentFieldValue(int fieldIndex);
		/// <summary>
		/// Routine which will flag all subscribers of the EntityContentsChanged event that this entity's contents is changed.
		/// </summary>
		void FlagMeAsChanged();
		/// <summary>
		/// Overrides the GetHashCode routine. It will calculate a hashcode for this entity using the eXclusive OR of the 
		/// hashcodes of the primary key fields in this entity. That hashcode is returned. If no primary key fields are present,
		/// the hashcode of the base class is returned, which will not be unique.
		/// </summary>
		/// <returns>Hashcode for this entity object, based on its primary key field values</returns>
		int GetHashCode();
		/// <summary>
		/// Creates the requested predicate of the type specified. If no IConcurrencyPredicateFactory instance is stored in this entity instance, null
		/// is returned.
		/// </summary>
		/// <param name="predicateTypeToCreate">The type of predicate to create</param>
		/// <returns>A ready to use predicate to use in the query to execute, or null/Nothing if no IConcurrencyPredicateFactory instance is present, 
		/// in which case the predicate is ignored</returns>
		IPredicateExpression GetConcurrencyPredicate(ConcurrencyPredicateType predicateTypeToCreate);
		/// <summary>
		/// Validates the entity by calling a set IEntityValidator implementing object's Validate() method. If no IEntityValidator object is set
		/// true is returned.
		/// </summary>
		/// <returns>The result of IEntityValidator.Validate(this).</returns>
		/// <remarks>Called by save logic.</remarks>
		/// <exception cref="ORMEntityValidationException">If validation fails</exception>
		bool Validate();

		/// <summary>
		/// Marker for the entity object if the object is new and should be inserted when saved (true) or read from the
		/// database (false).
		/// </summary>
		bool IsNew {get; set;}
		/// <summary>
		/// Marker for the entity object if the object is 'dirty' (changed, true) or not (false). Affects/reads .Fields.IsDirty.
		/// </summary>
		bool IsDirty {get; set;}
		/// <summary>
		/// The validator object used to validate values for fields. This is a custom validator called after the build-in validations succeed.
		/// </summary>
		IValidator Validator {get; set;}
		/// <summary>
		/// The validator object used to validate the complete entity. Call <see cref="Validate"/> to use this validator.
		/// </summary>
		IEntityValidator EntityValidatorToUse {get; set;}
		/// <summary>
		/// Gets / sets the unique Object ID which is created at runtime when the entity is instantiated. Can be used for external caches.
		/// </summary>
		Guid ObjectID {get; set;}
		/// <summary>
		/// Returns true if this entity instance is in the middle of a deserialization process, for example during a ReadXml() call.
		/// For internal use only. 
		/// </summary>
		bool IsDeserializing {get; set;}
		/// <summary>
		/// Gets / sets the IConcurrencyPredicateFactory to use for <see cref="GetConcurrencyPredicate"/>.
		/// </summary>
		IConcurrencyPredicateFactory ConcurrencyPredicateFactoryToUse {get; set;}
		/// <summary>
		/// List of IEntityField references which form the primary key. Reads/Affects .Fields.PrimaryKeyFields
		/// </summary>
		ArrayList PrimaryKeyFields {get;}
	}


	/// <summary>
	/// Interface used for relations between IEntity* instances. 
	/// Generic
	/// </summary>
	public interface IEntityRelation
	{
		/// <summary>
		/// Adds a new pair of EntityField instances to the relation. Primary Key fields and Foreign Key Fields have to be added
		/// in pairs. Used by SelfServicing template set.
		/// </summary>
		/// <param name="primaryKeyField">The IEntityField instance which represents a field in the primary key in the relation</param>
		/// <param name="foreignKeyField">The IEntityField instance which represents the corresponding field in the foreign key in the relation</param>
		void AddEntityFieldPair(IEntityField primaryKeyField, IEntityField foreignKeyField);
		/// <summary>
		/// Adds a new pair of EntityFieldCore instances to the relation, including persistence info. Primary Key fields and Foreign Key Fields have to be added
		/// in pairs. Used by Adapter template set.
		/// </summary>
		/// <param name="primaryKeyField">The IEntityField instance which represents a field in the primary key in the relation</param>
		/// <param name="foreignKeyField">The IEntityField instance which represents the corresponding field in the foreign key in the relation</param>
		void AddEntityFieldPair(IEntityField2 primaryKeyField, IEntityField2 foreignKeyField);
		/// <summary>
		/// Returns in an arraylist all IFieldPersistenceInfo objects for the FK fields in this entityrelation
		/// </summary>
		/// <returns>ArrayList with the requested objects</returns>
		ArrayList GetAllFKFieldPersistenceInfoObjects();
		/// <summary>
		/// Returns in an arraylist all IFieldPersistenceInfo objects for the PK fields in this entityrelation
		/// </summary>
		/// <returns>ArrayList with the requested objects</returns>
		ArrayList GetAllPKFieldPersistenceInfoObjects();
		/// <summary>
		/// Gets the IFieldPersistenceInfo data for the PK field at index specified.
		/// </summary>
		/// <param name="index">index of the field in the list of PK fields.</param>
		/// <returns>IFieldPersistenceInfo object</returns>
		IFieldPersistenceInfo GetPKFieldPersistenceInfo(int index);
		/// <summary>
		/// Gets the IFieldPersistenceInfo data for the FK field at index specified.
		/// </summary>
		/// <param name="index">index of the field in the list of FK fields.</param>
		/// <returns>IFieldPersistenceInfo object</returns>
		IFieldPersistenceInfo GetFKFieldPersistenceInfo(int index);
		/// <summary>
		/// Sets the IFieldPersistenceInfo data for the PK field at index specified.
		/// </summary>
		/// <param name="index">index of the field in the list of PK fields.</param>
		/// <param name="persistenceInfo">The persistence info for the entity field at position index.</param>
		/// <remarks>Used by DataAccessAdapter objects.</remarks>
		void SetPKFieldPersistenceInfo(int index, IFieldPersistenceInfo persistenceInfo);
		/// <summary>
		/// Sets the IFieldPersistenceInfo data for the FK field at index specified.
		/// </summary>
		/// <param name="index">index of the field in the list of FK fields.</param>
		/// <param name="persistenceInfo">The persistence info for the entity field at position index.</param>
		/// <remarks>Used by DataAccessAdapter objects.</remarks>
		void SetFKFieldPersistenceInfo(int index, IFieldPersistenceInfo persistenceInfo);
		/// <summary>
		/// Gets the IEntityFieldCode information about the PK field at index specified
		/// </summary>
		/// <param name="index">index of field in the list of PK fields</param>
		/// <returns>IEntityFieldCode object</returns>
		IEntityFieldCore GetPKEntityFieldCore(int index);
		/// <summary>
		/// Gets the IEntityFieldCode information about the FK field at index specified
		/// </summary>
		/// <param name="index">index of field in the list of FK fields</param>
		/// <returns>IEntityFieldCode object</returns>
		IEntityFieldCore GetFKEntityFieldCore(int index);
		/// <summary>
		/// Returns in an arraylist all IEntityFieldCore objects for the PK fields in this entityrelation
		/// </summary>
		/// <returns>ArrayList with the requested objects</returns>
		ArrayList GetAllPKEntityFieldCoreObjects();
		/// <summary>
		/// Returns in an arraylist all IEntityFieldCore objects for the FK fields in this entityrelation
		/// </summary>
		/// <returns>ArrayList with the requested objects</returns>
		ArrayList GetAllFKEntityFieldCoreObjects();

		/// <summary>
		/// The relation type the IEntityRelation instance represents.
		/// </summary>
		RelationType TypeOfRelation {get; set;}
		/// <summary>
		/// Flag to signal if this relation is a 'weak' relation or not. Weak relations are optional relations, which means when A and B have a 
		/// weak relation, not all instances of A have to have a related instance of B.
		/// </summary>
		bool IsWeak {get; }
		/// <summary>
		/// Returns the amount of fields in the EntityRelation object.
		/// </summary>
		int AmountFields {get;}
		/// <summary>
		/// Custom filter for JOIN clauses which are added with AND to the ON clause resulting from this EntityRelation. By adding a
		/// predicate expression with fieldcomparevalue predicate objects for example, you can add extra filtering inside the JOIN.
		/// </summary>
		IPredicateExpression CustomFilter {get; set;}
	}


	/// <summary>
	/// Interface to define the relation between a parameter of a query and a field. This relation is used to find back a related EntityField
	/// when an Output Parameter is found in a query so the value of the Output Parameter can be assigned to the related EntityField.
	/// Generic
	/// </summary>
	public interface IParameterFieldRelation
	{
		/// <summary>
		/// The <see cref="IEntityFieldCore"/> in the relationship. Only settable via a constructor.
		/// </summary>
		IEntityFieldCore Field {get;}
		/// <summary>
		/// The Parameter in the relationship. Only settable via a constructor.
		/// </summary>
		IDataParameter Parameter {get; }
	}


	/// <summary>
	/// The interface for dynamic created queries.
	/// Generic
	/// </summary>
	public interface IQuery
	{
		/// <summary>
		/// The connection object to use with the <see cref="Command"/>
		/// </summary>
		IDbConnection Connection {get; set;}
		/// <summary>
		/// The command used for this query.
		/// </summary>
		IDbCommand Command {get; set;}
		/// <summary>
		/// The list of parameters used in the <see cref="Command"/>. 
		/// </summary>
		IDataParameterCollection Parameters {get; }
		/// <summary>
		/// Array list with the <see cref="IParameterFieldRelation"/> instances for the relations between IEntityFields and output parameters.
		/// </summary>
		ArrayList ParameterFieldRelations {get;}

		/// <summary>
		/// Adds a new <see cref="IParameterFieldRelation"/> to the collection of <see cref="ParameterFieldRelations"/>. An output parameter can be
		/// stored once in the collection.
		/// </summary>
		/// <param name="relation">The <see cref="IParameterFieldRelation"/> to add</param>
		void AddParameterFieldRelation(IParameterFieldRelation relation);
		/// <summary>
		/// Will walk all <see cref="IParameterFieldRelation"/> instances of this query and reflect the parameter values in the related fields.
		/// Only output parameters are taken into account.
		/// </summary>
		void ReflectOutputValuesInRelatedFields();
	}


	/// <summary>
	/// Interface for retrieval queries. These queries do return a resultset. Retrieval queries execute Select statements.
	/// Generic
	/// </summary>
	public interface IRetrievalQuery : IQuery
	{
		/// <summary>
		/// Executes the query contained by the IQuery instance. The connection has to be opened before calling Execute().
		/// </summary>
		/// <param name="behavior">The behavior setting to pass to the ExecuteReader method.</param>
		/// <returns>An open, ready to use IDataReader instance</returns>
		/// <exception cref="System.InvalidOperationException">When there is no command object inside the query object, 
		/// or no connection object inside the query object or the connection is closed.</exception>
		IDataReader Execute(CommandBehavior behavior);
	}


	/// <summary>
	/// Interface for a predicate. Predicates are expressions which result in true or false, and which are used in WHERE clauses.
	/// Generic
	/// </summary>
	public interface IPredicate
	{
		/// <summary>
		/// The list of parameters created when the Predicate was translated to text usable in a query. Only valid after a succesful call to
		/// <see cref="ToQueryText"/>
		/// </summary>
		ArrayList Parameters {get;}
		/// <summary>
		/// Flag for setting the Predicate to negate itself, i.e. to add 'NOT' to its result.
		/// </summary>
		bool Negate {get; set;}
		/// <summary>
		/// Object which will be used to create valid parameter objects, field names, including prefix/postfix characters, 
		/// and conversion routines, and field names, including prefix/postfix characters. 
		/// Uses the strategy pattern so the generic code can work with more than one target database.
		/// </summary>
		IDbSpecificCreator DatabaseSpecificCreator {get; set;}
		/// <summary>
		/// The PredicateType of this instance. Used to determine the instance nature without a lot of casting.
		/// </summary>
		int InstanceType {get; set;}

		/// <summary>
		/// Retrieves a ready to use text representation of the contained Predicate. 
		/// </summary>
		/// <param name="uniqueMarker">int counter which is appended to every parameter. The refcounter is increased by every parameter creation,
		/// making sure the parameter is unique in the predicate and also in the predicate expression(s) containing the predicate.</param>
		/// <returns>The contained Predicate in textual format.</returns>
		/// <exception cref="System.ApplicationException">When IPredicate.DatabaseSpecificCreator is not set to a valid value.</exception>
		string ToQueryText(ref int uniqueMarker);
	}

	
	/// <summary>
	/// Interface for a PredicateExpression, which is a grouped set of Predicates. A predicate expression is usable as a WHERE clause.
	/// Generic
	/// </summary>
	public interface IPredicateExpression : IPredicate
	{
		/// <summary>
		/// Adds an IPredicate implementing object to the PredicateExpression. This can be a Predicate derived class or a PredicateExpression. 
		/// If no object is present yet in the PredicateExpression, no operator is added, otherwise the object is added with an 'And'-operator. 
		/// </summary>
		/// <param name="predicateToAdd">The IPredicate implementing object to add</param>
		/// <exception cref="ArgumentNullException">When prPredicateToAdd is null</exception>
		void Add(IPredicate predicateToAdd);
		/// <summary>
		/// Adds an IPredicate implementing object to the PredicateExpression with an 'Or'-operator. 
		/// The object added can be a Predicate derived class or a PredicateExpression. If no objects are present yet in the PredicateExpression,
		/// the operator is ignored. 
		/// </summary>
		/// <param name="predicateToAdd">The IPredicate implementing object to add</param>
		/// <exception cref="ArgumentNullException">When prPredicateToAdd is null</exception>
		void AddWithOr(IPredicate predicateToAdd);
		/// <summary>
		/// Adds an IPredicate implementing object to the PredicateExpression with an 'And'-operator. 
		/// The object added can be a Predicate derived class or a PredicateExpression. If no objects are present yet in the PredicateExpression,
		/// the operator is ignored. 
		/// </summary>
		/// <param name="predicateToAdd">The IPredicate implementing object to add</param>
		/// <exception cref="ArgumentNullException">When prPredicateToAdd is null</exception>
		void AddWithAnd(IPredicate predicateToAdd);

		/// <summary>
		/// Gets the predicate expression element at the index specified
		/// </summary>
		IPredicateExpressionElement this[int index] {get;}
		/// <summary>
		/// Gets the amount of predicate expression elements in this predicate expression. This is including all operators.
		/// </summary>
		int Count {get;}
	}

	
	/// <summary>
	/// Interface for action queries. These queries do not return a resultset. Action queries execute Insert, Delete and Update statements.
	/// Generic
	/// </summary>
	public interface IActionQuery : IQuery
	{
		/// <summary>
		/// Executes the query contained by the IQuery instance. 
		/// </summary>
		/// <returns>The number of rows affected (if applicable), otherwise 0.</returns>
		/// <exception cref="System.InvalidOperationException">When there is no command object inside the query object, 
		/// or no connection object inside the query object</exception>
		int Execute();
	}


	/// <summary>
	/// Interface used for the elements which are physically stored in a PredicateExpression.
	/// Generic
	/// </summary>
	public interface IPredicateExpressionElement
	{
		/// <summary>
		/// The type of the Element. 
		/// </summary>
		PredicateExpressionElementType Type {get; set;}
		/// <summary>
		/// The contents of the Element
		/// </summary>
		object Contents {get; set;}
	}
	
	
	/// <summary>
	/// Interface which holds the generic information for entity field persistence of an entity field. Instances of this interface
	/// are passed to logic with an instance of the IEntityFieldCore interface. SelfServicing implements both interfaces in one interface: IEntityField.
	/// Generic
	/// </summary>
	public interface IFieldPersistenceInfo
	{
		/// <summary>
		/// The name of the catalog the SourceSchemaName is located in. 
		/// </summary>
		string SourceCatalogName {get; set;}
		/// <summary>
		/// The name of the schema which holds <see cref="SourceObjectName"/>. Schema is used to generate SQL on the fly. 
		/// A common schema name in SqlServer is f.e. 'dbo'.
		/// </summary>
		string SourceSchemaName {get; set;}
		/// <summary>
		/// The name of the source object which holds <see cref="SourceColumnName"/>. Can be a view or a table (or synonym of those). 
		/// Used to generate SQL on the fly.
		/// </summary>
		string SourceObjectName {get; set;}
		/// <summary>
		/// The name of the corresponding column in a view or table for an entityfield. This name is used to map a column in a resultset onto the entity field.
		/// </summary>
		string SourceColumnName {get;set;}
		/// <summary>
		/// The maximum length of the value of the entityfield (string/binary data). Is ignored for entityfields which hold non-string and non-binary values.
		/// ColumnMaxLength
		/// Used for update/insert operations on the column
		/// </summary>
		int SourceColumnMaxLength {get; set;}
		/// <summary>
		/// The type of the Column mapped onto the EntityField. The value stored here is the integer representation of the enum value of the type, f.e.
		/// SqlDbType.Int or OracleType.Int16
		/// Used for update/insert operations on the column
		/// </summary>
		int SourceColumnDbType {get; set;}
		/// <summary>
		/// Flag if the Column mapped onto the entityfield is nullable or not. 
		/// Used for update/insert operations on the column
		/// </summary>
		bool SourceColumnIsNullable {get; set;}
		/// <summary>
		/// The scale of the Column mapped onto the entityfield.
		/// Used for update/insert operations on the column
		/// </summary>
		byte SourceColumnScale {get; set;}
		/// <summary>
		/// The precision of the Column mapped onto the entityfield.
		/// Used for update/insert operations on the column
		/// </summary>
		byte SourceColumnPrecision {get; set;}
		/// <summary>
		/// If set to true, the Dynamic Query Engine (DQE) will assume the field is an Identity field and will act accordingly (i.e.: as the target database
		/// handles Identity fields: SqlServer will generate a new value itself, Oracle wants to have a sequence input.
		/// </summary>
		bool IsIdentity {get; }
		/// <summary>
		/// If <see cref="IsIdentity"/> is set to true, this property has to be set to the name of the sequence which supplies the value for the EntityField's
		/// corresponding table field. On SqlServer this is @@IDENTITY or SCOPE_IDENTITY() and only used when the row is succesfully inserted, however on Oracle
		/// this value is used to specify a new value and to retrieve the new value. Is undefined when <see cref="IsIdentity"/> is set to false.
		/// </summary>
		string IdentityValueSequenceName {get; }
	}


	/// <summary>
	/// Interface for DatabaseSpecificCreator objects, which use the Strategy pattern to supply IPredicate implementations with a way to
	/// create parameter objects, field names, including prefix/postfix characters, and conversion routines, which suit the target database.
	/// Generic
	/// </summary>
	public interface IDbSpecificCreator
	{
		/// <summary>
		/// Creates a valid Parameter based on the passed in IEntityField implementation.
		/// </summary>
		/// <param name="field">IEntityField instance used to base the parameter on.</param>
		/// <param name="direction">The direction for the parameter</param>
		/// <returns>Valid parameter for usage with the target database.</returns>
		IDataParameter CreateParameter(IEntityField field, ParameterDirection direction);
		/// <summary>
		/// Creates a valid Parameter based on the passed in IEntityFieldCore implementation and the passed in IFieldPersistenceInfo instance
		/// </summary>
		/// <param name="field">IEntityFieldCore instance used to base the parameter on.</param>
		/// <param name="persistenceInfo">Persistence information to create the parameter.</param>
		/// <param name="direction">The direction for the parameter</param>
		/// <returns>Valid parameter for usage with the target database.</returns>
		IDataParameter CreateParameter(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, ParameterDirection direction);
		/// <summary>
		/// Creates a valid Parameter for the pattern in a LIKE statement. This is a special case, because it shouldn't rely on the type of the
		/// field the LIKE statement is used with but should be the unicode varchar type. 
		/// </summary>
		/// <param name="fieldName">The name of the field the LIKE statement is used with.</param>
		/// <param name="pattern">The pattern to be passed as the value for the parameter. Is used to determine length of the parameter.</param>
		/// <returns>Valid parameter for usage with the target database.</returns>
		IDataParameter CreateLikeParameter(string fieldName, string pattern);
		/// <summary>
		/// Creates a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. This field name is
		/// not padded with an alias if that alias should be created. Effectively, this is the
		/// same as CreateFieldName(field persistence info, fieldname, false);
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		string CreateFieldName(IFieldPersistenceInfo persistenceInfo, string fieldName);
		/// <summary>
		/// Creats a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <param name="appendAlias">When true, the routine should construct an alias construction statement.</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		string CreateFieldName(IFieldPersistenceInfo persistenceInfo, string fieldName, bool appendAlias);
		/// <summary>
		/// Creates a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. This field name is
		/// not padded with an alias if that alias should be created. Effectively, this is the
		/// same as CreateFieldNameSimple(field persistence info, fieldname, false);. The fieldname is 'simple' in that
		/// it doesn't contain any catalog, schema or table references. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		string CreateFieldNameSimple(IFieldPersistenceInfo persistenceInfo, string fieldName);
		/// <summary>
		/// Creats a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. The fieldname is 'simple' in that
		/// it doesn't contain any catalog, schema or table references. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <param name="appendAlias">When true, the routine should construct an alias construction statement.</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		string CreateFieldNameSimple(IFieldPersistenceInfo persistenceInfo, string fieldName, bool appendAlias);
		/// <summary>
		/// Creates a valid object name (f.e. a name for a table or view) based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance which source object info is used to formulate the objectname</param>
		/// <returns>Valid object name</returns>
		string CreateObjectName(IFieldPersistenceInfo persistenceInfo);
		/// <summary>
		/// Converts the passed in comparison operator to a string usable in a query.
		/// </summary>
		/// <param name="comparisonOperator">Operator to convert to string</param>
		/// <returns>The string representation usable in a query of the operator passed in.</returns>
		string ConvertComparisonOperator(ComparisonOperator comparisonOperator);
		/// <summary>
		/// Converts the passed in sort operator to a string usable in a query
		/// </summary>
		/// <param name="operatorToConvert">sort operator to convert to a string</param>
		/// <returns>The string representation usable in a query of the operator passed in.</returns>
		string ConvertSortOperator(SortOperator operatorToConvert);
	}


	/// <summary>
	/// Interface for the class which supplies a default value for a specified .NET type. Necessary for NULL values read from the database.
	/// Generic
	/// </summary>
	public interface ITypeDefaultValue
	{
		/// <summary>
		/// Returns a default value for the type specified
		/// </summary>
		/// <param name="defaultValueType">The type which default value should be returned.</param>
		/// <returns>The default value for the type specified.</returns>
		/// <remarks>This is the 'slow' version of the routine. Do not use this routine unless you have to. It's slow because it uses reflection to
		/// determine the type's name.</remarks>
		object DefaultValue(System.Type defaultValueType);
	}


	/// <summary>
	/// Interface definition for a class which forms a single sort clause, thus an order by
	/// definition defined for a single IEntityField or IEntityField - IFieldPersistenceInfo combination
	/// PersistenceInfo will return the same object when an IEntityField is added to the object.
	/// Generic
	/// </summary>
	public interface ISortClause
	{
		/// <summary>
		/// IEntityField to sort on.
		/// </summary>
		IEntityField FieldToSort {get; set;}
		/// <summary>
		/// IEntityFieldCore to sort on.
		/// </summary>
		IEntityFieldCore FieldToSortCore {get; set;}
		/// <summary>
		/// Persistence information for FieldToSort. Can be a cast of the same object, when an IEntityField is
		/// added to this sort clause
		/// </summary>
		IFieldPersistenceInfo PersistenceInfo {get; set;}
		/// <summary>
		/// The sort operator to use for this sort clause
		/// </summary>
		SortOperator SortOperatorToUse {get; set;}
	}


	/// <summary>
	/// Interface for the class which contains the sort clauses used in IRetrievalQuery instances.
	/// Generic
	/// </summary>
	public interface ISortExpression
	{
		/// <summary>
		/// Adds the passed in sort clause to the list. 
		/// </summary>
		/// <param name="sortClauseToAdd">the sort clause to add</param>
		/// <returns>The index the sort clause was added to</returns>
		int Add(ISortClause sortClauseToAdd);
		/// <summary>
		/// Inserts the passed in sort clause at the index provided.
		/// </summary>
		/// <param name="index">Index to insert the sortclause at</param>
		/// <param name="sortClauseToAdd">the sort clause to insert</param>
		void Insert(int index, ISortClause sortClauseToAdd);
		/// <summary>
		/// Removes the given sort clause from the list.
		/// </summary>
		/// <param name="sortClauseToRemove">the sort clause to remove.</param>
		void Remove(ISortClause sortClauseToRemove);

		/// <summary>
		/// Indexer for this list.
		/// </summary>
		ISortClause this [int index] {get; set;}
		/// <summary>
		/// The amount of items currently stored in the ISortExpression
		/// </summary>
		int Count {get;}
	}


	/// <summary>
	/// Interface for validation classes used by <see cref="IEntityCore"/> implementing classes.
	/// Generic
	/// </summary>
	public interface IValidator
	{
		/// <summary>
		/// Validates the EntityFieldCore on the given fieldIndex with the given value. 
		/// This routine is called by the Entity's property value validator after the value has passed validation for destination column type and
		/// null values.
		/// </summary>
		/// <param name="fieldIndex">Index of IEntityFieldCore to validate</param>
		/// <param name="value">Value which should be stored in field with index fieldIndex. Will not be null (earlier logic filters out nulls before
		/// a call will be made to this routine).</param>
		/// <returns>true if the value is valid for the field, false otherwise</returns>
		bool Validate(int fieldIndex, object value);
	}


	/// <summary>
	/// Interface for the RelationCollection class which is used to stack relation objects between several entities to build
	/// a complete join path
	/// Generic
	/// NB: ToQueryText() has been removed, query text producing logic is moved to the DQE's, since Oracle 8i doesn't support ANSI
	/// joins.
	/// </summary>
	public interface IRelationCollection
	{
		/// <summary>
		/// Adds the passed in IEntityRelation instance to the list. Relations can be added more than once.
		/// The order is important.
		/// </summary>
		/// <param name="relationToAdd">IEntityRelation instance to add</param>
		/// <returns>Index of added relation in the list.</returns>
		IEntityRelation Add(IEntityRelation relationToAdd);
		/// <summary>
		/// Adds the passed in IEntityRelation instance to the list at position index. Relations can be added more than once.
		/// The order is important.
		/// </summary>
		/// <param name="relationToAdd">IEntityRelation instance to add</param>
		/// <param name="index">Index to add the relation to.</param>
		void Insert(IEntityRelation relationToAdd, int index);
		/// <summary>
		/// Removes the passed in IEntityRelation instance. Only the first instance will be removed.
		/// </summary>
		/// <param name="relationToRemove">IEntityRelation instance to remove</param>
		void Remove(IEntityRelation relationToRemove);
		/// <summary>
		/// Converts the set of relations to a set of nested JOIN query elements using ANSI join syntaxis. Oracle 8i doesn't support ANSI join syntaxis
		/// and therefore the OracleDQE has its own join code.
		/// It uses a database specific creator object for database specific syntaxis, like the format of the tables / views and fields. 
		/// </summary>
		/// <param name="uniqueMarker">int counter which is appended to every parameter. The refcounter is increased by every parameter creation,
		/// making sure the parameter is unique in the custom filter predicates</param>
		/// <returns>The string representation of the INNER JOIN expressions of the contained relations, when ObeyWeakRelations is set to false (default)
		/// or the string representation of the LEFT/RIGHT JOIN expressions of the contained relations, when ObeyWeakRelations is set to true</returns>
		/// <exception cref="ApplicationException">When the DatabaseSpecificCreator is not set.</exception>
		string ToQueryText(ref int uniqueMarker);

		/// <summary>
		/// Object which will be used to create valid parameter objects, field names, including prefix/postfix characters, 
		/// and conversion routines, and field names, including prefix/postfix characters. 
		/// Uses the strategy pattern so the generic code can work with more than one target database.
		/// </summary>
		IDbSpecificCreator DatabaseSpecificCreator {get; set;}
		/// <summary>
		/// Indexer in the collection.
		/// </summary>
		IEntityRelation this [int index] {get; set;}
		/// <summary>
		/// Gets / sets ObeyWeakRelations, which is the flag to signal the collection what kind of join statements to generate in the
		/// ToQueryText statement, which is called by the DQE. Weak relationships are relationships which are optional, for example a
		/// customer with no orders is possible, because the relationship between customer and order is based on a field in order.
		/// When this property is set to true (default: false), weak relationships will result in LEFT JOIN statements. When
		/// set to false (which is the default), INNER JOIN statements are used.
		/// </summary>
		bool ObeyWeakRelations { get; set;}
	}


	/// <summary>
	/// Interface base definition for TypedList classes
	/// </summary>
	public interface ITypedListCore
	{
		/// <summary>
		/// Returns the amount of rows in this typed list.
		/// </summary>
		int Count {get; }
		/// <summary>
		/// Gets / sets ObeyWeakRelations, which is the flag to signal the collection what kind of join statements to generate in the
		/// query statement. Weak relationships are relationships which are optional, for example a
		/// customer with no orders is possible, because the relationship between customer and order is based on a field in order.
		/// When this property is set to true (default: false), weak relationships will result in LEFT JOIN statements. When
		/// set to false (which is the default), INNER JOIN statements are used.
		/// </summary>
		bool ObeyWeakRelations {get; set;}
	}



	#endregion

	#region Obsolete Interfaces
	/// <summary>
	/// Interface for the definition of a SET Field = value clause for Update queries. 
	/// </summary>
	[Obsolete("Obsolete. Do not use.", true)]
	public interface IFieldValueSetClause
	{
		/// <summary>
		/// Field to set the <see cref="Value"/> of
		/// </summary>
		IEntityField Field {get; set;}
		/// <summary>
		/// Value to set <see cref="Field"/> to
		/// </summary>
		object Value {get; set;}
	}
	#endregion
}
