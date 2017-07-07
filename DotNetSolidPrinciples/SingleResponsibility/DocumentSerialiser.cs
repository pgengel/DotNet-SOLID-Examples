using Newtonsoft.Json;

namespace DotNetSolidPrinciples.SingleResponsibility
{
	public class DocumentSerialiser
	{
		public string Serialise(Document doc)
		{
			return JsonConvert.SerializeObject(doc);
		}
	}
}