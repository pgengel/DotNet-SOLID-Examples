using System.Collections.Generic;
using FileGenerator.Models;

namespace FileGenerator.BAL
{
	public interface IViewGenerator
	{
		string GenerateSqlView(List<SchemaTableRecord> records);
	}
}
