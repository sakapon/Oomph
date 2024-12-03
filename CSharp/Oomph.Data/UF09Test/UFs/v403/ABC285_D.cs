using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v404;

namespace UF09Test.UFs.v403
{
	// Test: https://atcoder.jp/contests/abc285/tasks/abc285_d
	class ABC285_D
	{
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

			var uf = new UnionFind<string>();
			foreach (var e in es)
			{
				if (!uf.Union(e[0], e[1])) return false;
			}
			return true;
		}
	}
}
