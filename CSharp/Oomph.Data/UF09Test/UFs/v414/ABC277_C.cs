using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v414;

namespace UF09Test.UFs.v414
{
	// Test: https://atcoder.jp/contests/abc277/tasks/abc277_c
	class ABC277_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Read2());

			var uf = new UnionFind<int, int>(Math.Max, false, v => v);
			foreach (var (a, b) in es)
			{
				uf.Union(a, b);
			}
			return uf.Find(1)?.Value ?? 1;
		}
	}
}
