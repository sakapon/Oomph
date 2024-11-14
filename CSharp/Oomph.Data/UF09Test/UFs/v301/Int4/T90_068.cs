using System;
using System.Linq;
using System.Text;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bp
	class T90_068
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read4());
			var sb = new StringBuilder();

			var uf = new UnionFind(n + 1);
			var vs = new long[n + 1];
			var u = new bool[qc];

			for (int qi = 0; qi < qc; qi++)
			{
				var (t, x, y, v) = qs[qi];

				if (t == 0)
				{
					uf.Union(x, y);
					vs[x + 1] = v;
				}
				else
				{
					u[qi] = uf.AreSame(x, y);
				}
			}

			// vs[1] = 0 とした場合
			for (int i = 2; i <= n; i++)
			{
				vs[i] = vs[i] - vs[i - 1];
			}

			for (int qi = 0; qi < qc; qi++)
			{
				var (t, x, y, v) = qs[qi];

				if (t == 1)
				{
					if (u[qi])
					{
						var d = v - vs[x];
						if ((x - y) % 2 != 0) d *= -1;
						sb.AppendLine((vs[y] + d).ToString());
					}
					else
					{
						sb.AppendLine("Ambiguous");
					}
				}
			}
			Console.Write(sb);
		}
	}
}
