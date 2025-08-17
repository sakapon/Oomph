using Oomph.Data.Collections10Lib.HashTables.Direct.v101;

namespace Collections10Test.HashTables.Direct
{
	// Test: https://atcoder.jp/contests/abc166/tasks/abc166_e
	// Test: https://atcoder.jp/contests/abc417/tasks/abc417_c
	class ABC166_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var r = 0L;
			var map = new DirectMap<int>(n);
			for (int i = 0; i < n; i++)
			{
				// i + a_i = j - a_j
				if (i + a[i] < n) map[i + a[i]]++;
				if (i - a[i] >= 0) r += map[i - a[i]];
			}
			return r;
		}
	}
}
