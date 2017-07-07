using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNetSolidPrinciples.OpenClose
{

	public interface IDocumentSerialiser
	{
		string Serialise(Document doc);
	}
	public class JsonSerialiser : IDocumentSerialiser
	{
		public string Serialise(Document doc)
		{
			return JsonConvert.SerializeObject(doc);
		}
	}
}
