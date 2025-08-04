
namespace Oomph.Data.Collections10Lib.HashTables.Chain
{
	public static class HashFuncs
	{
		static readonly Random Random = new();

		[Obsolete]
		public static readonly Func<uint, int, int> Division = (key, size) => (int)(key % size);

		public static Func<uint, int, int> CreateMultiplicationDouble() => CreateMultiplicationDouble(Random.NextDouble());
		public static Func<uint, int, int> CreateMultiplicationDouble(double a)
		{
			return (key, size) =>
			{
				var v = key * a;
				v -= Math.Floor(v);
				v *= size;
				return (int)v;
			};
		}

		public static Func<uint, int, int> CreateMultiplication() => CreateMultiplication((uint)Random.Next());
		public static Func<uint, int, int> CreateMultiplication(uint a)
		{
			return (key, size) =>
			{
				var v = (ulong)key * a;
				v &= uint.MaxValue;
				v /= (1UL << 32) / (ulong)size;
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
				v %= size;
				return (int)v;
			};
		}
	}
}
