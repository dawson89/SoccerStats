using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SoccerStats
{

	public class RootProject
	{
		public Badge[] Badge{ get; set; }
	}

	public class Badge
	{
		//internal readonly string counter;

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

		[JsonProperty(PropertyName = "earned_date")]
		public DateTime EarnedDate { get; set; }

		//[JsonProperty(PropertyName = "name")]
		public int NumberSelector { get; set; }

		public string FavoriteClass { get; set; }
	}

}
