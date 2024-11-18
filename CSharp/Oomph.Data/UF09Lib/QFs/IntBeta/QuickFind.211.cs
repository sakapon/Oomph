using System;
using System.Collections.Generic;
using System.Linq;

// int vertexes, node-based

namespace Oomph.Data.UF09Lib.QFs.v211
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class QuickFind
	{
		[System.Diagnostics.DebuggerDisplay(@"Size = {Size}")]
		public class Group
		{
			public List<int> Items { get; internal set; }
			public int Size => Items.Count;
		}

		readonly Group[] groups;
		public int ItemsCount => groups.Length;
		public int GroupsCount { get; private set; }

		public QuickFind(int n)
		{
			groups = new Group[n];
			for (int i = 0; i < n; ++i) groups[i] = new Group { Items = new List<int> { i } };
			GroupsCount = n;
		}

		public Group Find(int x) => groups[x];
		public bool AreSame(int x, int y) => groups[x] == groups[y];

		public bool Union(int x, int y)
		{
			var (gx, gy) = (groups[x], groups[y]);
			if (gx == gy) return false;

			if (gx.Size < gy.Size) (gx, gy) = (gy, gx);
			gx.Items.AddRange(gy.Items);
			foreach (var v in gy.Items) groups[v] = gx;
			--GroupsCount;
			return true;
		}

		public IEnumerable<Group> ToGroups() => groups.Distinct();
	}
}
