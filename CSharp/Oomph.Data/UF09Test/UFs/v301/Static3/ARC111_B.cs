using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
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

			var uf = new UnionFind(400000 + 1);
			var l = new List<int>();

			foreach (var (u, v) in es)
			{
				if (!uf.Union(u, v))
					l.Add(v);
			}

			var d = new int[400000 + 1];
			Array.Fill(d, 1);

			foreach (var v in l)
			{
				d[uf.Find(v).Key] = 0;
			}
			return uf.GetGroupInfoes().Sum(g => g.Size - d[g.Key]);
		}
	}
}
