using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/arc112/tasks/arc112_d
	class ARC112_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var uf = new UnionFind(h + w);
			uf.Union(0, h);
			uf.Union(0, h + w - 1);
			uf.Union(h - 1, h);

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					if (s[i][j] == '#')
						uf.Union(i, h + j);
				}

			var ch = Enumerable.Range(0, h).GroupBy(uf.Find).Count();
			var cw = Enumerable.Range(h, w).GroupBy(uf.Find).Count();
			return Math.Min(ch, cw) - 1;
		}
	}
}
