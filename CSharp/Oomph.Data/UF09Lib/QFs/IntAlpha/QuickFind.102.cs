using System;
using System.Collections.Generic;

// weighted-union heuristic
// O(log n)

namespace Oomph.Data.UF09Lib.QFs.v102
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
		public bool AreSame(int x, int y) => groups[x] == groups[y];

		public bool Union(int x, int y)
		{
			var (gx, gy) = (groups[x], groups[y]);
			if (gx == gy) return false;

			if (gx.Count < gy.Count) (gx, gy) = (gy, gx);
			gx.AddRange(gy);
			foreach (var v in gy) groups[v] = gx;
			return true;
		}
	}
}
