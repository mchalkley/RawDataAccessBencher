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
using System.Text;
using System.Data;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Implementation of a LIKE predicate expression, using the following formats:
	/// IEntityField(Core) LIKE Parameter (f.e. Foo LIKE @Foo )
	/// A specified pattern will be set as the parameters value.
	/// </summary>
	[Serializable]
	public class FieldLikePredicate : Predicate
	{
		#region Class Member Declarations
		private IEntityFieldCore		_field;
		private IFieldPersistenceInfo	_persistenceInfo;
		private string					_pattern;
		#endregion

		
		/// <summary>
		/// CTor
		/// </summary>
		public FieldLikePredicate()
		{
			InitClass(null, null, string.Empty, false, true);
		}


		/// <summary>
		/// CTor for Field LIKE Pattern. 
		/// </summary>
		/// <param name="field">Field to compare with the LIKE operator</param>
		/// <param name="pattern">Pattern to use in the LIKE expression</param>
		public FieldLikePredicate(IEntityField field, string pattern)
		{
			InitClass(field, field, pattern, false, true);
		}


		/// <summary>
		/// CTor for Field LIKE Pattern. 
		/// </summary>
		/// <param name="field">Field to compare with the LIKE operator</param>
		/// <param name="pattern">Pattern to use in the LIKE expression</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		public FieldLikePredicate(IEntityField field, string pattern, bool negate)
		{
			InitClass(field, field, pattern, negate, true);
		}


		/// <summary>
		/// CTor for Field LIKE Pattern. 
		/// </summary>
		/// <param name="field">Field to compare with the LIKE operator</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="pattern">Pattern to use in the LIKE expression</param>
		public FieldLikePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, string pattern)
		{
			InitClass(field, persistenceInfo, pattern, false, false);
		}


		/// <summary>
		/// CTor for Field LIKE Pattern. 
		/// </summary>
		/// <param name="field">Field to compare with the LIKE operator</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="pattern">Pattern to use in the LIKE expression</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		public FieldLikePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, string pattern, bool negate)
		{
			InitClass(field, persistenceInfo, pattern, negate, false);
		}


		/// <summary>
		/// Implements the IPredicate ToQueryText method. Retrieves a ready to use text representation of the contained Predicate.
		/// </summary>
		/// <param name="uniqueMarker">int counter which is appended to every parameter. The refcounter is increased by every parameter creation,
		/// making sure the parameter is unique in the predicate and also in the predicate expression(s) containing the predicate.</param>
		/// <returns>The contained Predicate in textual format.</returns>
		/// <exception cref="System.ApplicationException">When IPredicate.DatabaseSpecificCreator is not set to a valid value.</exception>
		public override string ToQueryText(ref int uniqueMarker)
		{
			if(_field==null)
			{
				return "";
			}

			if(base.DatabaseSpecificCreator==null)
			{
				throw new System.ApplicationException("DatabaseSpecificCreator object not set. Cannot create query part.");
			}

			base.Parameters.Clear();

			StringBuilder queryText = new StringBuilder(64);
			
			if(base.Negate)
			{
				queryText.Append("NOT ");
			}

			// create parameter 
			uniqueMarker++;
			IDataParameter parameter = base.DatabaseSpecificCreator.CreateLikeParameter(String.Format("{0}{1}", _field.Name, uniqueMarker), _pattern);
			base.Parameters.Add(parameter);

			queryText.AppendFormat("{0} LIKE {1}", base.DatabaseSpecificCreator.CreateFieldName(_persistenceInfo, _field.Name), parameter.ParameterName);
			return queryText.ToString();
		}


		/// <summary>
		/// Initializes the class
		/// </summary>
		/// <param name="field"></param>
		/// <param name="persistenceInfo"></param>
		/// <param name="pattern"></param>
		/// <param name="negate"></param>
		/// <param name="selfServicing"></param>
		private void InitClass(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, string pattern, bool negate, bool selfServicing)
		{
			_field = field;
			_persistenceInfo = persistenceInfo;
			_pattern = pattern;
			base.Negate=negate;
			base.SelfServicing = selfServicing;
			base.InstanceType = (int)PredicateType.FieldLikePredicate;
		}


		#region Class Property Declarations
		/// <summary>
		/// Field used in the comparison expression (SelfServicing).
		/// </summary>
		/// <exception cref="InvalidOperationException">if this object was constructed using a non-selfservicing constructor.</exception>
		public virtual IEntityField Field
		{
			get 
			{ 
				if(!base.SelfServicing)
				{
					// not applicable
					throw new InvalidOperationException("This object was constructed using a non-selfservicing constructor. Can't retrieve an IEntityField after that.");
				}
				return (IEntityField)_field; 
			}
		}

		/// <summary>
		/// Field used in the comparison expression (IEntityFieldCore).
		/// </summary>
		public virtual IEntityFieldCore FieldCore
		{
			get 
			{ 
				return _field; 
			}
		}

		/// <summary>
		/// Gets / sets persistenceInfo for field
		/// </summary>
		/// <exception cref="InvalidOperationException">When a value is set for this property and this object was created using a selfservicing constructor.</exception>
		public IFieldPersistenceInfo PersistenceInfo
		{
			get
			{
				return _persistenceInfo;
			}
			set
			{
				if(base.SelfServicing)
				{
					// not applicable
					throw new InvalidOperationException("This object was constructed using a selfservicing constructor. Can't set persistence info after that.");
				}
				_persistenceInfo = value;
			}
		}
		
		/// <summary>
		/// Gets / sets the pattern to use in a Field LIKE Pattern clause. 
		/// </summary>
		public virtual string Pattern
		{
			get { return _pattern; }
			set 
			{ 
				_pattern = value; 
			}
		}
		
		#endregion
		
	}
}