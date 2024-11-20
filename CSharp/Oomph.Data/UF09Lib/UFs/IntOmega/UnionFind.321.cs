using System;
using System.Collections.Generic;
using System.Linq;

// int vertexes, data augmentation (relative)
// TValue には、零元、逆元、加算が求められます。
// TValue を一般的な作用素として利用するには、(f + g)(x) = f(g(x)) となるように Addition を定義します。

namespace Oomph.Data.UF09Lib.UFs.v321
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TValue>
	{
		public class Node
		{
			public int Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			// 親を基準とした相対値
			internal TValue Value;
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}, Value = {Value}" : $"{Key} (not root), Value = {Value}";
		}

		readonly Node[] nodes;
		public int ItemsCount => nodes.Length;
		public int GroupsCount { get; private set; }
		readonly Func<TValue, TValue> inverse;
		readonly Func<TValue, TValue, TValue> add;

		// (parent root, child root)
		public event Action<int, int> United;

		public UnionFind(int n, TValue v0, Func<TValue, TValue> inverse, Func<TValue, TValue, TValue> add)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i, Value = v0 };
			GroupsCount = n;
			this.inverse = inverse;
			this.add = add;
		}

		Node Find(Node n)
		{
			if (n.Parent == null) return n;

			var r = Find(n.Parent);
			// 注意: 一般的な作用素の場合の順序
			n.Value = add(n.Value, n.Parent.Value);
			return n.Parent = r;
		}

		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y, TValue x2y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			if (nx.Size < ny.Size)
			{
				(nx, ny) = (ny, nx);
				(x, y) = (y, x);
				x2y = inverse(x2y);
			}
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
			// 注意: 一般的な作用素の場合の順序
			ny.Value = add(inverse(nodes[y].Value), add(x2y, nodes[x].Value));
			United?.Invoke(nx.Key, ny.Key);
			return true;
		}

		// 根とサイズの情報のみを取得します。
		public Node[] GetGroupInfoes() => Array.FindAll(nodes, n => n.Parent == null);
		public ILookup<Node, int> ToGroups() => nodes.ToLookup(Find, n => n.Key);

		public TValue GetX2Y(int x, int y)
		{
			if (!AreSame(x, y)) throw new InvalidOperationException($"{x} and {y} are not in the same set.");
			return add(nodes[y].Value, inverse(nodes[x].Value));
		}
		public bool Verify(int x, int y, TValue x2y) => AreSame(x, y) && EqualityComparer<TValue>.Default.Equals(nodes[y].Value, add(x2y, nodes[x].Value));
		public bool UpdateY(int x, int y, TValue x2y)
		{
			if (!AreSame(x, y)) return false;
			nodes[y].Value = add(x2y, nodes[x].Value);
			return true;
		}
	}
}
