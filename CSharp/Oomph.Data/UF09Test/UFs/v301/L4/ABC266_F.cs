using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L4
{
	// Test: https://atcoder.jp/contests/abc266/tasks/abc266_f
	class ABC266_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				map[v].Add(u);
			}

			var q = new Queue<int>();
			for (int v = 1; v <= n; v++)
			{
				if (map[v].Count == 1) q.Enqueue(v);
			}

			var r = new List<bool>();
			var uf = new UnionFind(n + 1);

			while (q.TryDequeue(out var v))
			{
				var nv = map[v][0];
				uf.Union(v, nv);
				map[nv].Remove(v);
				if (map[nv].Count == 1) q.Enqueue(nv);
			}

			foreach (var (x, y) in qs)
			{
				r.Add(uf.AreSame(x, y));
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
