using System;
using System.IO;

namespace DotNetSolidPrinciples.SingleResponsibility
{
	public class FormatConverter
	{

		public readonly DocumentStorage _documentStorage;
		public readonly InputParse _inputParse;
		public readonly DocumentSerialiser _documentSerialiser;
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

			var doc = _inputParse.ParseInput(input);
			var serialiseDoc = _documentSerialiser.Serialise(doc);

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