using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			fileName = Path.Combine(directory.FullName, "recentbadges.json");
			SerializeBadgeToFile(bottomTenBadges, fileName);

			// Not being used for this project
			// var bottomTwentyBadges = GetBottomTwentyBadges(badges);

			// fileName = Path.Combine(directory.FullName, "recenttwentybadges.json");
			// SerializeBadgeToFile(bottomTwentyBadges, fileName);

			string yes = "Y";
			string no = "N";
			Console.WriteLine("Would you like to view the ten most recent badges earned? (Enter Y or N )");
			var Answer = Console.ReadLine();

			while (Answer == yes)
			{
				for (int i = 0; i < bottomTenBadges.Count; i++)
					{
						var displayNumber = i + 1;
						var badge = bottomTenBadges[i];
						Console.WriteLine(displayNumber.ToString() + " Badge ID Number: " + badge.Id + " Badge Name: " + badge.Name + " Date Earned: " + badge.EarnedDate.ToShortDateString() + " Favorite: " + badge.FavoriteClass);
					}

				Console.WriteLine("To add, update, or delete a Badges Favorite information enter number of the line you would like to select? (If your done enter E to exit)");

				var answerNo = Console.ReadLine();
				var indexAnswer = int.Parse(answerNo);
				var goFind = indexAnswer - 1;

				Console.Write("Badge ID Number: " + bottomTenBadges[goFind].Id + " Badge Name: " + bottomTenBadges[goFind].Name + " Favorite: " + bottomTenBadges[goFind].FavoriteClass);
				var wantsToQuit = "E";
				var wantsToDo = Console.ReadLine();

					if (wantsToDo == wantsToQuit)
					{
						break;
					}
					else
					{
						bottomTenBadges[goFind].FavoriteClass = wantsToDo;
					}

			}

			while (Answer == no)
			{
				Console.WriteLine("Then piss off");
				break;
			}

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
		// Not being used right now
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
