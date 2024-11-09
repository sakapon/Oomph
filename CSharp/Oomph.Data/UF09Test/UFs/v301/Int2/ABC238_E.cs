using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int2
{
	// Test: https://atcoder.jp/contests/abc238/tasks/abc238_e
	class ABC238_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, qc) = Read2();
			var es = Array.ConvertAll(new bool[qc], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (l, r) in es)
			{
				uf.Union(l - 1, r);
			}
			return uf.AreSame(0, n);
		}
	}
}
