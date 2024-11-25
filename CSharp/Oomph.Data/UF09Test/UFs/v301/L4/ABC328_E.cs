using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L4
{
	// Test: https://atcoder.jp/contests/abc328/tasks/abc328_e
	class ABC328_E
	{
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (int, int, long) Read3L() { var a = ReadL(); return ((int)a[0], (int)a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, k) = Read3L();
			var es = Array.ConvertAll(new bool[m], _ => Read3L());

			var r = 1L << 60;

			Combination(es, n - 1, p =>
			{
				var uf = new UnionFind(n + 1);
				var s = 0L;

				foreach (var (u, v, w) in p)
				{
					uf.Union(u, v);
					s += w;
				}

				s %= k;
				if (uf.GroupsCount == 2 && r > s) r = s;
				return false;
			});

			return r;
		}

		public static void Combination<T>(T[] a, int r, Func<T[], bool> action)
		{
			var n = a.Length;
			var p = new T[r];
			DFS(0, 0);

			bool DFS(int v, int si)
			{
				if (v == r) return action(p);

				for (int i = si; r - v <= n - i; ++i)
				{
					p[v] = a[i];
					if (DFS(v + 1, i + 1)) return true;
				}
				return false;
			}
		}
	}
}
