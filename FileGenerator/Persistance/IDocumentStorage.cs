using System.Collections.Generic;
using FileGenerator.Models;

namespace FileGenerator.Persistance
{
	public interface IDocumentStorage
	{
		List<Record> ReadDocument(string fileName);
		bool PersistDocument(SqlView sqlView, string targetLocation);
	}
}
