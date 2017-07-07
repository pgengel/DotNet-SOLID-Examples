using System;
using System.IO;

namespace DotNetSolidPrinciples.SingleResponsibility
{
	public class DocumentStorage
	{
		public string GetData(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw  new FileNotFoundException();
			}

			using (var stream = File.OpenRead(fileName))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public void PersistDocument(string serialiseDoc, string targetFileName)
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
}