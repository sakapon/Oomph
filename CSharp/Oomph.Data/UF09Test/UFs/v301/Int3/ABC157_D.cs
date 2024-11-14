using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int3
{
	// Test: https://atcoder.jp/contests/abc157/tasks/abc157_d
	class ABC157_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, k) = Read3();
			var es1 = Array.ConvertAll(new bool[m], _ => Read2());
			var es2 = Array.ConvertAll(new bool[k], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (a, b) in es1)
			{
				uf.Union(a, b);
			}

			var r = Enumerable.Range(0, n + 1).Select(v => uf.Find(v).Size - 1).ToArray();

			foreach (var (a, b) in es1)
			{
				r[a]--;
				r[b]--;
			}
			foreach (var (c, d) in es2)
			{
				if (!uf.AreSame(c, d)) continue;
				r[c]--;
				r[d]--;
			}
			return string.Join(" ", r[1..]);
		}
	}
}
