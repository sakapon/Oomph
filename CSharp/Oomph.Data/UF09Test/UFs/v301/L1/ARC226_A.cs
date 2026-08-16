using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L1
{
	// Test: https://atcoder.jp/contests/arc226/tasks/arc226_a
	class ARC226_A
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());
			Array.Sort(ps);

			var uf = new UnionFind(n);

			for (int i = 0; i < n; i++)
			{
				var (si, ti) = ps[i];

				for (int j = i + 1; j < n; j++)
				{
					var (sj, tj) = ps[j];
					if (ti <= sj) break;

					if (!uf.Union(i, j)) return 0;
				}
			}

			var r = 1L;
			var c = uf.GroupsCount;
			while (c-- > 0)
				r = r * 2 % M;
			return r;
		}
	}
}
