using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static1
{
	// Test: https://atcoder.jp/contests/abl/tasks/abl_c
	// Test: https://atcoder.jp/contests/arc032/tasks/arc032_2
	class ABL_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (a, b) in es)
				uf.Union(a, b);
			return uf.GroupsCount - 2;
		}
	}
}
