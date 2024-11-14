using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int2
{
	// Test: https://atcoder.jp/contests/abc231/tasks/abc231_d
	class ABC231_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n + 1);
			var degs = new int[n + 1];

			foreach (var (a, b) in es)
			{
				if (!uf.Union(a, b)) return false;
				if (++degs[a] > 2) return false;
				if (++degs[b] > 2) return false;
			}
			return true;
		}
	}
}
