using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L3
{
	// Test: https://atcoder.jp/contests/arc151/tasks/arc151_b
	class ARC151_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var p = Read();
			p = Array.ConvertAll(p, x => x - 1);

			var r = 0L;
			var uf = new UnionFind(n);

			for (int i = 0; i < n; i++)
			{
				if (uf.AreSame(i, p[i])) continue;

				var d = MPow(m, uf.GroupsCount - 1) * (m - 1) % M;
				r += d * MHalf;
				r %= M;

				uf.Union(i, p[i]);
			}
			return r;
		}

		const long M = 998244353;
		const long MHalf = (M + 1) / 2;
		// 0^0 は未定義
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
	}
}
