using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/abc256/tasks/abc256_e
	class ABC256_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var x = Read().Prepend(0).ToArray();
			var c = Read().Prepend(0).ToArray();

			var r = 0L;
			var uf = new UnionFind(n + 1);
			var uf2 = new UnionFind(n + 1);
			uf2.United += (v, nv) =>
			{
				if (c[v] > c[nv]) c[v] = c[nv];
			};

			for (int v = 1; v <= n; v++)
			{
				if (uf.Union(v, x[v])) continue;

				for (var u = x[v]; u != v; u = x[u])
				{
					uf2.Union(u, x[u]);
				}
				r += c[uf2.Find(v).Key];
			}
			return r;
		}
	}
}
