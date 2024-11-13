using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.MSTs
{
	// Test: https://atcoder.jp/contests/abc235/tasks/abc235_e
	class ABC235_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, qc) = Read3();
			var es = Enumerable.Range(0, m + qc).Select(i =>
			{
				var a = Read();
				return (i, u: a[0], v: a[1], w: a[2]);
			}).ToArray();

			var r = new bool[qc];
			var uf = new UnionFind(n + 1);

			foreach (var (i, u, v, _) in es.OrderBy(e => e.w))
				if (i < m) uf.Union(u, v);
				else r[i - m] = !uf.AreSame(u, v);

			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
