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
		public bool GenerateFile(string sourceLocationFileName, string targetLocation)
		{
			try
			{
				var records = _documentStorage.ReadDocument(sourceLocationFileName);

				List<View> views = _viewGenerator.GenerateSqlViews(records);

				foreach (var view in views)
				{
					_documentStorage.PersistDocument(view, targetLocation);
				}

				return true;
			}

			catch (Exception)
			{
				return false;
			}
		}
	}
}
