using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{
	class Program
	{
		static void Main(string[] args)
		

		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo directory = new DirectoryInfo(currentDirectory);
			var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
			var fileContents = ReadSoccerResults(fileName);
				
		}

		public static string ReadFile(string filename)
		{
			using (var reader = new StreamReader(filename))
			{
				return reader.ReadToEnd();
			}
		}

		public static List<string[]> ReadSoccerResults(string fileName)
		{
			var soccerResults = new List<string[]>();
			using (var reader = new StreamReader(fileName))
			{
				string line = "";
				while ((line = reader.ReadLine()) != null)
				{
					string[] values = line.Split(',');
					soccerResults.Add(values);
				}
			}
			return soccerResults;
		}
	}
}
