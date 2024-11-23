using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v221;

namespace UF09Test.UFs.v221
{
	// Test: https://atcoder.jp/contests/abc087/tasks/arc090_b
	class ARC090_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read3());

			var uf = new UnionFind(n + 1);

			foreach (var (l, r, d) in es)
			{
				if (uf.Union(l, r, d) || uf.Verify(l, r, d)) continue;
				return false;
			}
			return true;
		}
	}
}
