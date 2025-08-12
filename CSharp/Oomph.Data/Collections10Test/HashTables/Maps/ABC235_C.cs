using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc235/tasks/abc235_c
	class ABC235_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int x, int k) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var a = Read();
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var map = new ChainHashMap<int, List<int>>(n, new List<int>());
			for (int i = 0; i < n; i++)
			{
				var node = map.GetNode(a[i]);
				if (node != null) node.Value.Add(i);
				else map[a[i]] = new List<int> { i };
			}

			return string.Join("\n", qs.Select(q =>
			{
				var l = map[q.x];
				return q.k - 1 < l.Count ? l[q.k - 1] + 1 : -1;
			}));
		}
	}
}
