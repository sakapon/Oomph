using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc045/tasks/arc061_b
	class ARC061_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w, n) = Read3();
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var map = new ChainHashMap<(int, int), int>(9 * n);

			foreach (var (a, b) in ps)
			{
				for (int i = a - 2; i <= a; i++)
					for (int j = b - 2; j <= b; j++)
					{
						if (!(1 <= i && i <= h - 2)) continue;
						if (!(1 <= j && j <= w - 2)) continue;
						map[(i, j)]++;
					}
			}

			var r = new long[10];
			foreach (var c in map.GetValues())
				r[c]++;
			r[0] = (long)(h - 2) * (w - 2) - r[1..].Sum();

			return string.Join("\n", r);
		}
	}
}
