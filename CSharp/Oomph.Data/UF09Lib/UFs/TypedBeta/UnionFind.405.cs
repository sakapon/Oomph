using System;
using System.Collections.Generic;
using System.Linq;

// typed vertexes
// 静的に頂点を登録する方式

namespace Oomph.Data.UF09Lib.UFs.v405
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TKey>
	{
		public class Node
		{
			public TKey Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}" : $"{Key} (not root)";
		}

		readonly Dictionary<TKey, Node> nodes = new Dictionary<TKey, Node>();
		public int ItemsCount => nodes.Count;
		public int GroupsCount { get; private set; }

		// (parent root, child root)
		public event Action<TKey, TKey> United;

		// キーの重複可
		public UnionFind(IEnumerable<TKey> keys)
		{
			foreach (var key in keys)
				if (!nodes.ContainsKey(key)) nodes[key] = new Node { Key = key };
			GroupsCount = nodes.Count;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(TKey x) => Find(nodes[x]);
		public bool AreSame(TKey x, TKey y) => Find(x) == Find(y);

		public bool Union(TKey x, TKey y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			if (nx.Size < ny.Size) (nx, ny) = (ny, nx);
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
			United?.Invoke(nx.Key, ny.Key);
			return true;
		}

		// 根とサイズの情報のみを取得します。
		public IEnumerable<Node> GetGroupInfoes() => nodes.Values.Where(n => n.Parent == null);
		public ILookup<Node, TKey> ToGroups() => nodes.Values.ToLookup(Find, n => n.Key);
	}
}
