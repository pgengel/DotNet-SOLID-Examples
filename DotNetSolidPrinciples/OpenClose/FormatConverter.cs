using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSolidPrinciples.OpenClose
{
	public class FormatConverter
	{

		public readonly DocumentStorage _documentStorage;
		public readonly InputParser InputParser;
		public readonly JsonSerialiser JsonSerialiser;
		public FormatConverter()
		{

		}

		public bool ConvertFormat(string sourceFileName, string targetFileName)
		{
			string input;
			try
			{
				input = _documentStorage.GetData(sourceFileName);
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e);
				return false;
			}

			var doc = InputParser.ParseInput(input);
			var serialiseDoc = JsonSerialiser.Serialise(doc);

			try
			{
				_documentStorage.PersistDocument(serialiseDoc, targetFileName);
			}
			catch (AccessViolationException e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}
	}
}
