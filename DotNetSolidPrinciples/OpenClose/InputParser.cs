using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace DotNetSolidPrinciples.OpenClose
{

	public class InputParser //Meyer
	{
		public virtual Document ParseInput(string input)
		{
			Document doc;
			try
			{
				var xdoc = XDocument.Parse(input);
				doc = new Document();
				doc.Title = xdoc.Root.Element("title").Value;
				doc.Text = xdoc.Root.Element("text").Value;
			}
			catch (Exception)
			{
				throw new InvalidInputFormatException();
			}

			return doc;
		}

		public class JsonInputParser : InputParser
		{
			public override Document ParseInput(string input)
			{
				return JsonConvert.DeserializeObject<Document>(input);
			}
		}
	}

	
}
