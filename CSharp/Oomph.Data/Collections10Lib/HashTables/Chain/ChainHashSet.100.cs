
namespace Oomph.Data.Collections10Lib.HashTables.Chain.v100
{
	// Add, Contains, Remove
	// Count, Clear
	public class ChainHashSet<T>
	{
		static readonly double a = (Math.Sqrt(5) - 1) / 2;
		static int Hash0(uint key, int size)
		{
			var v = key * a;
			v -= Math.Floor(v);
			v *= size;
			return (int)v;
		}

		public class Node
		{
			public T Item;
			public Node Next;
		}

		readonly int size;
		readonly Node[] nodes;
		public int Count { get; private set; }
		public IEqualityComparer<T> Comparer { get; }
		readonly Func<uint, int, int> hashFunc;
		int Hash(T key) => hashFunc((uint)(key?.GetHashCode() ?? 0), size);

		public ChainHashSet(int size, IEqualityComparer<T> comparer = null, Func<uint, int, int> hashFunc = null)
		{
			nodes = new Node[this.size = size];
			Comparer = comparer ?? EqualityComparer<T>.Default;
			this.hashFunc = hashFunc ?? Hash0;
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
