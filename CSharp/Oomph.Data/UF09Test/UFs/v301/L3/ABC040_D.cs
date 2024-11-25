using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L3
{
	// Test: https://atcoder.jp/contests/abc040/tasks/abc040_d
	class ABC040_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int y) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int y) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read3());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var r = new int[qc];
			var uf = new UnionFind(n + 1);

			foreach (var i in Enumerable.Range(0, qc + m).OrderByDescending(i => i < qc ? qs[i].y : es[i - qc].y))
			{
				if (i < qc)
				{
					var (v, _) = qs[i];
					r[i] = uf.Find(v).Size;
				}
				else
				{
					var (u, v, _) = es[i - qc];
					uf.Union(u, v);
				}
			}
			return string.Join("\n", r);
		}
	}
}
