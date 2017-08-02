using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DotNetSolidPrinciples.OpenClose;
using FileGenerator.Models;
using FileGenerator.Persistance;
using FileGenerator.Persistance.Factory;

namespace FileGenerator.BAL
{
	public class FileGenerator
	{
		private const string connectionString = "server=gamingdb1\\inst2; database=casino; user id=sa; password=!@#$%A1";
		internal const string ProcBuildViewForTable = "PII.pr_BuildViewForTable @SchemaName, @TableName, @ViewTable";
		private readonly IDocumentStorage _documentStorage;
		private readonly IDbConnectionFactory _connectionFactory;
		
		public FileGenerator(IDocumentStorage documentStorage, IDbConnectionFactory connectionFactory)
		{
			_documentStorage = documentStorage;
			_connectionFactory = connectionFactory;
		}
		public bool GenerateFile(string sourceFileName, string targetFileName)
		{
			List<SchemaTableRecord> records;

			try
			{
				records = _documentStorage.ReadDocument(sourceFileName);
			}
			catch (FileNotFoundException)
			{
				return false;
			}

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

			try
			{
				
				_documentStorage.PersistDocument(fileContent, targetFileName);
			}
			catch (AccessViolationException)
			{
				return false;
			}

			return true;
		}
	}

}
