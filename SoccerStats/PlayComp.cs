using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{
	public class PlayComp : IComparer<Badge>
	{
		public int Compare(Badge x, Badge y)
		{
			return x.Id.CompareTo(y.Id);
		}
	}
}
