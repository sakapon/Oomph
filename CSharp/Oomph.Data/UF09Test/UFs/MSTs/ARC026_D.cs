using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.MSTs
{
	// Test: https://atcoder.jp/contests/arc026/tasks/arc026_4
	class ARC026_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int c, int t) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read4());

			return First(0, 1 << 30, x =>
			{
				var s = 0.0;
				var uf = new UnionFind(n + 1);

				foreach (var (u, v, c, t) in es.OrderBy(p => p.c - p.t * x))
				{
					var d = c - t * x;
					if (d <= 0 || !uf.AreSame(u, v))
					{
						s += d;
						uf.Union(u, v);
					}
				}
				return s <= 0;
			}, 2);
		}

		static double First(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
			return r;
		}
	}
}
