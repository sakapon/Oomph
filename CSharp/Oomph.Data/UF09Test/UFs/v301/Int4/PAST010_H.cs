using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/past202203-open/tasks/past202203_h
	class PAST010_H
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var r = new List<string>();
			var ls = Enumerable.Range(0, n + 1).Select(v => new List<int> { v }).ToArray();

			var uf = new UnionFind(n + 1);
			uf.United += (p, c) =>
			{
				ls[p].AddRange(ls[c]);
			};

			foreach (var q in qs)
			{
				if (q[0] == 1)
				{
					var (u, v) = (q[1], q[2]);
					uf.Union(u, v);
				}
				else
				{
					var u = q[1];
					u = uf.Find(u).Key;
					ls[u].Sort();
					r.Add(string.Join(" ", ls[u]));
				}
			}
			return string.Join("\n", r);
		}
	}
}
