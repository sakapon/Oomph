
namespace Oomph.Data.Collections10Lib.HashTables.Chain
{
	public static class HashFuncs
	{
		static readonly Random Random = new();

		public static Func<int, int, int> CreateMultiplication() => CreateMultiplication(Random.NextDouble());
		public static Func<int, int, int> CreateMultiplication(double a)
		{
			return (key, size) =>
			{
				var v = (uint)key * a;
				v -= Math.Floor(v);
				v *= size;
				return (int)v;
			};
		}

		public static Func<int, int, int> CreateUniversal() => CreateUniversal(Random.Next(), Random.Next());
		public static Func<int, int, int> CreateUniversal(int a, int b)
		{
			return (key, size) =>
			{
				var v = (uint)key * a + b;
				v %= 4294968001;
				v %= size;
				return (int)v;
			};
		}
	}
}
