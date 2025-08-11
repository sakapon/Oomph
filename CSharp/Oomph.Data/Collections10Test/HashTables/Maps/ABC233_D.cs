using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc233/tasks/abc233_d
	class ABC233_D
	{
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k) = Read2L();
			var a = ReadL();

			var r = 0L;
			var s = 0L;
			var map = new ChainHashMap<long, int>((int)n);
			map[0] = 1;

			foreach (var v in a)
			{
				s += v;
				r += map[s - k];
				map[s]++;
			}
			return r;
		}
	}
}
