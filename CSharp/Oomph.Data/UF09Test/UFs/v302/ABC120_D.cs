using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v302;

namespace UF09Test.UFs.v302
{
	// Test: https://atcoder.jp/contests/abc120/tasks/abc120_d
	class ABC120_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var r = new List<long>();
			var s = 0L;
			var uf = new UnionFind(n + 1);

			foreach (var (a, b) in es.Reverse())
			{
				uf.Union(a, b);
			}

			for (int j = 0; j < m; j++)
			{
				if (uf.Undo(out var a, out var b))
				{
					s += (long)uf.Find(a).Size * uf.Find(b).Size;
				}
				r.Add(s);
			}
			return string.Join("\n", r);
		}
	}
}
