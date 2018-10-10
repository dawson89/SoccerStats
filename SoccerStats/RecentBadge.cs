using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{
	public class RecentBadge : IComparer<Badge>
	{
		public int Compare(Badge x, Badge y)
		{
			return x.EarnedDate.CompareTo(y.EarnedDate) * -1;
		} 
	}
}
