using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.Grid
{
	// Test: https://atcoder.jp/contests/aising2019/tasks/aising2019_c
	class AIS19_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var p = s0.SelectMany(t => t.Select(c => c == '#')).ToArray();

			var iv = Array.ConvertAll(p, b => b ? (1, 0) : (0, 1));
			var uf = new UnionFind<(int c, int d)>(n, (a, b) => (a.c + b.c, a.d + b.d), false, iv);

			for (int i = 0; i < h; i++)
				for (int j = 1; j < w; j++)
				{
					var v = w * i + j;
					if (p[v] != p[v - 1]) uf.Union(v, v - 1);
				}

			for (int j = 0; j < w; j++)
				for (int i = 1; i < h; i++)
				{
					var v = w * i + j;
					if (p[v] != p[v - w]) uf.Union(v, v - w);
				}

			return uf.GetGroupInfoes().Sum(g => (long)g.Value.c * g.Value.d);
		}
	}
}
