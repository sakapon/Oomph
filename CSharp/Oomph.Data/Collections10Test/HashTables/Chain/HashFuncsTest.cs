using Oomph.Data.Collections10Lib.HashTables.Chain;

namespace Collections10Test.HashTables.Chain
{
	class HashFuncsTest
	{
		static void Main()
		{
			var h = HashFuncs.CreateMultiplicationDouble(HashFuncs.MagicDouble);
			AreEqual(67, h(123456, 14));

			h = HashFuncs.CreateMultiplication(HashFuncs.MagicUInt32);
			AreEqual(67, h(123456, 14));

			TestHash(h);
			TestHash(HashFuncs.CreateMultiplicationDouble());
			TestHash(HashFuncs.CreateMultiplication());
			TestHash(HashFuncs.CreateUniversal());
		}

		static void TestHash(Func<uint, int, int> h)
		{
			var seq = Enumerable.Range(700, 600).Select(i => h((uint)i, 10));
			Console.WriteLine(seq.Distinct().Count());
		}

		public static void AreEqual<T>(T expected, T actual)
		{
			if (!EqualityComparer<T>.Default.Equals(expected, actual))
				throw new InvalidOperationException("Not Equal");
		}
	}
}
