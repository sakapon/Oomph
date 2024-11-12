using System;
using System.Collections.Generic;
using System.Linq;

// int vertexes, undo
// O(log n)

namespace Oomph.Data.UF09Lib.UFs.v302
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
		readonly Stack<(int rx, int ry)> history = new Stack<(int rx, int ry)>();

		// (parent root, child root)
		public event Action<int, int> United;
		public event Action<int, int> Undone;

		public UnionFind(int n)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i };
			GroupsCount = n;
		}

		// 経路圧縮なし
		Node Find(Node n) => n.Parent == null ? n : Find(n.Parent);
		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		// 合併しない場合も履歴を記録します。
		public bool Union(int x, int y)
		{
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry)
			{
				history.Push((rx.Key, ry.Key));
				return false;
			}

			if (rx.Size < ry.Size) (rx, ry) = (ry, rx);
			ry.Parent = rx;
			rx.Size += ry.Size;
			--GroupsCount;
			history.Push((rx.Key, ry.Key));
			United?.Invoke(rx.Key, ry.Key);
			return true;
		}

		// 合併しない場合も履歴が記録されているため、分離されるとは限りません。
		public bool Undo() => Undo(out var _, out var _);
		public bool Undo(out int rx, out int ry)
		{
			if (history.Count == 0)
			{
				rx = ry = -1;
				return false;
			}

			(rx, ry) = history.Pop();
			if (rx == ry) return false;

			var nrx = nodes[rx];
			var nry = nodes[ry];
			nry.Parent = null;
			nrx.Size -= nry.Size;
			++GroupsCount;
			Undone?.Invoke(rx, ry);
			return true;
		}

		// 根とサイズの情報のみを取得します。
		public Node[] GetGroupInfoes() => Array.FindAll(nodes, n => n.Parent == null);
		public ILookup<Node, int> ToGroups() => nodes.ToLookup(Find, n => n.Key);
	}
}
