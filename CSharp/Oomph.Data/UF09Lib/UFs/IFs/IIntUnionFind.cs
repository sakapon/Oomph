namespace Oomph.Data.UF09Lib.UFs.IFs
{
	public interface IIntUnionFind
	{
		public int Find(int x);
		public bool Union(int x, int y);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
	}
}
