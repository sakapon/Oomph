
namespace Oomph.Data.Values10Lib.Arrays.ZHs.v100
{
	public class ZobristHashSet<T> : HashSet<T>
	{
		const int B = (int)(2654435769L - (1L << 32));
		static int Hash(T o) => o?.GetHashCode() ?? 0;

		int h;
		public override int GetHashCode() => h;

		public new bool Add(T item)
		{
			var r = base.Add(item);
			if (r) h ^= B * Hash(item);
			return r;
		}

		public new bool Remove(T item)
		{
			var r = base.Remove(item);
			if (r) h ^= B * Hash(item);
			return r;
		}
	}
}
