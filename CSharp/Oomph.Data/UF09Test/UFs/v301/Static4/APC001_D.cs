using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static4
{
	// Test: https://atcoder.jp/contests/apc001/tasks/apc001_d
	class APC001_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n);
			foreach (var (x, y) in es)
				uf.Union(x, y);

			// uf.SetsCount == n - m
			if (n - m - 1 == 0) return 0;
			if (2 * (n - m - 1) > n) return "Impossible";

			var r = 0L;
			var cl2 = new List<int>();

			foreach (var g in uf.ToGroups())
			{
				var cs = g.Select(v => a[v]).ToArray();
				Array.Sort(cs);
				r += cs[0];
				cl2.AddRange(cs[1..]);
			}

			var cs2 = cl2.ToArray();
			Array.Sort(cs2);
			r += cs2[..(n - m - 2)].Sum(c => (long)c);
			return r;
		}
	}
}
