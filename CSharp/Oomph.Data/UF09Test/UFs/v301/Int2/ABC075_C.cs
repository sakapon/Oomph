using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int2
{
	// Test: https://atcoder.jp/contests/abc075/tasks/abc075_c
	class ABC075_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			return Enumerable.Range(0, m).Count(IsBridge);

			bool IsBridge(int ei)
			{
				var uf = new UnionFind(n + 1);

				for (int j = 0; j < m; j++)
				{
					if (j == ei) continue;

					var (a, b) = es[j];
					uf.Union(a, b);
				}
				return uf.GroupsCount != 2;
			}
		}
	}
}
