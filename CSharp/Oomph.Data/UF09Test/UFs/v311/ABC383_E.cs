using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc383/tasks/abc383_e
	class ABC383_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int w) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, k) = Read3();
			var es = Array.ConvertAll(new bool[m], _ => Read3());
			var a = Read();
			var b = Read();

			var ac = new int[n + 1];
			var bc = new int[n + 1];
			foreach (var v in a) ac[v]++;
			foreach (var v in b) bc[v]++;

			var r = 0L;
			var iv = Enumerable.Range(0, n + 1).Select(v => (ac[v], bc[v])).ToArray();
			var uf = new UnionFind<(int a, int b)>(n + 1, (x, y) => (x.a + y.a, x.b + y.b), false, iv);

			foreach (var (u, v, w) in es.OrderBy(e => e.w))
			{
				if (!uf.Union(u, v)) continue;

				var (s, t) = uf.Find(u).Value;
				var c = Math.Min(s, t);
				r += (long)w * c;
				s -= c;
				t -= c;
				uf.Find(u).Value = (s, t);
			}
			return r;
		}
	}
}
