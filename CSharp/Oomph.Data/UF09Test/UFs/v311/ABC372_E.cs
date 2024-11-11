using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc372/tasks/abc372_e
	class ABC372_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			var r = new List<int>();
			var iv = Enumerable.Range(0, n + 1).Select(i => new List<int> { i }).ToArray();
			var uf = new UnionFind<List<int>>(n + 1, (l, l2) =>
			{
				l.AddRange(l2);
				l.Sort();
				l.Reverse();
				while (l.Count > 10) l.RemoveAt(l.Count - 1);
				return l;
			}, false, iv);

			foreach (var (t, u, v) in qs)
			{
				if (t == 1)
				{
					uf.Union(u, v);
				}
				else
				{
					var l = uf.Find(u).Value;
					r.Add(l.Count >= v ? l[v - 1] : -1);
				}
			}
			return string.Join("\n", r);
		}
	}
}
