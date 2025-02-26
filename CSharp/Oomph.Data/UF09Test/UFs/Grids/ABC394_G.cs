using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc394/tasks/abc394_g
	class ABC394_G
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var f0 = Array.ConvertAll(new bool[h], _ => Read());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var n = h * w;
			var f = f0.SelectMany(t => t).ToArray();

			var r = new int[qc];
			var qiset = Array.ConvertAll(new bool[n], _ => new HashSet<int>());

			for (int qi = 0; qi < qc; qi++)
			{
				var q = qs[qi];
				if ((q[0], q[1]) == (q[3], q[4]))
				{
					r[qi] = Math.Abs(q[2] - q[5]);
				}
				else
				{
					qiset[w * (q[0] - 1) + q[1] - 1].Add(qi);
					qiset[w * (q[3] - 1) + q[4] - 1].Add(qi);
				}
			}

			var tv = -1;
			var uf = new UnionFind(n);
			uf.United += (pv, cv) =>
			{
				if (qiset[pv].Count < qiset[cv].Count) (qiset[pv], qiset[cv]) = (qiset[cv], qiset[pv]);
				foreach (var qi in qiset[cv])
				{
					if (qiset[pv].Remove(qi))
					{
						var q = qs[qi];
						if (q[2] > f[tv] && q[5] > f[tv])
						{
							r[qi] = q[2] + q[5] - 2 * f[tv];
						}
						else
						{
							r[qi] = Math.Abs(q[2] - q[5]);
						}
					}
					else
					{
						qiset[pv].Add(qi);
					}
				}
			};

			var nexts = new List<int>();
			foreach (var v in Enumerable.Range(0, n).OrderBy(v => -f[v]))
			{
				tv = v;
				var (i, j) = (v / w, v % w);

				nexts.Clear();
				if (i > 0) nexts.Add(v - w);
				if (i + 1 < h) nexts.Add(v + w);
				if (j > 0) nexts.Add(v - 1);
				if (j + 1 < w) nexts.Add(v + 1);

				foreach (var nv in nexts)
				{
					if (f[nv] < f[v]) continue;
					uf.Union(v, nv);
				}
			}
			return string.Join("\n", r);
		}
	}
}
