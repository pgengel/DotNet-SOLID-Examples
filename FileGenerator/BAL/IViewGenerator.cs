using System.Collections.Generic;
using FileGenerator.Models;

namespace FileGenerator.BAL
{
	public interface IViewGenerator
	{
		List<SqlView> GenerateSqlViews(List<Record> records);
	}
}
