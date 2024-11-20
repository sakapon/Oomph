using System.Numerics;
using System.Text;
using Oomph.Data.UF11Lib.UFs.v321;

namespace UF11Test.UFs.v321
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bp
	class T90_068
	{
		struct Op : IUnaryNegationOperators<Op, Op>, IAdditionOperators<Op, Op, Op>
		{
			public int a;
			public long b;

			public Op() : this(1, 0) { }
			public Op(int a, long b)
			{
				this.a = a;
				this.b = b;
			}

			public readonly long GetValue(long x) => a * x + b;
			public static Op operator -(Op f) => new(1 / f.a, -f.b / f.a);
			public static Op operator +(Op f, Op g) => new(f.a * g.a, f.a * g.b + f.b);
		}

		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read4());
			var sb = new StringBuilder();

			var uf = new UnionFind<Op>(n + 1);

			foreach (var (t, x, y, v) in qs)
			{
				if (t == 0)
				{
					uf.Union(x, y, new Op(-1, v));
				}
				else
				{
					if (uf.AreSame(x, y))
					{
						var f = uf.GetX2Y(x, y);
						sb.AppendLine(f.GetValue(v).ToString());
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
