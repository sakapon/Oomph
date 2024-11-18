using System;
using System.Collections.Generic;
using System.Linq;

// int vertexes, array-based

namespace Oomph.Data.UF09Lib.QFs.v201
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class QuickFind
	{
		readonly List<int>[] groups;
		public int ItemsCount => groups.Length;
		public int GroupsCount { get; private set; }

		public QuickFind(int n)
		{
			groups = new List<int>[n];
			for (int i = 0; i < n; ++i) groups[i] = new List<int> { i };
			GroupsCount = n;
		}

		public List<int> Find(int x) => groups[x];
		public bool AreSame(int x, int y) => groups[x] == groups[y];
		public int GetGroupSize(int x) => groups[x].Count;

		public bool Union(int x, int y)
		{
			var (gx, gy) = (groups[x], groups[y]);
			if (gx == gy) return false;

			if (gx.Count < gy.Count) (gx, gy) = (gy, gx);
			gx.AddRange(gy);
			foreach (var v in gy) groups[v] = gx;
			--GroupsCount;
			return true;
		}

		public IEnumerable<List<int>> ToGroups() => groups.Distinct();
	}
}
