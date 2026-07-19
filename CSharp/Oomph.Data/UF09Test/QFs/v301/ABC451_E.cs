using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.QFs.v301;

namespace UF09Test.QFs.v301
{
	// Test: https://atcoder.jp/contests/abc451/tasks/abc451_e
	class ABC451_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n - 1], _ => Read());

			a = a.Append(new int[0])
				.Select((row, i) => new int[i + 1].Concat(row).ToArray())
				.ToArray();

			int GetDistance(int u, int v)
			{
				if (u > v) (u, v) = (v, u);
				return a[u][v];
			}

			var es = new List<(int d, int u, int v)>();
			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					es.Add((a[i][j], i, j));

			var uf = new QuickFind(n);
			foreach (var (d, u, v) in es.OrderBy(e => e.d))
			{
				if (uf.AreSame(u, v)) continue;

				foreach (var uu in uf.Find(u).Items)
					foreach (var vv in uf.Find(v).Items)
					{
						var expected = GetDistance(uu, vv);
						var actual = GetDistance(uu, u) + d + GetDistance(v, vv);
						if (expected != actual) return false;
					}

				uf.Union(u, v);
			}
			return true;
		}
	}
}
