using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v321;

// bipartite graph
namespace UF09Test.UFs.v321
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

			var uf = new UnionFind<bool>(n + 1, false, x => x, (x, y) => x ^ y);

			for (int i = 0; i < m; i++)
			{
				var (u, v) = (a[i], b[i]);
				if (uf.Union(u, v, true) || uf.Verify(u, v, true)) continue;
				return false;
			}
			return true;
		}
	}
}
