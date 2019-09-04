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
using System.Text;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Class which is used to stack relation objects between several entities to build a complete join path
	/// </summary>
	[Serializable]
	public class RelationCollection : CollectionBase, IRelationCollection
	{
		#region Class Member Declarations
			private IDbSpecificCreator	_databaseSpecificCreator;
			private bool				_obeyWeakRelations;

			[NonSerialized]
			private ArrayList			_customFilterParameters;
		#endregion
		
		
		/// <summary>
		/// CTor
		/// </summary>
		public RelationCollection()
		{
			_obeyWeakRelations=false;
			_customFilterParameters = new ArrayList();
		}


		/// <summary>
		/// Adds the passed in IEntityRelation instance to the list. Relations can be added more than once.
		/// The order is important.
		/// </summary>
		/// <param name="relationToAdd">IEntityRelation instance to add</param>
		/// <returns>the added relation</returns>
		public IEntityRelation Add(IEntityRelation relationToAdd)
		{
			List.Add(relationToAdd);
			return relationToAdd;
		}


		/// <summary>
		/// Adds the passed in IEntityRelation instance to the list at position index. Relations can be added more than once.
		/// The order is important.
		/// </summary>
		/// <param name="relationToAdd">IEntityRelation instance to add</param>
		/// <param name="index">Index to add the relation to.</param>
		public void Insert(IEntityRelation relationToAdd, int index)
		{
			List.Insert(index, relationToAdd);
		}


		/// <summary>
		/// Removes the passed in IEntityRelation instance. Only the first instance will be removed.
		/// </summary>
		/// <param name="relationToRemove">IEntityRelation instance to remove</param>
		public void Remove(IEntityRelation relationToRemove)
		{
			List.Remove(relationToRemove);
		}


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
		public string ToQueryText(ref int uniqueMarker)
		{
			if(_databaseSpecificCreator==null)
			{
				throw new System.ApplicationException("DatabaseSpecificCreator object not set. Cannot create query part.");
			}

			StringBuilder queryText = new StringBuilder(128);
			
			// clear any previously created objects
			_customFilterParameters = new ArrayList();

			// create a sorted list for database elements which will participate in the joins. This list is to determine if an element is
			// already in the join so it isn't added again, but instead the other side of the relation is added. This is necessary because
			// relations are specified with 1 statement but have 2 directions.
			SortedList addedDBElements = new SortedList(new CaseInsensitiveComparer());
			for(int i=0;i<List.Count;i++)
			{
				IEntityRelation relation = this[i];
				string dbElement1, dbElement2;
				bool element1IsAddedWeak, element2IsAddedWeak;

				if(i==0)
				{
					// emit first object. These are always added.
					dbElement1 = _databaseSpecificCreator.CreateObjectName(relation.GetPKFieldPersistenceInfo(0));
					dbElement2 = _databaseSpecificCreator.CreateObjectName(relation.GetFKFieldPersistenceInfo(0));
					if(_obeyWeakRelations && relation.IsWeak)
					{
						if(relation.TypeOfRelation==RelationType.ManyToOne)
						{
							// FK first, always join towards the FK in this situation (m:1 relation)
							queryText.AppendFormat(" {0} LEFT JOIN {1} ON", dbElement2, dbElement1);
							element1IsAddedWeak=true;
							element2IsAddedWeak=false;
						}
						else
						{
							// PK first, always join towards the PK in this situation (1:n or 1:1 relation)
							queryText.AppendFormat(" {0} LEFT JOIN {1} ON", dbElement1, dbElement2);
							element1IsAddedWeak=false;
							element2IsAddedWeak=true;
						}
					}
					else
					{
						// no weak relation or no weak relations should be obeyed: inner join
						queryText.AppendFormat(" {0} INNER JOIN {1} ON", dbElement1, dbElement2);
						element1IsAddedWeak=false;
						element2IsAddedWeak=false;
					}
					addedDBElements.Add(dbElement1, element1IsAddedWeak);
					addedDBElements.Add(dbElement2, element2IsAddedWeak);
				}
				else
				{
					// A rel B. Always try to join B. If B is already in the pack, don't join B again. 
					switch(relation.TypeOfRelation)
					{
						case RelationType.ManyToOne:
							// B contains PK fields
							dbElement1=_databaseSpecificCreator.CreateObjectName(relation.GetPKFieldPersistenceInfo(0));
							dbElement2=_databaseSpecificCreator.CreateObjectName(relation.GetFKFieldPersistenceInfo(0));
							break;
						case RelationType.OneToOne:
							// unknown what B is, pk or fk
							dbElement1=_databaseSpecificCreator.CreateObjectName(relation.GetPKFieldPersistenceInfo(0));
							dbElement2=_databaseSpecificCreator.CreateObjectName(relation.GetFKFieldPersistenceInfo(0));
							break;
						default:
							// 1:n. B contains FK fields
							dbElement1=_databaseSpecificCreator.CreateObjectName(relation.GetFKFieldPersistenceInfo(0));
							dbElement2=_databaseSpecificCreator.CreateObjectName(relation.GetPKFieldPersistenceInfo(0));
							break;
					}

					bool relationIsWeak = relation.IsWeak;

					if(addedDBElements.ContainsKey(dbElement1))
					{
						if(relation.TypeOfRelation == RelationType.OneToOne)
						{
							// try other side. If not present, weakness changes.
							string tmp = dbElement1;
							dbElement1 = dbElement2;
							dbElement2 = tmp;
							if(addedDBElements.ContainsKey(dbElement1))
							{
								// both sides are already added. skip this one
								continue;
							}
							else
							{
								// sides changed, weakness changes too
								relationIsWeak = !relationIsWeak;
							}
						}
						else
						{
							// already added, skip
							continue;
						}
					}

					// dbElement1 is scheduled to be added to the join list. dbElement2 is already in that joinlist. 
					// check if dbElement2 is added weak. If so, dbElement1 has to be added weak too, no matter if the relation is strong or not.
					relationIsWeak = (relationIsWeak || (bool)addedDBElements[dbElement2]);

					// check if we're dealing with a weak relationship and that we should care. If weak relationships are not an issue, the 
					// join statement is already set to INNER
					if(_obeyWeakRelations && relationIsWeak)
					{
						// is weak. Always join towards left element in the expression: (join set) JOIN (new element), thus LEFT.
						queryText.AppendFormat(" LEFT JOIN {0} ON", dbElement1);
						element1IsAddedWeak=true;
					}
					else
					{
						// no weak relation or do not obey weak relations: INNER JOIN
						queryText.AppendFormat(" INNER JOIN {0} ON", dbElement1);
						element1IsAddedWeak=false;
					}

					addedDBElements.Add(dbElement1, element1IsAddedWeak);
				}

				// create ON clauses.
				for(int j=0;j<relation.AmountFields;j++)
				{
					if(j>0)
					{
						queryText.Append(" AND");
					}
					queryText.AppendFormat(" {0}={1}", 
						_databaseSpecificCreator.CreateFieldName(relation.GetPKFieldPersistenceInfo(j), relation.GetPKEntityFieldCore(j).Name),
						_databaseSpecificCreator.CreateFieldName(relation.GetFKFieldPersistenceInfo(j), relation.GetFKEntityFieldCore(j).Name));
				}

				// if this EntityRelation has a custom filter, add that filter with AND. 
				if(relation.CustomFilter!=null)
				{
					if(relation.CustomFilter.Count>0)
					{
						relation.CustomFilter.DatabaseSpecificCreator = _databaseSpecificCreator;
						queryText.AppendFormat(" AND {0}", relation.CustomFilter.ToQueryText(ref uniqueMarker));
						// add parameters created by this custom filter to our general list.
						_customFilterParameters.AddRange(relation.CustomFilter.Parameters);
					}
				}
			}
			return queryText.ToString();
		}


		#region Class Property Declarations
		/// <summary>
		/// Object which will be used to create valid parameter objects, field names, including prefix/postfix characters, 
		/// and conversion routines, and field names, including prefix/postfix characters. 
		/// Uses the strategy pattern so the generic code can work with more than one target database.
		/// </summary>
		public IDbSpecificCreator DatabaseSpecificCreator
		{
			get { return _databaseSpecificCreator; }
			set { _databaseSpecificCreator = value; }
		}

		/// <summary>
		/// Indexer in the collection.
		/// </summary>
		public IEntityRelation this [int index]
		{
			get { return (IEntityRelation)this.List[index]; }
			set { List[index] = value;}
		}

		/// <summary>
		/// Gets / sets ObeyWeakRelations, which is the flag to signal the collection what kind of join statements to generate in the
		/// ToQueryText statement, which is called by the DQE. Weak relationships are relationships which are optional, for example a
		/// customer with no orders is possible, because the relationship between customer and order is based on a field in order.
		/// When this property is set to true (default: false), weak relationships will result in LEFT JOIN statements. When
		/// set to false (which is the default), INNER JOIN statements are used.
		/// </summary>
		public bool ObeyWeakRelations
		{
			get
			{
				return _obeyWeakRelations;
			}
			set
			{
				_obeyWeakRelations = value;
			}
		}


		/// <summary>
		/// Gets Custom Filter Parameters, created in ToQueryText and which are used in custom filters.
		/// </summary>
		public ArrayList CustomFilterParameters
		{
			get
			{
				return _customFilterParameters;
			}
		}
		#endregion
	}
}