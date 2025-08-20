using Oomph.Data.Values10Lib.Arrays.RHs.v101;

namespace Values10Test.Arrays.RHs
{
	// Test: https://atcoder.jp/contests/abc284/tasks/abc284_f
	class ABC284_F
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var t = Console.ReadLine();

			var tr = t.ToCharArray();
			Array.Reverse(tr);

			var rh = new RollingHashArray<char>(t.ToCharArray());
			var rhr = new RollingHashArray<char>(tr);

			for (int i = 0; i <= n; i++)
			{
				if (rh.GetHashCodeByCount(0, i) == rhr.GetHashCodeByCount(n - i, i) && rh.GetHashCodeByCount(n + i, n - i) == rhr.GetHashCodeByCount(n, n - i))
				{
					var s = t[..i] + t[(n + i)..];
					return $"{s}\n{i}";
				}
			}
			return -1;
		}
	}
}
