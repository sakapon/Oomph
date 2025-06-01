using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
{
	// Test: https://atcoder.jp/contests/abc408/tasks/abc408_e
	class ABC408_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int u, int v, int w) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read3());

			var r = 0;
			var eis = Enumerable.Range(0, m).ToArray();

			for (int k = 30 - 1; k >= 0; k--)
			{
				var t = Array.FindAll(eis, ei => (es[ei].w & (1 << k)) == 0);

				var uf = new UnionFind(n + 1);
				foreach (var ei in t)
				{
					var (u, v, _) = es[ei];
					uf.Union(u, v);
				}

				if (uf.AreSame(1, n))
					eis = t;
				else
					r |= 1 << k;
			}
			return r;
		}
	}
}
