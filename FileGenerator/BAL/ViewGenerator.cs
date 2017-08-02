using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FileGenerator.Models;
using FileGenerator.Persistance.Factory;

namespace FileGenerator.BAL
{
	public class ViewGenerator : IViewGenerator
	{
		private const string connectionString = "server=gamingdb1\\inst2; database=casino; user id=sa; password=!@#$%A1";
		internal const string ProcBuildViewForTable = "PII.pr_BuildViewForTable @SchemaName, @TableName, @ViewTable";

		private readonly IDbConnectionFactory _connectionFactory;

		public ViewGenerator(IDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public string GenerateSqlView(List<SchemaTableRecord> records)
		{
			//exec the proc
			string fileContent = "";

			using (var conn = _connectionFactory.Open(connectionString))
			{
				var p = new DynamicParameters();
				foreach (SchemaTableRecord record in records)
				{
					p.Add("@SchemaName", record.SchemaName);
					p.Add("@TableName", record.TableName);
					p.Add("@ViewTable", dbType: DbType.String, direction: ParameterDirection.Output);
					conn.Query<int>(ProcBuildViewForTable, p);
					fileContent = p.Get<string>("@ViewTable");
				}
			}

			return fileContent;
		}
	}
}
