using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FileGenerator.Models;
using FileGenerator.Persistance;
using FileGenerator.Persistance.Factory;

namespace FileGenerator.BAL
{
	public class FileGenerator
	{
		private const string connectionString = "server=gamingdb1\\inst2; database=casino; user id=sa; password=!@#$%A1";
		internal const string ProcBuildViewForTable = "PII.pr_BuildViewForTable @SchemaName, @TableName, @ViewTable";
		private readonly IDbConnectionFactory _connectionFactory;

		private readonly DocumentStorage _documentStorage;

		public FileGenerator(DocumentStorage documentStorage, IDbConnectionFactory connectionFactory)
		{
			_documentStorage = documentStorage;
			_connectionFactory = connectionFactory;
		}
		public bool GenerateFile(string sourceFileName, string targetFileName)
		{
			List<SchemaTableNameDocument> schemaTableName = new List<SchemaTableNameDocument>();
			string input;
			try
			{
				input = _documentStorage.ReadDocument(sourceFileName);
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e);
				return false;
			}

			//exec the proc
			string fileContent = "";

			using (var conn = _connectionFactory.Open(connectionString))
			{
				var p = new DynamicParameters();
				p.Add("@SchemaName", schemaTableName.SchemaName);
				p.Add("@TableName", schemaTableName.TableName);
				p.Add("@ViewTable", dbType: DbType.String, direction: ParameterDirection.Output);
				conn.Query<int>(ProcBuildViewForTable, p);
				fileContent = p.Get<string>("@ViewTable");
			}

			try
			{
				
				_documentStorage.PersistDocument(fileContent, targetFileName);
			}
			catch (AccessViolationException e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}
	}

}
