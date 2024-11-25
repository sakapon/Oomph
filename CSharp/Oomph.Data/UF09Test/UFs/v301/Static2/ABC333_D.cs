using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static2
{
	// Test: https://atcoder.jp/contests/abc333/tasks/abc333_d
	class ABC333_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var uf = new UnionFind(n + 1);

			foreach (var (u, v) in es)
			{
				if (u != 1)
					uf.Union(u, v);
			}
			return n - uf.GetGroupInfoes().Max(g => g.Size);
		}
	}
}
