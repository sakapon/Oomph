using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int3
{
	// Test: https://atcoder.jp/contests/abc304/tasks/abc304_e
	class ABC304_E
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());
			var k = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[k], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (u, v) in es)
			{
				uf.Union(u, v);
			}

			var set = new HashSet<(int, int)>();
			foreach (var (x, y) in ps)
			{
				set.Add((uf.Find(x).Key, uf.Find(y).Key));
				set.Add((uf.Find(y).Key, uf.Find(x).Key));
			}

			var r = new List<bool>();
			foreach (var (x, y) in qs)
			{
				r.Add(!set.Contains((uf.Find(x).Key, uf.Find(y).Key)));
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
