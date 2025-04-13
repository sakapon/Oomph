using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.L2
{
	// Test: https://atcoder.jp/contests/tessoku-book/tasks/tessoku_book_em
	class TB_B66
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var r = new List<bool>();
			var uf = new UnionFind(n + 1);

			foreach (var x in Enumerable.Range(1, m).Except(qs.Where(q => q[0] == 1).Select(q => q[1])))
			{
				var (u, v) = es[x - 1];
				uf.Union(u, v);
			}

			Array.Reverse(qs);
			foreach (var q in qs)
			{
				if (q[0] == 1)
				{
					var x = q[1];
					var (u, v) = es[x - 1];
					uf.Union(u, v);
				}
				else
				{
					r.Add(uf.AreSame(q[1], q[2]));
				}
			}

			r.Reverse();
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
