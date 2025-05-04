using System;
using System.Collections.Generic;

// int vertexes, array-based

namespace Oomph.Graphs.Trees09Lib.Trees.v100
{
	public class Tree
	{
		public int Count { get; }
		public int Root { get; }
		public List<int>[] Map { get; }
		public int[] Depths { get; }
		public int[] Parents { get; }

		public Tree(int n, int root, List<int>[] map)
		{
			Count = n;
			Root = root;
			Map = map;
			Depths = Array.ConvertAll(Map, _ => -1);
			Parents = Array.ConvertAll(Map, _ => -1);

			Depths[root] = 0;
			DFS(root);

			void DFS(int v)
			{
				foreach (var nv in Map[v])
				{
					if (nv == Parents[v]) continue;
					Depths[nv] = Depths[v] + 1;
					Parents[nv] = v;
					DFS(nv);
				}
			}
		}
	}
}
