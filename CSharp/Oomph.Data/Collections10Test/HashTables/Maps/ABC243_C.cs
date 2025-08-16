using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc243/tasks/abc243_c
	class ABC243_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());
			var s = Console.ReadLine();

			var map = new ChainHashMap<int, int>(n / 2, int.MaxValue);

			for (int i = 0; i < n; i++)
			{
				if (s[i] != 'R') continue;

				var (x, y) = ps[i];
				map.GetOrAddNode(y).Value.Chmin(x);
			}

			for (int i = 0; i < n; i++)
			{
				if (s[i] != 'L') continue;

				var (x, y) = ps[i];
				if (map[y] < x) return true;
			}
			return false;
		}
	}
}
