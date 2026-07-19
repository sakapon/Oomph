using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L3
{
	// Test: https://atcoder.jp/contests/abc447/tasks/abc447_e
	class ABC447_E
	{
		const long M = 998244353;
		const long MHalf = (M + 1) / 2;

		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var p = 1L;
			for (int i = 0; i < m; i++)
				p = p * 2 % M;

			var r = 0L;
			var uf = new UnionFind(n + 1);

			Array.Reverse(es);
			foreach (var (u, v) in es)
			{
				if (uf.GroupsCount != 3)
					uf.Union(u, v);
				else if (!uf.AreSame(u, v))
					r = (r + p) % M;

				p = p * MHalf % M;
			}
			return r;
		}
	}
}
