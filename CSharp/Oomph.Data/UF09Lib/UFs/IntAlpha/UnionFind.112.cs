using System;

// union by rank
// O(log n)

namespace Oomph.Data.UF09Lib.UFs.v112
{
	public class UnionFind
	{
		readonly int[] parents, ranks;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			ranks = new int[n];
		}

		public int Find(int x) => parents[x] == -1 ? x : Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (ranks[x] < ranks[y]) (x, y) = (y, x);
			parents[y] = x;
			if (ranks[x] == ranks[y]) ++ranks[x];
			return true;
		}
	}
}
