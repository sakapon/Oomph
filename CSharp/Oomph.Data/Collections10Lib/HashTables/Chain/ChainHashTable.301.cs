
namespace Oomph.Data.Collections10Lib.HashTables.Chain.v301
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

	[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Value = {Value}")]
	public class ChainNode<TKey, TValue>
	{
		public TKey Key { get; init; }
		public TValue Value { get; set; }
		internal ChainNode<TKey, TValue> Next;

		internal ChainNode<TKey, TValue> ListPrevious, ListNext;
	}

	class ChainHashTable<TKey, TValue>
	{
		ChainNode<TKey, TValue>[] table;
		internal readonly IEqualityComparer<TKey> Comparer;

		public ChainHashTable(int bitSize, IEqualityComparer<TKey> comparer)
		{
			table = new ChainNode<TKey, TValue>[1 << bitSize];
			Comparer = comparer;
		}

		public ref ChainNode<TKey, TValue> GetNode(TKey key, int h)
		{
			ref var n = ref table[h];
			for (; n != null; n = ref n.Next)
				if (Comparer.Equals(n.Key, key)) return ref n;
			return ref n;
		}

		public void Add(ChainNode<TKey, TValue> node, int h)
		{
			node.Next = table[h];
			table[h] = node;
		}

		public void Remove(ref ChainNode<TKey, TValue> node)
		{
			var n = node;
			node = node.Next;
			n.Next = null;
		}

		public void Resize(int bitSize, ChainNode<TKey, TValue> anchor, Func<uint, int, int> hashFunc)
		{
			table = new ChainNode<TKey, TValue>[1 << bitSize];
			for (var n = anchor.ListNext; n != anchor; n = n.ListNext)
				Add(n, hashFunc((uint)(n.Key?.GetHashCode() ?? 0), bitSize));
		}
	}

	// Add, ContainsKey, Remove, Item[], GetNode
	// Count, DefaultValue, Comparer, Clear
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ChainHashMap<TKey, TValue> : IEnumerable<ChainNode<TKey, TValue>>
	{
		const int DefaultBitSize = 3;
		int bitSize = DefaultBitSize;
		ChainHashTable<TKey, TValue> table;
		public int Count { get; private set; }
		public TValue DefaultValue { get; }
		public IEqualityComparer<TKey> Comparer => table.Comparer;

		static int HashDefault(uint key, int size) => (int)((key * 2654435769) >> 32 - size);
		internal readonly Func<uint, int, int> hashFunc;
		int Hash(TKey key) => hashFunc((uint)(key?.GetHashCode() ?? 0), bitSize);

		public ChainHashMap(TValue iv = default, IEqualityComparer<TKey> comparer = null, Func<uint, int, int> hashFunc = null)
		{
			table = new(bitSize, comparer ?? ComparerHelper.GetDefaultEquality<TKey>());
			DefaultValue = iv;
			this.hashFunc = hashFunc ?? HashDefault;

			ListAnchor.ListPrevious = ListAnchor.ListNext = ListAnchor;
		}

		public void Clear()
		{
			bitSize = DefaultBitSize;
			table = new(bitSize, table.Comparer);
			Count = 0;
		}

		public ChainNode<TKey, TValue> GetNode(TKey key) => table.GetNode(key, Hash(key));
		public bool ContainsKey(TKey key) => GetNode(key) != null;

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
				var n = table.GetNode(key, h);
				if (n == null) AddStrictly(key, value, h);
				else n.Value = value;
			}
		}

		public bool Add(TKey key, TValue value)
		{
			var h = Hash(key);
			var n = table.GetNode(key, h);
			if (n != null) return false;
			AddStrictly(key, value, h);
			return true;
		}

		public bool Remove(TKey key)
		{
			var h = Hash(key);
			ref var n = ref table.GetNode(key, h);
			if (n == null) return false;
			RemoveStrictly(ref n);
			return true;
		}

		#region Private Methods
		void AddStrictly(TKey key, TValue value, int h)
		{
			var node = new ChainNode<TKey, TValue> { Key = key, Value = value };
			table.Add(node, h);
			++Count;

			var last = ListAnchor.ListPrevious;
			ListAnchor.ListPrevious = last.ListNext = node;
			node.ListPrevious = last;
			node.ListNext = ListAnchor;

			Resize();
		}

		void RemoveStrictly(ref ChainNode<TKey, TValue> nodeRef)
		{
			var node = nodeRef;
			node.ListPrevious.ListNext = node.ListNext;
			node.ListNext.ListPrevious = node.ListPrevious;
			node.ListPrevious = node.ListNext = null;

			--Count;
			table.Remove(ref nodeRef);
		}

		void Resize()
		{
			if (Count > 1 << bitSize - 1) table.Resize(++bitSize, ListAnchor, hashFunc);
		}
		#endregion

		internal readonly ChainNode<TKey, TValue> ListAnchor = new();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ChainNode<TKey, TValue>> GetEnumerator() { for (var n = ListAnchor.ListNext; n != ListAnchor; n = n.ListNext) yield return n; }
	}

	// Add, Contains, Remove
	// Count, Comparer, Clear
	public class ChainHashSet<T> : IEnumerable<T>
	{
		readonly ChainHashMap<T, bool> map;
		public ChainHashSet(IEqualityComparer<T> comparer = null, Func<uint, int, int> hashFunc = null) => map = new(default, comparer, hashFunc);
		public int Count => map.Count;
		public IEqualityComparer<T> Comparer => map.Comparer;
		public void Clear() => map.Clear();
		public bool Contains(T item) => map.ContainsKey(item);
		public bool Add(T item) => map.Add(item, false);
		public bool Remove(T item) => map.Remove(item);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var n = map.ListAnchor.ListNext; n != map.ListAnchor; n = n.ListNext) yield return n.Key; }
	}
}
