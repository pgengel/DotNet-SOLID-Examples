using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using FileGenerator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace FileGenerator.Persistance
{
	class CsvDocumentStorage : IDocumentStorage
	{
		public List<SchemaTableRecord> ReadDocument(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}

			try
			{
				using (CsvReader reader = new CsvReader(new StreamReader(File.OpenRead(fileName))))
				{
					List<SchemaTableRecord> records = reader.GetRecords<SchemaTableRecord>().ToList();
					return records;
				}

			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}
		}

		public bool PersistDocument(View view, string targetLocation)
		{
			try
			{
				using (var sw = new StreamWriter(File.Open(targetLocation + view.FileName, FileMode.Create, FileAccess.Write)))
				{
					sw.Write(view.FileContent);
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
		public List<SchemaTableRecord> ReadDocument(string fileName)
		{

			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}

			try
			{
				using (var reader = new JsonTextReader(new StreamReader(File.OpenRead(fileName))))
				{
					List<SchemaTableRecord> schemaTableNames = new List<SchemaTableRecord>();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.StartObject)
						{	
							// Load object from the stream
							var schemaTable = JObject.Load(reader);
						
							schemaTableNames.Add(new SchemaTableRecord
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

		public bool PersistDocument(View view, string targetLocation)
		{
			try
			{
				using (var sw = new StreamWriter(File.Open(targetLocation + view.FileName, FileMode.Create, FileAccess.Write)))
				{
					sw.Write(view.FileContent);
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