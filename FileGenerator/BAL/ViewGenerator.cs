using System;
using System.Collections.Generic;
using System.Configuration;
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
	  private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
	  internal const string ProcBuildViewForTable = "PII.pr_BuildViewForTable @SchemaName, @TableName, @ViewName, @ViewTable";

		private readonly IDbConnectionFactory _connectionFactory;

		public ViewGenerator(IDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public List<View> GenerateSqlViews(List<Record> records)
		{
			//exec the proc
			List<View> fileContents = new List<View>();

			try
			{
				using (var conn = _connectionFactory.Open(ConnectionString))
				{
					var p = new DynamicParameters();
					foreach (Record record in records)
					{
						p.Add("@SchemaName", record.SchemaName);
						p.Add("@TableName", record.TableName);
						p.Add("@ViewName", dbType: DbType.String, direction: ParameterDirection.Output);
						p.Add("@ViewTable", dbType: DbType.String, direction: ParameterDirection.Output);
						conn.Query<int>(ProcBuildViewForTable, p);
						fileContents.Add(new View
						{
							FileContent = p.Get<string>("@ViewTable"),
							FileName = p.Get<string>("@ViewName")
						});
					}
				}
				return fileContents;
			}
			catch (Exception e)
			{
				throw new AccessViolationException();
			}
		}
	}
}
