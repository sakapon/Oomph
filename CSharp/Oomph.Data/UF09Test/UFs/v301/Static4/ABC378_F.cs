using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static4
{
	// Test: https://atcoder.jp/contests/abc378/tasks/abc378_f
	class ABC378_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				map[v].Add(u);
			}

			var r = 0L;
			var uf = new UnionFind(n + 1);

			foreach (var (u, v) in es)
			{
				if (map[u].Count == 3 && map[v].Count == 3)
					uf.Union(u, v);
			}

			foreach (var g in uf.ToGroups())
			{
				if (map[g.Key.Key].Count != 3) continue;

				var c = 0L;
				foreach (var v in g)
				{
					foreach (var nv in map[v])
					{
						if (map[nv].Count == 2) c++;
					}
				}
				r += c * (c - 1) / 2;
			}
			return r;
		}
	}
}
