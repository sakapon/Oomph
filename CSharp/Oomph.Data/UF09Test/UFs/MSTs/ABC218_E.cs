using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.MSTs
{
	// Test: https://atcoder.jp/contests/abc218/tasks/abc218_e
	class ABC218_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int c) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read3());

			var r = 0L;
			var uf = new UnionFind(n + 1);

			foreach (var (u, v, c) in es.OrderBy(e => e.c))
			{
				if (!uf.Union(u, v) && c >= 0) r += c;
			}
			return r;
		}
	}
}
