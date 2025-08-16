using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc348/tasks/abc348_c
	class ABC348_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var map = new ChainHashMap<int, int>(n, int.MaxValue);
			foreach (var (a, c) in ps)
				map.GetOrAddNode(c).Value.Chmin(a);
			return map.GetValues().Max();
		}
	}
}
