using Oomph.Data.Collections10Lib.HashTables.Chain;

namespace Collections10Test.HashTables.Chain
{
	class HashFuncsTest
	{
		static readonly double a = (Math.Sqrt(5) - 1) / 2;
		static readonly uint b = (uint)(a * (1L << 32));

		static void Main()
		{
			var h = HashFuncs.CreateMultiplicationDouble(a);
			AreEqual(67, h(123456, 1 << 14));

			h = HashFuncs.CreateMultiplication(b);
			AreEqual(67, h(123456, 1 << 14));

			var seq = Enumerable.Range(700, 600).Select(i => h((uint)i, 1 << 10));
			Console.WriteLine(seq.Distinct().Count());
		}

		public static void AreEqual<T>(T expected, T actual)
		{
			if (!EqualityComparer<T>.Default.Equals(expected, actual))
				throw new InvalidOperationException();
		}
	}
}
