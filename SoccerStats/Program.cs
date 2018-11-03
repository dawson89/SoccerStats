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
		//public static int NumberRequest { get; private set; }

		static void Main(string[] args)

		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo directory = new DirectoryInfo(currentDirectory);

			var fileName = Path.Combine(directory.FullName, "dawson89.json");
			var badges = DeserializeBadges(fileName);
			var badgeRequest = GetRecentBadges(badges);

			var customBadgeExport = ExportBadges(badges);
			fileName = Path.Combine(directory.FullName, "bottomten.json");
			SerializeBadgeToFile(customBadgeExport, fileName);

			string yes = "Y";
			var Answer = yes;


			Console.WriteLine("Would you like to view Dawson's most recent badges earned? (Enter Y or N )");
			var start = Console.ReadLine();

			if (start == "no")
			{
				Console.WriteLine("Then piss off");
				return;
			}

			while (Answer == yes)
			{
				// Loops through all
				for (int i = 0; i < badgeRequest.Count; i++)
				{
					var displayNumber = i + 1;
					var badge = badgeRequest[i];
					Console.WriteLine(displayNumber.ToString() + " Badge ID Number: " + badge.Id + " Badge Name: " + badge.Name + " Date Earned: " + badge.EarnedDate.ToShortDateString() + " Favorite: " + badge.FavoriteClass);
				}

				Console.WriteLine("Enter the number of the line you would like to add/edit/delete (Hint: Q to quit and A list all lines");
				var continueApp = Console.ReadLine();
				var indexAnswer = int.Parse(continueApp);
				var goFind = indexAnswer - 1;
		
					while (continueApp != "N")
					{
						Console.Write("Badge ID Number: " + badgeRequest[goFind].Id + " Badge Name: " + badgeRequest[goFind].Name + " Favorite: " + badgeRequest[goFind].FavoriteClass);
						badgeRequest[goFind].FavoriteClass = Console.ReadLine();

						Console.WriteLine("If you would like to edit another badge enter the number of the line your would like to select. Hint: ls to list all bages");
						continueApp = Console.ReadLine();
							if (continueApp == "ls")
							{
								break;
							}
						indexAnswer = int.Parse(continueApp);
						goFind = indexAnswer - 1;
						continue; ;
					}
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

		// Returns the number of badges requested for viewing
		public static List<Badge> GetRecentBadges(List<Badge> badges)
		{
			var badgesRequest = new List<Badge>();
			badges.Sort(new RecentBadge());
			int counter = 0;

			Console.Write("How many badges would you like to see? ");
			var Request = Console.ReadLine();
			var NumberRequest = int.Parse(Request);

			foreach (var badge in badges)
			{
				badgesRequest.Add(badge);
				counter++;
				if (counter == NumberRequest)
					break;
			}
			return badgesRequest;
		}

	
		// Exports a json file with the values 
		public static List<Badge> ExportBadges(List<Badge> badges)
		{
			Console.Write("How many badges would you like to export?");
			var Request = Console.ReadLine();
			var NumberRequest = int.Parse(Request);

			var customBadgeExport = new List<Badge>();
			badges.Sort(new RecentBadge());
			int counter = 0;
			foreach (var badge in badges)
			{
				customBadgeExport.Add(badge);
				counter++;
				if (counter == NumberRequest)
					break;
			}
			return customBadgeExport;
		}

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
