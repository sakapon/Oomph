using System.Numerics;

// int vertexes, data augmentation (relative)
// TValue には、零元、逆元、加算が求められます。
// TValue を一般的な作用素として利用するには、(f + g)(x) = f(g(x)) となるように Addition を定義します。

namespace Oomph.Data.UF11Lib.UFs.v321
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TValue> where TValue : IUnaryNegationOperators<TValue, TValue>, IAdditionOperators<TValue, TValue, TValue>, new()
	{
		public class Node
		{
			public int Key { get; internal set; }
			internal Node Parent;
			public int Size { get; internal set; } = 1;
			// 親を基準とした相対値
			internal TValue Value = new();
			public override string ToString() => Parent == null ? $"{Key}, Size = {Size}, Value = {Value}" : $"{Key} (not root), Value = {Value}";
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

		Node Find(Node n)
		{
			if (n.Parent == null) return n;

			var r = Find(n.Parent);
			// 注意: 一般的な作用素の場合の順序
			n.Value += n.Parent.Value;
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
				x2y = -x2y;
			}
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
			// 注意: 一般的な作用素の場合の順序
			ny.Value = -nodes[y].Value + x2y + nodes[x].Value;
			United?.Invoke(nx.Key, ny.Key);
			return true;
		}

		// 根とサイズの情報のみを取得します。
		public Node[] GetGroupInfoes() => Array.FindAll(nodes, n => n.Parent == null);
		public ILookup<Node, int> ToGroups() => nodes.ToLookup(Find, n => n.Key);

		public TValue GetX2Y(int x, int y)
		{
			if (!AreSame(x, y)) throw new InvalidOperationException($"{x} and {y} are not in the same set.");
			return nodes[y].Value + -nodes[x].Value;
		}
		public bool Verify(int x, int y, TValue x2y) => AreSame(x, y) && EqualityComparer<TValue>.Default.Equals(nodes[y].Value, x2y + nodes[x].Value);
	}
}
