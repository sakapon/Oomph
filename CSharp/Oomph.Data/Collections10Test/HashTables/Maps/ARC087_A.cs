using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc082/tasks/arc087_a
	class ARC087_A
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var map = new ChainHashMap<int, int>(n);
			foreach (var x in a) map[x]++;
			return map.Sum(p => p.Value < p.Key ? p.Value : p.Value - p.Key);
		}
	}
}
