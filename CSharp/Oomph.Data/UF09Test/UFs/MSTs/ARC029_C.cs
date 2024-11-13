using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.MSTs
{
	// Test: https://atcoder.jp/contests/arc029/tasks/arc029_3
	class ARC029_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int w) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var cs = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));
			var es = Array.ConvertAll(new bool[m], _ => Read3());

			var r = 0L;
			var uf = new UnionFind(n + 1);
			var cs2 = Enumerable.Range(0, n).Select(i => (0, i + 1, w: cs[i]));

			foreach (var (u, v, w) in cs2.Concat(es).OrderBy(p => p.w))
			{
				if (uf.Union(u, v)) r += w;
			}
			return r;
		}
	}
}
