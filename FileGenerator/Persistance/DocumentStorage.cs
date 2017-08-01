using System;
using System.Collections.Generic;
using System.IO;
using FileGenerator.Models;

namespace FileGenerator.Persistance
{
	public class DocumentStorage : IDocumentStorage
	{
		public List<SchemaTableNameDocument> ReadDocument(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}

			using (var stream = File.OpenRead(fileName))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public void PersistDocument(string fileContent, string targetFileName)
		{
			try
			{
				using (var stream = File.Open(targetFileName, FileMode.Create, FileAccess.Write))
				using (var sw = new StreamWriter(stream))
				{
					sw.Write(fileContent);
					sw.Close();
				}
			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}
		}
	}
}