// mutable array
// hash: ∑ B^i a_i

namespace Oomph.Data.Values10Lib.Arrays.RHs.v110
{
	public class RollingHashArray<T>
	{
		const int B = (int)(2654435769L - (1L << 32));
		static int Hash(T o) => o?.GetHashCode() ?? 0;

		readonly T[] a;
		public T[] Raw => a;
		readonly int n;
		readonly int[] pow;
		int h;

		public RollingHashArray(T[] a)
		{
			this.a = a;
			n = a.Length;

			pow = new int[n + 1];
			pow[0] = 1;

			for (int i = 0; i < n; ++i)
			{
				pow[i + 1] = pow[i] * B;
				h += pow[i] * Hash(a[i]);
			}
		}

		public T this[int i]
		{
			get => a[i];
			set
			{
				h -= pow[i] * Hash(a[i]);
				a[i] = value;
				h += pow[i] * Hash(a[i]);
			}
		}

		public override int GetHashCode() => h;
	}
}
