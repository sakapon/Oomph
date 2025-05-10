using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Graphs.Trees10Lib.Trees.v200;

namespace Trees10Test.Trees.v300
{
	// Test: https://atcoder.jp/contests/abc014/tasks/abc014_4
	class LCA_ABC014_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var tree = new Tree(n + 1, es, 1);

			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				var d = GetLcaDepth(tree, a, b);
				return tree.Depths[a] + tree.Depths[b] - 2 * d + 1;
			}));
		}

		static int GetLcaDepth(Tree tree, int a, int b)
		{
			if (tree.TourMap[b][0] < tree.TourMap[a][0]) (a, b) = (b, a);
			if (tree.TourMap[b][^1] <= tree.TourMap[a][^1]) return tree.Depths[a];

			var (so, eo) = (tree.TourMap[a][^1], tree.TourMap[b][0]);

			return First(0, Math.Min(tree.Depths[a], tree.Depths[b]), dx =>
			{
				var l = tree.DepthToTourMap[dx];
				var si = First(0, l.Count, x => l[x] >= so);
				var ei = First(0, l.Count, x => l[x] > eo);
				return si < ei;
			});
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
