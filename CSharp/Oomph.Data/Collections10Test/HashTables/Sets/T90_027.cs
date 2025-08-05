using Oomph.Data.Collections10Lib.HashTables.Chain.v100;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_aa
	class T90_027
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());

			var set = new ChainHashSet<string>(18);

			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			for (int i = 1; i <= n; i++)
			{
				var s = Console.ReadLine();
				if (set.Add(s)) Console.WriteLine(i);
			}
			Console.Out.Flush();
		}
	}
}
