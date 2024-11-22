using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static1
{
	// Test: https://atcoder.jp/contests/abc284/tasks/abc284_c
	// Test: https://atcoder.jp/contests/abc126/tasks/abc126_e
	class ABC284_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (u, v) in es)
				uf.Union(u, v);
			return uf.GroupsCount - 1;
		}
	}
}
