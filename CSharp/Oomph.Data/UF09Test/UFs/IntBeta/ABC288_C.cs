using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v201;

namespace UF09Test.UFs.IntBeta
{
	// Test: https://atcoder.jp/contests/abc288/tasks/abc288_c
	class ABC288_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var r = 0;
			var uf = new UnionFind(n + 1);
			foreach (var (a, b) in es)
				if (!uf.Union(a, b)) r++;
			return r;
		}
	}
}
