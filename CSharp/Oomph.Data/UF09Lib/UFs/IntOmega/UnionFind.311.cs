using System;
using System.Linq;

// int vertexes, data augmentation

namespace Oomph.Data.UF09Lib.UFs.v311
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TValue>
	{
		public class Node
		{
			public int Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			public TValue Value { get; set; }
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}, Value = {Value}" : $"{Key} (not root)";
		}

		readonly Node[] nodes;
		public int ItemsCount => nodes.Length;
		public int GroupsCount { get; private set; }
		public Func<TValue, TValue, TValue> MergeValues { get; }

		public UnionFind(int n, Func<TValue, TValue, TValue> mergeValues, TValue[] values = null)
		{
			values ??= new TValue[n];
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i, Value = values[i] };
			GroupsCount = n;
			MergeValues = mergeValues;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry) return false;

			// 左右の順序を保って値をマージします。
			var v = MergeValues(rx.Value, ry.Value);

			if (rx.Size < ry.Size) (rx, ry) = (ry, rx);
			ry.Parent = rx;
			rx.Size += ry.Size;
			--GroupsCount;
			rx.Value = v;
			return true;
		}

		// 根とサイズと値の情報のみを取得します。
		public Node[] GetSetInfoes() => Array.FindAll(nodes, n => n.Parent == null);
		public ILookup<Node, int> ToGroups() => nodes.ToLookup(Find, n => n.Key);
	}
}
