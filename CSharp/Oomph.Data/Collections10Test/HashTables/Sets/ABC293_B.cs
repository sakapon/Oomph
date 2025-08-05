using Oomph.Data.Collections10Lib.HashTables.Chain.v201;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc293/tasks/abc293_b
	class ABC293_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var set = new ChainHashSet<int>(18);
			for (int i = 0; i < n; i++)
				set.Add(i + 1);

			for (int i = 0; i < n; i++)
				if (set.Contains(i + 1)) set.Remove(a[i]);

			return $"{set.Count}\n" + string.Join(" ", set);
		}
	}
}
