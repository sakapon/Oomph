using Oomph.Data.Values10Lib.Arrays.RHs.v101;

namespace Values10Test.Arrays.RHs
{
	// Test: https://atcoder.jp/contests/abc398/tasks/abc398_f
	class ABC398_F
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var s = Console.ReadLine().ToCharArray();
			var n = s.Length;

			var sr = s.ToArray();
			Array.Reverse(sr);

			var rh = new RollingHashArray<char>(s);
			var rhr = new RollingHashArray<char>(sr);

			for (int i = 0; i < n; i++)
			{
				if (!IsPalindrome(i, n)) continue;
				return new string(s) + new string(sr[(n - i)..]);
			}
			throw new InvalidOperationException();

			bool IsPalindrome(int l, int r)
			{
				var len = (r - l) / 2;
				return rh.GetHashCode(l, l + len) == rhr.GetHashCode(n - r, n - r + len);
			}
		}
	}
}
