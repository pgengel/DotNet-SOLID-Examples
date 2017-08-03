using System.Collections.Generic;
using FileGenerator.BAL;
using FileGenerator.Persistance;
using FileGenerator.Persistance.Factory;
using System.Configuration;

namespace FileGenerator
{
  class Program
  {
    static void Main(string[] args)
    {

      var piiList = new List<string>(ConfigurationManager.AppSettings["pii_exclusions"].Split(new char[] { ';' }));
      //  BAL.FileGenerator fileGenerator = new BAL.FileGenerator(new CsvDocumentStorage(), new ViewGenerator(new SqlDbConnectionFactory()));
      //fileGenerator.GenerateFile("", "");
    }
  }
}
