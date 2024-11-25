using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
{
	// Test: https://atcoder.jp/contests/abc131/tasks/abc131_f
	class ABC131_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var uf = new UnionFind(200000 + 1);
			foreach (var (x, y) in ps)
				uf.Union(x, 100000 + y);

			var r = 0L;
			foreach (var g in uf.ToGroups())
			{
				var c = g.Key.Size;
				if (c == 1) continue;

				var c0 = g.LongCount(v => v <= 100000);
				r += c0 * (c - c0);
			}
			return r - n;
		}
	}
}
