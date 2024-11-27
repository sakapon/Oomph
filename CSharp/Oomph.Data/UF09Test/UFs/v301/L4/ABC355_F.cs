using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L4
{
	// Test: https://atcoder.jp/contests/abc355/tasks/abc355_f
	class ABC355_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var es = Array.ConvertAll(new bool[n - 1], _ => Read3());
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			const int k = 10;
			var r = new List<int>();
			var ufs = Array.ConvertAll(new bool[k], _ => new UnionFind(n + 1));

			foreach (var (u, v, w) in es)
			{
				for (int j = w; j < k; j++)
				{
					ufs[j].Union(u, v);
				}
			}

			foreach (var (u, v, w) in qs)
			{
				for (int j = w; j < k; j++)
				{
					ufs[j].Union(u, v);
				}

				r.Add(ufs.Sum(uf => uf.GroupsCount - 2));
			}
			return string.Join("\n", r);
		}
	}
}
