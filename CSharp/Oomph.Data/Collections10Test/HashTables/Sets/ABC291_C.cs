using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc291/tasks/abc291_c
	class ABC291_C
	{
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();

			var (x, y) = (0, 0);
			var set = new ChainHashSet<(int, int)>();
			set.Add((x, y));

			foreach (var c in s)
			{
				if (c == 'R') x++;
				if (c == 'L') x--;
				if (c == 'U') y++;
				if (c == 'D') y--;

				if (!set.Add((x, y))) return true;
			}
			return false;
		}
	}
}
