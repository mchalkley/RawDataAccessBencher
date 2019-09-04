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
using System.Data;

using LLBL2003.AdventureWorks2008.EntityClasses;
using LLBL2003.AdventureWorks2008.FactoryClasses;
using LLBL2003.AdventureWorks2008.CollectionClasses;
using LLBL2003.AdventureWorks2008.HelperClasses;
using LLBL2003.AdventureWorks2008.ValidatorClasses;
using LLBL2003.AdventureWorks2008;

using SD.LLBLGen.Pro.ORMSupportClasses2003;
using SD.LLBLGen.Pro.DQE.SqlServer2003;

namespace LLBL2003.AdventureWorks2008.DaoClasses
{
	/// <summary>
	/// General DAO class for the CustomerAddress Entity. It will perform database oriented actions for
	/// a entity of type 'CustomerAddressEntity'. This DAO works on an EntityFields object. 
	/// </summary>
	public class CustomerAddressDAO : DaoBase
	{
		/// <summary>
		/// CTor
		/// </summary>
		public CustomerAddressDAO()
		{
		}

		/// <summary>
		/// Adds the given fields to the database as a new 'CustomerAddress' entity.
		/// </summary>
		/// <param name="fields">The EntityField data to use for the insert.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <returns>true if the addition was succesful, false otherwise</returns>
		/// <exception cref="ArgumentNullException">When fields is null</exception>
		public bool AddCustomerAddress(IEntityFields fields, ITransaction containingTransaction)
		{
			if(fields==null)
			{
				throw new ArgumentNullException("fields", "fields can't be null");
			}

			IActionQuery insertQuery = DynamicQueryEngine.CreateInsertDQ(fields, DbUtils.DetermineConnectionToUse(containingTransaction));
			bool wasSuccesful = (base.ExecuteActionQuery(insertQuery, containingTransaction) > 0);
			if(wasSuccesful)
			{
				insertQuery.ReflectOutputValuesInRelatedFields();
			}
			return wasSuccesful;
		}

	
		/// <summary>
		/// Updates an entity of type 'CustomerAddressEntity' using the given fields. 
		/// </summary>
		/// <param name="fields">The EntityField data to use for the update</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <returns>true if the update was succesful, false otherwise</returns>
		/// <exception cref="ArgumentNullException">When fields is null</exception>
		public bool UpdateCustomerAddress(IEntityFields fields, ITransaction containingTransaction)
		{
			if(fields==null)
			{
				throw new ArgumentNullException("fields", "fields can't be null");
			}

			if(!fields.IsDirty)
			{
				// not changed, nothing to update
				return true;
			}
			
			IPredicateExpression updateFilter = new PredicateExpression();
			updateFilter.Add(new FieldCompareValuePredicate(fields[(int)CustomerAddressFieldIndex.CustomerID], ComparisonOperator.Equal));
			updateFilter.Add(new FieldCompareValuePredicate(fields[(int)CustomerAddressFieldIndex.AddressID], ComparisonOperator.Equal));
			return (CreateAndRunUpdateQuery(fields, containingTransaction, updateFilter) > 0);
		}
		

		/// <summary>
		/// Updates an entity of type 'CustomerAddressEntity' using the given fields. 
		/// </summary>
		/// <param name="fields">The EntityField data to use for the update</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="updateRestriction">Predicate expression, meant for concurrency checks in an Update query</param>
		/// <returns>true if the update was succesful, false otherwise</returns>
		/// <exception cref="ArgumentNullException">When fields is null</exception>
		public bool UpdateCustomerAddress(IEntityFields fields, ITransaction containingTransaction, IPredicate updateRestriction)
		{
			if(fields==null)
			{
				throw new ArgumentNullException("fields", "fields can't be null");
			}

			if(!fields.IsDirty)
			{
				// not changed, nothing to update
				return true;
			}
			
			IPredicateExpression updateFilter = new PredicateExpression();
			updateFilter.Add(new FieldCompareValuePredicate(fields[(int)CustomerAddressFieldIndex.CustomerID], ComparisonOperator.Equal));
			updateFilter.Add(new FieldCompareValuePredicate(fields[(int)CustomerAddressFieldIndex.AddressID], ComparisonOperator.Equal));
			updateFilter.Add(updateRestriction);
			return (CreateAndRunUpdateQuery(fields, containingTransaction, updateFilter) > 0);
		}
		

		/// <summary>
		/// Reads the data of an entity of type 'CustomerAddressEntity' data into EntityFields object and returns that object. Which data is read is determined using
		/// the passed in Primary Key field(s)
		/// </summary>
		/// <param name="fieldsToFetch">The collecion of fields to fetch. Contained data will be overwritten.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="customerID">PK value for CustomerAddress which data should be fetched into this CustomerAddress object</param>
		/// <param name="addressID">PK value for CustomerAddress which data should be fetched into this CustomerAddress object</param>
		/// <returns>fields read</returns>
		/// <exception cref="ArgumentNullException">When fieldsToFetch is null</exception>
		public IEntityFields FetchCustomerAddress(IEntityFields fieldsToFetch, ITransaction containingTransaction, System.Int32 customerID, System.Int32 addressID)
		{
			fieldsToFetch.State = EntityState.New;
			IPredicateExpression selectFilter = new PredicateExpression();
			selectFilter.Add(new FieldCompareValuePredicate(fieldsToFetch[(int)CustomerAddressFieldIndex.CustomerID], ComparisonOperator.Equal, customerID));
			selectFilter.Add(new FieldCompareValuePredicate(fieldsToFetch[(int)CustomerAddressFieldIndex.AddressID], ComparisonOperator.Equal, addressID));
			CreateAndRunSingleInstanceRetrievalQuery(fieldsToFetch, containingTransaction, selectFilter);
			return fieldsToFetch;
		}


		/// <summary>
		/// Deletes an entity of type 'CustomerAddressEntity' from the persistent storage. Which entity is deleted is determined from the passed in EntityFields object.
		/// </summary>
		/// <param name="fields">The EntityField data to use for the deletion</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="deleteRestriction">Predicate expression, meant for concurrency checks in a delete query. Can be null.</param>
		/// <returns>true if the delete was succesful, false otherwise</returns>
		/// <exception cref="ArgumentNullException">When fields is null</exception>
		public bool DeleteCustomerAddress(IEntityFields fields, ITransaction containingTransaction, IPredicate deleteRestriction)
		{
			if(fields==null)
			{
				throw new ArgumentNullException("fields", "fields can't be null");
			}

			IPredicateExpression deleteFilter = new PredicateExpression();
			deleteFilter.Add(new FieldCompareValuePredicate(fields[(int)CustomerAddressFieldIndex.CustomerID], ComparisonOperator.Equal));
			deleteFilter.Add(new FieldCompareValuePredicate(fields[(int)CustomerAddressFieldIndex.AddressID], ComparisonOperator.Equal));
			if(deleteRestriction!=null)
			{
				deleteFilter.AddWithAnd(deleteRestriction);
			}
			// Has primary key, fields contains values. Pass the first field as FieldInTarget so the routine can determine what the
			// target object in the catalog is.
			return (CreateAndRunDeleteQuery(fields[0], containingTransaction, deleteFilter) > 0);
		}
	
	
	
		/// <summary>
		/// Retrieves in the calling CustomerAddressCollection object all CustomerAddressEntity objects which have data in common
		/// with the specified related Entities. If one is omitted, that entity is not used as a filter. 
		/// </summary>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="collectionToFill">Collection to fill with the entity objects retrieved</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="validatorToUse">The Validator object to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="filter">Extra filter to limit the resultset. Predicate expression can be null, in which case it will be ignored.</param>
		/// <param name="addressInstance">AddressEntity instance to use as a filter for the CustomerAddressEntity objects to return</param>
		/// <param name="addressTypeInstance">AddressTypeEntity instance to use as a filter for the CustomerAddressEntity objects to return</param>
		/// <param name="customerInstance">CustomerEntity instance to use as a filter for the CustomerAddressEntity objects to return</param>
		/// <returns>true if succeeded, false otherwise</returns>
		public bool GetMulti(ITransaction containingTransaction, IEntityCollection collectionToFill, long maxNumberOfItemsToReturn, ISortExpression sortClauses, 
				IEntityFactory entityFactoryToUse, IValidator validatorToUse, IPredicateExpression filter, AddressEntity addressInstance, AddressTypeEntity addressTypeInstance, CustomerEntity customerInstance)
		{
			IEntityFields fieldsToReturn = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			IPredicateExpression selectFilter = CreateFilterUsingForeignKeys(addressInstance, addressTypeInstance, customerInstance, fieldsToReturn);
			if(filter!=null)
			{
				// add contents to select filter, with AND
				selectFilter.AddWithAnd(filter);
			}
			CreateAndRunMultiInstanceRetrievalQuery(fieldsToReturn, containingTransaction, collectionToFill, maxNumberOfItemsToReturn, sortClauses, entityFactoryToUse, selectFilter, validatorToUse);
			return true;
		}
	

		/// <summary>
		/// Retrieves in the calling CustomerAddressCollection object all CustomerAddressEntity objects
		/// which match with the specified filter, formulated in the predicate or predicate expression definition.
		/// </summary>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="collectionToFill">Collection to fill with the entity objects retrieved</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="validatorToUse">The Validator object to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="selectFilter">A predicate or predicate expression which should be used as filter for the entities to retrieve.</param>
		/// <returns>true if succeeded, false otherwise</returns>
		public bool GetMulti(ITransaction containingTransaction, IEntityCollection collectionToFill, long maxNumberOfItemsToReturn, 
				ISortExpression sortClauses, IEntityFactory entityFactoryToUse, IValidator validatorToUse, IPredicate selectFilter)
		{
			IEntityFields fieldsToReturn = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			CreateAndRunMultiInstanceRetrievalQuery(fieldsToReturn, containingTransaction, collectionToFill, maxNumberOfItemsToReturn, sortClauses, entityFactoryToUse, selectFilter, validatorToUse);
			return true;
		}

		
		/// <summary>
		/// Retrieves in the calling CustomerAddressCollection object all CustomerAddressEntity objects
		/// which match with the specified filter, formulated in the predicate or predicate expression definition, using the passed in relations to 
		/// construct the total query.
		/// </summary>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="collectionToFill">Collection to fill with the entity objects retrieved</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="validatorToUse">The Validator object to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="selectFilter">A predicate or predicate expression which should be used as filter for the entities to retrieve.</param>
		/// <param name="relations">The set of relations to walk to construct to total query.</param>
		/// <returns>true if succeeded, false otherwise</returns>
		public bool GetMulti(ITransaction containingTransaction, IEntityCollection collectionToFill, long maxNumberOfItemsToReturn, 
			ISortExpression sortClauses, IEntityFactory entityFactoryToUse, IValidator validatorToUse, IPredicate selectFilter, IRelationCollection relations)
		{
			IEntityFields fieldsToReturn = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			CreateAndRunMultiInstanceRetrievalQuery(fieldsToReturn, containingTransaction, collectionToFill, maxNumberOfItemsToReturn, sortClauses, 
					entityFactoryToUse, selectFilter, validatorToUse, relations);
			return true;
		}


		/// <summary>
		/// Retrieves entities of the type 'CustomerAddressEntity' in a datatable which match the specified filter. 
		/// It will always create a new connection to the database.
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="selectFilter">A predicate or predicate expression which should be used as filter for the entities to retrieve.</param>
		/// <returns>a filled datatable if succeeded, false otherwise</returns>
		public DataTable GetMultiAsDataTable(long maxNumberOfItemsToReturn, ISortExpression sortClauses, IPredicate selectFilter)
		{
			IEntityFields fieldsToReturn = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			return CreateAndRunMultiRowRetrievalQuery(fieldsToReturn, maxNumberOfItemsToReturn, sortClauses, selectFilter);
		}


		/// <summary>
		/// Retrieves entities of the type 'CustomerAddressEntity' in a datatable which match the specified filter. 
		/// It will always create a new connection to the database.
		/// </summary>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="selectFilter">A predicate or predicate expression which should be used as filter for the entities to retrieve.</param>
		/// <param name="relations">The set of relations to walk to construct to total query.</param>
		/// <returns>a filled datatable if succeeded, false otherwise</returns>
		public DataTable GetMultiAsDataTable(long maxNumberOfItemsToReturn, ISortExpression sortClauses, IPredicate selectFilter, IRelationCollection relations)
		{
			IEntityFields fieldsToReturn = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			return CreateAndRunMultiRowRetrievalQuery(fieldsToReturn, maxNumberOfItemsToReturn, sortClauses, selectFilter, relations);
		}

	
		/// <summary>
		/// Deletes from the persistent storage all 'CustomerAddress' entities which have data in common
		/// with the specified related Entities. If one is omitted, that entity is not used as a filter. 
		/// </summary>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="addressInstance">AddressEntity instance to use as a filter for the CustomerAddressEntity objects to delete</param>
		/// <param name="addressTypeInstance">AddressTypeEntity instance to use as a filter for the CustomerAddressEntity objects to delete</param>
		/// <param name="customerInstance">CustomerEntity instance to use as a filter for the CustomerAddressEntity objects to delete</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		public int DeleteMulti(ITransaction containingTransaction, AddressEntity addressInstance, AddressTypeEntity addressTypeInstance, CustomerEntity customerInstance)
		{
			IEntityFields fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			IPredicateExpression deleteFilter = CreateFilterUsingForeignKeys(addressInstance, addressTypeInstance, customerInstance, fields);
			return CreateAndRunDeleteQuery(fields[0], containingTransaction, deleteFilter);
		}
	

		/// <summary>
		/// Deletes from the persistent storage all 'CustomerAddress' entities which match with the specified filter, formulated in
		/// the predicate or predicate expression definition.
		/// </summary>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="deleteFilter">A predicate or predicate expression which should be used as filter for the entities to delete.</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		public int DeleteMulti(ITransaction containingTransaction, IPredicate deleteFilter)
		{
			IEntityFields fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			return CreateAndRunDeleteQuery(fields[0], containingTransaction, deleteFilter);
		}


		/// <summary>
		/// Deletes from the persistent storage all 'CustomerAddress' entities which match with the specified filter, formulated in
		/// the predicate or predicate expression definition.
		/// </summary>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="deleteFilter">A predicate or predicate expression which should be used as filter for the entities to delete.</param>
		/// <param name="relations">The set of relations to walk to construct the total query.</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		public int DeleteMulti(ITransaction containingTransaction, IPredicate deleteFilter, IRelationCollection relations)
		{
			IEntityFields fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			return CreateAndRunDeleteQuery(fields[0], containingTransaction, deleteFilter, relations);
		}


	
		/// <summary>
		/// Updates in the persistent storage all entities which have data in common 
		/// with the specified related Entities. If one is omitted, that entity is not used as a filter. 
		/// Which fields are updated in those matching entities depends on which fields are
		/// <i>changed</i> in the passed in IEntityFields instance fieldsWithNewValues. The new values of these fields are read from fieldsWithNewValues. 
		/// </summary>
		/// <param name="fieldsWithNewValues">IEntityFields instance which holds the new values for the matching entities to update. Only
		/// changed fields are taken into account</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="addressInstance">AddressEntity instance to use as a filter for the CustomerAddressEntity objects to update</param>
		/// <param name="addressTypeInstance">AddressTypeEntity instance to use as a filter for the CustomerAddressEntity objects to update</param>
		/// <param name="customerInstance">CustomerEntity instance to use as a filter for the CustomerAddressEntity objects to update</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		public int UpdateMulti(IEntityFields fieldsWithNewValues, ITransaction containingTransaction, AddressEntity addressInstance, AddressTypeEntity addressTypeInstance, CustomerEntity customerInstance)
		{
			if(!fieldsWithNewValues.IsDirty)
			{
				// nothing to update
				return 0;
			}

			IEntityFields fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.CustomerAddressEntity);
			IPredicateExpression updateFilter = CreateFilterUsingForeignKeys(addressInstance, addressTypeInstance, customerInstance, fields);
			return CreateAndRunUpdateQuery(fieldsWithNewValues, containingTransaction, updateFilter);
		}
	

		/// <summary>
		/// Updates in the persistent storage all entities which match with the specified filter, formulated in
		/// the predicate or predicate expression definition. Which fields are updated in those matching entities depends on which fields are
		/// <i>changed</i> in the passed in IEntityFields instance fieldsWithNewValues. The new values of these fields are read from fieldsWithNewValues. 
		/// </summary>
		/// <param name="fieldsWithNewValues">IEntityFields instance which holds the new values for the matching entities to update. Only
		/// changed fields are taken into account</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="updateFilter">A predicate or predicate expression which should be used as filter for the entities to update.</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		public int UpdateMulti(IEntityFields fieldsWithNewValues, ITransaction containingTransaction, IPredicate updateFilter)
		{
			if(!fieldsWithNewValues.IsDirty)
			{
				// nothing to update
				return 0;
			}

			return CreateAndRunUpdateQuery(fieldsWithNewValues, containingTransaction, updateFilter);
		}


		/// <summary>
		/// Updates in the persistent storage all entities which match with the specified filter, formulated in
		/// the predicate or predicate expression definition. Which fields are updated in those matching entities depends on which fields are
		/// <i>changed</i> in the passed in IEntityFields instance fieldsWithNewValues. The new values of these fields are read from fieldsWithNewValues. 
		/// </summary>
		/// <param name="fieldsWithNewValues">IEntityFields instance which holds the new values for the matching entities to update. Only
		/// changed fields are taken into account</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="updateFilter">A predicate or predicate expression which should be used as filter for the entities to update.</param>
		/// <param name="relations">The set of relations to walk to construct the total query.</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		public int UpdateMulti(IEntityFields fieldsWithNewValues, ITransaction containingTransaction, IPredicate updateFilter, IRelationCollection relations)
		{
			if(!fieldsWithNewValues.IsDirty)
			{
				// nothing to update
				return 0;
			}

			return CreateAndRunUpdateQuery(fieldsWithNewValues, containingTransaction, updateFilter, relations);
		}


		/// <summary>
		/// Formulates and runs a Delete query based on the fields and filter specified. It will run in the specified transaction if available.
		/// </summary>
		/// <param name="fieldInTarget">Field to determine the target of the entity/entities to delete.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="deleteFilter">Filter to use to filter out the entities to delete</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		private int CreateAndRunDeleteQuery(IEntityField fieldInTarget, ITransaction containingTransaction, IPredicate deleteFilter)
		{
			IActionQuery deleteQuery = DynamicQueryEngine.CreateDeleteDQ(fieldInTarget, DbUtils.DetermineConnectionToUse(containingTransaction), deleteFilter);
			return base.ExecuteActionQuery(deleteQuery, containingTransaction);
		}


		/// <summary>
		/// Formulates and runs a Delete query based on the fields and filter specified. It will run in the specified transaction if available.
		/// </summary>
		/// <param name="fieldInTarget">Field to determine the target of the entity/entities to delete.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="deleteFilter">Filter to use to filter out the entities to delete</param>
		/// <param name="relations">The set of relations to walk to construct the total query.</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		private int CreateAndRunDeleteQuery(IEntityField fieldInTarget, ITransaction containingTransaction, IPredicate deleteFilter, IRelationCollection relations)
		{
			IActionQuery deleteQuery = DynamicQueryEngine.CreateDeleteDQ(fieldInTarget, DbUtils.DetermineConnectionToUse(containingTransaction), deleteFilter, relations);
			return base.ExecuteActionQuery(deleteQuery, containingTransaction);
		}


		/// <summary>
		/// Formulates and runs an Update query based on the fields and filter specified. It will run in the specified transaction if available.
		/// </summary>
		/// <param name="fields">Fields to determine the target of the entity/entities to update.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="updateFilter">Filter to use to filter out the entities to update</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		private int CreateAndRunUpdateQuery(IEntityFields fields, ITransaction containingTransaction, IPredicate updateFilter)
		{
			IActionQuery updateQuery = DynamicQueryEngine.CreateUpdateDQ(fields, DbUtils.DetermineConnectionToUse(containingTransaction), updateFilter);
			return base.ExecuteActionQuery(updateQuery, containingTransaction);
		}


		/// <summary>
		/// Formulates and runs an Update query based on the fields and filter specified. It will run in the specified transaction if available.
		/// </summary>
		/// <param name="fields">Fields to determine the target of the entity/entities to update.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="updateFilter">Filter to use to filter out the entities to update</param>
		/// <param name="relations">The set of relations to walk to construct the total query.</param>
		/// <returns>Amount of entities affected, if the used persistent storage has rowcounting enabled.</returns>
		private int CreateAndRunUpdateQuery(IEntityFields fields, ITransaction containingTransaction, IPredicate updateFilter, IRelationCollection relations)
		{
			IActionQuery updateQuery = DynamicQueryEngine.CreateUpdateDQ(fields, DbUtils.DetermineConnectionToUse(containingTransaction), updateFilter, relations);
			return base.ExecuteActionQuery(updateQuery, containingTransaction);
		}


		/// <summary>
		/// Formulates and runs a single instance Retrieval query based on the fields and filter specified. It will run in the specified transaction if available.
		/// </summary>
		/// <param name="fieldsToReturn">Fields which are filled by the retrieval query</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="selectFilter">Filter to use to filter out the entity to retrieve</param>
		private void CreateAndRunSingleInstanceRetrievalQuery(IEntityFields fieldsToReturn, ITransaction containingTransaction, IPredicate selectFilter)
		{
			IRetrievalQuery selectQuery = DynamicQueryEngine.CreateSelectDQ(fieldsToReturn, DbUtils.DetermineConnectionToUse(containingTransaction), selectFilter, 0, null, null);
			base.ExecuteSingleRowRetrievalQuery(selectQuery, containingTransaction, fieldsToReturn);
		}


		/// <summary>
		/// Formulates and runs a multi instance Retrieval query based on the fields and filter specified. It will run in the specified transaction if available.
		/// </summary>
		/// <param name="fieldsToReturn">IEntityFields implementation which forms the definition of the resultset to return.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="collectionToFill">Collection to fill with the entity objects retrieved</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="selectFilter">Filter to use to filter out the entity to retrieve</param>
		/// <param name="validatorToUse">The Validator object to use when creating entity objects during a GetMulti() call.</param>
		private void CreateAndRunMultiInstanceRetrievalQuery(IEntityFields fieldsToReturn, ITransaction containingTransaction, IEntityCollection collectionToFill, 
			long maxNumberOfItemsToReturn, ISortExpression sortClauses, IEntityFactory entityFactoryToUse, IPredicate selectFilter, IValidator validatorToUse)
		{
			IRetrievalQuery selectQuery = DynamicQueryEngine.CreateSelectDQ(fieldsToReturn, DbUtils.DetermineConnectionToUse(containingTransaction), 
				selectFilter, maxNumberOfItemsToReturn, sortClauses, null);
			base.ExecuteMultiRowRetrievalQuery(selectQuery, containingTransaction, entityFactoryToUse, collectionToFill, false, validatorToUse);
		}


		/// <summary>
		/// Formulates and runs a multi instance Retrieval query based on the fields and filter specified. It will run in the specified transaction if available.
		/// Double instances are filtered out.
		/// </summary>
		/// <param name="fieldsToReturn">IEntityFields implementation which forms the definition of the resultset to return.</param>
		/// <param name="containingTransaction">A containing transaction, if caller is added to a transaction, or null if not.</param>
		/// <param name="collectionToFill">Collection to fill with the entity objects retrieved</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When null is specified, no sorting is applied.</param>
		/// <param name="entityFactoryToUse">The EntityFactory to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="selectFilter">Filter to use to filter out the entity to retrieve</param>
		/// <param name="validatorToUse">The Validator object to use when creating entity objects during a GetMulti() call.</param>
		/// <param name="relations">The set of relations to walk to construct to total query.</param>
		private void CreateAndRunMultiInstanceRetrievalQuery(IEntityFields fieldsToReturn, ITransaction containingTransaction, IEntityCollection collectionToFill, 
			long maxNumberOfItemsToReturn, ISortExpression sortClauses, IEntityFactory entityFactoryToUse, IPredicate selectFilter, IValidator validatorToUse, IRelationCollection relations)
		{
			IRetrievalQuery selectQuery = DynamicQueryEngine.CreateSelectDQ(fieldsToReturn, DbUtils.DetermineConnectionToUse(containingTransaction), 
				selectFilter, maxNumberOfItemsToReturn, sortClauses, relations, true, null);
			base.ExecuteMultiRowRetrievalQuery(selectQuery, containingTransaction, entityFactoryToUse, collectionToFill, false, validatorToUse);
		}

	
		/// <summary>
		/// Formulates and runs a multi row Retrieval query based on the fields and filter specified. It will always create a new connection.
		/// </summary>
		/// <param name="fieldsToReturn">IEntityFields implementation which forms the definition of the resultset to return.</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="selectFilter">Filter to use to filter out the entity to retrieve</param>
		/// <returns>DataTable with the rows requested</returns>
		private DataTable CreateAndRunMultiRowRetrievalQuery(IEntityFields fieldsToReturn, long maxNumberOfItemsToReturn, ISortExpression sortClauses, IPredicate selectFilter)
		{
			IRetrievalQuery selectQuery = DynamicQueryEngine.CreateSelectDQ(fieldsToReturn, DbUtils.CreateConnection(), 
				selectFilter, maxNumberOfItemsToReturn, sortClauses, null);
			return base.ExecuteMultiRowDataTableRetrievalQuery(selectQuery, DbUtils.CreateDataAdapter());
		}


		/// <summary>
		/// Formulates and runs a multi row Retrieval query based on the fields and filter specified. It will always create a new connection.
		/// </summary>
		/// <param name="fieldsToReturn">IEntityFields implementation which forms the definition of the resultset to return.</param>
		/// <param name="maxNumberOfItemsToReturn"> The maximum number of items to return with this retrieval query. 
		/// If the used Dynamic Query Engine supports it, 'TOP' is used to limit the amount of rows to return. 
		/// When set to 0, no limitations are specified.</param>
		/// <param name="sortClauses">The order by specifications for the sorting of the resultset. When not specified, no sorting is applied.</param>
		/// <param name="selectFilter">Filter to use to filter out the entity to retrieve</param>
		/// <param name="relations">The set of relations to walk to construct to total query.</param>
		/// <returns>DataTable with the rows requested</returns>
		private DataTable CreateAndRunMultiRowRetrievalQuery(IEntityFields fieldsToReturn, long maxNumberOfItemsToReturn, ISortExpression sortClauses, IPredicate selectFilter, IRelationCollection relations)
		{
			IRetrievalQuery selectQuery = DynamicQueryEngine.CreateSelectDQ(fieldsToReturn, DbUtils.CreateConnection(), 
				selectFilter, maxNumberOfItemsToReturn, sortClauses, relations, false, null);
			return base.ExecuteMultiRowDataTableRetrievalQuery(selectQuery, DbUtils.CreateDataAdapter());
		}

	
		/// <summary>
		/// Creates a PredicateExpression which should be used as a filter when any combination of available foreign keys is specified.
		/// </summary>
		/// <param name="addressInstance">AddressEntity instance to use as a filter for the CustomerAddressEntity objects</param>
		/// <param name="addressTypeInstance">AddressTypeEntity instance to use as a filter for the CustomerAddressEntity objects</param>
		/// <param name="customerInstance">CustomerEntity instance to use as a filter for the CustomerAddressEntity objects</param>
		/// <param name="fieldsToReturn">IEntityFields implementation which forms the definition of the fieldset of the target entity.</param>
		/// <returns>A ready to use PredicateExpression based on the passed in foreign key value holders.</returns>
		private IPredicateExpression CreateFilterUsingForeignKeys(AddressEntity addressInstance, AddressTypeEntity addressTypeInstance, CustomerEntity customerInstance, IEntityFields fieldsToReturn)
		{
			IPredicateExpression selectFilter = new PredicateExpression();
			
			if(addressInstance != null)
			{
				selectFilter.Add(new FieldCompareValuePredicate(fieldsToReturn[(int)CustomerAddressFieldIndex.AddressID], ComparisonOperator.Equal, addressInstance.AddressID));
			}
			
			if(addressTypeInstance != null)
			{
				selectFilter.Add(new FieldCompareValuePredicate(fieldsToReturn[(int)CustomerAddressFieldIndex.AddressTypeID], ComparisonOperator.Equal, addressTypeInstance.AddressTypeID));
			}
			
			if(customerInstance != null)
			{
				selectFilter.Add(new FieldCompareValuePredicate(fieldsToReturn[(int)CustomerAddressFieldIndex.CustomerID], ComparisonOperator.Equal, customerInstance.CustomerID));
			}
			
			return selectFilter;
		}
	
	}
}
