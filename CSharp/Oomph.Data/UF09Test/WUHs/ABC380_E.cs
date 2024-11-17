using System;
using System.Collections.Generic;
using System.Linq;

namespace UF09Test.WUHs
{
	// Test: https://atcoder.jp/contests/abc380/tasks/abc380_e
	class ABC380_E
	{
		class Group
		{
			public int Left, Right, Color;
			public int Size => Right - Left + 1;
			public Group(int i) => Left = Right = Color = i;
		}

		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var r = new List<int>();

			// cell id -> group
			var gs = Enumerable.Range(0, n + 2).Select(i => new Group(i)).ToArray();

			// color id -> count
			var counts = new int[n + 2];
			Array.Fill(counts, 1);

			void Union(Group gl, Group gr)
			{
				var m = gl.Left;
				var M = gr.Right;

				if (gl.Size < gr.Size) (gl, gr) = (gr, gl);
				for (int i = gr.Left; i <= gr.Right; i++)
					gs[i] = gl;

				gl.Left = gr.Left = m;
				gl.Right = gr.Right = M;
			}

			foreach (var q in qs)
			{
				if (q[0] == 1)
				{
					var (x, c) = (q[1], q[2]);
					var g = gs[x];

					counts[g.Color] -= g.Size;
					counts[c] += g.Size;
					g.Color = c;

					var gl = gs[g.Left - 1];
					var gr = gs[g.Right + 1];

					if (gl.Color == c) Union(gl, gs[x]);
					if (gr.Color == c) Union(gs[x], gr);
				}
				else
				{
					var c = q[1];
					r.Add(counts[c]);
				}
			}
			return string.Join("\n", r);
		}
	}
}
