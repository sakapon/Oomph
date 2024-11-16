using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static2
{
	// Test: https://atcoder.jp/contests/abc206/tasks/abc206_d
	class ABC206_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var uf = new UnionFind(200000 + 1);
			for (int i = 0; i < n; i++)
			{
				uf.Union(a[i], a[n - 1 - i]);
			}
			return uf.GetGroupInfoes().Sum(g => g.Size - 1);
		}
	}
}
