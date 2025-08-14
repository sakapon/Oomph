using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc105/tasks/abc105_d
	class ABC105_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();

			var r = 0L;
			var s = 0;
			var map = new ChainHashMap<int, int>(n);
			map[0] = 1;

			foreach (var v in a)
			{
				s += v;
				s %= m;
				r += map[s]++;
			}
			return r;
		}
	}
}
