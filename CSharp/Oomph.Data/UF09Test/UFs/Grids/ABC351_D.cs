using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc351/tasks/abc351_d
	class ABC351_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var s = s0.SelectMany(t => t).ToArray();

			var uf = new UnionFind(n);

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					var v = w * i + j;

					if (j > 0)
					{
						if (s[v] == '#' && s[v - 1] == '.') s[v - 1] = '!';
						if (s[v] == '.' && s[v - 1] == '#') s[v] = '!';
					}

					if (i > 0)
					{
						if (s[v] == '#' && s[v - w] == '.') s[v - w] = '!';
						if (s[v] == '.' && s[v - w] == '#') s[v] = '!';
					}
				}

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					var v = w * i + j;

					if (j > 0)
					{
						if (s[v] == '.' && s[v - 1] == '.') uf.Union(v, v - 1);
					}

					if (i > 0)
					{
						if (s[v] == '.' && s[v - w] == '.') uf.Union(v, v - w);
					}
				}

			var gs = uf.GetGroupInfoes();
			var keys = Enumerable.Range(0, n).Select(v => uf.Find(v).Key).ToArray();

			var sets = new HashSet<int>[n];
			foreach (var g in gs)
			{
				if (s[g.Key] == '.')
					sets[g.Key] = new HashSet<int>();
			}

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					var v = w * i + j;

					if (j > 0)
					{
						if (s[v] == '!' && s[v - 1] == '.') sets[keys[v - 1]].Add(v);
						if (s[v] == '.' && s[v - 1] == '!') sets[keys[v]].Add(v - 1);
					}

					if (i > 0)
					{
						if (s[v] == '!' && s[v - w] == '.') sets[keys[v - w]].Add(v);
						if (s[v] == '.' && s[v - w] == '!') sets[keys[v]].Add(v - w);
					}
				}

			return gs.Max(g => g.Size + (sets[g.Key]?.Count ?? 0));
		}
	}
}
