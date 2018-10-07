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
			var fileContents = ReadFile(fileName);
			string[] fileLines = fileContents.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			foreach(var line in fileLines)
			{
				Console.WriteLine(line);
			}
				
		}

		public static string ReadFile(string filename)
		{
			using (var reader = new StreamReader(filename))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
