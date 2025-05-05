using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Graphs.Trees09Lib.Trees.v101;

namespace Trees09Test.Trees.v300
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_c
	class T90_003
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var rn = Enumerable.Range(1, n).ToArray();
			var tree = new Tree(n + 1, es, 1);
			var d = tree.Nodes.Max(v => v.Depth);
			var root = tree.Nodes.First(v => v.Depth == d);
			tree = new Tree(n + 1, es, root.Id);
			d = tree.Nodes.Max(v => v.Depth);
			return d + 1;
		}
	}
}
