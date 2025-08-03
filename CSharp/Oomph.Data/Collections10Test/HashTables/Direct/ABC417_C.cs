using Oomph.Data.Collections10Lib.HashTables.Direct.v101;

namespace Collections10Test.HashTables.Direct
{
	// Test: https://atcoder.jp/contests/abc417/tasks/abc417_c
	class ABC417_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var max = 400000;
			var ci = new DirectMap<int>(max);
			var cj = new DirectMap<int>(max);

			for (int i = 0; i < n; i++)
			{
				ci[i + a[i]]++;
				if (i >= a[i]) cj[i - a[i]]++;
			}
			return Enumerable.Range(0, max).Sum(i => (long)ci[i] * cj[i]);
		}
	}
}
