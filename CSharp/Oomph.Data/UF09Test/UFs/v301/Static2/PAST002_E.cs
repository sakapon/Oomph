using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static2
{
	// Test: https://atcoder.jp/contests/past202004-open/tasks/past202004_e
	class PAST002_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var uf = new UnionFind(n + 1);
			for (int i = 0; i < n; i++)
			{
				uf.Union(i + 1, a[i]);
			}
			var r = Enumerable.Range(1, n).Select(v => uf.Find(v).Size);
			return string.Join(" ", r);
		}
	}
}
