using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/abc181/tasks/abc181_f
	class ABC181_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var d = new int[n, n];
			for (int i = 0; i < n; i++)
			{
				var (xi, yi) = ps[i];

				for (int j = i + 1; j < n; j++)
				{
					var (xj, yj) = ps[j];

					var dx = xj - xi;
					var dy = yj - yi;
					d[i, j] = dx * dx + dy * dy;
				}
			}

			return Last(0, 100, r =>
			{
				// 上の境界: n, 下の境界: n + 1
				var uf = new UnionFind(n + 2);

				for (int i = 0; i < n; i++)
				{
					var (_, yi) = ps[i];

					if (100 - yi < r * 2) uf.Union(i, n);
					if (yi + 100 < r * 2) uf.Union(i, n + 1);

					for (int j = i + 1; j < n; j++)
					{
						if (d[i, j] < r * r * 4) uf.Union(i, j);
					}
				}
				return !uf.AreSame(n, n + 1);
			}, 6);
		}

		static double Last(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
			return l;
		}
	}
}
