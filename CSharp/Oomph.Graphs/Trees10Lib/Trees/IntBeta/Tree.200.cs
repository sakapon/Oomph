// int vertexes, array-based

namespace Oomph.Graphs.Trees10Lib.Trees.v200
{
	public class Tree
	{
		public static List<int>[] ToListMap(int n, (int u, int v)[] edges, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in edges)
			{
				map[u].Add(v);
				if (twoway) map[v].Add(u);
			}
			return map;
		}

		public int Count { get; }
		public List<int>[] Map { get; }
		public int Root { get; }
		public int[] Parents { get; }
		public int[] Depths { get; }

		// この Euler Tour では方向を記録しません。
		// order -> vertex
		public int[] Tour { get; }
		// vertex -> orders
		public List<int>[] TourMap { get; }
		// depth -> orders
		public List<int>[] DepthToTourMap { get; }

		public Tree(int n, (int u, int v)[] edges, int root)
		{
			Count = n;
			Map = ToListMap(n, edges, true);
			Root = root;
			Parents = Array.ConvertAll(Map, _ => -1);
			Depths = Array.ConvertAll(Map, _ => -1);

			var tour = new List<int>();
			TourMap = Array.ConvertAll(Map, _ => new List<int>());
			DepthToTourMap = Array.ConvertAll(Map, _ => new List<int>());

			Depths[root] = 0;
			DFS(root);

			Tour = tour.ToArray();

			void DFS(int v)
			{
				TourMap[v].Add(tour.Count);
				DepthToTourMap[Depths[v]].Add(tour.Count);
				tour.Add(v);

				foreach (var nv in Map[v])
				{
					if (nv == Parents[v]) continue;
					Parents[nv] = v;
					Depths[nv] = Depths[v] + 1;
					DFS(nv);

					TourMap[v].Add(tour.Count);
					DepthToTourMap[Depths[v]].Add(tour.Count);
					tour.Add(v);
				}
			}
		}
	}
}
