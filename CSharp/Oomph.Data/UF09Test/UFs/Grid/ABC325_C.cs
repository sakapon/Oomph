using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grid
{
	// Test: https://atcoder.jp/contests/abc325/tasks/abc325_c
	class ABC325_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var s = s0.SelectMany(t => t).ToArray();

			var uf = new UnionFind(n);

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					var v = w * i + j;

					if (j > 0)
					{
						if (s[v] == '#' && s[v - 1] == '#') uf.Union(v, v - 1);
					}

					if (i > 0)
					{
						if (s[v] == '#' && s[v - w] == '#') uf.Union(v, v - w);
					}

					if (i > 0 && j > 0)
					{
						if (s[v] == '#' && s[v - w - 1] == '#') uf.Union(v, v - w - 1);
						if (s[v - 1] == '#' && s[v - w] == '#') uf.Union(v - 1, v - w);
					}
				}

			return uf.GetGroupInfoes().Count(g => s[g.Key] == '#');
		}
	}
}
