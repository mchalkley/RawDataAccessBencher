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

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Implementation of the ISortExpression interface. This class contains the 
	/// sort clauses used in IRetrievalQuery instances.
	/// </summary>
	[Serializable]
	public class SortExpression : CollectionBase, ISortExpression
	{
		/// <summary>
		/// CTor
		/// </summary>
		public SortExpression()
		{
		}


		/// <summary>
		/// CTor which initially adds the passed in sort clause. This is an accelerator constructor to 
		/// make code more compact.
		/// </summary>
		/// <param name="sortClauseToAdd">Sort clause to add.</param>
		public SortExpression(ISortClause sortClauseToAdd)
		{
			this.Add(sortClauseToAdd);
		}


		/// <summary>
		/// Adds the passed in sort clause to the list. 
		/// </summary>
		/// <param name="sortClauseToAdd">the sort clause to add</param>
		/// <returns>The index the sort clause was added to</returns>
		public int Add(ISortClause sortClauseToAdd)
		{
			return List.Add(sortClauseToAdd);
		}


		/// <summary>
		/// Inserts the passed in sort clause at the index provided.
		/// </summary>
		/// <param name="index">Index to insert the sortclause at</param>
		/// <param name="sortClauseToAdd">the sort clause to insert</param>
		public void Insert(int index, ISortClause sortClauseToAdd)
		{
			List.Insert(index, sortClauseToAdd);
		}


		/// <summary>
		/// Removes the given sort clause from the list.
		/// </summary>
		/// <param name="sortClauseToRemove">the sort clause to remove.</param>
		public void Remove(ISortClause sortClauseToRemove)
		{
			List.Remove(sortClauseToRemove);
		}


		#region Class Property Declarations
		/// <summary>
		/// Indexer for this list.
		/// </summary>
		public ISortClause this [int index]
		{
			get { return (ISortClause)List[index]; }
			set { List[index] = value; }
		}
		#endregion
	}
}
