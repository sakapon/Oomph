using System;

// short, path compression, from 101
// O(log n)

namespace Oomph.Data.UF09Lib.UFs.v211
{
	class UF
	{
		int[] p;
		public UF(int n) { p = new int[n]; Array.Fill(p, -1); }
		public int Find(int x) => p[x] == -1 ? x : p[x] = Find(p[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public void Union(int x, int y) { if ((x = Find(x)) != (y = Find(y))) p[y] = x; }
	}
}
