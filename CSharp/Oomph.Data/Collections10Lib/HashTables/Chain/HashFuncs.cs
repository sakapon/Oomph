
namespace Oomph.Data.Collections10Lib.HashTables.Chain
{
	// (key, bit size) => hash value
	public static class HashFuncs
	{
		public static readonly double MagicDouble = (Math.Sqrt(5) - 1) / 2;
		// 2654435769
		public static readonly uint MagicUInt32 = (uint)(MagicDouble * (1L << 32));

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
				var v = key * a;
				v >>= 32 - size;
				return (int)v;
			};
		}

		public static Func<uint, int, int> CreateUniversal() => CreateUniversal(NextUInt32(uint.MaxValue) + 1, NextUInt32());
		public static Func<uint, int, int> CreateUniversal(uint a, uint b)
		{
			return (key, size) =>
			{
				var v = (ulong)key * a + b;
				v %= 4294967311;
				v &= (1U << size) - 1;
				return (int)v;
			};
		}
	}
}
