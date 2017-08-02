using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerator.Models;

namespace FileGenerator.Persistance
{
	public interface IDocumentStorage
	{
		List<SchemaTableRecord> ReadDocument(string fileName);
		bool PersistDocument(View view, string targetLocation);
	}
}
