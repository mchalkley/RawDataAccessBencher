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
	/// Implementation of IFieldValueSetClause
	/// </summary>
	[Serializable]
	[Obsolete("Obsolete. Do not use.", true)]
	public class FieldValueSetClause : IFieldValueSetClause
	{
		#region Class Member Declarations
		private IEntityField	_field;
		private object			_value;
		#endregion
		
		
		/// <summary>
		/// CTor
		/// </summary>
		public FieldValueSetClause()
		{
			_field = null;
			_value = null;
		}


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="field">Field to set the value of</param>
		/// <param name="value">Value to set the field to</param>
		public FieldValueSetClause(IEntityField field, object value)
		{
			_field=field;
			_value=value;
		}


		#region Class Property Declarations
		/// <summary>
		/// Field to set the value of
		/// </summary>
		public virtual IEntityField Field
		{
			get { return _field; }
			set { _field = value; }
		}
		
		/// <summary>
		/// Value to set the field to
		/// </summary>
		public virtual object Value
		{
			get { return _value; }
			set { _value = value; }
		}
		#endregion
		

	}
}
