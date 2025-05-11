namespace Trees10Test.Trees.DFS
{
	// Test: https://atcoder.jp/contests/abc255/tasks/abc255_f
	class ABC255_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var P = Read();
			var I = Read();

			if (P[0] != 1) return -1;

			var pq = new Queue<int>(P);
			var iq = new Queue<int>(I);
			var u = new bool[n + 1];
			var r = Array.ConvertAll(new bool[n + 1], _ => new int[2]);

			var ok = DFS(-1, -1);
			if (!ok) return -1;
			return string.Join("\n", r[1..].Select(a => $"{a[0]} {a[1]}"));

			bool DFS(int pv, int index)
			{
				var v = pq.Dequeue();
				u[v] = true;
				if (pv != -1) r[pv][index] = v;
				if (iq.Peek() != v && !DFS(v, 0)) return false;
				if (iq.Dequeue() != v) return false;
				if (iq.Count > 0 && !u[iq.Peek()] && !DFS(v, 1)) return false;
				return true;
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
