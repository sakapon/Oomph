
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

		public bool Contains(int item)
		{
			return b[item];
		}

		public bool Add(int item)
		{
			if (b[item]) return false;
			b[item] = true;
			++Count;
			return true;
		}

		public bool Remove(int item)
		{
			if (!b[item]) return false;
			b[item] = false;
			--Count;
			return true;
		}
	}
}
