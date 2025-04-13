using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L1
{
	// Test: https://atcoder.jp/contests/atc001/tasks/unionfind_a
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_a
	// Test: https://atcoder.jp/contests/tessoku-book/tasks/tessoku_book_bn
	class ATC001_B
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
			var uf = new UnionFind(n);

			foreach (var (t, u, v) in qs)
			{
				if (t == 0)
				{
					uf.Union(u, v);
				}
				else
				{
					r.Add(uf.AreSame(u, v));
				}
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
			//return string.Join("\n", r.Select(b => b ? 1 : 0));
		}
	}
}
