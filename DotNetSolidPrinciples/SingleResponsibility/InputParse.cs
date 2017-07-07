using System;
using System.Xml.Linq;

namespace DotNetSolidPrinciples.SingleResponsibility
{
	public class InputParse
	{
		public Document ParseInput(string input)
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
	}
}