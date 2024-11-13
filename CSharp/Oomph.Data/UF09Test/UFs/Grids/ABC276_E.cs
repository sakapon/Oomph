using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc276/tasks/abc276_e
	class ABC276_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var s = s0.SelectMany(t => t).ToArray();

			var sv = Array.IndexOf(s, 'S');
			var si = sv / w;
			var sj = sv % w;
			var uf = new UnionFind(n);

			for (int i = 0; i < h; i++)
				for (int j = 1; j < w; j++)
				{
					var v = w * i + j;
					if (s[v] == '.' && s[v - 1] == '.') uf.Union(v, v - 1);
				}

			for (int j = 0; j < w; j++)
				for (int i = 1; i < h; i++)
				{
					var v = w * i + j;
					if (s[v] == '.' && s[v - w] == '.') uf.Union(v, v - w);
				}

			var nexts = new List<int>();
			if (si > 0) nexts.Add(sv - w);
			if (si + 1 < h) nexts.Add(sv + w);
			if (sj > 0) nexts.Add(sv - 1);
			if (sj + 1 < w) nexts.Add(sv + 1);

			return nexts.Select(uf.Find).Distinct().Count() < nexts.Count;
		}
	}
}
