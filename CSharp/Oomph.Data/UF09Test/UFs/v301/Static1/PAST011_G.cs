using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static1
{
	// Test: https://atcoder.jp/contests/past202206-open/tasks/past202206_g
	class PAST011_G
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (a, b) in es)
				uf.Union(a, b);
			return uf.GroupsCount == 2;
		}
	}
}
