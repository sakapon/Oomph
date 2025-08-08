namespace Collections10Test
{
	public static class TestHelper
	{
		public static void AreEqual<T>(T expected, T actual)
		{
			if (!EqualityComparer<T>.Default.Equals(expected, actual))
				throw new InvalidOperationException($"expected: {expected}, actual: {actual}");
		}

		public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
		{
			if (!expected.SequenceEqual(actual))
				throw new InvalidOperationException($"expected: {expected}, actual: {actual}");
		}

		public static void MeasureTime(Action test)
		{
			var startTime = DateTime.Now;
			test();
			var span = DateTime.Now - startTime;
			Console.WriteLine($"{span.TotalSeconds:F3} sec");
		}
	}
}
