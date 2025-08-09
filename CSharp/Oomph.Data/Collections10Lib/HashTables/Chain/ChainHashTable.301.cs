
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
			Clear(bitSize);
			Comparer = comparer;
		}

		public void Clear(int bitSize)
		{
			table = new ChainNode<TKey, TValue>[1 << bitSize];
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
	}

	class NodeList<TKey, TValue>
	{
		readonly ChainNode<TKey, TValue> anchor = new();
		public NodeList() => Clear();
		public void Clear() => anchor.ListPrevious = anchor.ListNext = anchor;

		static void Connect(ChainNode<TKey, TValue> previous, ChainNode<TKey, TValue> next)
		{
			previous.ListNext = next;
			next.ListPrevious = previous;
		}

		public void Add(ChainNode<TKey, TValue> node)
		{
			Connect(anchor.ListPrevious, node);
			Connect(node, anchor);
		}

		public void Remove(ChainNode<TKey, TValue> node)
		{
			Connect(node.ListPrevious, node.ListNext);
			//node.ListPrevious = node.ListNext = null;
		}

		public IEnumerable<ChainNode<TKey, TValue>> GetNodes()
		{
			for (var n = anchor.ListNext; n != anchor; n = n.ListNext)
				yield return n;
		}
		public IEnumerable<TKey> GetKeys()
		{
			for (var n = anchor.ListNext; n != anchor; n = n.ListNext)
				yield return n.Key;
		}
		public IEnumerable<TValue> GetValues()
		{
			for (var n = anchor.ListNext; n != anchor; n = n.ListNext)
				yield return n.Value;
		}
	}

	// Add, Contains, Remove, Item[], GetNode
	// Count, DefaultValue, Comparer, Clear
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ChainHashMap<TKey, TValue> : IEnumerable<ChainNode<TKey, TValue>>
	{
		const int DefaultBitSize = 3;

		readonly ChainHashTable<TKey, TValue> table;
		readonly NodeList<TKey, TValue> nodeList = new();

		int bitSize = DefaultBitSize;
		int CountSup => 1 << bitSize - 1;
		public int Count { get; private set; }

		public TValue DefaultValue { get; }
		public IEqualityComparer<TKey> Comparer => table.Comparer;

		static uint HashDefault(uint key, int size) => (key * 2654435769) >> 32 - size;
		readonly Func<uint, int, uint> hashFunc;
		int Hash(TKey key) => (int)hashFunc((uint)(key?.GetHashCode() ?? 0), bitSize);

		public ChainHashMap(TValue iv = default, IEqualityComparer<TKey> comparer = null, Func<uint, int, uint> hashFunc = null)
		{
			table = new(bitSize, comparer ?? ComparerHelper.GetDefaultEquality<TKey>());
			DefaultValue = iv;
			this.hashFunc = hashFunc ?? HashDefault;
		}

		public void Clear()
		{
			bitSize = DefaultBitSize;
			table.Clear(bitSize);
			nodeList.Clear();
			Count = 0;
		}

		public ChainNode<TKey, TValue> GetNode(TKey key) => table.GetNode(key, Hash(key));
		public bool Contains(TKey key) => GetNode(key) != null;

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
			nodeList.Add(node);
			++Count;

			if (CountSup < Count)
			{
				++bitSize;
				ResizeStrictly();
			}
		}

		void RemoveStrictly(ref ChainNode<TKey, TValue> node)
		{
			--Count;
			nodeList.Remove(node);
			table.Remove(ref node);
		}

		void ResizeStrictly()
		{
			table.Clear(bitSize);
			foreach (var n in nodeList.GetNodes())
				table.Add(n, Hash(n.Key));
		}
		#endregion

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ChainNode<TKey, TValue>> GetEnumerator() => nodeList.GetNodes().GetEnumerator();

		public IEnumerable<TKey> GetKeys() => nodeList.GetKeys();
		public IEnumerable<TValue> GetValues() => nodeList.GetValues();

		public void AddItems((TKey, TValue)[] items, bool strictly = false)
		{
			var newSize = Count + items.Length;
			var bitSize0 = bitSize;
			while (CountSup < newSize) ++bitSize;
			if (bitSize0 < bitSize) ResizeStrictly();

			if (strictly)
				foreach (var (key, value) in items)
					AddStrictly(key, value, Hash(key));
			else
				foreach (var (key, value) in items)
					Add(key, value);
		}
	}

	// Add, Contains, Remove
	// Count, Comparer, Clear
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ChainHashSet<TKey> : IEnumerable<TKey>
	{
		readonly ChainHashMap<TKey, bool> map;
		public ChainHashSet(IEqualityComparer<TKey> comparer = null, Func<uint, int, uint> hashFunc = null) => map = new(default, comparer, hashFunc);
		public int Count => map.Count;
		public IEqualityComparer<TKey> Comparer => map.Comparer;
		public void Clear() => map.Clear();
		public bool Contains(TKey key) => map.Contains(key);
		public bool Add(TKey key) => map.Add(key, false);
		public bool Remove(TKey key) => map.Remove(key);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<TKey> GetEnumerator() => map.GetKeys().GetEnumerator();

		public void AddKeys(TKey[] keys, bool strictly = false) => map.AddItems(Array.ConvertAll(keys, key => (key, false)), strictly);
	}
}
