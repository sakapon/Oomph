using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc217/tasks/abc217_d
	class ABC217_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int c, int x) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (l, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var xs = qs.Select(q => q.x).Distinct().Append(0).Append(l).ToArray();
			Array.Sort(xs);

			// key: 木材の左端
			var map = Enumerable.Range(0, xs.Length).ToDictionary(i => xs[i]);
			// value: 木材の長さ
			var iv = Enumerable.Range(0, xs.Length - 1).Select(i => xs[i + 1] - xs[i]).ToArray();

			var r = new List<int>();
			var uf = new UnionFind<int>(iv.Length, (x, y) => x + y, false, iv);

			foreach (var x in xs.Except(qs.Where(q => q.c == 1).Select(q => q.x)))
			{
				if (x == 0 || x == l) continue;
				var v = map[x];
				uf.Union(v, v - 1);
			}

			for (int qi = qc - 1; qi >= 0; qi--)
			{
				var (c, x) = qs[qi];
				var v = map[x];

				if (c == 1)
				{
					uf.Union(v, v - 1);
				}
				else
				{
					r.Add(uf.Find(v).Value);
				}
			}

			r.Reverse();
			return string.Join("\n", r);
		}
	}
}
