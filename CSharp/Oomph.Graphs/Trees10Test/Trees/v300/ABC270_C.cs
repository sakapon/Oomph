using Oomph.Graphs.Trees10Lib.Common.Arrays;
using Oomph.Graphs.Trees10Lib.Trees.v210;

namespace Trees10Test.Trees.v300
{
	// Test: https://atcoder.jp/contests/abc270/tasks/abc270_c
	class ABC270_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, x, y) = Read3();
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var g = new UnweightedGraph(n + 1, es, true);
			var tree = new Tree(g, y);
			return string.Join(" ", tree.GetPath(x));
		}
	}
}
