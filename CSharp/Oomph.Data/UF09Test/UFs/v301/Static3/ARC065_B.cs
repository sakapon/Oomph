using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
{
	// Test: https://atcoder.jp/contests/abc049/tasks/arc065_b
	class ARC065_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k, l) = Read3();
			var es1 = Array.ConvertAll(new bool[k], _ => Read2());
			var es2 = Array.ConvertAll(new bool[l], _ => Read2());

			var uf1 = new UnionFind(n + 1);
			foreach (var (u, v) in es1)
				uf1.Union(u, v);

			var uf2 = new UnionFind(n + 1);
			foreach (var (u, v) in es2)
				uf2.Union(u, v);

			var r = new int[n + 1];
			var q = Enumerable.Range(0, n + 1).GroupBy(v => (uf1.Find(v).Key, uf2.Find(v).Key));

			foreach (var g in q)
			{
				var c = g.Count();
				foreach (var v in g)
					r[v] = c;
			}

			return string.Join(" ", r[1..]);
		}
	}
}
