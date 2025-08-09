using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc343/tasks/abc343_d
	class ABC343_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, t) = Read2();
			var ps = Array.ConvertAll(new bool[t], _ => Read2());

			var r = new List<int>();
			var c = 1;

			var s = new long[n + 1];
			var d = new ChainHashMap<long, int>(t);
			d[0] = n;

			foreach (var (a, b) in ps)
			{
				var p0 = s[a];
				var p1 = s[a] += b;

				if (--d[p0] == 0) c--;
				if (d[p1]++ == 0) c++;

				r.Add(c);
			}
			return string.Join("\n", r);
		}
	}
}
