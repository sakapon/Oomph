
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
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class FixedChainHashMap<TKey, TValue> : IEnumerable<FixedChainHashMap<TKey, TValue>.Node>
	{
		static int HashDefault(uint key, int size) => (int)((key * 2654435769) >> 32 - size);

		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Value = {Value}")]
		public class Node
		{
			public TKey Key { get; init; }
			public TValue Value { get; set; }
			internal Node Next;

			internal Node ListPrevious, ListNext;
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
			var n = GetNode(key, h);
			if (n == null) return AddStrictly(key, value, h);
			else return false;
		}

		public bool Remove(TKey key)
		{
			var h = Hash(key);
			for (ref var n = ref nodes[h]; n != null; n = ref n.Next)
				if (Comparer.Equals(n.Key, key)) return RemoveStrictly(ref n);
			return false;
		}

		#region Private Methods
		bool AddStrictly(TKey key, TValue value, int h)
		{
			var node = nodes[h] = new Node { Key = key, Value = value, Next = nodes[h] };

			if (Count++ == 0)
			{
				ListFirst = node.ListPrevious = node.ListNext = node;
			}
			else
			{
				var last = ListFirst.ListPrevious;
				ListFirst.ListPrevious = last.ListNext = node;
				node.ListPrevious = last;
				node.ListNext = ListFirst;
			}
			return true;
		}

		bool RemoveStrictly(ref Node n)
		{
			var node = n;
			if (--Count == 0)
			{
				ListFirst = node.ListPrevious = node.ListNext = null;
			}
			else
			{
				if (ListFirst == node) ListFirst = node.ListNext;
				node.ListPrevious.ListNext = node.ListNext;
				node.ListNext.ListPrevious = node.ListPrevious;
				node.ListPrevious = node.ListNext = null;
			}

			n = n.Next;
			node.Next = null;
			return true;
		}
		#endregion

		internal Node ListFirst;
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<Node> GetEnumerator()
		{
			var n = ListFirst;
			do yield return n;
			while ((n = n.ListNext) != ListFirst);
		}
	}

	// Add, Contains, Remove
	// Count, Comparer, Clear
	public class ChainHashSet<T> : IEnumerable<T>
	{
		readonly FixedChainHashMap<T, bool> map;
		public ChainHashSet(int bitSize, IEqualityComparer<T> comparer = null, Func<uint, int, int> hashFunc = null) => map = new(bitSize, default, comparer, hashFunc);
		public int Count => map.Count;
		public IEqualityComparer<T> Comparer => map.Comparer;
		public void Clear() => map.Clear();
		public bool Contains(T item) => map.ContainsKey(item);
		public bool Add(T item) => map.Add(item, false);
		public bool Remove(T item) => map.Remove(item);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator()
		{
			var n = map.ListFirst;
			do yield return n.Key;
			while ((n = n.ListNext) != map.ListFirst);
		}
	}
}
