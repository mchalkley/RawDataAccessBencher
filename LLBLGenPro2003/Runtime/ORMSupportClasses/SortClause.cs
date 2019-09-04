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

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Class which implements ISortClause, a class which forms a single sort clause, thus an order by
	/// definition defined for a single IEntityField.
	/// </summary>
	[Serializable]
	public class SortClause : ISortClause
	{
		#region Class Member Declarations
		private	IEntityFieldCore		_fieldToSortCore;
		private IEntityField			_fieldToSort;
		private IFieldPersistenceInfo	_persistenceInfo;
		private SortOperator			_sortOperatorToUse;
		#endregion
		
		
		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="fieldToSort">IEntityField to sort on</param>
		/// <param name="sortOperatorToUse">the sort operator to use for this sort clause</param>
		public SortClause(IEntityField fieldToSort, SortOperator sortOperatorToUse)
		{
			_fieldToSort = fieldToSort;
			_fieldToSortCore = fieldToSort;
			_persistenceInfo = fieldToSort;
			_sortOperatorToUse = sortOperatorToUse;
		}


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="fieldToSort">IEntityFieldCore to sort on</param>
		/// <param name="persistenceInfo">Persistence info of fieldToSort</param>
		/// <param name="sortOperatorToUse">the sort operator to use for this sort clause</param>
		public SortClause(IEntityFieldCore fieldToSort, IFieldPersistenceInfo persistenceInfo, SortOperator sortOperatorToUse)
		{
			_fieldToSort = null;
			_fieldToSortCore = fieldToSort;
			_persistenceInfo = persistenceInfo;
			_sortOperatorToUse = sortOperatorToUse;
		}


		#region Class Property Declarations
		/// <summary>
		/// IEntityField to sort on. Will be null if this object is constructed using a non-selfservicing constructor.
		/// </summary>
		public IEntityField FieldToSort
		{
			get { return _fieldToSort; }
			set { _fieldToSort = value; }
		}

		/// <summary>
		/// IEntityFieldCore to sort on.
		/// </summary>
		public IEntityFieldCore FieldToSortCore
		{
			get
			{
				return _fieldToSortCore;
			}
			set
			{
				_fieldToSortCore = value;
			}
		}

		/// <summary>
		/// Persistence information for FieldToSort. Can be a cast of the same object, when an IEntityField is
		/// added to this sort clause
		/// </summary>
		public IFieldPersistenceInfo PersistenceInfo
		{
			get
			{
				return _persistenceInfo;
			}
			set
			{
				_persistenceInfo = value;
			}
		}
		
		/// <summary>
		/// The sort operator to use for this sort clause
		/// </summary>
		public SortOperator SortOperatorToUse
		{
			get { return _sortOperatorToUse; }
			set { _sortOperatorToUse = value; }
		}
		#endregion
	}
}
