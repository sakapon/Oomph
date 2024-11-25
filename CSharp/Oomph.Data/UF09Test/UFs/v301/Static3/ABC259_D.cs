using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
{
	// Test: https://atcoder.jp/contests/abc259/tasks/abc259_d
	class ABC259_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var (sx, sy, tx, ty) = Read4();
			var ps = Array.ConvertAll(new bool[n], _ => Read3L());

			bool IsOnCircumference(int pi, long x, long y)
			{
				var (x0, y0, r) = ps[pi];
				x -= x0;
				y -= y0;
				return x * x + y * y == r * r;
			}

			var si = Enumerable.Range(0, n).First(i => IsOnCircumference(i, sx, sy));
			var ti = Enumerable.Range(0, n).First(i => IsOnCircumference(i, tx, ty));

			var uf = new UnionFind(n);

			for (int i = 0; i < n; i++)
			{
				var (xi, yi, ri) = ps[i];

				for (int j = i + 1; j < n; j++)
				{
					var (xj, yj, rj) = ps[j];

					xj -= xi;
					yj -= yi;
					var d2 = xj * xj + yj * yj;
					if ((ri - rj) * (ri - rj) <= d2 && d2 <= (ri + rj) * (ri + rj))
					{
						uf.Union(i, j);
					}
				}
			}
			return uf.AreSame(si, ti);
		}
	}
}
