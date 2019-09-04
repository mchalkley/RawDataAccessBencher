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
using System.Data;

namespace SD.LLBLGen.Pro.ORMSupportClasses2003
{
	/// <summary>
	/// Class to define the relation between a parameter of a query and a field. This relation is 
	/// used to find back a related EntityFieldCore instance when an Output Parameter is found in a query so the value 
	/// of the Output Parameter can be assigned to the related EntityField
	/// </summary>
	[Serializable]
	public class ParameterFieldRelation : IParameterFieldRelation 
	{
		#region Class Member Declarations
		private IEntityFieldCore	_field;
		private IDataParameter		_parameter;
		#endregion
		
		
		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="field">The <see cref="IEntityFieldCore"/> in the relationship.</param>
		/// <param name="parameter">The Parameter in the relationship.</param>
		public ParameterFieldRelation(IEntityFieldCore field, IDataParameter parameter)
		{
			_field = field;
			_parameter = parameter;
		}


		#region Class Property Declarations
		/// <summary>
		/// The <see cref="IEntityFieldCore"/> in the relationship. Only settable via a constructor.
		/// </summary>
		public IEntityFieldCore Field
		{
			get
			{
				return _field;
			}
		}

		/// <summary>
		/// The Parameter in the relationship. Only settable via a constructor.
		/// </summary>
		public System.Data.IDataParameter Parameter
		{
			get
			{
				return _parameter;
			}
		}
		#endregion
	}
}
