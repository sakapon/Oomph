using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int2
{
	// Test: https://atcoder.jp/contests/abc097/tasks/arc097_b
	class ARC097_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var p = Read();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (x, y) in es)
			{
				uf.Union(x, y);
			}
			return Enumerable.Range(0, n).Count(i => uf.AreSame(i + 1, p[i]));
		}
	}
}
