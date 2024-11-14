using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.v301.Int4
{
	// Test: https://atcoder.jp/contests/abc247/tasks/abc247_f
	class ABC247_F
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Read();
			var q = Read();

			var uf = new UnionFind(n + 1);
			for (int i = 0; i < n; i++)
				uf.Union(p[i], q[i]);

			// 各連結成分に含まれるカードは、{ (1, 2), (2, 3), (3, 1) } のような形式。
			// 選択したカードの番号が覆う区間の長さを考えると、「n」「n - 1 (左端を選択しない)」「n - 1 (右端を選択しない)」に分類される。
			// e[n]: 区間の長さが n である場合の数 とすると、
			// e = (1, 1, 2, 3, 5, ...) (n >= 1)
			// e[i] = e[i - 1] + e[i - 2]
			// f[i] = e[i] + 2 * e[i - 1]
			// f もまたリュカ数列となる。

			var f = new long[n + 1];
			f[0] = 2;
			f[1] = 1;
			for (int i = 2; i <= n; i++)
				f[i] = (f[i - 1] + f[i - 2]) % M;

			return uf.GetGroupInfoes().Select(g => f[g.Size]).Aggregate((x, y) => x * y % M);
		}
	}
}
