using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc164/tasks/abc164_c
	// Test: https://atcoder.jp/contests/abc226/tasks/abc226_b
	class ABC164_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

			var set = new ChainHashSet<string>();
			set.AddKeys(ss);
			return set.Count;
		}
	}
}
