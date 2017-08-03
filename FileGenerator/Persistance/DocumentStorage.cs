using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using FileGenerator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileGenerator.Persistance
{
	class CsvDocumentStorage : IDocumentStorage
	{
		public List<Record> ReadDocument(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}

			try
			{
				using (CsvReader reader = new CsvReader(new StreamReader(File.OpenRead(fileName))))
				{
					List<Record> records = reader.GetRecords<Record>().ToList();
					return records;
				}

			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}
		}

		public bool PersistDocument(SqlView sqlView, string targetLocation)
		{
			try
			{
				using (var sw = new StreamWriter(File.Open(targetLocation + sqlView.FileName, FileMode.Create, FileAccess.Write)))
				{
					sw.Write(sqlView.FileContent);
					sw.Close();
				}
				return true;
			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}
		}
	}

	class JsonDocumentStorage : IDocumentStorage
	{
		public List<Record> ReadDocument(string fileName)
		{

			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}

			try
			{
				using (var reader = new JsonTextReader(new StreamReader(File.OpenRead(fileName))))
				{
					List<Record> schemaTableNames = new List<Record>();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.StartObject)
						{	
							// Load object from the stream
							var schemaTable = JObject.Load(reader);
						
							schemaTableNames.Add(new Record
							{
								TableName = schemaTable.Values<string>("schema").ToString(),
								SchemaName = schemaTable.Values<string>("table").ToString()
							});

							return schemaTableNames;
						}
					}
					return schemaTableNames;
				}
			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}

		}

		public bool PersistDocument(SqlView sqlView, string targetLocation)
		{
			try
			{
				using (var sw = new StreamWriter(File.Open(targetLocation + sqlView.FileName, FileMode.Create, FileAccess.Write)))
				{
					sw.Write(sqlView.FileContent);
					sw.Close();
				}
				return true;
			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}
		}
	}
}