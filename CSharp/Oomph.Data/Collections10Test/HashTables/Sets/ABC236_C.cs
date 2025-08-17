using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc236/tasks/abc236_c
	class ABC236_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var ss = Console.ReadLine().Split();
			var ts = Console.ReadLine().Split();

			var set = new ChainHashSet<string>();
			set.AddKeys(ts, true);
			return string.Join("\n", ss.Select(s => set.Contains(s) ? "Yes" : "No"));
		}
	}
}
