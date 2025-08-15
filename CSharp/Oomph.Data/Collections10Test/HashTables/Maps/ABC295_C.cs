using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// 50 –œŒ
	// Test: https://atcoder.jp/contests/abc295/tasks/abc295_c
	class ABC295_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var map = new ChainHashMap<int, int>(n);
			foreach (var x in a) map.GetOrAddNode(x).Value++;
			return map.Sum(p => p.Value / 2);
		}
	}
}
