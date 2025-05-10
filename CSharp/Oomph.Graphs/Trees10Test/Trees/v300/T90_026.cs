using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Graphs.Trees10Lib.Trees.v100;

namespace Trees10Test.Trees.v300
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_z
	class T90_026
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var map = ToListMap(n + 1, es, true);
			var tree = new Tree(n + 1, map, 1);

			var rn = Enumerable.Range(1, n).ToArray();
			var r = Array.FindAll(rn, v => tree.Depths[v] % 2 == 0);
			if (r.Length < n / 2)
				r = Array.FindAll(rn, v => tree.Depths[v] % 2 == 1);
			return string.Join(" ", r[..(n / 2)]);
		}

		public static List<int>[] ToListMap(int n, (int u, int v)[] es, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				if (twoway) map[v].Add(u);
			}
			return map;
		}
	}
}
