// mutable array

namespace Oomph.Data.Values10Lib.Arrays.ZHs.v100
{
	public class ZobristHashArray<T>
	{
		// 2^32 * (√5-1) / 2 に近い素数
		const int B = (int)(2654435761L - (1L << 32));
		// 2^32 * (√3-1) / 2 に近い素数
		const int C = 1572067127;
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
				h ^= B * i + C * Hash(a[i]);
		}

		public T this[int i]
		{
			get => a[i];
			set
			{
				h ^= B * i + C * Hash(a[i]);
				a[i] = value;
				h ^= B * i + C * Hash(a[i]);
			}
		}

		public override int GetHashCode() => h;
	}
}
