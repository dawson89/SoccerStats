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

		public static List<GameResult> ReadSoccerResults(string fileName)
		{
			var soccerResults = new List<GameResult>();
			using (var reader = new StreamReader(fileName))
			{
				string line = "";
				reader.ReadLine();
				while ((line = reader.ReadLine()) != null)
				{
					var gameResult = new GameResult();
					string[] values = line.Split(',');
	
					DateTime gameDate;
					if (DateTime.TryParse(values[0], out gameDate))
					{
						gameResult.GameDate = gameDate;
					}
					gameResult.TeamName = values[1];
					HomeOrAway homeOrAway;
					if (Enum.TryParse(values[2], out homeOrAway))
					{
						gameResult.HomeOrAway = homeOrAway;
					}
					soccerResults.Add(gameResult);
				}
			}
			return soccerResults;
		}
	}
}
