using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Maps
{
	// Test: https://atcoder.jp/contests/abc137/tasks/abc137_c
	class ABC137_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

			var r = 0L;
			var map = new ChainHashMap<string, int>(n);
			foreach (var s in ss)
			{
				var cs = s.ToCharArray();
				Array.Sort(cs);
				var t = new string(cs);
				r += map.GetOrAddNode(t).Value++;
			}
			return r;
		}
	}
}
