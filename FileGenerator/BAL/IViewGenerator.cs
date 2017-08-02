using System.Collections.Generic;
using FileGenerator.Models;

namespace FileGenerator.BAL
{
	public interface IViewGenerator
	{
		List<View> GenerateSqlView(List<SchemaTableRecord> records);
	}
}
