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
	/// Implementation of a Field compare-operator Value expression, using the following format:
	/// IEntityField(Core) ComparisonOperator Parameter (f.e. Foo = @Foo)
	/// There is no check for types between the value specified and the specified IEntityField.
	/// </summary>
	[Serializable]
	public class FieldCompareValuePredicate : Predicate
	{
		#region Class Member Declarations
		private IEntityFieldCore			_field;
		private IFieldPersistenceInfo		_persistenceInfo;
		private ComparisonOperator			_comparisonOperator;
		private object						_value;
		#endregion

		/// <summary>
		/// CTor
		/// </summary>
		public FieldCompareValuePredicate()
		{
			InitClass(null, null, ComparisonOperator.Equal, null, false, true);
		}


		/// <summary>
		/// CTor. Creates Field ComparisonOperator Parameter clause. The value to compare with is retrieved from the passed in field object.
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="comparisonOperator">Operator to use in the comparison</param>
		public FieldCompareValuePredicate(IEntityField field, ComparisonOperator comparisonOperator)
		{
			InitClass(field, field, comparisonOperator, field.CurrentValue, false, true);
		}

		
		/// <summary>
		/// CTor. Creates Field ComparisonOperator Parameter clause
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="comparisonOperator">Operator to use in the comparison</param>
		/// <param name="value">Value to set for the parameter</param>
		public FieldCompareValuePredicate(IEntityField field, ComparisonOperator comparisonOperator, object value)
		{
			InitClass(field, field, comparisonOperator, value, false, true);
		}


		/// <summary>
		/// CTor. Creates Field ComparisonOperator Parameter clause
		/// </summary>
		/// <param name="field"></param>
		/// <param name="comparisonOperator"></param>
		/// <param name="value">Value to set for the parameter</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		public FieldCompareValuePredicate(IEntityField field, ComparisonOperator comparisonOperator, object value, bool negate)
		{
			InitClass(field, field, comparisonOperator, value, negate, true);
		}


		/// <summary>
		/// CTor. Creates Field ComparisonOperator Parameter clause. The value to compare with is retrieved from the passed in field object.
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="comparisonOperator">Operator to use in the comparison</param>
		public FieldCompareValuePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, ComparisonOperator comparisonOperator)
		{
			InitClass(field, persistenceInfo, comparisonOperator, field.CurrentValue, false, false);
		}


		/// <summary>
		/// CTor. Creates Field ComparisonOperator Parameter clause. The value to compare with is retrieved from the passed in field object.
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="comparisonOperator">Operator to use in the comparison</param>
		/// <param name="value">Value to set for the parameter</param>
		public FieldCompareValuePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, ComparisonOperator comparisonOperator, object value)
		{
			InitClass(field, persistenceInfo, comparisonOperator, value, false, false);
		}


		/// <summary>
		/// CTor. Creates Field ComparisonOperator Parameter clause. The value to compare with is retrieved from the passed in field object.
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="comparisonOperator">Operator to use in the comparison</param>
		/// <param name="value">Value to set for the parameter</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		public FieldCompareValuePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, ComparisonOperator comparisonOperator, 
				object value, bool negate)
		{
			InitClass(field, persistenceInfo, comparisonOperator, value, negate, false);
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

			StringBuilder queryText = new StringBuilder(128);
			
			if(base.Negate)
			{
				queryText.Append("NOT ");
			}

			// create parameter 
			IDataParameter parameter = base.DatabaseSpecificCreator.CreateParameter(_field, _persistenceInfo, ParameterDirection.Input);
			base.Parameters.Add(parameter);
			uniqueMarker++;
			parameter.Value = _value;
			parameter.ParameterName += uniqueMarker.ToString();

			queryText.AppendFormat("{0} {1} {2}", base.DatabaseSpecificCreator.CreateFieldName(_persistenceInfo, _field.Name), 
					base.DatabaseSpecificCreator.ConvertComparisonOperator(_comparisonOperator), parameter.ParameterName);

			return queryText.ToString();
		}


		/// <summary>
		/// Initializes the class.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="persistenceInfo"></param>
		/// <param name="comparisonOperator"></param>
		/// <param name="value"></param>
		/// <param name="negate"></param>
		/// <param name="selfServicing"></param>
		private void InitClass(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, ComparisonOperator comparisonOperator, 
				object value, bool negate, bool selfServicing)
		{
			_field = field;
			_persistenceInfo = persistenceInfo;
			_comparisonOperator = comparisonOperator;
			_value = value;
			base.Negate=negate;
			base.SelfServicing = selfServicing;
			base.InstanceType = (int)PredicateType.FieldCompareValuePredicate;
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
		/// Operator to use in the comparison
		/// </summary>
		public virtual ComparisonOperator Operator
		{
			get { return _comparisonOperator; }
			set { _comparisonOperator = value; }
		}
		
		/// <summary>
		/// Value to set for the parameter
		/// </summary>
		public virtual object Value
		{
			get { return _value; }
			set { _value = value; }
		}
		#endregion
		
	}
}