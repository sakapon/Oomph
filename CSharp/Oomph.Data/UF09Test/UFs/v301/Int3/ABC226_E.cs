using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int3
{
	// Test: https://atcoder.jp/contests/abc226/tasks/abc226_e
	class ABC226_E
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			if (n != m) return 0;

			var ecs = new int[n + 1];
			var uf = new UnionFind(n + 1);
			uf.United += (v, nv) =>
			{
				ecs[v] += ecs[nv];
			};

			foreach (var (u, v) in es)
			{
				uf.Union(u, v);
				ecs[uf.Find(u).Key]++;
			}

			var r = 1L;
			foreach (var g in uf.GetGroupInfoes()[1..])
			{
				if (g.Size != ecs[g.Key]) return 0;
				r = r * 2 % M;
			}
			return r;
		}
	}
}
