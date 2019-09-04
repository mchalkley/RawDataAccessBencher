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
using System.Collections;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Implementation of a Field compare-range Values expression, using the following format:
	/// IEntityField(Core) ComparisonOperator Parameters (f.e. Foo IN (@Foo1, @Foo2 ... ))
	/// There is no check for types between the value specified and the specified IEntityField.
	/// </summary>
	[Serializable]
	public class FieldCompareRangePredicate : Predicate
	{
		#region Class Member Declarations
		private IEntityFieldCore         _field;
		private IFieldPersistenceInfo		_persistenceInfo;
		private ArrayList                _values;
		#endregion

		/// <summary>
		/// CTor
		/// </summary>
		public FieldCompareRangePredicate()
		{
			InitClass(null, null, false, true, new object[0]);
		}


		/// <summary>
		/// CTor. Creates Field IN (values) clause
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="values">Value range to set for the IN clause. Specify any range of values.
		/// If a single array is passed or an ArrayList, this will be converted to a range of values.</param>
		public FieldCompareRangePredicate(IEntityField field, params object[] values)
		{
			InitClass(field, field, false, true, values);
		}


		/// <summary>
		/// CTor. Creates Field IN (values) clause
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		/// <param name="values">Value range to set for the IN clause. Specify any range of values.
		/// If a single array is passed or an ArrayList, this will be converted to a range of values.</param>
		public FieldCompareRangePredicate(IEntityField field, bool negate, params object[] values)
		{
			InitClass(field, field, negate, true, values);
		}


		/// <summary>
		/// CTor. Creates Field IN (values) clause
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="values">Value range to set for the IN clause. Specify any range of values.
		/// If a single array is passed or an ArrayList, this will be converted to a range of values.</param>
		public FieldCompareRangePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, params object[] values)
		{
			InitClass(field, persistenceInfo, false, false, values);
		}


		/// <summary>
		/// CTor. Creates Field IN (values) clause
		/// </summary>
		/// <param name="field">Field used in the comparison expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		/// <param name="values">Value range to set for the IN clause. Specify any range of values.
		/// If a single array is passed or an ArrayList, this will be converted to a range of values.</param>
		public FieldCompareRangePredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, bool negate, params object[] values)
		{
			InitClass(field, persistenceInfo, negate, false, values);
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

			if(_values.Count<=0)
			{
				return "";
			}

			if(base.DatabaseSpecificCreator==null)
			{
				throw new System.ApplicationException("DatabaseSpecificCreator object not set. Cannot create query part.");
			}

			base.Parameters.Clear();

			StringBuilder queryText = new StringBuilder(128);
            
			queryText.AppendFormat("{0} ", base.DatabaseSpecificCreator.CreateFieldName(_persistenceInfo, _field.Name));

			if(base.Negate)
			{
				queryText.Append("NOT ");
			}

			queryText.Append("IN (");

			// create parameters, one for each value
			for (int i = 0; i < _values.Count; i++)
			{
				if(i>0)
				{
					queryText.Append(", ");
				}

				IDataParameter parameter = base.DatabaseSpecificCreator.CreateParameter(_field, _persistenceInfo, ParameterDirection.Input);
				base.Parameters.Add(parameter);
				uniqueMarker++;
				parameter.Value = _values[i];
				parameter.ParameterName += uniqueMarker.ToString();

				queryText.Append(parameter.ParameterName);
			}
			queryText.Append(")");
			return queryText.ToString();
		}


		/// <summary>
		/// Initializes the class.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="persistenceInfo"></param>
		/// <param name="negate"></param>
		/// <param name="selfServicing"></param>
		/// <param name="values"></param>
		private void InitClass(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, bool negate, bool selfServicing, params object[] values)
		{
			_field = field;
			_persistenceInfo = persistenceInfo;
			base.Negate=negate;
			base.SelfServicing = selfServicing;
			base.InstanceType = (int)PredicateType.FieldCompareRangePredicate;
			_values = new ArrayList();
			// convert the passed in set of values to a valid set of values. If an array is passed in, it has to be unwrapped. if an ICollection has been
			// passed in, it has to be unwrapped as well.
			for (int i = 0; i < values.Length; i++)
			{
				// check type of values[i]. If required, unwrap it.
				Type typeOfValue = values[i].GetType();
				if(typeOfValue.IsArray)
				{
					// unwrap array. If this is a multi-dimensional array, it will create an exception
					Array currentValue = (Array)values[i];
					for(int j=0;j<currentValue.Length;j++)
					{
						_values.Add(currentValue.GetValue(j));
					}
					// done.
					continue;
				}
				// not an array, test for implementation of ICollection
				ICollection col = values[i] as ICollection;
				if(col==null)
				{
					// not an arraylist, add the value directly
					_values.Add(values[i]);
				}
				else
				{
					// it's an ICollection supporting object with values. add it
					_values.AddRange(col);
				}
			}
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
		/// Values to set for the IN Clause
		/// </summary>
		public virtual ArrayList Values
		{
			get { return _values; }
			set { _values = value; }
		}
		#endregion
        
	}
}