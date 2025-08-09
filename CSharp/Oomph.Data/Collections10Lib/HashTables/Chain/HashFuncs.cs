
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
		public static readonly Func<uint, int, uint> Division = (key, size) => key & ((1U << size) - 1);

		public static Func<uint, int, uint> CreateMultiplicationDouble() => CreateMultiplicationDouble(Random.NextDouble());
		public static Func<uint, int, uint> CreateMultiplicationDouble(double a)
		{
			return (key, size) =>
			{
				var v = key * a;
				v -= Math.Floor(v);
				v *= 1 << size;
				return (uint)v;
			};
		}

		public static Func<uint, int, uint> CreateMultiplication() => CreateMultiplication(NextUInt32());
		public static Func<uint, int, uint> CreateMultiplication(uint a)
		{
			return (key, size) =>
			{
				key *= a;
				key >>= 32 - size;
				return key;
			};
		}

		public static Func<uint, int, uint> CreateUniversal() => CreateUniversal(NextUInt32(uint.MaxValue) + 1, NextUInt32());
		public static Func<uint, int, uint> CreateUniversal(uint a, uint b)
		{
			return (key, size) =>
			{
				var v = (ulong)key * a + b;
				v %= 4294967311;
				v &= (1U << size) - 1;
				return (uint)v;
			};
		}
	}
}
