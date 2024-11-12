using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v302;

namespace UF09Test.UFs.v302
{
	// Test: https://atcoder.jp/contests/abc229/tasks/abc229_e
	class ABC229_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var map = es.ToLookup(e => e.u, e => e.v);

			var r = new List<int>();
			var uf = new UnionFind(n + 1);

			for (int v = n; v > 0; v--)
			{
				foreach (var nv in map[v])
				{
					uf.Union(v, nv);
				}
			}

			for (int v = 1; v <= n; v++)
			{
				foreach (var _ in map[v])
				{
					uf.Undo();
				}

				r.Add(uf.GroupsCount - v - 1);
			}
			return string.Join("\n", r);
		}
	}
}
