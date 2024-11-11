using System;
using System.Collections.Generic;

namespace Oomph.Data.UF09Lib.Helpers
{
	public static class MergeOps
	{
		public static Func<int, int, int> Int32_Add { get; } = (x, y) => x + y;
		public static Func<long, long, long> Int64_Add { get; } = (x, y) => x + y;
		public static Func<int, int, int> Int32_Min { get; } = (x, y) => x <= y ? x : y;
		public static Func<int, int, int> Int32_Max { get; } = (x, y) => x >= y ? x : y;

		public static Func<int, int, int> Int32_ArgMin(int[] a) => (x, y) => a[x] <= a[y] ? x : y;

		public static Func<int, int, int> Int32_Update { get; } = (x, y) => x != int.MinValue ? x : y;

		public static List<T> Merge<T>(List<T> o1, List<T> o2)
		{
			if (o1.Count < o2.Count) (o1, o2) = (o2, o1);
			o1.AddRange(o2);
			return o1;
		}

		public static Dictionary<TKey, int> Merge<TKey>(Dictionary<TKey, int> o1, Dictionary<TKey, int> o2)
		{
			if (o1.Count < o2.Count) (o1, o2) = (o2, o1);
			foreach (var (k, v) in o2)
				o1[k] = o1.GetValueOrDefault(k) + v;
			return o1;
		}

		public static HashSet<T> Merge<T>(HashSet<T> o1, HashSet<T> o2)
		{
			if (o1.Count < o2.Count) (o1, o2) = (o2, o1);
			foreach (var v in o2)
				o1.Add(v);
			return o1;
		}

		// XOR の場合。SymmetricExceptWith メソッドでも可。
		public static HashSet<T> MergeXor<T>(HashSet<T> o1, HashSet<T> o2)
		{
			if (o1.Count < o2.Count) (o1, o2) = (o2, o1);
			foreach (var v in o2)
				if (!o1.Add(v)) o1.Remove(v);
			return o1;
		}
	}
}
