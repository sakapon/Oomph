using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.Grids
{
	// Test: https://atcoder.jp/contests/abc370/tasks/abc370_d
	class ABC370_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w, qc) = Read3();
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			static (int, int) Merge((int m, int M) x, (int m, int M) y) => (Math.Min(x.m, y.m), Math.Max(x.M, y.M));

			var iv0 = Enumerable.Range(0, w).Select(j => (j, j)).ToArray();
			var iv1 = Enumerable.Range(0, h).Select(i => (i, i)).ToArray();

			var u = Array.ConvertAll(new bool[h], _ => new bool[w]);
			var yoko = Array.ConvertAll(new bool[h], _ => new UnionFind<(int, int)>(w, Merge, false, iv0));
			var tate = Array.ConvertAll(new bool[w], _ => new UnionFind<(int, int)>(h, Merge, false, iv1));

			void Bomb(int r, int c)
			{
				u[r][c] = true;

				var t = c - 1;
				if (t >= 0 && u[r][t]) yoko[r].Union(c, t);
				t = c + 1;
				if (t < w && u[r][t]) yoko[r].Union(c, t);

				t = r - 1;
				if (t >= 0 && u[t][c]) tate[c].Union(r, t);
				t = r + 1;
				if (t < h && u[t][c]) tate[c].Union(r, t);
			}

			foreach (var q in qs)
			{
				var (r, c) = q;
				r--;
				c--;

				if (u[r][c])
				{
					var (m, M) = yoko[r].Find(c).Value;
					m--;
					M++;
					if (m >= 0) Bomb(r, m);
					if (M < w) Bomb(r, M);

					(m, M) = tate[c].Find(r).Value;
					m--;
					M++;
					if (m >= 0) Bomb(m, c);
					if (M < h) Bomb(M, c);
				}
				else
				{
					Bomb(r, c);
				}
			}
			return u.Sum(t => t.Count(b => !b));
		}
	}
}
