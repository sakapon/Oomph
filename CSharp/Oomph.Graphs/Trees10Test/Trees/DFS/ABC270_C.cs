namespace Trees10Test.Trees.DFS
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

			var map = ToListMap(n + 1, es, true);
			var path = new Stack<int>();
			DFS(y, -1);
			return string.Join(" ", path);

			bool DFS(int v, int pv)
			{
				path.Push(v);
				if (v == x) return true;
				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					if (DFS(nv, v)) return true;
				}
				path.Pop();
				return false;
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
