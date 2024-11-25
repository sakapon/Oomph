using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L2
{
	// Test: https://atcoder.jp/contests/abc120/tasks/abc120_d
	class ABC120_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var r = new long[m];
			var s = (long)n * (n - 1) / 2;
			var uf = new UnionFind(n + 1);

			for (int j = m - 1; j >= 0; j--)
			{
				r[j] = s;
				var (a, b) = es[j];
				var t = (long)uf.Find(a).Size * uf.Find(b).Size;
				if (uf.Union(a, b)) s -= t;
			}
			return string.Join("\n", r);
		}
	}
}
