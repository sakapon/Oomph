// int vertexes, node-based

namespace Oomph.Graphs.Trees10Lib.Trees.v101
{
	public class Tree
	{
		public class Node
		{
			public int Id { get; }
			public List<Node> Nexts { get; } = new List<Node>();
			public Node Parent { get; internal set; }
			public int Depth { get; internal set; } = -1;

			public Node(int id)
			{
				Id = id;
			}
		}

		public Node[] Nodes { get; }
		public int Count => Nodes.Length;
		public Node Root { get; }

		public Tree(int n, (int u, int v)[] edges, int root)
		{
			Nodes = new Node[n];
			for (int v = 0; v < n; ++v)
				Nodes[v] = new Node(v);

			foreach (var (u, v) in edges)
			{
				Nodes[u].Nexts.Add(Nodes[v]);
				Nodes[v].Nexts.Add(Nodes[u]);
			}

			Root = Nodes[root];
			Root.Depth = 0;
			DFS(Root);
		}

		static void DFS(Node v)
		{
			foreach (var nv in v.Nexts)
			{
				if (nv == v.Parent) continue;
				nv.Parent = v;
				nv.Depth = v.Depth + 1;
				DFS(nv);
			}
		}
	}
}
