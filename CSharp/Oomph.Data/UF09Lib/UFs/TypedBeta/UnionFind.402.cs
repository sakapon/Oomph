using System;
using System.Collections.Generic;
using System.Linq;

// typed vertexes
// 動的に頂点を登録する方式

namespace Oomph.Data.UF09Lib.UFs.v402
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, UnitedCount = {UnitedCount}")]
	public class UnionFind<TKey>
	{
		public class Node
		{
			public TKey Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}" : $"{Key} (not root)";
		}

		// 問合せ時に、暗黙的にノードを作成します。
		readonly Dictionary<TKey, Node> nodes = new Dictionary<TKey, Node>();
		Node GetNode(TKey key)
		{
			if (!nodes.TryGetValue(key, out var n))
				nodes[key] = n = new Node { Key = key };
			return n;
		}

		// 登録されている頂点の数
		public int ItemsCount => nodes.Count;
		public int UnitedCount { get; private set; }

		// (parent root, child root)
		public event Action<TKey, TKey> United;

		public bool Contains(TKey x) => nodes.ContainsKey(x);
		public bool Add(TKey x)
		{
			if (nodes.ContainsKey(x)) return false;
			nodes[x] = new Node { Key = x };
			return true;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(TKey x) => Find(GetNode(x));
		public bool AreSame(TKey x, TKey y) => Find(x) == Find(y);

		public bool Union(TKey x, TKey y)
		{
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry) return false;

			if (rx.Size < ry.Size) (rx, ry) = (ry, rx);
			ry.Parent = rx;
			rx.Size += ry.Size;
			++UnitedCount;
			United?.Invoke(rx.Key, ry.Key);
			return true;
		}

		// 根とサイズの情報のみを取得します。
		public IEnumerable<Node> GetGroupInfoes() => nodes.Values.Where(n => n.Parent == null);
		public ILookup<Node, TKey> ToGroups() => nodes.Values.ToLookup(Find, n => n.Key);
	}
}
