using Oomph.Graphs.Trees10Lib.Common.Arrays;
using Oomph.Graphs.Trees10Lib.Trees.v210;

namespace Trees10Test.Trees.LCA
{
	// Test: https://atcoder.jp/contests/past201912-open/tasks/past201912_k
	class PAST001_K
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var g = new UnweightedGraph(n + 1);
			var root = 0;

			for (int v = 1; v <= n; v++)
			{
				if (p[v - 1] == -1)
					root = v;
				else
					g.AddEdge(p[v - 1], v, false);
			}

			var tree = new Tree(g, root);
			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				return tree.IsAncestor(b, a) ? "Yes" : "No";
			}));
		}
	}
}
