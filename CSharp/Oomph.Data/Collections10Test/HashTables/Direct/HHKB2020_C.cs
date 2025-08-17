using Oomph.Data.Collections10Lib.HashTables.Direct.v201;

namespace Collections10Test.HashTables.Direct
{
	// Test: https://atcoder.jp/contests/hhkb2020/tasks/hhkb2020_c
	class HHKB2020_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Read();

			var max = 200000;
			var set = new DirectSet(max + 1);
			for (int i = 0; i <= max; i++)
				set.Add(i);

			var r = new List<int>();
			var m = 0;
			foreach (var v in p)
			{
				set.Remove(v);
				if (m == v)
					m = Enumerable.Range(m + 1, max).First(set.Contains);
				r.Add(m);
			}
			return string.Join("\n", r);
		}
	}
}
