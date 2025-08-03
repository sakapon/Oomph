
namespace Oomph.Data.Collections10Lib.HashTables.Direct.v101
{
	// Add, ContainsKey, Remove, Item[]
	// Count, Clear
	public class DirectMap<TValue>
	{
		readonly bool[] b;
		readonly TValue[] values;
		public int Count { get; private set; }

		public DirectMap(int n)
		{
			b = new bool[n];
			values = new TValue[n];
		}

		public void Clear()
		{
			Array.Clear(b);
			Count = 0;
		}

		public TValue this[int key]
		{
			get => b[key] ? values[key] : throw new KeyNotFoundException();
			set
			{
				if (!b[key]) ++Count;
				b[key] = true;
				values[key] = value;
			}
		}

		public bool ContainsKey(int key)
		{
			return b[key];
		}

		public bool Add(int key, TValue value)
		{
			if (b[key]) return false;
			b[key] = true;
			values[key] = value;
			++Count;
			return true;
		}

		public bool Remove(int key)
		{
			if (!b[key]) return false;
			b[key] = false;
			--Count;
			return true;
		}
	}

	// Add, Contains, Remove
	// Count, Clear
	public class DirectSet
	{
		readonly DirectMap<bool> map;
		public DirectSet(int n) => map = new DirectMap<bool>(n);
		public int Count => map.Count;
		public void Clear() => map.Clear();
		public bool Contains(int item) => map.ContainsKey(item);
		public bool Add(int item) => map.Add(item, false);
		public bool Remove(int item) => map.Remove(item);
	}
}
