using System;
using System.Collections.Generic;
using System.Linq;

// typed vertexes, data augmentation
// 静的に頂点を登録する方式
// 登録されていない頂点を呼び出した場合、KeyNotFoundException

namespace Oomph.Data.UF09Lib.UFs.v411
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TKey, TValue>
	{
		public class Node
		{
			public TKey Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			public TValue Value { get; set; }
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}, Value = {Value}" : $"{Key} (not root)";
		}

		readonly Dictionary<TKey, Node> nodes = new Dictionary<TKey, Node>();
		public int ItemsCount => nodes.Count;
		public int GroupsCount { get; private set; }

		public Func<TValue, TValue, TValue> MergeValues { get; }
		public bool KeepOrder { get; }

		// (parent root, child root)
		public event Action<TKey, TKey> United;

		// キーの重複可
		public UnionFind(Func<TValue, TValue, TValue> mergeValues, bool keepOrder, IEnumerable<(TKey, TValue)> collection)
		{
			if (collection != null)
				foreach (var (key, value) in collection)
					if (!nodes.ContainsKey(key)) nodes[key] = new Node { Key = key, Value = value };
			GroupsCount = nodes.Count;
			MergeValues = mergeValues;
			KeepOrder = keepOrder;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(TKey x) => Find(nodes[x]);
		public bool AreSame(TKey x, TKey y) => Find(x) == Find(y);

		public bool Union(TKey x, TKey y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			TValue v = default;
			// 左右の順序を保って値をマージします。
			if (KeepOrder) v = MergeValues(nx.Value, ny.Value);

			if (nx.Size < ny.Size) (nx, ny) = (ny, nx);
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
			// 親子の順序で値をマージします。
			if (!KeepOrder) v = MergeValues(nx.Value, ny.Value);
			nx.Value = v;
			United?.Invoke(nx.Key, ny.Key);
			return true;
		}

		// 根とサイズと値の情報のみを取得します。
		public IEnumerable<Node> GetGroupInfoes() => nodes.Values.Where(n => n.Parent == null);
		public ILookup<Node, TKey> ToGroups() => nodes.Values.ToLookup(Find, n => n.Key);
	}
}
