using Oomph.Graphs.Trees10Lib.Common.Arrays;
using Oomph.Graphs.Trees10Lib.Trees.v210;

namespace Trees10Test.Trees.LCA
{
	// Test: https://atcoder.jp/contests/abc014/tasks/abc014_4
	class ABC014_D
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

			var g = new UnweightedGraph(n + 1, es, true);
			var tree = new Tree(g, 1);

			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				var lca = tree.GetLca(a, b);
				return tree.Depths[a] + tree.Depths[b] - 2 * tree.Depths[lca] + 1;
			}));
		}
	}
}
