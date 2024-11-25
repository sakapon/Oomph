using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static2
{
	// Test: https://atcoder.jp/contests/past202112-open/tasks/past202112_g
	class PAST009_G
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
			var map = new bool[n + 1, n + 1];

			foreach (var q in qs)
			{
				var (t, u, v) = q;
				if (t == 1)
				{
					if (u > v) (u, v) = (v, u);
					map[u, v] ^= true;
				}
				else
				{
					r.Add(Check(u, v));
				}
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));

			bool Check(int s, int t)
			{
				var uf = new UnionFind(n + 1);
				for (int u = 1; u <= n; u++)
					for (int v = u + 1; v <= n; v++)
						if (map[u, v])
							uf.Union(u, v);
				return uf.AreSame(s, t);
			}
		}
	}
}
