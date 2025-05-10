namespace Trees10Test.Trees.DFS
{
	// Test: https://atcoder.jp/contests/arc030/tasks/arc030_2
	class ARC030_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, x) = Read2();
			var h = Read();
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var map = ToListMap(n + 1, es, true);
			return DFS(x, -1);

			int DFS(int v, int pv)
			{
				var r = 0;
				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					var nr = DFS(nv, v);
					if (nr == 0 && h[nv - 1] == 0) continue;
					r += nr + 2;
				}
				return r;
			}
		}

		public static List<int>[] ToListMap(int n, (int u, int v)[] edges, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in edges)
			{
				map[u].Add(v);
				if (twoway) map[v].Add(u);
			}
			return map;
		}
	}
}
