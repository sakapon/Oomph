using Oomph.Graphs.Trees10Lib.Common.Arrays;
using Oomph.Graphs.Trees10Lib.Trees.v210;

namespace Trees10Test.Trees.v300
{
	// Test: https://atcoder.jp/contests/abc209/tasks/abc209_d
	class ABC209_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var g = new UnweightedGraph(n + 1, es, true);
			var tree = new Tree(g, 1);
			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				return (tree.Depths[a] - tree.Depths[b]) % 2 == 0 ? "Town" : "Road";
			}));
		}
	}
}
