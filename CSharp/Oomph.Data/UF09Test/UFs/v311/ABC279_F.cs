using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v311;

namespace UF09Test.UFs.v311
{
	// Test: https://atcoder.jp/contests/abc279/tasks/abc279_f
	class ABC279_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var r = new List<int>();
			// box id -> one of balls
			var balls = Enumerable.Range(0, n + qc + 1).ToArray();
			// key: ball id, value: box id
			var uf = new UnionFind<int>(n + qc + 1, (x, y) => x, true, balls);
			var k = n;

			void UnionBoxes(int x, int y)
			{
				if (balls[y] == -1) return;

				if (balls[x] == -1)
				{
					uf.Find(balls[y]).Value = x;
				}
				else
				{
					uf.Union(balls[x], balls[y]);
				}
				balls[x] = balls[y];
				balls[y] = -1;
			}

			foreach (var q in qs)
			{
				var x = q[1];

				if (q[0] == 1)
				{
					UnionBoxes(x, q[2]);
				}
				else if (q[0] == 2)
				{
					UnionBoxes(x, ++k);
				}
				else
				{
					r.Add(uf.Find(x).Value);
				}
			}
			return string.Join("\n", r);
		}
	}
}
