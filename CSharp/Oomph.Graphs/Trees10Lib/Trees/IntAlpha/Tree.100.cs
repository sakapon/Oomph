// int vertexes, array-based

namespace Oomph.Graphs.Trees10Lib.Trees.v100
{
	public class Tree
	{
		public int Count { get; }
		public List<int>[] Map { get; }
		public int Root { get; }
		public int[] Parents { get; }
		public int[] Depths { get; }

		public Tree(int n, List<int>[] map, int root)
		{
			Count = n;
			Map = map;
			Root = root;
			Parents = Array.ConvertAll(Map, _ => -1);
			Depths = Array.ConvertAll(Map, _ => -1);

			Depths[root] = 0;
			DFS(root);
		}

		void DFS(int v)
		{
			foreach (var nv in Map[v])
			{
				if (nv == Parents[v]) continue;
				Parents[nv] = v;
				Depths[nv] = Depths[v] + 1;
				DFS(nv);
			}
		}
	}
}
