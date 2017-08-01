using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerator.Models;

namespace FileGenerator.Persistance
{
	interface IDocumentStorage
	{
		List<SchemaTableNameDocument> ReadDocument(string fileName);
		void PersistDocument(string fileContent, string targetFileName);
	}
}
