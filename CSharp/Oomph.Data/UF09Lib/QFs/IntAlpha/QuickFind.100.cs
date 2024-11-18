using System;
using System.Collections.Generic;

// no technique
// O(n)

namespace Oomph.Data.UF09Lib.QFs.v100
{
	public class QuickFind
	{
		readonly List<int>[] groups;

		public QuickFind(int n)
		{
			groups = new List<int>[n];
			for (int i = 0; i < n; ++i) groups[i] = new List<int> { i };
		}

		public List<int> Find(int x) => groups[x];
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			var (gx, gy) = (groups[x], groups[y]);
			if (gx == gy) return false;

			gx.AddRange(gy);
			foreach (var v in gy) groups[v] = gx;
			return true;
		}
	}
}
