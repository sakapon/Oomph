using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/arc111/tasks/arc111_b
	class ARC111_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Read2());

			// 辺の数
			var uf = new UnionFind<int>(400000 + 1, (x, y) => x + y, false);
			foreach (var (u, v) in es)
			{
				uf.Union(u, v);
				uf.Find(u).Value++;
			}
			return uf.GetGroupInfoes().Sum(g => Math.Min(g.Size, g.Value));
		}
	}
}
