using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

// fake vertexes
namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc219/tasks/abc219_e
	class ABC219_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			const int h = 4;
			const int w = 4;
			var a0 = Array.ConvertAll(new bool[h], _ => Read());

			const int n = h * w;
			var p = a0.SelectMany(t => t.Select(x => x != 0)).ToArray();

			var r = 0;
			var f = new bool[n];

			for (int x = 0; x < 1 << n; x++)
			{
				for (int v = 0; v < n; v++)
					f[v] = (x & (1 << v)) != 0;

				var ok = true;
				for (int v = 0; v < n; v++)
				{
					if (p[v] && !f[v])
					{
						ok = false;
						break;
					}
				}
				if (!ok) continue;

				var uf = new UnionFind(n + 1);

				for (int i = 0; i < h; i++)
					for (int j = 0; j < w; j++)
					{
						var v = w * i + j;

						if (j > 0)
						{
							if (f[v] == f[v - 1]) uf.Union(v, v - 1);
						}

						if (i > 0)
						{
							if (f[v] == f[v - w]) uf.Union(v, v - w);
						}

						// 外周
						if (i == 0 || j == 0 || i == h - 1 || j == w - 1)
						{
							if (!f[v]) uf.Union(v, n);
						}
					}

				if (uf.GroupsCount == 2) r++;
			}
			return r;
		}
	}
}
