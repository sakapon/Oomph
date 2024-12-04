using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc292/tasks/abc292_d
	class ABC292_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind<int>(n + 1, (x, y) => x + y, false);
			foreach (var (u, v) in es)
			{
				if (!uf.Union(u, v))
					uf.Find(v).Value++;
			}
			uf.Find(0).Value++;
			return uf.GetGroupInfoes().All(g => g.Value == 1);
		}
	}
}
