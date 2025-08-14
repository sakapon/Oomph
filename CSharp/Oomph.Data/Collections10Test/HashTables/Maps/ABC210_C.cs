using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc210/tasks/abc210_c
	class ABC210_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k) = Read2();
			var c = Read();

			var r = 0;
			var map = new ChainHashMap<int, int>(n);

			for (int i = 0; i < n; i++)
			{
				map[c[i]]++;
				if (i >= k)
				{
					var node = map.GetNode(c[i - k]);
					if (--node.Value == 0) map.Remove(c[i - k]);
				}
				r.Chmax(map.Count);
			}
			return r;
		}
	}

	public static class MathEx
	{
		public static int Chmax(this ref int x, int v) => x < v ? x = v : x;
		public static int Chmin(this ref int x, int v) => x > v ? x = v : x;
		public static T Chmax<T>(this ref T x, T v) where T : struct, IComparable<T> => x.CompareTo(v) < 0 ? x = v : x;
		public static T Chmin<T>(this ref T x, T v) where T : struct, IComparable<T> => x.CompareTo(v) > 0 ? x = v : x;
	}
}
