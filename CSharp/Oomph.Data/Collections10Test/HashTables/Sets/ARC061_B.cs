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

			var set = new ChainHashSet<(int, int)>();
			set.AddKeys(ps, true);

			var r = new long[10];
			var set2 = new ChainHashSet<(int, int)>(n);

			foreach (var (a, b) in ps)
			{
				for (int i = a - 2; i <= a; i++)
					for (int j = b - 2; j <= b; j++)
					{
						if (!(1 <= i && i <= h - 2)) continue;
						if (!(1 <= j && j <= w - 2)) continue;
						if (!set2.Add((i, j))) continue;
						r[Count(i, j)]++;
					}
			}

			r[0] = (long)(h - 2) * (w - 2) - r[1..].Sum();
			return string.Join("\n", r);

			int Count(int a, int b)
			{
				var c = 0;
				for (int i = 0; i < 3; i++)
					for (int j = 0; j < 3; j++)
						if (set.Contains((a + i, b + j))) c++;
				return c;
			}
		}
	}
}
