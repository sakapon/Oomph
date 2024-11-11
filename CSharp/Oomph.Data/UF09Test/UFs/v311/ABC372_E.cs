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
			var iv = Array.ConvertAll(new bool[n + 1], _ => new int[20]);
			for (int i = 1; i <= n; i++) iv[i][^1] = i;

			var uf = new UnionFind<int[]>(n + 1, (a, b) =>
			{
				Array.Copy(b, 10, a, 0, 10);
				Array.Sort(a);
				return a;
			}, false, iv);

			foreach (var (t, u, v) in qs)
			{
				if (t == 1)
				{
					uf.Union(u, v);
				}
				else
				{
					var a = uf.Find(u).Value;
					r.Add(a[^v] != 0 ? a[^v] : -1);
				}
			}
			return string.Join("\n", r);
		}
	}
}
