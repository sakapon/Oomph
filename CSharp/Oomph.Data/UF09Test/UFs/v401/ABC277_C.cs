using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v401;

namespace UF09Test.UFs.v401
{
	// Test: https://atcoder.jp/contests/abc277/tasks/abc277_c
	class ABC277_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Read2());

			var keys = es.Select(e => e.u).Concat(es.Select(e => e.v)).Append(1);
			var uf = new UnionFind<int>(keys);
			foreach (var (a, b) in es)
			{
				uf.Union(a, b);
			}
			return uf.ToGroups()[uf.Find(1)].Max();
		}
	}
}
