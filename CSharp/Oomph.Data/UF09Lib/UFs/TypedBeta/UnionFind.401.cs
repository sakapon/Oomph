﻿using System;
using System.Linq;

// int vertexes

namespace Oomph.Data.UF09Lib.UFs.v401
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind
	{
		public class Node
		{
			public int Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}" : $"{Key} (not root)";
		}

		readonly Node[] nodes;
		public int ItemsCount => nodes.Length;
		public int GroupsCount { get; private set; }

		// (parent root, child root)
		public event Action<int, int> United;

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
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry) return false;

			if (rx.Size < ry.Size) (rx, ry) = (ry, rx);
			ry.Parent = rx;
			rx.Size += ry.Size;
			--GroupsCount;
			United?.Invoke(rx.Key, ry.Key);
			return true;
		}

		// 根とサイズの情報のみを取得します。
		public Node[] GetGroupInfoes() => Array.FindAll(nodes, n => n.Parent == null);
		public ILookup<Node, int> ToGroups() => nodes.ToLookup(Find, n => n.Key);
	}
}