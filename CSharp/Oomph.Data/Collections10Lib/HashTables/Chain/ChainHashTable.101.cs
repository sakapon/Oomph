
namespace Oomph.Data.Collections10Lib.HashTables.Chain.v101
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
	public class ChainHashMap<TKey, TValue>
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

		public ChainHashMap(int bitSize, TValue v0 = default, IEqualityComparer<TKey> comparer = null, Func<uint, int, int> hashFunc = null)
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

		Node GetNode(TKey key)
		{
			var h = Hash(key);
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
				var n = GetNode(key);
				if (n == null) Add(key, value);
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
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Key, key)) return false;

			nodes[h] = new Node { Key = key, Value = value, Next = nodes[h] };
			++Count;
			return true;
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

	// Add, Contains, Remove
	// Count, Comparer, Clear
	public class ChainHashSet<T>
	{
		readonly ChainHashMap<T, bool> map;
		public ChainHashSet(int bitSize, IEqualityComparer<T> comparer = null, Func<uint, int, int> hashFunc = null) => map = new(bitSize, default, comparer, hashFunc);
		public int Count => map.Count;
		public IEqualityComparer<T> Comparer => map.Comparer;
		public void Clear() => map.Clear();
		public bool Contains(T item) => map.ContainsKey(item);
		public bool Add(T item) => map.Add(item, false);
		public bool Remove(T item) => map.Remove(item);
	}
}
