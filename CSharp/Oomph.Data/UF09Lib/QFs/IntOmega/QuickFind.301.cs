using System;
using System.Collections.Generic;

// int vertexes

namespace Oomph.Data.UF09Lib.QFs.v301
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class QuickFind
	{
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Size = {Size}")]
		public class Group
		{
			public int Key { get; internal set; }
			public List<int> Items { get; internal set; }
			public int Size => Items.Count;
		}

		readonly Group[] map, groups;
		public int ItemsCount => map.Length;
		public int GroupsCount { get; private set; }

		// (parent root, child root)
		public event Action<int, int> United;

		public QuickFind(int n)
		{
			map = new Group[n];
			for (int i = 0; i < n; ++i) map[i] = new Group { Key = i, Items = new List<int> { i } };
			groups = (Group[])map.Clone();
			GroupsCount = n;
		}

		public Group Find(int x) => map[x];
		public bool AreSame(int x, int y) => map[x] == map[y];

		public bool Union(int x, int y)
		{
			var (gx, gy) = (map[x], map[y]);
			if (gx == gy) return false;

			if (gx.Size < gy.Size) (gx, gy) = (gy, gx);
			gx.Items.AddRange(gy.Items);
			foreach (var v in gy.Items) map[v] = gx;
			groups[gy.Key] = null;
			--GroupsCount;
			United?.Invoke(gx.Key, gy.Key);
			return true;
		}

		public Group[] ToGroups() => Array.FindAll(groups, g => g != null);
	}
}
