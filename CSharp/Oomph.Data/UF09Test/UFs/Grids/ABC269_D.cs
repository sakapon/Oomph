using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc269/tasks/abc269_d
	class ABC269_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var ds = new[] { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, -1) };
			var uf = new UnionFind(n);

			for (int i = 0; i < n; i++)
			{
				var (xi, yi) = ps[i];

				for (int j = i + 1; j < n; j++)
				{
					var (xj, yj) = ps[j];

					var d = (xi - xj, yi - yj);
					if (Array.IndexOf(ds, d) != -1)
						uf.Union(i, j);
				}
			}
			return uf.GroupsCount;
		}
	}
}
