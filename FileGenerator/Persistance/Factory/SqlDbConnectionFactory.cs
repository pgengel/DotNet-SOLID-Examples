using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerator.BAL;

namespace FileGenerator.Persistance.Factory
{
	/// <summary>A SqlConnection factory.</summary>
	public class SqlDbConnectionFactory : IDbConnectionFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns></returns>
		public IDbConnection Open(string connectionString)
		{
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			return (IDbConnection)sqlConnection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns></returns>
		public async Task<IDbConnection> OpenAsync(string connectionString)
		{
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			await sqlConnection.OpenAsync();
			return (IDbConnection)sqlConnection;
		}
	}
}
