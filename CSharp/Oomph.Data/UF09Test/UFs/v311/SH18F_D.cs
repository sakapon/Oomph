using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.Helpers;
using Oomph.Data.UF09Lib.UFs.v301;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/soundhound2018-summer-final-open/tasks/soundhound2018_summer_final_d
	class SH18F_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			var r = new List<bool>();
			var iv = Array.ConvertAll(new bool[n + 1], _ => new HashSet<(int, int)>());
			var uf = new UnionFind<HashSet<(int, int)>>(n + 1, MergeOps.Merge, false, iv);
			var uf2 = new UnionFind(n + 1);

			foreach (var q in qs)
			{
				var (t, u, v) = q;
				if (u < v) (u, v) = (v, u);

				if (t == 1)
				{
					uf.Union(u, v);
					uf.Find(u).Value.Add((u, v));
				}
				else if (t == 2)
				{
					var set = uf.Find(u).Value;
					foreach (var (a, b) in set)
					{
						uf2.Union(a, b);
					}
					set.Clear();
				}
				else
				{
					r.Add(uf2.AreSame(u, v) || uf.Find(u).Value.Contains((u, v)));
				}
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
