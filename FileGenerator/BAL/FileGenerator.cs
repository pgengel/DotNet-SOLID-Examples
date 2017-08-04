using System;
using System.Collections.Generic;
using FileGenerator.Models;
using FileGenerator.Persistance;


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

				List<SqlView> views = _viewGenerator.GenerateSqlViews(records);

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
