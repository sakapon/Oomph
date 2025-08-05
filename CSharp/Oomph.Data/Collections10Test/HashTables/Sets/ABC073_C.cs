using Oomph.Data.Collections10Lib.HashTables.Chain.v101;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc073/tasks/abc073_c
	class ABC073_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var set = new ChainHashSet<int>(18);
			foreach (var x in a)
				if (!set.Add(x)) set.Remove(x);
			return set.Count;
		}
	}
}
