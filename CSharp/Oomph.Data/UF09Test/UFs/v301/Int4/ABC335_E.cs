using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/abc335/tasks/abc335_e
	class ABC335_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read().Prepend(0).ToArray();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n + 1);
			foreach (var (u, v) in es)
			{
				if (a[u] == a[v])
					uf.Union(u, v);
			}

			var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			var indeg = new int[n + 1];

			foreach (var e in es)
			{
				var (u, v) = e;

				u = uf.Find(u).Key;
				v = uf.Find(v).Key;
				if (u == v) continue;

				if (a[u] > a[v]) (u, v) = (v, u);
				map[u].Add(v);
				indeg[v]++;
			}

			var dp = new int[n + 1];
			dp[uf.Find(1).Key] = 1;

			var q = new Queue<int>();
			for (int v = 1; v <= n; v++)
			{
				if (indeg[v] == 0)
					q.Enqueue(v);
			}

			while (q.TryDequeue(out var v))
			{
				foreach (var nv in map[v])
				{
					if (dp[v] > 0)
						Chmax(ref dp[nv], dp[v] + 1);

					if (--indeg[nv] == 0)
						q.Enqueue(nv);
				}
			}

			return dp[uf.Find(n).Key];
		}

		public static int Chmax(ref int x, int v) => x < v ? x = v : x;
	}
}
