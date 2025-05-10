using System;
using System.Collections.Generic;

namespace Oomph.Graphs.Trees10Lib.Common.Arrays
{
	public class UnweightedGraph
	{
		public int Count => Map.Length;
		public List<int>[] Map { get; }

		public UnweightedGraph(int n) => Map = Array.ConvertAll(new bool[n], _ => new List<int>());
		public UnweightedGraph(int n, (int u, int v)[] edges, bool twoway) : this(n)
		{
			foreach (var (u, v) in edges) AddEdge(u, v, twoway);
		}

		public void AddEdge(int u, int v, bool twoway)
		{
			Map[u].Add(v);
			if (twoway) Map[v].Add(u);
		}
	}

	public class WeightedGraph
	{
		public int Count => Map.Length;
		public List<(int, int)>[] Map { get; }

		public WeightedGraph(int n) => Map = Array.ConvertAll(new bool[n], _ => new List<(int, int)>());
		public WeightedGraph(int n, (int u, int v, int w)[] edges, bool twoway) : this(n)
		{
			foreach (var (u, v, w) in edges) AddEdge(u, v, w, twoway);
		}

		public void AddEdge(int u, int v, int w, bool twoway)
		{
			Map[u].Add((v, w));
			if (twoway) Map[v].Add((u, w));
		}
	}
}
