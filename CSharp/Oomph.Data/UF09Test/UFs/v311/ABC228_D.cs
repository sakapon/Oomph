using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc228/tasks/abc228_d
	class ABC228_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2L());

			const int n = 1 << 20;
			var a = new long[n];
			Array.Fill(a, -1);
			var rn = Enumerable.Range(0, n).ToArray();

			var r = new List<long>();
			// a[i] == -1 である最も近い右のインデックス i
			var uf = new UnionFind<int>(n, (x, y) => y, true, rn);

			foreach (var (t, x) in qs)
			{
				if (t == 1)
				{
					var h = uf.Find((int)(x % n)).Value;
					a[h] = x;
					uf.Union(h, (h + 1) % n);
				}
				else
				{
					r.Add(a[x % n]);
				}
			}
			return string.Join("\n", r);
		}
	}
}
