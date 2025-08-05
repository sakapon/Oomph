
namespace Oomph.Data.Collections10Lib.HashTables.Chain.v201
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

	// Add, ContainsKey, Remove, Item[]
	// Count, DefaultValue, Comparer, Clear
	public class FixedChainHashMap<TKey, TValue>
	{
		static int HashDefault(uint key, int size) => (int)((key * 2654435769) >> 32 - size);

		public class Node
		{
			public TKey Key { get; init; }
			public TValue Value { get; set; }
			internal Node Next;
		}

		readonly int bitSize;
		readonly Node[] nodes;
		public int Count { get; private set; }
		public TValue DefaultValue { get; }
		public IEqualityComparer<TKey> Comparer { get; }
		readonly Func<uint, int, int> hashFunc;
		int Hash(TKey key) => hashFunc((uint)(key?.GetHashCode() ?? 0), bitSize);

		public FixedChainHashMap(int bitSize, TValue v0 = default, IEqualityComparer<TKey> comparer = null, Func<uint, int, int> hashFunc = null)
		{
			this.bitSize = bitSize;
			nodes = new Node[1 << bitSize];
			DefaultValue = v0;
			Comparer = comparer ?? ComparerHelper.GetDefaultEquality<TKey>();
			this.hashFunc = hashFunc ?? HashDefault;
		}

		public void Clear()
		{
			Array.Clear(nodes);
			Count = 0;
		}

		public Node GetNode(TKey key) => GetNode(key, Hash(key));
		Node GetNode(TKey key, int h)
		{
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Key, key)) return n;
			return null;
		}

		public TValue this[TKey key]
		{
			get
			{
				var n = GetNode(key);
				return n != null ? n.Value : DefaultValue;
			}
			set
			{
				var h = Hash(key);
				var n = GetNode(key, h);
				if (n == null) AddStrictly(key, value, h);
				else n.Value = value;
			}
		}

		public bool ContainsKey(TKey key)
		{
			return GetNode(key) != null;
		}

		public bool Add(TKey key, TValue value)
		{
			var h = Hash(key);
			if (GetNode(key, h) != null) return false;

			AddStrictly(key, value, h);
			return true;
		}

		void AddStrictly(TKey key, TValue value, int h)
		{
			nodes[h] = new Node { Key = key, Value = value, Next = nodes[h] };
			++Count;
		}

		public bool Remove(TKey key)
		{
			var h = Hash(key);
			for (ref var n = ref nodes[h]; n != null; n = ref n.Next)
				if (Remove(ref n, key)) return true;
			return false;
		}

		bool Remove(ref Node n, TKey key)
		{
			if (!Comparer.Equals(n.Key, key)) return false;
			n = n.Next;
			--Count;
			return true;
		}
	}
}
