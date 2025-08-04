using System.Text;
using Oomph.Data.Collections10Lib.HashTables.Chain.v100;

namespace Collections10Test.HashTables.Chain
{
	// Test: https://atcoder.jp/contests/abc278/tasks/abc278_c
	class ABC278_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main()
		{
			var (n, qc) = Read2();

			var sb = new StringBuilder();
			var set = new ChainHashSet<(int, int)>(20);

			while (qc-- > 0)
			{
				var (t, a, b) = Read3();

				if (t == 1)
				{
					set.Add((a, b));
				}
				else if (t == 2)
				{
					set.Remove((a, b));
				}
				else
				{
					sb.AppendLine(set.Contains((a, b)) && set.Contains((b, a)) ? "Yes" : "No");
				}
			}
			Console.Write(sb);
		}
	}
}
