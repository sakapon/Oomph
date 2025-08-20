// immutable array
// hash: ∑ B^i a_i
// h[i]: h[i, n]

namespace Oomph.Data.Values10Lib.Arrays.RHs.v101
{
	public class RollingHashArray<T>
	{
		// M * (√5-1) / 2 に近い整数
		const long B = 618033991;
		const long M = 1000000007;
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
		static int Hash(T o) => o?.GetHashCode() ?? 0;

		readonly T[] a;
		readonly int n;
		readonly long[] pow, h;

		public RollingHashArray(T[] a)
		{
			this.a = a;
			n = a.Length;

			pow = new long[n + 1];
			pow[0] = 1;
			h = new long[n + 1];

			for (int i = 0; i < n; ++i)
				pow[i + 1] = pow[i] * B % M;
			for (int i = n - 1; i >= 0; --i)
				h[i] = (h[i + 1] * B + Hash(a[i])) % M;
		}

		public T this[int i] => a[i];

		public override int GetHashCode() => (int)h[0];
		public int GetHashCode(int l, int r) => (int)MInt(h[l] - h[r] * pow[r - l]);
		public int GetHashCodeByCount(int start, int count) => GetHashCode(start, start + count);
	}
}
