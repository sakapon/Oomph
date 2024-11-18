using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.QFs.v301;

namespace UF09Test.QFs.v301
{
	// Test: https://atcoder.jp/contests/arc106/tasks/arc106_b
	class ARC106_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, m) = Read2();
			var a = ReadL();
			var b = ReadL();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var uf = new QuickFind(n);
			foreach (var (c, d) in es)
			{
				uf.Union(c - 1, d - 1);
			}
			return uf.ToGroups().All(g => g.Items.Sum(v => a[v]) == g.Items.Sum(v => b[v]));
		}
	}
}
