using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

// bipartite matching, fake vertexes
namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/arc036/tasks/arc036_d
	class ARC036_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read4());

			var r = new List<bool>();
			var uf = new UnionFind(2 * n + 1);

			foreach (var (w, x, y, z) in qs)
			{
				if (w == 1)
				{
					if (z % 2 == 0)
					{
						uf.Union(x, y);
						uf.Union(x + n, y + n);
					}
					else
					{
						uf.Union(x, y + n);
						uf.Union(x + n, y);
					}
				}
				else
				{
					r.Add(uf.AreSame(x, y));
				}
			}
			return string.Join("\n", r.Select(b => b ? "YES" : "NO"));
		}
	}
}
