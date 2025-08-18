// immutable array
// hash: ∑ B^i a_i
// h[i]: h[i, n]

namespace Oomph.Data.Values10Lib.Arrays.RHs.v100
{
	public class RollingHashArray<T>
	{
		const int B = (int)(2654435769L - (1L << 32));
		static int Hash(T o) => o?.GetHashCode() ?? 0;

		readonly T[] a;
		readonly int n;
		readonly int[] pow, h;

		public RollingHashArray(T[] a)
		{
			this.a = a;
			n = a.Length;

			pow = new int[n + 1];
			pow[0] = 1;
			h = new int[n + 1];

			for (int i = 0; i < n; ++i)
				pow[i + 1] = pow[i] * B;
			for (int i = n - 1; i >= 0; --i)
				h[i] = h[i + 1] * B + Hash(a[i]);
		}

		public T this[int i] => a[i];

		public override int GetHashCode() => h[0];
		public int GetHashCode(int l, int r) => h[l] - h[r] * pow[r - l];
	}
}
