using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RecentBadges
{
	class Program
	{
		static void Main(string[] args)


		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo directory = new DirectoryInfo(currentDirectory);

			var fileName = Path.Combine(directory.FullName, "dawson89.json");
			var badges = DeserializeBadges(fileName);
			var bottomTenBadges = GetBottomTenBadges(badges);

			// fileName = Path.Combine(directory.FullName, "recentbadges.json");
			// SerializeBadgeToFile(bottomTenBadges, fileName);

			string yes = "Y";
			string no = "N";

			Console.Write("Would you like to view Dawson's recently earned badges?  Hint: Y or N ");
			string Answer = Console.ReadLine();
			Answer = Answer.ToUpper();
			if (Answer != yes)
			{
				Console.WriteLine("Whoops something went wrong try typing Y");
				Answer = Console.ReadLine();
				Answer = Answer.ToUpper();
			}


				while (Answer == yes)
				{
					for (int i = 0; i < bottomTenBadges.Count; i++)
					{
						var displayNumber = i + 1;
						var badge = bottomTenBadges[i];
						Console.WriteLine(displayNumber.ToString() + " ID: " + badge.Id + " Name: " + badge.Name + " Date Earned: " + badge.EarnedDate.ToShortDateString() + " Favorite: " + badge.FavoriteClass);
					}

					Console.Write("To add/update/delete favorite information enter the number of the line you would like to select? ");

					var answerNo = Console.ReadLine();
					var indexAnswer = int.Parse(answerNo);
					var goFind = indexAnswer - 1;

					Console.Write("Badge ID Number: " + bottomTenBadges[goFind].Id + " Badge Name: " + bottomTenBadges[goFind].Name + " Favorite: " + bottomTenBadges[goFind].FavoriteClass);
					bottomTenBadges[goFind].FavoriteClass = Console.ReadLine();
					Console.WriteLine("Would like to make another change? ");
					var AnswerTwo = Console.ReadLine();
					if (AnswerTwo == "N")
					{
						Console.Write("How many badges do you want to include in your export? ");
						var ExportNumber = Console.ReadLine();
						var ExportAnswer = int.Parse(ExportNumber);
						Answer = "no";
					}
					else
					{
						continue;
					}




				
			}
			while (Answer == no)
			{
				Console.WriteLine("then piss off");
				break;
			}

		//	Exports a new json file contain all changes that were made
			fileName = Path.Combine(directory.FullName, "recenttenbadges.json");
			SerializeBadgeToFile(bottomTenBadges, fileName);
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
			Console.Write("How many badges do you want to include in your export? ");
			var ExportNumber = Console.ReadLine();
			var ExportAnswer = int.Parse(ExportNumber);

			var bottomTenBadges = new List<Badge>();
			badges.Sort(new RecentBadge());
			int counter = 0;
			foreach (var badge in badges)
			{
				bottomTenBadges.Add(badge);
				counter++;
				if (counter == (ExportAnswer))
					break;
			}
			return bottomTenBadges;
		}

		//public static List<Badge> GetBottomTwentyBadges(List<Badge> badges)
		//{
		//	var bottomTwentyBadges = new List<Badge>();
		//	badges.Sort(new RecentBadge());
		//	int counter = 0;
		//	foreach (var badge in badges)
		//	{
		//		bottomTwentyBadges.Add(badge);
		//		counter++;
		//		if (counter == 20)
		//			break;
		//	}
		//	return bottomTwentyBadges;
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
