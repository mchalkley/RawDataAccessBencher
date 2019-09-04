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
using System.ComponentModel;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Summary description for EntityPropertyDescriptor2.
	/// </summary>
	[Serializable]
	public class EntityPropertyDescriptor2 : PropertyDescriptor
	{
		#region Class Member Declarations
		private IEntityField2	_field;
		private Type			_typeOfBindedObject;
		private bool			_isReadOnly;
		#endregion
		

		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="field">Field which is exposed as a property and which has to be described in a PropertyDescriptor</param>
		/// <param name="typeOfBindedObject">The type of object this property is a property of</param>
		/// <param name="isReadOnly">True if the field is an identity field/primary key field and the entity itself is not new</param>
		public EntityPropertyDescriptor2(IEntityField2 field, Type typeOfBindedObject, bool isReadOnly):base(field.Name, null)
		{
			_field = field;
			_typeOfBindedObject = typeOfBindedObject;
			_isReadOnly = isReadOnly;
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		/// <param name="component"></param>
		/// <returns>always false (not supported)</returns>
		public override bool CanResetValue(object component)
		{
			return false;
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		/// <param name="component"></param>
		/// <returns>current value of associated IEntityField</returns>
		public override object GetValue(object component)
		{
			return ((IEntity2)component).Fields[_field.Name].CurrentValue;
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		/// <param name="component"></param>
		/// <param name="value"></param>
		public override void SetValue(object component, object value)
		{
			//((IEntity2)component).SetNewFieldValue(_field.FieldIndex, value);
			component.GetType().GetProperty(this.Name).SetValue(component, value, null);
		}


		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		/// <param name="component"></param>
		/// <returns>always false (not supported)</returns>
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override Type ComponentType
		{
			get
			{
				return _typeOfBindedObject;
			}
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override string DisplayName
		{
			get
			{
				return _field.Name;
			}
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override bool IsBrowsable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override bool IsReadOnly
		{
			get
			{
				return _isReadOnly;
			}
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override string Name
		{
			get
			{
				return _field.Name;
			}
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override Type PropertyType
		{
			get
			{
				return _field.DataType;
			}
		}

		/// <summary>
		/// See PropertyDescriptor class.
		/// </summary>
		public override void ResetValue(object component)
		{
			// empty
		}
	}
}
