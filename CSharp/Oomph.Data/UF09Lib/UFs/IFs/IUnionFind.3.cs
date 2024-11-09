namespace Oomph.Data.UF09Lib.UFs.IFs.v3
{
	/// <summary>
	/// <typeparamref name="TKey"/> 型の要素からなる集合の、互いに素な部分集合を管理します。
	/// </summary>
	/// <typeparam name="TKey">集合の要素の型。</typeparam>
	public interface IUnionFind<TKey>
	{
		public class Node
		{
			public TKey Key { get; }
		}

		/// <summary>
		/// <paramref name="x"/> が属するグループを取得します。
		/// </summary>
		/// <param name="x">要素。</param>
		/// <returns><paramref name="x"/> が属するグループ。</returns>
		public Node Find(TKey x);

		/// <summary>
		/// 指定された 2 つの要素が属するグループを合併します。
		/// </summary>
		/// <param name="x">要素。</param>
		/// <param name="y">要素。</param>
		/// <returns>2 つのグループが新たに合併された場合、<see langword="true"/>。それ以外の場合、<see langword="false"/>。</returns>
		public bool Union(TKey x, TKey y);

		/// <summary>
		/// 指定された 2 つの要素が同一のグループに属しているかどうかを判定します。
		/// </summary>
		/// <param name="x">要素。</param>
		/// <param name="y">要素。</param>
		/// <returns>指定された 2 つの要素が同一のグループに属しているかどうかを示すブール値。</returns>
		public bool AreSame(TKey x, TKey y) => Find(x) == Find(y);
	}
}
