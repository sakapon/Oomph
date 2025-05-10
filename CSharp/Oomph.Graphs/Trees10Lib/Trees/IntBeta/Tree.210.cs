using Oomph.Graphs.Trees10Lib.Common.Arrays;

// int vertexes, array-based

namespace Oomph.Graphs.Trees10Lib.Trees.v210
{
	public class Tree
	{
		public int Count => Map.Length;
		public List<int>[] Map { get; }
		public int Root { get; private set; }
		public int[] Parents { get; }
		public int[] Depths { get; }

		// この Euler Tour では方向を記録しません。
		// order -> vertex
		public int[] Tour { get; private set; }
		// vertex -> orders
		public List<int>[] TourMap { get; private set; }
		// depth -> orders
		public List<int>[] DepthToTourMap { get; private set; }

		public Tree(UnweightedGraph g, int root)
		{
			Map = g.Map;
			Parents = Array.ConvertAll(Map, _ => -1);
			Depths = Array.ConvertAll(Map, _ => -1);
			Reroot(root);
		}

		void Reroot(int root)
		{
			var tour = new List<int>();
			TourMap = Array.ConvertAll(Map, _ => new List<int>());
			DepthToTourMap = Array.ConvertAll(Map, _ => new List<int>());

			Root = root;
			Parents[root] = -1;
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

		public IEnumerable<int> GetPath(int v)
		{
			for (; v != -1; v = Parents[v]) yield return v;
		}

		public bool IsAncestor(int a, int b)
		{
			return TourMap[a][0] <= TourMap[b][0] && TourMap[b][^1] <= TourMap[a][^1];
		}

		public int GetLcaDepth(int a, int b)
		{
			if (TourMap[b][0] < TourMap[a][0]) (a, b) = (b, a);
			if (TourMap[b][^1] <= TourMap[a][^1]) return Depths[a];

			var (so, eo) = (TourMap[a][^1], TourMap[b][0]);

			return First(0, Math.Min(Depths[a], Depths[b]), dx =>
			{
				var l = DepthToTourMap[dx];
				var si = First(0, l.Count, x => l[x] >= so);
				var ei = First(0, l.Count, x => l[x] > eo);
				return si < ei;
			});
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
