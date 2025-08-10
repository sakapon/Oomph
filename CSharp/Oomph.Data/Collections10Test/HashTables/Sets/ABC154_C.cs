using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc154/tasks/abc154_c
	class ABC154_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve() ? "YES" : "NO");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var set = new ChainHashSet<int>(n);
			return a.All(set.Add);
		}
	}
}
