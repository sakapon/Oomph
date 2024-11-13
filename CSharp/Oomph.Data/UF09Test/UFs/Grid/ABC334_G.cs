using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v302;

namespace UF09Test.UFs.Grid
{
	// Test: https://atcoder.jp/contests/abc334/tasks/abc334_g
	class ABC334_G
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var p = s0.SelectMany(t => t.Select(c => c == '#')).ToArray();

			var n2 = 1;
			while (n2 < n) n2 <<= 1;
			var map = Array.ConvertAll(new bool[n2], _ => new List<int>());

			for (int i = 0; i < h; i++)
				for (int j = 1; j < w; j++)
				{
					var v = w * i + j;
					if (p[v] && p[v - 1])
					{
						map[v].Add(v - 1);
						map[v - 1].Add(v);
					}
				}

			for (int j = 0; j < w; j++)
				for (int i = 1; i < h; i++)
				{
					var v = w * i + j;
					if (p[v] && p[v - w])
					{
						map[v].Add(v - w);
						map[v - w].Add(v);
					}
				}

			var ccs = ConnectedComponentsOnRemove(n2, Array.ConvertAll(map, l => l.ToArray()));
			var greensCount = p.Count(b => b);
			return Enumerable.Range(0, n).Sum(v => p[v] ? (long)ccs[v] - n2 + greensCount - 1 : 0) % M * MInv(greensCount) % M;
		}

		static int[] ConnectedComponentsOnRemove(int n, int[][] map)
		{
			var uf = new UnionFind(n);
			var ccs = new int[n];
			Rec(0, n);
			return ccs;

			// [l, r) の頂点が削除された状態
			void Rec(int l, int r)
			{
				if (r - l == 1)
				{
					ccs[l] = uf.GroupsCount;
					return;
				}

				var c = (l + r) / 2;

				{
					var ec = 0;
					for (int v = c; v < r; v++)
						foreach (var nv in map[v])
							if (nv < l || c <= nv)
							{
								uf.Union(v, nv);
								ec++;
							}
					Rec(l, c);
					while (ec-- > 0)
						uf.Undo();
				}

				{
					var ec = 0;
					for (int v = l; v < c; v++)
						foreach (var nv in map[v])
							if (nv < c || r <= nv)
							{
								uf.Union(v, nv);
								ec++;
							}
					Rec(c, r);
					while (ec-- > 0)
						uf.Undo();
				}
			}
		}

		const long M = 998244353;
		// 0^0 は未定義
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	}
}
