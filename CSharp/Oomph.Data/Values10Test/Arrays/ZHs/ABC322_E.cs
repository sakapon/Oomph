using Oomph.Data.Values10Lib.Arrays.ZHs.v100;

namespace Values10Test.Arrays.ZHs
{
	// Test: https://atcoder.jp/contests/abc322/tasks/abc322_e
	class ABC322_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k, p) = Read3();
			var ps = Array.ConvertAll(new bool[n], _ => Read());

			var dp = new Dictionary<int, (int[] a, long c)>();
			var ia = new int[k];
			dp[Hash(ia)] = (ia, 0);

			foreach (var ca in ps)
			{
				var c = ca[0];
				var a = ca[1..];

				foreach (var (h, ac) in dp.ToArray())
				{
					var (a0, c0) = ac;
					var na = Add(a0, a, p);
					var nc = c0 + c;

					var nh = Hash(na);
					if (!dp.TryGetValue(nh, out var nac0) || nac0.c > nc)
						dp[nh] = (na, nc);
				}
			}

			Array.Fill(ia, p);
			return dp.GetValueOrDefault(Hash(ia), (null, -1)).c;
		}

		static int Hash<T>(T[] a) => new ZobristHashArray<T>(a).GetHashCode();

		static int[] Add(int[] v1, int[] v2, int max)
		{
			var r = new int[v1.Length];
			for (int i = 0; i < v1.Length; ++i)
			{
				r[i] = v1[i] + v2[i];
				if (r[i] > max) r[i] = max;
			}
			return r;
		}
	}
}
