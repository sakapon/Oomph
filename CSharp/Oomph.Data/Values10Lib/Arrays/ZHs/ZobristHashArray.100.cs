// mutable array

namespace Oomph.Data.Values10Lib.Arrays.ZHs.v100
{
	public class ZobristHashArray<T>
	{
		const int B = (int)(2654435769L - (1L << 32));
		static int Hash(T o) => o?.GetHashCode() ?? 0;

		readonly T[] a;
		public T[] Raw => a;
		readonly int n;
		int h;

		public ZobristHashArray(T[] a)
		{
			this.a = a;
			n = a.Length;

			for (int i = 0; i < n; ++i)
				h ^= B * Hash(a[i]);
		}

		public T this[int i]
		{
			get => a[i];
			set
			{
				h ^= B * Hash(a[i]);
				a[i] = value;
				h ^= B * Hash(a[i]);
			}
		}

		public override int GetHashCode() => h;
	}
}
