using System;

// no technique
// O(n)
namespace Oomph.Data.UF09Lib.UFs.v101
{
	public class UnionFind
	{
		readonly int[] parents;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
		}

		public int Find(int x) => parents[x] == -1 ? x : Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			parents[y] = x;
			return true;
		}
	}
}
