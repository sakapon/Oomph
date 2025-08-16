
namespace Oomph.Data.Collections10Lib.HashTables.Direct.v201
{
	// Add, Contains, Remove, Item[]
	// Count, DefaultValue, Clear
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class DirectMap<TValue> : IEnumerable<(int, TValue)>
	{
		readonly bool[] b;
		readonly TValue[] values;
		public int Count { get; private set; }
		public TValue DefaultValue { get; }

		public DirectMap(int n, TValue iv = default)
		{
			b = new bool[n];
			values = new TValue[n];
			DefaultValue = iv;
		}

		public void Clear()
		{
			Array.Clear(b);
			Count = 0;
		}

		public TValue this[int key]
		{
			get => b[key] ? values[key] : DefaultValue;
			set
			{
				if (!b[key]) ++Count;
				b[key] = true;
				values[key] = value;
			}
		}

		public bool Contains(int key)
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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<(int, TValue)> GetEnumerator() { for (int i = 0; i < b.Length; ++i) if (b[i]) yield return (i, values[i]); }

		public IEnumerable<int> GetKeys() { for (int i = 0; i < b.Length; ++i) if (b[i]) yield return i; }
		public IEnumerable<TValue> GetValues() { for (int i = 0; i < b.Length; ++i) if (b[i]) yield return values[i]; }
	}

	// Add, Contains, Remove
	// Count, Clear
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class DirectSet : IEnumerable<int>
	{
		readonly DirectMap<bool> map;
		public DirectSet(int n) => map = new(n);
		public int Count => map.Count;
		public void Clear() => map.Clear();
		public bool Contains(int key) => map.Contains(key);
		public bool Add(int key) => map.Add(key, false);
		public bool Remove(int key) => map.Remove(key);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator() => map.GetKeys().GetEnumerator();
	}
}
