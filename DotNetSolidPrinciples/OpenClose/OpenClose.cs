﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DotNetSolidPrinciples.OpenClose
{
	class OpenClose : IDemo
	{
		public void Run(string[] args)
		{
			var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\input.xml");
			var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\output.json");

			var formatConverter = new FormatConverter();
			if (!formatConverter.ConvertFormat(sourceFileName, targetFileName))
			{
				Console.Write("Conversion failed");
				Console.ReadKey();
			}

			var input = GetInput(sourceFileName);
			var doc = GetDocument(input);
			var serialiseDoc = SerialiseDocument(doc);
			PersistDocument(serialiseDoc, targetFileName);
		}

		private static void PersistDocument(string serialiseDoc, string targetFileName)
		{
			throw new NotImplementedException();
		}

		private static string SerialiseDocument(Document doc)
		{
			throw new NotImplementedException();
		}

		private static Document GetDocument(string input)
		{
			throw new NotImplementedException();
		}

		private static string GetInput(string sourceFileName)
		{
			throw new NotImplementedException();
		}
	}
}
