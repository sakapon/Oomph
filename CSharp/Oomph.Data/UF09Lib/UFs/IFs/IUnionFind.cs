namespace Oomph.Data.UF09Lib.UFs.IFs
{
	public interface IUnionFind<TKey>
	{
		public class Node { }
		public Node Find(TKey x);
		public bool Union(TKey x, TKey y);
		public bool AreSame(TKey x, TKey y) => Find(x) == Find(y);
	}
}
