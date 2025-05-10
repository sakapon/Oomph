namespace Trees10Test.Trees.Others
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

			var map = ToMap(n + 1, es, true);
			var r = n - 1;
			var q = new Queue<int>();

			for (int u = 1; u <= n; u++)
			{
				if (u == x) continue;
				if (h[u - 1] == 0 && map[u].Count == 1) q.Enqueue(u);
			}

			while (q.TryDequeue(out var v))
			{
				if (map[v].Count == 0) continue;
				r--;
				var u = map[v].First();
				map[v].Clear();
				map[u].Remove(v);
				if (u == x) continue;
				if (h[u - 1] == 0 && map[u].Count == 1) q.Enqueue(u);
			}
			return r * 2;
		}

		public static HashSet<int>[] ToMap(int n, (int u, int v)[] edges, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new HashSet<int>());
			foreach (var (u, v) in edges)
			{
				map[u].Add(v);
				if (twoway) map[v].Add(u);
			}
			return map;
		}
	}
}
