using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc413/tasks/abc413_g
	class ABC413_G
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (h, w, k) = Read3();
			var ps = Array.ConvertAll(new bool[k], _ => Read2());

			var map = Enumerable.Range(0, k).ToDictionary(v => ps[v]);
			var uf = new UnionFind(k + 2);
			var sv = k;
			var ev = k + 1;

			for (int v = 0; v < k; v++)
			{
				var (i, j) = ps[v];

				if (map.ContainsKey((i - 1, j))) uf.Union(v, map[(i - 1, j)]);
				if (map.ContainsKey((i + 1, j))) uf.Union(v, map[(i + 1, j)]);
				if (map.ContainsKey((i, j - 1))) uf.Union(v, map[(i, j - 1)]);
				if (map.ContainsKey((i, j + 1))) uf.Union(v, map[(i, j + 1)]);
				if (map.ContainsKey((i - 1, j - 1))) uf.Union(v, map[(i - 1, j - 1)]);
				if (map.ContainsKey((i - 1, j + 1))) uf.Union(v, map[(i - 1, j + 1)]);
				if (map.ContainsKey((i + 1, j - 1))) uf.Union(v, map[(i + 1, j - 1)]);
				if (map.ContainsKey((i + 1, j + 1))) uf.Union(v, map[(i + 1, j + 1)]);

				if (i == 1 || j == w) uf.Union(v, sv);
				if (j == 1 || i == h) uf.Union(v, ev);
			}
			return !uf.AreSame(sv, ev);
		}
	}
}
