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
	/// Implementation of the ActionQuery class. 
	/// </summary>
	[Serializable]
	public class ActionQuery : Query, IActionQuery
	{
		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="commandToUse">Command to use</param>
		public ActionQuery(IDbCommand commandToUse):base(commandToUse)
		{
		}


		/// <summary>
		/// CTor
		/// </summary>
		/// <param name="connectionToUse">Connection object to use</param>
		/// <param name="commandToUse">Command to use</param>
		public ActionQuery(IDbConnection connectionToUse, IDbCommand commandToUse)
				:base(connectionToUse, commandToUse)
		{
		}


		/// <summary>
		/// Executes the query contained by the IQuery instance. If there was nothing to execute, 0 is returned.
		/// </summary>
		/// <returns>The number of rows affected (if applicable), otherwise 0.</returns>
		/// <exception cref="System.InvalidOperationException">When there is no command object inside the query object, 
		/// or no connection object inside the query object</exception>
		public int Execute()
		{
			if(base.Command==null)
			{
				throw new InvalidOperationException("No Command present. Nothing to execute.");
			}

			if(base.Connection==null)
			{
				throw new InvalidOperationException("No Connection present. Cannot execute command.");
			}

			// check if there is something to execute
			if(base.Command.CommandText.Length==0)
			{
				// no. return 'succeeded' (1 row affected at least)
				return 1;
			}

			bool wasConnectionClosed = (base.Connection.State != ConnectionState.Open);

			// execute the query
			try
			{
				if(wasConnectionClosed)
				{
					base.Connection.Open();
				}

				return base.Command.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				// throw a catchable exception with detailed query information.
				throw new ORMQueryExecutionException(
						String.Format("An exception was caught during the execution of an action query: {0}. Check InnerException, QueryExecuted and Parameters of this exception to examine the cause of this exception.", ex.Message), 
						base.ToString(), base.Parameters, ex);
			}
			finally
			{
				if(wasConnectionClosed)
				{
					base.Connection.Close();
				}
			}
		}
	}
}