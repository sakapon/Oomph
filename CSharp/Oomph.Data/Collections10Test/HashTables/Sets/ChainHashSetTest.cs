using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	class ChainHashSetTest
	{
		static readonly Random Random = new();
		static void Main()
		{
			Set_Equal();

			var n = 1 << 20;
			var data_int = Enumerable.Range(0, n).ToArray();
			TestHelper.MeasureTime(() => Set_Ops(data_int));
			data_int = Array.ConvertAll(data_int, _ => Random.Next());
			TestHelper.MeasureTime(() => Set_Ops(data_int));
		}

		public static void Set_Equal()
		{
			var n = 1 << 10;
			var data = Enumerable.Range(0, n).Select(_ => Random.Next(n)).ToArray();

			var u = new bool[n];
			var set = new ChainHashSet<int>();

			foreach (var i in data)
			{
				u[i] = true;
				set.Add(i);
			}
			TestHelper.AreEqual(u.Count(b => b), set.Count);

			for (var i = 0; i < n; i++)
			{
				TestHelper.AreEqual(u[i], set.Contains(i));
			}

			foreach (var i in data)
			{
				set.Remove(i);
			}
			TestHelper.AreEqual(0, set.Count);
			TestHelper.AreEqual(0, set.ToArray().Length);
		}

		public static void Set_Ops<T>(T[] data)
		{
			var set = new ChainHashSet<T>();
			foreach (var item in data)
				set.Add(item);
			foreach (var item in data)
				set.Contains(item);
			foreach (var item in data)
				set.Remove(item);
		}
	}
}
