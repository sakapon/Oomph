using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/abc380/tasks/abc380_e
	class ABC380_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var r = new List<int>();
			var uf = new UnionFind(n + 2);

			// color id -> count
			var counts = new int[n + 2];
			Array.Fill(counts, 1);

			// cell id -> color id
			var colors = Enumerable.Range(0, n + 2).ToArray();
			var ms = Enumerable.Range(0, n + 2).ToArray();
			var Ms = Enumerable.Range(0, n + 2).ToArray();

			uf.United += (v, nv) =>
			{
				ms[v] = Math.Min(ms[v], ms[nv]);
				Ms[v] = Math.Max(Ms[v], Ms[nv]);
			};

			foreach (var q in qs)
			{
				if (q[0] == 1)
				{
					var (x, c) = (q[1], q[2]);
					var g = uf.Find(x);
					x = g.Key;

					var c0 = colors[x];
					counts[c0] -= g.Size;
					counts[c] += g.Size;

					var m = ms[x];
					var M = Ms[x];

					if (colors[uf.Find(m - 1).Key] == c)
						uf.Union(m, m - 1);
					if (colors[uf.Find(M + 1).Key] == c)
						uf.Union(m, M + 1);

					x = uf.Find(x).Key;
					colors[x] = c;
				}
				else
				{
					var c = q[1];
					r.Add(counts[c]);
				}
			}
			return string.Join("\n", r);
		}
	}
}
