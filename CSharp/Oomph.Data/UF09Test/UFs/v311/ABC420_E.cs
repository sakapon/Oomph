using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc420/tasks/abc420_e
	class ABC420_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var r = new List<bool>();
			var bs = new bool[n + 1];

			// count of black vertices
			var uf = new UnionFind<int>(n + 1, (x, y) => x + y, false);

			foreach (var q in qs)
			{
				if (q[0] == 1)
				{
					uf.Union(q[1], q[2]);
				}
				else if (q[0] == 2)
				{
					var v = q[1];
					var d = bs[v] ? -1 : 1;
					uf.Find(v).Value += d;
					bs[v] ^= true;
				}
				else
				{
					var v = q[1];
					r.Add(uf.Find(v).Value > 0);
				}
			}
			return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
		}
	}
}
