using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SoccerStats
{
	class Program
	{
		static void Main(string[] args)


		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo directory = new DirectoryInfo(currentDirectory);

			//var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
			//var fileContents = ReadSoccerResults(fileName);
			//fileName = Path.Combine(directory.FullName, "players.json");
			//var players = DeserializePlayers(fileName);

			var fileName = Path.Combine(directory.FullName, "dawson89.json");
			var badges = DeserializeBadges(fileName);

			//var topTenPlayers = GetTopTenPlayers(players);
			//foreach (var player in topTenPlayers)
			//{
			//	Console.WriteLine("Name: " + player.FirstName + " PPG: " + player.PointsPerGame);
			//}
			//fileName = Path.Combine(directory.FullName, "bottomten.json");
			//SerializeBadgeToFile(bottomTenBadges, fileName);

			// var bottomTenPlayers = GetBottomTenPlayers(players);
			// foreach (var player in bottomTenPlayers)
			//{
			//	Console.WriteLine("Name: " + player.FirstName + " PPG: " + player.PointsPerGame);
			//}
			// fileName = Path.Combine(directory.FullName, "bottomten.json");
			// SerializePlayerToFile(bottomTenPlayers, fileName);

			var bottomTenBadges = GetBottomTenBadges(badges);
			//foreach (var badge in bottomTenBadges)
			//{
			//	Console.WriteLine("Badge Name: " + badge.Name + " ID Number: " + badge.Id + " " + badge.EarnedDate.ToShortDateString());
			//}
			fileName = Path.Combine(directory.FullName, "recentbadges.json");
			SerializeBadgeToFile(bottomTenBadges, fileName);

			var bottomTwentyBadges = GetBottomTwentyBadges(badges);
			//foreach (var badge in bottomTwentyBadges)
			//{
			//	Console.WriteLine("Badge Name: " + badge.Name + " ID Number: " + badge.Id + " " + badge.EarnedDate.ToShortDateString());
			//}
			fileName = Path.Combine(directory.FullName, "recenttwentybadges.json");
			SerializeBadgeToFile(bottomTwentyBadges, fileName);

			string yes = "Y";
			string no = "N";
			Console.WriteLine("Would you like to view the ten most recent badges earned? (Hint: Enter Y yes or N no)");
			var Answer = Console.ReadLine();
			while (Answer == yes)
			{
				//Console.WriteLine("Options");

				//forin (var badge in bottomTenBadges)
				for (int i = 0; i < bottomTenBadges.Count; i++)

				{
					var displayNumber = i + 1;
					var badge = bottomTenBadges[i];
					Console.WriteLine(displayNumber.ToString() + " Badge ID Number: " + badge.Id + " Badge Name: " + badge.Name + " Date Earned: " + badge.EarnedDate.ToShortDateString() + " Favorite Class: " + badge.FavoriteClass);


				}

				Console.WriteLine("Which badge would you like to edit/update/delete? Please enter a number 1-10");
		
				var answerNo = Console.ReadLine();
				var indexAnswer = int.Parse(answerNo);
				var goFind = indexAnswer - 1;


				Console.WriteLine("Badge ID Number: " + bottomTenBadges[goFind].Id + " Favorite Badge: " + bottomTenBadges[goFind].FavoriteClass);
				Console.ReadLine();

				//foreach (var badge in bottomTenBadges)
				//{

				//	Console.WriteLine(" Badge ID Number: " + badge.Id + " Badge Name: " + badge.Name + " Date Earned: " + badge.EarnedDate.ToShortDateString() + " Favorite Class: " + badge.FavoriteClass);
				//}
				//break;
			}
			//	Console.WriteLine("Would you like to edit an entry?");
			//	Answer = Console.ReadLine();
			//	if (Answer == yes)
			//	{
			//		Console.WriteLine("which entry would you like to edit? Hint: 1-10");
			//		var thisIsHard = Console.ReadLine();
			//		Console.WriteLine("You selected " + thisIsHard);
			//		Console.WriteLine("Did you like this class? Hint: 1 = yes and 2 = no");
			//		var thisIsHardAnswer = Console.ReadLine();

			//	}
			//	else
			//	{
			//		break;
			//	}
			//};

			//Console.WriteLine("Select which entry you would like to edit? Hint: 1-10");
			//Console.ReadLine();

			//while (Answer == no)
			//{
			//	Console.WriteLine("Then piss off");
			//	break;

			//}



			//while (Answer != no & Answer != yes)
			//{
			//	Console.WriteLine("Then piss off");
			//	break;
			//}


		}

		public static List<Badge> DeserializeBadges(string fileName)
		{
			var badges = new List<Badge>();

			var serializer = new JsonSerializer();
			using (var reader = new StreamReader(fileName))
			using (var jsonReader = new JsonTextReader(reader))
			{
				badges = serializer.Deserialize<List<Badge>>(jsonReader);

			}
			return badges;
		}

		public static string ReadFile(string fileName)
		{
			using (var reader = new StreamReader(fileName))
			{
				return reader.ReadToEnd();
			}
		}

		// Returns the ten most recent Badges Earned
		public static List<Badge> GetBottomTenBadges(List<Badge> badges)
		{
			var bottomTenBadges = new List<Badge>();
			badges.Sort(new RecentBadge());
			int counter = 0;
			foreach (var badge in badges)
			{
				bottomTenBadges.Add(badge);
				counter++;
				if (counter == 10)
					break;
			}
			return bottomTenBadges;
		}

		// Returns the twenty most recent Badges Earned
		public static List<Badge> GetBottomTwentyBadges(List<Badge> badges)
		{
			var bottomTwentyBadges = new List<Badge>();
			badges.Sort(new RecentBadge());
			int counter = 0;
			foreach (var badge in badges)
			{
				bottomTwentyBadges.Add(badge);
				counter++;
				if (counter == 20)
					break;
			}
			return bottomTwentyBadges;
		}

		//public static List<GameResult> ReadSoccerResults(string fileName)
		//{
		//	var soccerResults = new List<GameResult>();
		//	using (var reader = new StreamReader(fileName))
		//	{
		//		string line = "";
		//		reader.ReadLine();
		//		while ((line = reader.ReadLine()) != null)
		//		{
		//			var gameResult = new GameResult();
		//			string[] values = line.Split(',');

		//			DateTime gameDate;
		//			if (DateTime.TryParse(values[0], out gameDate))
		//			{
		//				gameResult.GameDate = gameDate;
		//			}
		//			gameResult.TeamName = values[1];
		//			HomeOrAway homeOrAway;
		//			if (Enum.TryParse(values[2], out homeOrAway))
		//			{
		//				gameResult.HomeOrAway = homeOrAway;
		//			}
		//			int parseInt;
		//			if (int.TryParse(values[3], out parseInt))
		//			{
		//				gameResult.Goals = parseInt;
		//			}
		//			if (int.TryParse(values[4], out parseInt))
		//			{
		//				gameResult.GoalAttempts = parseInt;
		//			}
		//			if (int.TryParse(values[5], out parseInt))
		//			{
		//				gameResult.ShotsOnGoal = parseInt;
		//			}
		//			if (int.TryParse(values[6], out parseInt))
		//			{
		//				gameResult.ShotsOffGoal = parseInt;
		//			}

		//			double possessionPercent;
		//			if (double.TryParse(values[7], out possessionPercent))
		//			{
		//				gameResult.PosessionPercent = possessionPercent;
		//			}
		//			soccerResults.Add(gameResult);
		//		}
		//	}
		//	return soccerResults;
		//}
		//public static List<Player> DeserializePlayers(string fileName)
		//{
		//	var players = new List<Player>();
		//	var serializer = new JsonSerializer();
		//	using (var reader = new StreamReader(fileName))
		//	using (var jsonReader = new JsonTextReader(reader))
		//	{
		//		players = serializer.Deserialize<List<Player>>(jsonReader);
		//	}


		//		return players;
		//}
		//public static List<Player> GetTopTenPlayers(List<Player> players)
		//{
		//	var topTenPlayers = new List<Player>();
		//	players.Sort(new PlayerComparer());
		//	int counter = 0;
		//	foreach (var player in players)
		//	{
		//		topTenPlayers.Add(player);
		//		counter++;
		//		if (counter == 10)
		//			break;
		//	}
		//	return topTenPlayers;
		//}
		//public static List<Badge> GetBottomTenBadges(List<Badge> badges)
		//{
		//	var bottomTenBadgess = new List<Badge>();
		//	badges.Sort(new PlayComp());
		//	int counter = 0;
		//	foreach (var badge in badges)
		//	{
		//		bottomTenBadges.Add(badge);
		//		counter++;
		//		if (counter == 10)
		//			break;
		//	}
		//	return bottomTenBadges;
		//}

		public static void SerializeBadgeToFile(List<Badge> badges, string fileName)
		{

			var serializer = new JsonSerializer();
			using (var writer = new StreamWriter(fileName))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, badges);
			}


		}
	}
}
