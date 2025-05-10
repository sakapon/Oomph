
namespace Oomph.Graphs.Trees10Lib.Common.Arrays
{
	public static class UnweightedTreeHelper
	{
		public static bool IsTree(UnweightedGraph g, int sv)
		{
			var u = new bool[g.Count];
			if (!DFS(sv, -1)) return false;
			return Array.TrueForAll(u[sv..], b => b);

			bool DFS(int v, int pv)
			{
				u[v] = true;
				foreach (var nv in g.Map[v])
				{
					if (nv == pv) continue;
					if (u[nv]) return false;
					DFS(nv, v);
				}
				return true;
			}
		}
	}
}
