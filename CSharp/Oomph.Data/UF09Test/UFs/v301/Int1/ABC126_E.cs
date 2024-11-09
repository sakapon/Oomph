using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int1
{
	// Test: https://atcoder.jp/contests/abc126/tasks/abc126_e
	class ABC126_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read3());

			var uf = new UnionFind(n + 1);
			foreach (var (x, y, _) in es)
				uf.Union(x, y);
			return uf.GroupsCount - 1;
		}
	}
}
