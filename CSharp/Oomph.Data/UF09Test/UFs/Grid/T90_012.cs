using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grid
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_l
	class T90_012
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var n = h * w;

			var r = new List<bool>();
			var p = new bool[n];
			var uf = new UnionFind(n);

			foreach (var q in qs)
			{
				if (q[0] == 1)
				{
					var (i, j) = (q[1] - 1, q[2] - 1);
					var v = w * i + j;
					p[v] = true;

					if (i > 0 && p[v - w]) uf.Union(v, v - w);
					if (i + 1 < h && p[v + w]) uf.Union(v, v + w);
					if (j > 0 && p[v - 1]) uf.Union(v, v - 1);
					if (j + 1 < w && p[v + 1]) uf.Union(v, v + 1);
				}
				else
				{
					var (i, j) = (q[1] - 1, q[2] - 1);
					var u = w * i + j;
					(i, j) = (q[3] - 1, q[4] - 1);
					var v = w * i + j;
					r.Add(p[u] && uf.AreSame(u, v));
				}
			}

			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
