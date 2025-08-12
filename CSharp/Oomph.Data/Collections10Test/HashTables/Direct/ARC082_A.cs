using Oomph.Data.Collections10Lib.HashTables.Direct.v101;

namespace Collections10Test.HashTables.Direct
{
	// Test: https://atcoder.jp/contests/abc072/tasks/arc082_a
	class ARC082_A
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var max = 100000;
			var map = new DirectMap<int>(max);
			foreach (var v in a)
				map[v]++;

			return Enumerable.Range(1, max - 2).Max(i => map[i - 1] + map[i] + map[i + 1]);
		}
	}
}
