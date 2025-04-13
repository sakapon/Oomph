using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L2
{
	// Test: https://atcoder.jp/contests/abc401/tasks/abc401_e
	class ABC401_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var mapU = es.ToLookup(e => e.u, e => e.v);
			var mapL = es.ToLookup(e => e.v, e => e.u);

			var r = new int[n + 1];
			var uf = new UnionFind(n + 1);
			var setU = new HashSet<int>();

			for (int v = 1; v <= n; v++)
			{
				setU.Remove(v);
				foreach (var nv in mapU[v])
					setU.Add(nv);

				foreach (var nv in mapL[v])
					uf.Union(v, nv);

				r[v] = uf.Find(1).Size == v ? setU.Count : -1;
			}
			return string.Join("\n", r[1..]);
		}
	}
}
