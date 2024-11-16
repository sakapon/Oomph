using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/arc107/tasks/arc107_c
	class ARC107_C
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k) = Read2();
			var a = Array.ConvertAll(new bool[n], _ => Read());

			var rn = Enumerable.Range(0, n).ToArray();

			var r = SolveColumn(a);
			a = Array.ConvertAll(rn, j => Array.ConvertAll(rn, i => a[i][j]));
			r *= SolveColumn(a);
			return r % M;

			long SolveColumn(int[][] a)
			{
				var uf = new UnionFind(n);

				for (int x = 0; x < n; x++)
				{
					for (int y = x + 1; y < n; y++)
					{
						if (Array.TrueForAll(rn, i => a[i][x] + a[i][y] <= k))
						{
							uf.Union(x, y);
						}
					}
				}

				var r = 1L;
				foreach (var g in uf.GetGroupInfoes())
				{
					var c = g.Size;
					for (int i = 1; i <= c; i++)
					{
						r *= i;
						r %= M;
					}
				}
				return r;
			}
		}
	}
}
