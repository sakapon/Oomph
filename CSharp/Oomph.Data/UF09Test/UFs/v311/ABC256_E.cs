using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc256/tasks/abc256_e
	class ABC256_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var x = Read().Prepend(0).ToArray();
			var c = Read().Prepend(0).ToArray();

			var indeg = new int[n + 1];
			foreach (var v in x) indeg[v]++;

			var q = new Queue<int>();
			for (int v = 1; v <= n; v++)
			{
				if (indeg[v] == 0) q.Enqueue(v);
			}

			while (q.TryDequeue(out var v))
			{
				var nv = x[v];
				if (--indeg[nv] == 0) q.Enqueue(nv);
			}

			var uf = new UnionFind<int>(n + 1, Math.Min, false, c);

			for (int v = 1; v <= n; v++)
			{
				if (indeg[v] == 0) continue;
				uf.Union(v, x[v]);
			}
			return uf.GetGroupInfoes().Sum(g => g.Size > 1 ? g.Value : 0L);
		}
	}
}
