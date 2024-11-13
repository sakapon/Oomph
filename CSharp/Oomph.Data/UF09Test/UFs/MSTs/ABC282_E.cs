using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.MSTs
{
	// Test: https://atcoder.jp/contests/abc282/tasks/abc282_e
	class ABC282_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();

			long GetScore(int x, int y) => (MPow(x, y, m) + MPow(y, x, m)) % m;
			var es = new List<(int, int, long c)>();

			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					es.Add((i, j, GetScore(a[i], a[j])));

			var r = 0L;
			var uf = new UnionFind(n);

			foreach (var (u, v, c) in es.OrderBy(e => -e.c))
			{
				if (uf.Union(u, v)) r += c;
			}
			return r;
		}

		static long MPow(long b, long i, long M)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
	}
}
