using System.Collections.Generic;
using FileGenerator.Models;

namespace FileGenerator.BAL
{
	public interface IViewGenerator
	{
		List<View> GenerateSqlViews(List<Record> records);
	}
}
