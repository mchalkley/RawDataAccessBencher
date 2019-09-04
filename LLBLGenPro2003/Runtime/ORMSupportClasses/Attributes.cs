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
	/// Attribute to use on properties which return an entity collection in the Adapter template set.
	/// This attribute will tell the property descriptor construction code to construct a list of 
	/// properties of the type set as the value of the attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class TypeContainedAttribute : Attribute
	{
		#region Class Member Declarations
		private Type	_typeContainedInCollection;
		#endregion


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="typeContainedInCollection">The type of the objects contained in the collection
		/// returned by the property this attribute is applied to.</param>
		public TypeContainedAttribute(Type typeContainedInCollection)
		{
			_typeContainedInCollection = typeContainedInCollection;
		}


		#region Class Property Declarations
		/// <summary>
		/// Gets typeContainedInCollection
		/// </summary>
		public Type TypeContainedInCollection
		{
			get
			{
				return _typeContainedInCollection;
			}
		}

		#endregion


	}
}
