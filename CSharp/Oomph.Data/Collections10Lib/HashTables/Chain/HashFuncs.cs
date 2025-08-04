
namespace Oomph.Data.Collections10Lib.HashTables.Chain
{
	// (key, bit size) => hash value
	public static class HashFuncs
	{
		static readonly Random Random = new();
		public static uint NextUInt32() => NextUInt32(1L << 32);
		public static uint NextUInt32(double maxValue) => (uint)(Random.NextDouble() * maxValue);

		[Obsolete]
		public static readonly Func<uint, int, int> Division = (key, size) => (int)(key & ((1U << size) - 1));

		public static Func<uint, int, int> CreateMultiplicationDouble() => CreateMultiplicationDouble(Random.NextDouble());
		public static Func<uint, int, int> CreateMultiplicationDouble(double a)
		{
			return (key, size) =>
			{
				var v = key * a;
				v -= Math.Floor(v);
				v *= 1 << size;
				return (int)v;
			};
		}

		public static Func<uint, int, int> CreateMultiplication() => CreateMultiplication(NextUInt32());
		public static Func<uint, int, int> CreateMultiplication(uint a)
		{
			return (key, size) =>
			{
				var v = (ulong)key * a;
				v &= uint.MaxValue;
				v >>= 32 - size;
				return (int)v;
			};
		}

		public static Func<uint, int, int> CreateUniversal() => CreateUniversal(Random.Next(), Random.Next());
		public static Func<uint, int, int> CreateUniversal(int a, int b)
		{
			return (key, size) =>
			{
				var v = key * a + b;
				v %= 4294967311;
				v &= (1 << size) - 1;
				return (int)v;
			};
		}
	}
}
