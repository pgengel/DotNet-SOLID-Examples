using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerator.Models;

namespace FileGenerator.Persistance
{
	public interface IDocumentStorage
	{
		List<Record> ReadDocument(string fileName);
		bool PersistDocument(SqlView sqlView, string targetLocation);
	}
}
