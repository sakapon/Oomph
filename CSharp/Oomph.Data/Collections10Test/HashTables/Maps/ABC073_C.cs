using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc073/tasks/abc073_c
	class ABC073_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var map = new ChainHashMap<int, int>(n);
			foreach (var x in a) map[x]++;
			return map.Count(p => p.Value % 2 == 1);
		}
	}
}
