//////////////////////////////////////////////////////////////////////
// Part of the Dynamic Query Engine (DQE) for SqlServer, used in the generated code. 
// LLBLGen Pro specific. Do not modify. 
// LLBLGen Pro is (c) 2002-2003 Solutions Design. All rights reserved.
// http://www.llblgen.com
// http://www.sd.nl/llblgen
//////////////////////////////////////////////////////////////////////
// The sourcecode for this DQE is released as BSD2 licensed open source, so licensees and others can
// modify, update, extend or use it to write other DQE's. Do NOT, under any circumstance, change the
// INTERFACE of the DQE. Each DQE has to implement the same methods to make it work with LLBLGen Pro's
// generated code. 
//////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Copyright (c)2003 Solutions Design. All rights reserved.
// 
// This DQE is released under the following license: (BSD2)
// -------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using SD.LLBLGen.Pro.ORMSupportClasses2003;

namespace SD.LLBLGen.Pro.DQE.SqlServer2003
{
	/// <summary>
	/// Implements IDbSpecificCreator for SqlServer. 
	/// </summary>
	[Serializable]
	public class SqlServerSpecificCreator : IDbSpecificCreator
	{
		/// <summary>
		/// CTor
		/// </summary>
		public SqlServerSpecificCreator()
		{
		}


		/// <summary>
		/// Creates a valid Parameter based on the passed in IEntityField implementation.
		/// </summary>
		/// <param name="field">IEntityField instance used to base the parameter on.</param>
		/// <param name="direction">The direction for the parameter</param>
		/// <returns>Valid parameter for usage with the target database.</returns>
		public IDataParameter CreateParameter(IEntityField field, ParameterDirection direction)
		{
			return CreateParameter((IEntityFieldCore)field, (IFieldPersistenceInfo)field, direction);
		}


		/// <summary>
		/// Creates a valid Parameter based on the passed in IEntityFieldCore implementation and the passed in IFieldPersistenceInfo instance
		/// </summary>
		/// <param name="field">IEntityFieldCore instance used to base the parameter on.</param>
		/// <param name="persistenceInfo">Persistence information to create the parameter.</param>
		/// <param name="direction">The direction for the parameter</param>
		/// <returns>Valid parameter for usage with the target database.</returns>
		public IDataParameter CreateParameter(IEntityFieldCore field, IFieldPersistenceInfo persistenceInfo, ParameterDirection direction)
		{
			object value=field.CurrentValue;
			if(value==null)
			{
				value=System.DBNull.Value;
			}

			return new SqlParameter(CreateParameterName(field.Alias), (SqlDbType)persistenceInfo.SourceColumnDbType, persistenceInfo.SourceColumnMaxLength, 
				direction, persistenceInfo.SourceColumnIsNullable, persistenceInfo.SourceColumnPrecision, persistenceInfo.SourceColumnScale, 
				"", DataRowVersion.Current, value);
		}


		/// <summary>
		/// Creates a valid Parameter for the pattern in a LIKE statement. This is a special case, because it shouldn't rely on the type of the
		/// field the LIKE statement is used with but should be the unicode varchar type. 
		/// </summary>
		/// <param name="fieldName">The name of the field the LIKE statement is used with.</param>
		/// <param name="pattern">The pattern to be passed as the value for the parameter. Is used to determine length of the parameter.</param>
		/// <returns>Valid parameter for usage with the target database.</returns>
		public IDataParameter CreateLikeParameter(string fieldName, string pattern)
		{
			return new SqlParameter(CreateParameterName(fieldName), SqlDbType.NVarChar, pattern.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, pattern);
		}


		/// <summary>
		/// Creates a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. This field name is
		/// not padded with an alias if that alias should be created. Effectively, this is the
		/// same as CreateFieldName(field persistence info, fieldname, false);
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		public string CreateFieldName(IFieldPersistenceInfo persistenceInfo, string fieldName)
		{
			return CreateFieldName(persistenceInfo, fieldName, false);
		}


		/// <summary>
		/// Creats a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <param name="appendAlias">When true, the routine should construct an alias construction statement.</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		public string CreateFieldName(IFieldPersistenceInfo persistenceInfo, string fieldName, bool appendAlias)
		{
			StringBuilder name = new StringBuilder(128);

			if(persistenceInfo.SourceCatalogName.Length>0)
			{
				name.Append("[" + persistenceInfo.SourceCatalogName + "].[" + persistenceInfo.SourceSchemaName + 
					"].[" + persistenceInfo.SourceObjectName + "].[" + persistenceInfo.SourceColumnName + "]");
			}
			else
			{
				name.Append("[" + persistenceInfo.SourceSchemaName + "].[" + persistenceInfo.SourceObjectName + "].[" + persistenceInfo.SourceColumnName + "]");
			}

			if(appendAlias)
			{
				if(fieldName!=persistenceInfo.SourceColumnName)
				{
					name.AppendFormat(" AS [{0}]", fieldName);
				}
			}

			return name.ToString();
		}


		/// <summary>
		/// Creates a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. This field name is
		/// not padded with an alias if that alias should be created. Effectively, this is the
		/// same as CreateFieldNameSimple(field persistence info, fieldname, false);. The fieldname is 'simple' in that
		/// it doesn't contain any catalog, schema or table references. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		public string CreateFieldNameSimple(IFieldPersistenceInfo persistenceInfo, string fieldName)
		{
			return CreateFieldNameSimple(persistenceInfo, fieldName, false);
		}


		/// <summary>
		/// Creats a valid field name based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. The fieldname is 'simple' in that
		/// it doesn't contain any catalog, schema or table references. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance used to formulate the fieldname</param>
		/// <param name="fieldName">name of the entity field, to determine if an alias is required</param>
		/// <param name="appendAlias">When true, the routine should construct an alias construction statement.</param>
		/// <returns>Valid field name for usage with the target database.</returns>
		public string CreateFieldNameSimple(IFieldPersistenceInfo persistenceInfo, string fieldName, bool appendAlias)
		{
			if(appendAlias)
			{
				if(fieldName!=persistenceInfo.SourceColumnName)
				{
					return string.Format("[{0}] AS [{1}]", persistenceInfo.SourceColumnName, fieldName);
				}
				else
				{
					return string.Format("[{0}]", persistenceInfo.SourceColumnName);
				}
			}
			else
			{
				return string.Format("[{0}]", persistenceInfo.SourceColumnName);
			}
		}


		/// <summary>
		/// Creates a valid object name (f.e. a name for a table or view) based on the passed in IFieldPersistenceInfo implementation. The fieldname is
		/// ready to use in queries and contains all pre/postfix characters required. 
		/// </summary>
		/// <param name="persistenceInfo">IFieldPersistenceInfo instance which source object info is used to formulate the objectname</param>
		/// <returns>Valid object name</returns>
		public string CreateObjectName(IFieldPersistenceInfo persistenceInfo)
		{
			if(persistenceInfo.SourceCatalogName.Length>0)
			{
				// catalog name present
				return string.Format("[{0}].[{1}].[{2}]", persistenceInfo.SourceCatalogName, persistenceInfo.SourceSchemaName, persistenceInfo.SourceObjectName);
			}
			else
			{
				// no catalog present, do not embed it into the name
				return string.Format("[{0}].[{1}]", persistenceInfo.SourceSchemaName, persistenceInfo.SourceObjectName);
			}
		}

		
		/// <summary>
		/// Converts the passed in comparison operator to a string usable in a query.
		/// </summary>
		/// <param name="operatorToConvert">Operator to convert to string</param>
		/// <returns>The string representation usable in a query of the operator passed in.</returns>
		public string ConvertComparisonOperator(ComparisonOperator operatorToConvert)
		{
			string toReturn = "";

			switch(operatorToConvert)
			{
				case ComparisonOperator.Equal:
					toReturn = "=";
					break;
				case ComparisonOperator.GreaterEqual:
					toReturn = ">=";
					break;
				case ComparisonOperator.GreaterThan:
					toReturn = ">";
					break;
				case ComparisonOperator.LessEqual:
					toReturn = "<=";
					break;
				case ComparisonOperator.LesserThan:
					toReturn = "<";
					break;
				case ComparisonOperator.NotEqual:
					toReturn = "<>";
					break;
				default:
					toReturn = "";
					break;
			}

			return toReturn;
		}


		/// <summary>
		/// Converts the passed in sort operator to a string usable in a query
		/// </summary>
		/// <param name="operatorToConvert">sort operator to convert to a string</param>
		/// <returns>The string representation usable in a query of the operator passed in.</returns>
		public string ConvertSortOperator(SortOperator operatorToConvert)
		{
			string toReturn ="";
			switch(operatorToConvert)
			{
				case SortOperator.Ascending:
					toReturn = "ASC";
					break;
				case SortOperator.Descending:
					toReturn = "DESC";
					break;
			}

			return toReturn;
		}


		/// <summary>
		/// Creates from the passed in EntityField name a name usable for a Parameter. All spaces will be replaced by "_" characters. A "@" is added
		/// as a prefix. 
		/// </summary>
		/// <param name="fieldName">EntityField name to use as base for the parameter name.</param>
		/// <returns>Usable parameter name.</returns>
		private string CreateParameterName(string fieldName)
		{
			return "@" + fieldName.Replace(" ", "_");
		}
	}
}