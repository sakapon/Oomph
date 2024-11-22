using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

// bipartite matching, fake vertexes
namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/abc327/tasks/abc327_d
	class ABC327_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, m) = Read2();
			var a = Read();
			var b = Read();

			var uf = new UnionFind(2 * n + 1);

			for (int j = 0; j < m; j++)
			{
				var (u, v) = (a[j], b[j]);
				uf.Union(u, v + n);
				uf.Union(u + n, v);
			}
			return Enumerable.Range(1, n).All(v => !uf.AreSame(v, v + n));
		}
	}
}
