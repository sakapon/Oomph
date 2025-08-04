using Oomph.Data.Collections10Lib.HashTables.Chain.v100;

namespace Collections10Test.HashTables.Chain
{
	// Test: https://atcoder.jp/contests/abc278/tasks/abc278_c
	class ABC278_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			var r = new List<bool>();
			var set = new ChainHashSet<(int, int)>(20);

			foreach (var (t, a, b) in qs)
			{
				if (t == 1)
				{
					set.Add((a, b));
				}
				else if (t == 2)
				{
					set.Remove((a, b));
				}
				else
				{
					r.Add(set.Contains((a, b)) && set.Contains((b, a)));
				}
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
