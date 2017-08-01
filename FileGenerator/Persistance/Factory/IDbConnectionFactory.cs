using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerator.Persistance.Factory
{
	/// <summary>
	///   Defines the interface for a SQL Server database connection factory.
	/// </summary>
	public interface IDbConnectionFactory
	{
		/// <summary>
		///   Creates and opens a database connection, given a connection string.
		/// </summary>
		/// <param name="connectionString">The connection string provifing details about the connection.</param>
		/// <returns>The opened connection.</returns>
		IDbConnection Open(string connectionString);

		/// <summary>
		///   Creates and opens a database connection, given a connection string.
		/// </summary>
		/// <param name="connectionString">The connection string provifing details about the connection.</param>
		/// <returns>The opened connection.</returns>
		Task<IDbConnection> OpenAsync(string connectionString);
	}
}
