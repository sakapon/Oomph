
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

	// Add, Contains, Remove
	// Count, Comparer, Clear
	public class ChainHashSet<T>
	{
		static int HashDefault(uint key, int size) => (int)((key * 2654435769) >> 32 - size);

		public class Node
		{
			public T Item { get; init; }
			internal Node Next;
		}

		readonly int bitSize;
		readonly Node[] nodes;
		public int Count { get; private set; }
		public IEqualityComparer<T> Comparer { get; }
		readonly Func<uint, int, int> hashFunc;
		int Hash(T key) => hashFunc((uint)(key?.GetHashCode() ?? 0), bitSize);

		public ChainHashSet(int bitSize, IEqualityComparer<T> comparer = null, Func<uint, int, int> hashFunc = null)
		{
			this.bitSize = bitSize;
			nodes = new Node[1 << bitSize];
			Comparer = comparer ?? ComparerHelper.GetDefaultEquality<T>();
			this.hashFunc = hashFunc ?? HashDefault;
		}

		public void Clear()
		{
			Array.Clear(nodes);
			Count = 0;
		}

		public bool Contains(T item)
		{
			var h = Hash(item);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Item, item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			var h = Hash(item);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Item, item)) return false;

			nodes[h] = new Node { Item = item, Next = nodes[h] };
			++Count;
			return true;
		}

		public bool Remove(T item)
		{
			var h = Hash(item);
			for (ref var n = ref nodes[h]; n != null; n = ref n.Next)
				if (Remove(ref n, item)) return true;
			return false;
		}

		bool Remove(ref Node n, T item)
		{
			if (!Comparer.Equals(n.Item, item)) return false;
			n = n.Next;
			--Count;
			return true;
		}
	}
}
