using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Static3
{
	// Test: https://atcoder.jp/contests/arc027/tasks/arc027_2
	class ARC027_B
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();
			var t = Console.ReadLine();

			var st = s + t;
			var rn = Enumerable.Range(0, n).ToArray();

			// 数字が確定しているインデックス
			var u = rn.Select(i => char.IsNumber(s[i]) || char.IsNumber(t[i])).ToArray();

			var uf = new UnionFind(n);
			foreach (var g in Enumerable.Range(0, 2 * n).GroupBy(i => st[i]))
			{
				if (char.IsNumber(g.Key)) continue;

				var a = g.Select(i => i < n ? i : i - n).ToArray();
				for (int j = 1; j < a.Length; j++)
					uf.Union(a[j - 1], a[j]);
			}

			var r = 1L;
			foreach (var g in uf.ToGroups())
			{
				if (g.Any(i => u[i])) continue;
				r *= g.Contains(0) ? 9 : 10;
			}
			return r;
		}
	}
}
