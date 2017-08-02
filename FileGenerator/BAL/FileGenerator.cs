using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DotNetSolidPrinciples.OpenClose;
using FileGenerator.Models;
using FileGenerator.Persistance;
using FileGenerator.Persistance.Factory;

namespace FileGenerator.BAL
{
	public class FileGenerator
	{
		private readonly IDocumentStorage _documentStorage;
		private readonly IViewGenerator _viewGenerator;

		public FileGenerator(IDocumentStorage documentStorage, IViewGenerator viewGenerator)
		{
			_documentStorage = documentStorage;
			_viewGenerator = viewGenerator;
		}
		public bool GenerateFile(string sourceFileName, string targetFileName)
		{
			List<SchemaTableRecord> records;

			try
			{
				records = _documentStorage.ReadDocument(sourceFileName);
			}
			catch (FileNotFoundException)
			{
				return false;
			}

			string fileContent = _viewGenerator.GenerateSqlView(records);

			try
			{

				_documentStorage.PersistDocument(fileContent, targetFileName);
			}
			catch (AccessViolationException)
			{
				return false;
			}

			return true;
		}

	}

}
