using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int2
{
	// Test: https://atcoder.jp/contests/abc293/tasks/abc293_d
	class ABC293_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Console.ReadLine().Split());

			var uf = new UnionFind(n + 1);
			var c = 0;

			foreach (var e in es)
			{
				var u = int.Parse(e[0]);
				var v = int.Parse(e[2]);

				if (!uf.Union(u, v)) c++;
			}
			return $"{c} {uf.GroupsCount - c - 1}";
		}
	}
}
