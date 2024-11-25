using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
{
	// Test: https://atcoder.jp/contests/abc277/tasks/abc277_d
	class ABC277_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = ReadL();

			Array.Sort(a);
			var s = a.Sum();
			var uf = new UnionFind(n);

			for (int i = 1; i < n; i++)
			{
				var d = a[i] - a[i - 1];
				if (d == 0 || d == 1)
				{
					uf.Union(i, i - 1);
				}
			}

			if (a[0] == 0 && a[^1] == m - 1)
			{
				uf.Union(0, n - 1);
			}

			return s - uf.ToGroups().Max(g => g.Sum(i => a[i]));
		}
	}
}
