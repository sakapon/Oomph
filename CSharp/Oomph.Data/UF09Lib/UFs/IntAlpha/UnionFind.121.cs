using System;

// union by size
// O(log n)
namespace Oomph.Data.UF09Lib.UFs.v121
{
	public class UnionFind
	{
		readonly int[] parents, sizes;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
		}

		public int Find(int x) => parents[x] == -1 ? x : Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetGroupSize(int x) => sizes[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (sizes[x] < sizes[y]) (x, y) = (y, x);
			parents[y] = x;
			sizes[x] += sizes[y];
			return true;
		}
	}
}
