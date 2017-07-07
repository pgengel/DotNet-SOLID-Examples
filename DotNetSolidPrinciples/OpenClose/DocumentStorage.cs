using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSolidPrinciples.OpenClose
{
	public abstract class DocumentStorage
	{
		public abstract string GetData(string fileName);
		public abstract void PersistDocument(string serialiseDoc, string targetFileName);
	}

	public class FileDocumentStorage : DocumentStorage
	{
		public override string GetData(string fileName)
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

		public override void PersistDocument(string serialiseDoc, string targetFileName)
		{
			try
			{
				using (var stream = File.Open(targetFileName, FileMode.Create, FileAccess.Write))
				using (var sw = new StreamWriter(stream))
				{
					sw.Write(serialiseDoc);
					sw.Close();
				}
			}
			catch (Exception)
			{
				throw new AccessViolationException();
			}
		}
	}

	public class BlobDocumentStorage : DocumentStorage
	{
		public override string GetData(string fileName)
		{
			throw new NotImplementedException();//just for show, but not allowed according to LSP
		}

		public override void PersistDocument(string serialiseDoc, string targetFileName)
		{
			throw new NotImplementedException();//just for show, but not allowed according to LSP
		}
	}
}
