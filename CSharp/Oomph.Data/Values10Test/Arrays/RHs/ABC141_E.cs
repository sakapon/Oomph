using Oomph.Data.Values10Lib.Arrays.RHs.v101;

namespace Values10Test.Arrays.RHs
{
	// WA
	// Test: https://atcoder.jp/contests/abc141/tasks/abc141_e
	class ABC141_E
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();

			var rh = new RollingHashArray<char>(s.ToCharArray());
			return Last(0, n / 2, Check);

			bool Check(int len)
			{
				return Enumerable.Range(0, n + 1)
					.TakeWhile(si => si + len <= n)
					.GroupBy(si => rh.GetHashCode(si, si + len))
					.Any(g =>
					{
						var a = g.ToArray();
						for (int i = 1; i < a.Length; i++)
							if (a[i] - a[i - 1] >= len) return true;
						return false;
					});
			}
		}

		static int Last(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}
	}
}
