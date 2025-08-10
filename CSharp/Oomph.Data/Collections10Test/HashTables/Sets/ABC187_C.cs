using Oomph.Data.Collections10Lib.HashTables.Chain.v301;

namespace Collections10Test.HashTables.Sets
{
	// Test: https://atcoder.jp/contests/abc187/tasks/abc187_c
	class ABC187_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

			var set = new ChainHashSet<string>();
			set.AddKeys(Array.FindAll(ss, s => s.StartsWith('!')));

			return ss.FirstOrDefault(s => set.Contains('!' + s)) ?? "satisfiable";
		}
	}
}
