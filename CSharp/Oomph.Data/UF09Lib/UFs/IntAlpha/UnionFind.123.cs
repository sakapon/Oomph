using System;

// path compression, union by size
// parents[root] = -size
// O(α(n))
namespace Oomph.Data.UF09Lib.UFs.v123
{
	public class UnionFind
	{
		readonly int[] parents;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
		}

		public int Find(int x) => parents[x] < 0 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetGroupSize(int x) => -parents[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (parents[x] > parents[y]) (x, y) = (y, x);
			parents[x] += parents[y];
			parents[y] = x;
			return true;
		}
	}
}
