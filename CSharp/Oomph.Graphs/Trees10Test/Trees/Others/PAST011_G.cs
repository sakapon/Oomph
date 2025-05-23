﻿using Oomph.Graphs.Trees10Lib.Common.Arrays;

namespace Trees10Test.Trees.Others
{
	// Test: https://atcoder.jp/contests/past202206-open/tasks/past202206_g
	class PAST011_G
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var g = new UnweightedGraph(n + 1, es, true);
			return UnweightedTreeHelper.IsTree(g, 1);
		}
	}
}
