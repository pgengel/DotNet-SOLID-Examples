using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;
using FileGenerator.Models;
using FileGenerator.Persistance.Factory;

namespace FileGenerator.BAL
{
	public class ViewGenerator : IViewGenerator
	{
	  private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
	  internal const string ProcBuildViewForTable = "PII.pr_BuildViewForObject @SchemaName, @ObjectName, @FileName, @FileContent";

		private readonly IDbConnectionFactory _connectionFactory;

		public ViewGenerator(IDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public List<SqlView> GenerateSqlViews(List<Record> records)
		{
			//exec the proc
			List<SqlView> fileContents = new List<SqlView>();

			try
			{
				using (var conn = _connectionFactory.Open(ConnectionString))
				{
					var p = new DynamicParameters();
					foreach (Record record in records)
					{
						p.Add("@SchemaName", record.SchemaName);
						p.Add("@ObjectName", record.TableName);
						p.Add("@FileName", dbType: DbType.String, direction: ParameterDirection.Output);
						p.Add("@FileContent", dbType: DbType.String, direction: ParameterDirection.Output);
						conn.Query<int>(ProcBuildViewForTable, p);
						fileContents.Add(new SqlView
						{
							FileContent = p.Get<string>("@FileContent"),
							FileName = p.Get<string>("@FileName")
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
