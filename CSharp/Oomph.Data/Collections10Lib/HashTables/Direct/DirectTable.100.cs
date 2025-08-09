
namespace Oomph.Data.Collections10Lib.HashTables.Direct.v100
{
	// Add, Contains, Remove
	// Count, Clear
	public class DirectSet
	{
		readonly bool[] b;
		public int Count { get; private set; }

		public DirectSet(int n)
		{
			b = new bool[n];
		}

		public void Clear()
		{
			Array.Clear(b);
			Count = 0;
		}

		public bool Contains(int key)
		{
			return b[key];
		}

		public bool Add(int key)
		{
			if (b[key]) return false;
			b[key] = true;
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
}
