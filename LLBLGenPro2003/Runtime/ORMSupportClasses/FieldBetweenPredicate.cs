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
	/// Implementation of a Field Between ValueBegin And ValueEnd expression, using the following format:
	/// IEntityField(Core) Between Parameter1 And Parameter2 (f.e Foo BETWEEN @Foo1 AND Foo2)
	/// There is no check for types between the values specified and the specified IEntityField.
	/// </summary>
	[Serializable]
	public class FieldBetweenPredicate : Predicate
	{
		#region Class Member Declarations
		private IEntityFieldCore		_field;
		private IFieldPersistenceInfo	_persistenceInfo;
		private object					_valueBegin, _valueEnd;
		#endregion
		
		/// <summary>
		/// CTor
		/// </summary>
		public FieldBetweenPredicate()
		{
			InitClass(null, null, null, null, false, true);
		}


		/// <summary>
		/// CTor. Creates the Field Between Value1 And Value2 expression (Self servicing version)
		/// </summary>
		/// <param name="field">Field used in the Between expression</param>
		/// <param name="valueBegin">Begin value in the Between clause</param>
		/// <param name="valueEnd">End value in the Between clause</param>
		public FieldBetweenPredicate(IEntityField field, object valueBegin, object valueEnd)
		{
			InitClass(field, field, valueBegin, valueEnd, false, true);
		}


		/// <summary>
		/// CTor. Creates the Field Between Value1 And Value2 expression.
		/// </summary>
		/// <param name="field">Field used in the Between expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="valueBegin">Begin value in the Between clause</param>
		/// <param name="valueEnd">End value in the Between clause</param>
		public FieldBetweenPredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, object valueBegin, object valueEnd)
		{
			InitClass(field, persistenceInfo, valueBegin, valueEnd, false, false);
		}


		/// <summary>
		/// CTor. Creates the Field Between Value1 And Value2 expression
		/// </summary>
		/// <param name="field">Field used in the Between expression</param>
		/// <param name="valueBegin">Begin value in the Between clause</param>
		/// <param name="valueEnd">End value in the Between clause</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		public FieldBetweenPredicate(IEntityField field, object valueBegin, object valueEnd, bool negate)
		{
			InitClass(field, field, valueBegin, valueEnd, negate, true);
		}


		/// <summary>
		/// CTor. Creates the Field Between Value1 And Value2 expression
		/// </summary>
		/// <param name="field">Field used in the Between expression</param>
		/// <param name="persistenceInfo">The persistence info object for the field</param>
		/// <param name="valueBegin">Begin value in the Between clause</param>
		/// <param name="valueEnd">End value in the Between clause</param>
		/// <param name="negate">Flag to make this expression add NOT to itself</param>
		public FieldBetweenPredicate(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, object valueBegin, object valueEnd, bool negate)
		{
			InitClass(field, persistenceInfo, valueBegin, valueEnd, negate, false);
		}


		/// <summary>
		/// Implements the IPredicate ToQueryText method. Retrieves a ready to use text representation of the contained Predicate.
		/// The two parameters for begin and end value are constructed using the field's name plus the postfixes 'Begin' and 'End'.
		/// Generates SQL-92 syntaxis for BETWEEN, which is accepted by all databases known. 
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
			queryText.Append(base.DatabaseSpecificCreator.CreateFieldName(_persistenceInfo, _field.Name));

			if(base.Negate)
			{
				queryText.Append(" NOT");
			}

			// create parameters
			IDataParameter parameterBegin = base.DatabaseSpecificCreator.CreateParameter(_field, _persistenceInfo, ParameterDirection.Input);
			uniqueMarker++;
			parameterBegin.ParameterName += uniqueMarker.ToString();
			base.Parameters.Add(parameterBegin);
			parameterBegin.Value = _valueBegin;

			IDataParameter parameterEnd = base.DatabaseSpecificCreator.CreateParameter(_field, _persistenceInfo, ParameterDirection.Input);
			uniqueMarker++;
			parameterEnd.ParameterName += uniqueMarker.ToString();
			base.Parameters.Add(parameterEnd);
			parameterEnd.Value = _valueEnd;

			queryText.AppendFormat(" BETWEEN {0} AND {1}", parameterBegin.ParameterName, parameterEnd.ParameterName);

			return queryText.ToString();
		}


		/// <summary>
		/// Inits the class
		/// </summary>
		/// <param name="field"></param>
		/// <param name="persistenceInfo"></param>
		/// <param name="valueBegin"></param>
		/// <param name="valueEnd"></param>
		/// <param name="negate"></param>
		/// <param name="selfServicing"></param>
		private void InitClass(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, object valueBegin, object valueEnd, bool negate, bool selfServicing)
		{
			_field=field;
			_persistenceInfo = persistenceInfo;
			_valueBegin = valueBegin;
			_valueEnd = valueEnd;
			base.Negate = negate;
			base.SelfServicing = selfServicing;
			base.InstanceType = (int)PredicateType.FieldBetweenPredicate;
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
		/// Gets / sets valueBegin
		/// </summary>
		public object ValueBegin
		{
			get
			{
				return _valueBegin;
			}
			set
			{
				_valueBegin = value;
			}
		}

		/// <summary>
		/// Gets / sets valueEnd
		/// </summary>
		public object ValueEnd
		{
			get
			{
				return _valueEnd;
			}
			set
			{
				_valueEnd = value;
			}
		}

		#endregion
	}
}
