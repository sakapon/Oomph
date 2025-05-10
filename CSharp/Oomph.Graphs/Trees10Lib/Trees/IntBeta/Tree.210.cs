using Oomph.Graphs.Trees10Lib.Common.Arrays;

// int vertexes, array-based

namespace Oomph.Graphs.Trees10Lib.Trees.v210
{
	public class Tree
	{
		public int Count => Map.Length;
		public List<int>[] Map { get; }
		public int Root { get; private set; }
		public int[] Parents { get; }
		public int[] Depths { get; }

		public Tree(UnweightedGraph g, int root)
		{
			Map = g.Map;
			Parents = Array.ConvertAll(Map, _ => -1);
			Depths = Array.ConvertAll(Map, _ => -1);
			Reroot(root);
		}

		void Reroot(int root)
		{
			Root = root;
			Parents[root] = -1;
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

		public IEnumerable<int> GetPath(int v)
		{
			for (; v != -1; v = Parents[v]) yield return v;
		}
	}
}
