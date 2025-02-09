using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L3
{
	// Test: https://atcoder.jp/contests/abc392/tasks/abc392_e
	class ABC392_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var roots = new SortedSet<int>(Enumerable.Range(1, n));
			var eis = new List<int>();
			var r = new List<string>();

			var uf = new UnionFind(n + 1);
			uf.United += (v, nv) =>
			{
				roots.Remove(nv);
			};

			for (int j = 0; j < m; j++)
			{
				var (u, v) = es[j];
				if (!uf.Union(u, v)) eis.Add(j);
			}

			foreach (var j in eis)
			{
				if (uf.GroupsCount == 2) break;

				var (u, v) = es[j];
				var root = uf.Find(v).Key;
				var nv = roots.Min;
				if (nv == root) nv = roots.Max;
				uf.Union(v, nv);
				r.Add($"{j + 1} {v} {nv}");
			}
			return $"{r.Count}\n" + string.Join("\n", r);
		}
	}
}
