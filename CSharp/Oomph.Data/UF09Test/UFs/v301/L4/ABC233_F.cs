using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L4
{
	// Test: https://atcoder.jp/contests/abc233/tasks/abc233_f
	class ABC233_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Read().Prepend(0).ToArray();
			var m = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new UnionFind(n + 1);
			// 全域森
			var map = Array.ConvertAll(new bool[n + 1], _ => new List<(int ei, int nv)>());

			for (int j = 0; j < m; j++)
			{
				var (u, v) = es[j];
				if (uf.Union(u, v))
				{
					map[u].Add((j + 1, v));
					map[v].Add((j + 1, u));
				}
			}

			for (int v = 1; v <= n; v++)
			{
				if (!uf.AreSame(v, p[v])) return -1;
			}

			var r = new List<int>();
			var q = new Queue<int>();

			for (int v = 1; v <= n; v++)
			{
				if (map[v].Count == 1) q.Enqueue(v);
			}

			while (q.TryDequeue(out var sv))
			{
				bool DFS(int v, int pv)
				{
					if (p[v] == sv) return true;

					foreach (var (ei, nv) in map[v])
					{
						if (nv == pv) continue;
						if (DFS(nv, v))
						{
							(p[v], p[nv]) = (p[nv], p[v]);
							r.Add(ei);
							return true;
						}
					}
					return false;
				}

				if (map[sv].Count == 0) continue;
				DFS(sv, -1);
				var nv = map[sv][0].nv;
				map[nv].RemoveAll(e => e.nv == sv);
				if (map[nv].Count == 1) q.Enqueue(nv);
			}

			return $"{r.Count}\n" + string.Join(" ", r);
		}
	}
}
