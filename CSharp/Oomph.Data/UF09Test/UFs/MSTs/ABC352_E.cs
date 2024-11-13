using System;
using System.Collections.Generic;
using System.Linq;
using Oomph.Data.UF09Lib.UFs.v301;

namespace UF09Test.UFs.MSTs
{
	// Test: https://atcoder.jp/contests/abc352/tasks/abc352_e
	class ABC352_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var ps = Array.ConvertAll(new bool[m], _ => (c: Read()[1], a: Read()));

			var r = 0L;
			var uf = new UnionFind(n + 1);
			foreach (var (c, a) in ps.OrderBy(p => p.c))
			{
				for (int i = 1; i < a.Length; i++)
				{
					if (uf.Union(a[0], a[i])) r += c;
				}
			}
			return uf.GroupsCount == 2 ? r : -1;
		}
	}
}
