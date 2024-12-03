using System;
using System.Collections.Generic;
using System.Linq;

// typed vertexes, data augmentation
// 動的に頂点を登録する方式

namespace Oomph.Data.UF09Lib.UFs.v414
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
		// 登録されている頂点の数
		public int ItemsCount => nodes.Count;
		public int UnitedCount { get; private set; }
		public int GroupsCount => nodes.Count - UnitedCount;

		public Func<TValue, TValue, TValue> MergeValues { get; }
		public bool KeepOrder { get; }
		readonly Func<TKey, TValue> createValue;

		// (parent root, child root)
		public event Action<TKey, TKey> United;

		public UnionFind(Func<TValue, TValue, TValue> mergeValues, bool keepOrder, Func<TKey, TValue> createValue)
		{
			MergeValues = mergeValues;
			KeepOrder = keepOrder;
			this.createValue = createValue;
		}

		public bool Contains(TKey x) => nodes.ContainsKey(x);
		public bool Add(TKey key, TValue value)
		{
			if (nodes.ContainsKey(key)) return false;
			nodes[key] = new Node { Key = key, Value = value };
			return true;
		}
		Node CreateNode(TKey key)
		{
			var n = new Node { Key = key, Value = createValue(key) };
			nodes[key] = n;
			return n;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(TKey x) => nodes.TryGetValue(x, out var n) ? Find(n) : null;

		public bool AreSame(TKey x, TKey y)
		{
			if (nodes.Comparer.Equals(x, y)) return true;
			var nx = Find(x);
			var ny = Find(y);
			return nx != null && nx == ny;
		}

		// 合併するときのみ、暗黙的にノードを作成します。
		public bool Union(TKey x, TKey y)
		{
			if (nodes.Comparer.Equals(x, y)) return false;
			var nx = Find(x) ?? CreateNode(x);
			var ny = Find(y) ?? CreateNode(y);
			if (nx == ny) return false;

			TValue v = default;
			// 左右の順序を保って値をマージします。
			if (KeepOrder) v = MergeValues(nx.Value, ny.Value);

			if (nx.Size < ny.Size) (nx, ny) = (ny, nx);
			ny.Parent = nx;
			nx.Size += ny.Size;
			++UnitedCount;
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
