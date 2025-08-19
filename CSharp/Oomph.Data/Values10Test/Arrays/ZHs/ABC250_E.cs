using Oomph.Data.Values10Lib.Arrays.ZHs.v100;

namespace Values10Test.Arrays.ZHs
{
	// Test: https://atcoder.jp/contests/abc250/tasks/abc250_e
	class ABC250_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();
			var b = Read();
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var aset = new ZobristHashSet<int>();
			var bset = new ZobristHashSet<int>();
			var ahash = new int[n + 1];
			var bhash = new int[n + 1];

			for (int i = 0; i < n; i++)
			{
				aset.Add(a[i]);
				bset.Add(b[i]);
				ahash[i + 1] = aset.GetHashCode();
				bhash[i + 1] = bset.GetHashCode();
			}

			var r = qs.Select(q => ahash[q.x] == bhash[q.y] ? "Yes" : "No");
			return string.Join("\n", r);
		}
	}
}
