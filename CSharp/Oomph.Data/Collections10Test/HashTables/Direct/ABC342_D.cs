using Oomph.Data.Collections10Lib.HashTables.Direct.v101;

namespace Collections10Test.HashTables.Direct
{
	// Test: https://atcoder.jp/contests/abc342/tasks/abc342_d
	class ABC342_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var r = 0L;
			var map = new DirectMap<int>(200000);

			for (int i = 0; i < n; i++)
			{
				ref var v = ref a[i];

				if (v == 0)
				{
					r += i;
					map[0]++;
				}
				else
				{
					for (int x = 2; x * x <= v; x++)
						while (v % (x * x) == 0)
							v /= x * x;
					r += map[0] + map[v]++;
				}
			}
			return r;
		}
	}
}
