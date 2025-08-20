namespace Values10Test.Arrays.ZHs
{
	// Test: https://atcoder.jp/contests/abc367/tasks/abc367_f
	class ABC367_F
	{
		// 2^32 * (Å„5-1) / 2 Ç…ãﬂÇ¢ëfêî
		const int B = (int)(2654435761L - (1L << 32));
		// 2^32 * (Å„3-1) / 2 Ç…ãﬂÇ¢ëfêî
		const int C = 1572067127;

		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int al, int ar, int bl, int br) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var a = Read();
			var b = Read();
			var qs = Array.ConvertAll(new bool[qc], _ => Read4());

			a = Array.ConvertAll(a, v => (v * B) ^ C);
			b = Array.ConvertAll(b, v => (v * B) ^ C);

			var sa = new StaticRSQ1(a);
			var sb = new StaticRSQ1(b);

			var r = qs.Select(q => sa.GetSum(q.al - 1, q.ar) == sb.GetSum(q.bl - 1, q.br) ? "Yes" : "No");
			return string.Join("\n", r);
		}
	}

	public class StaticRSQ1
	{
		int n;
		long[] s;
		public long[] Raw => s;
		public StaticRSQ1(int[] a)
		{
			n = a.Length;
			s = new long[n + 1];
			for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
		}

		public long GetSum(int l, int r)
		{
			if (r < 0 || n < l) return 0;
			if (l < 0) l = 0;
			if (n < r) r = n;
			return s[r] - s[l];
		}
	}
}
