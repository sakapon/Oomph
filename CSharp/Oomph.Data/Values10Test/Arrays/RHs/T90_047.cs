using Oomph.Data.Values10Lib.Arrays.RHs.v101;

namespace Values10Test.Arrays.RHs
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_au
	class T90_047
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();
			var t = Console.ReadLine().ToCharArray();

			var tr = Array.ConvertAll(t, c => c == 'G' ? 'B' : c == 'B' ? 'G' : c);
			var tg = Array.ConvertAll(t, c => c == 'B' ? 'R' : c == 'R' ? 'B' : c);
			var tb = Array.ConvertAll(t, c => c == 'R' ? 'G' : c == 'G' ? 'R' : c);

			var sh = new RollingHashArray<char>(s.ToCharArray());
			var thr = new RollingHashArray<char>(tr);
			var thg = new RollingHashArray<char>(tg);
			var thb = new RollingHashArray<char>(tb);

			var c = 0;
			for (var (i, j) = (-(n - 1), n - 1); i < n; i++, j--)
			{
				var h = sh.GetHashCode(Math.Max(0, i), Math.Min(n, i + n));

				var (l, r) = (Math.Max(0, j), Math.Min(n, j + n));
				if (h == thr.GetHashCode(l, r) || h == thg.GetHashCode(l, r) || h == thb.GetHashCode(l, r)) c++;
			}
			return c;
		}
	}
}
