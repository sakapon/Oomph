﻿using System;
using System.Collections.Generic;
using System.Linq;

// typed vertexes
// 動的に頂点を登録する方式

namespace Oomph.Data.UF09Lib.UFs.v403
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

		readonly Dictionary<TKey, Node> nodes = new Dictionary<TKey, Node>();
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
		Node CreateNode(TKey key)
		{
			var n = new Node { Key = key };
			nodes[key] = n;
			return n;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(TKey x) => nodes.TryGetValue(x, out var n) ? Find(n) : null;

		public bool AreSame(TKey x, TKey y)
		{
			if (nodes.Comparer.Equals(x, y)) return true;
			var nrx = Find(x);
			var nry = Find(y);
			return nrx != null && nrx == nry;
		}

		public bool Union(TKey x, TKey y)
		{
			if (nodes.Comparer.Equals(x, y)) return false;
			var rx = Find(x) ?? CreateNode(x);
			var ry = Find(y) ?? CreateNode(y);
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