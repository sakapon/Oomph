using System;
using System.Linq;

// int vertexes, array-based, from 103

namespace Oomph.Data.UF09Lib.UFs.v200
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind
	{
		readonly int[] parents, sizes;
		public int ItemsCount => parents.Length;
		public int GroupsCount { get; private set; }

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
			GroupsCount = n;
		}

		public int Find(int x) => parents[x] == -1 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetGroupSize(int x) => sizes[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (sizes[x] < sizes[y]) (x, y) = (y, x);
			parents[y] = x;
			sizes[x] += sizes[y];
			--GroupsCount;
			return true;
		}

		public ILookup<int, int> ToGroups() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
