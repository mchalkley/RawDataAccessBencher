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
	/// IRelationPredicateBucket implementation which can be used as a single unit to pass to a data-access adapter for 
	/// filtering over multi-entities.
	/// </summary>
	[Serializable]
	public class RelationPredicateBucket : IRelationPredicateBucket
	{
		#region Class Member Declarations
		private RelationCollection		_relations;
		private PredicateExpression		_predicateExpression;
		#endregion


		/// <summary>
		/// CTor
		/// </summary>
		public RelationPredicateBucket()
		{
			_relations = new RelationCollection();
			_predicateExpression = new PredicateExpression();
		}

		
		/// <summary>
		/// The relation collection with EntityRelation objects which is used in combination with the PredicateExpression in this bucket
		/// </summary>
		public IRelationCollection Relations
		{
			get
			{
				return _relations;
			}
		}

		/// <summary>
		/// The predicate expression to use in combination with the Relations in this bucket.
		/// </summary>
		public IPredicateExpression PredicateExpression
		{
			get
			{
				return _predicateExpression;
			}
		}
	}
}