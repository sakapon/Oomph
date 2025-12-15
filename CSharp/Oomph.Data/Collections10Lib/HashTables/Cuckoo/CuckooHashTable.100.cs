using Oomph.Data.Collections10Lib.HashTables.Chain;

namespace Oomph.Data.Collections10Lib.HashTables.Cuckoo.v100
{
	public static class ComparerHelper
	{
		public static IEqualityComparer<T> GetDefaultEquality<T>()
		{
			// Speeds up a string comparison that is independent of language.
			if (typeof(T) == typeof(string)) return (IEqualityComparer<T>)StringComparer.Ordinal;
			return EqualityComparer<T>.Default;
		}
	}

	// Add, Contains, Remove
	// Count, Comparer, Clear
	public class CuckooHashSet<TKey>
	{
		public class Node
		{
			public TKey Item { get; init; }
		}

		class Container
		{
			readonly int bitSize;
			readonly Node[] nodes;
			public IEqualityComparer<TKey> Comparer { get; }
			readonly Func<uint, int, uint> hashFunc = HashFuncs.CreateUniversal();
			int Hash(TKey key) => (int)hashFunc((uint)(key?.GetHashCode() ?? 0), bitSize);

			public Container(int bitSize, IEqualityComparer<TKey> comparer)
			{
				this.bitSize = bitSize;
				nodes = new Node[1 << bitSize];
				Comparer = comparer;
			}

			public void Clear()
			{
				Array.Clear(nodes);
			}

			public bool Contains(TKey key)
			{
				var h = Hash(key);
				var node = nodes[h];
				return node != null && Comparer.Equals(node.Item, key);
			}

			public bool AddIfEmpty(Node node)
			{
				var h = Hash(node.Item);
				ref var n = ref nodes[h];
				if (n == null)
				{
					n = node;
					return true;
				}
				return false;
			}

			// 追い出されたノードを返します。
			public Node Push(Node node)
			{
				var h = Hash(node.Item);
				var n0 = nodes[h];
				nodes[h] = node;
				return n0;
			}

			public bool Remove(TKey key)
			{
				var h = Hash(key);
				ref var node = ref nodes[h];
				if (node != null && Comparer.Equals(node.Item, key))
				{
					node = null;
					return true;
				}
				return false;
			}
		}

		readonly Container container1, container2;
		public int Count { get; private set; }

		public CuckooHashSet(int bitSize, IEqualityComparer<TKey> comparer = null)
		{
			comparer ??= ComparerHelper.GetDefaultEquality<TKey>();
			container1 = new Container(bitSize, comparer);
			container2 = new Container(bitSize, comparer);
		}

		Container GetOtherContainer(Container container) => container == container1 ? container2 : container1;

		public void Clear()
		{
			container1.Clear();
			container2.Clear();
			Count = 0;
		}

		public bool Contains(TKey key)
		{
			return container1.Contains(key) || container2.Contains(key);
		}

		public bool Add(TKey key)
		{
			if (Contains(key)) return false;
			var node = new Node { Item = key };
			if (!container1.AddIfEmpty(node))
				Push(node, container2);
			++Count;
			return true;
		}

		void Push(Node node, Container container)
		{
			var n = container.Push(node);
			if (n != null) Push(n, GetOtherContainer(container));
		}

		public bool Remove(TKey key)
		{
			var r = container1.Remove(key) || container2.Remove(key);
			if (r) --Count;
			return r;
		}
	}
}
