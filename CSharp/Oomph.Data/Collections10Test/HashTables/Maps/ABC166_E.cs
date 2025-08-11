using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc166/tasks/abc166_e
	class ABC166_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var r = 0L;
			var map = new ChainHashMap<int, int>(n);
			for (int i = 0; i < n; i++)
			{
				// i + a_i = j - a_j
				map[i + a[i]]++;
				r += map[i - a[i]];
			}
			return r;
		}
	}
}
