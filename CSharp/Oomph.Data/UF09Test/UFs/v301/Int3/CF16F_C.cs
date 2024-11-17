using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

// fake vertexes
namespace UF09Test.UFs.v301.Int3
{
	// Test: https://atcoder.jp/contests/cf16-final/tasks/codefestival_2016_final_c
	class CF16F_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "YES" : "NO");
		static bool Solve()
		{
			var (n, m) = Read2();
			var ps = Array.ConvertAll(new bool[n], _ => Read());

			var uf = new UnionFind(n + m);
			for (int i = 0; i < n; i++)
			{
				foreach (var l in ps[i][1..])
				{
					uf.Union(i, n + l - 1);
				}
			}

			var r = uf.Find(0);
			return Enumerable.Range(0, n).All(i => uf.Find(i) == r);
		}
	}
}
