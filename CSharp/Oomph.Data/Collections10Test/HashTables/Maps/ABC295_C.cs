using Oomph.Data.Collections10Lib.HashTables.Chain.v201;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc295/tasks/abc295_c
	class ABC295_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var map = new FixedChainHashMap<int, int>(20);
			foreach (var x in a) map[x]++;
			return map.Sum(p => p.Value / 2);
		}
	}
}
