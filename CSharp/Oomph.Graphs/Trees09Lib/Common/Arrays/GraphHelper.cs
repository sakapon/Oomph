using System;
using System.Collections.Generic;

namespace Oomph.Graphs.Trees09Lib.Common.Arrays
{
	public static class UnweightedGraphHelper
	{
		public static int[][] ToMap(int n, (int u, int v)[] edges, bool twoway) => Array.ConvertAll(ToListMap(n, edges, twoway), l => l.ToArray());
		public static List<int>[] ToListMap(int n, (int u, int v)[] edges, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in edges)
			{
				map[u].Add(v);
				if (twoway) map[v].Add(u);
			}
			return map;
		}
	}

	public static class WeightedGraphHelper
	{
		public static (int, int)[][] ToMap(int n, (int u, int v, int w)[] edges, bool twoway) => Array.ConvertAll(ToListMap(n, edges, twoway), l => l.ToArray());
		public static List<(int, int)>[] ToListMap(int n, (int u, int v, int w)[] edges, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<(int, int)>());
			foreach (var (u, v, w) in edges)
			{
				map[u].Add((v, w));
				if (twoway) map[v].Add((u, w));
			}
			return map;
		}
	}
}
