using System;
using System.Linq;

// int vertexes, node-based, from 103

namespace Oomph.Data.UF09Lib.UFs.v201
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
		public class Node
		{
			public int Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
		}

		readonly Node[] nodes;
		public int ItemsCount => nodes.Length;
		public int GroupsCount { get; private set; }

		public UnionFind(int n)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i };
			GroupsCount = n;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			if (nx.Size < ny.Size) (nx, ny) = (ny, nx);
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
			return true;
		}

		public ILookup<Node, int> ToGroups() => nodes.ToLookup(Find, n => n.Key);
	}
}
