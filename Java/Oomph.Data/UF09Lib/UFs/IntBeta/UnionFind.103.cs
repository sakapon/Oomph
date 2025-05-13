// path compression, union by size
// O(α(n))

public static class UnionFind
{
	int[] parents, sizes;

	public UnionFind(int n)
	{
		parents = new int[n];
		Arrays.fill(parents, -1);
		sizes = new int[n];
		Arrays.fill(sizes, 1);
	}

	public int Find(int x)
	{
		return parents[x] == -1 ? x : (parents[x] = Find(parents[x]));
	}
	public boolean AreSame(int x, int y)
	{
		return Find(x) == Find(y);
	}
	public int GetGroupSize(int x)
	{
		return sizes[Find(x)];
	}

	public boolean Union(int x, int y)
	{
		if ((x = Find(x)) == (y = Find(y))) return false;

		if (sizes[x] < sizes[y])
		{
			int t = x;
			x = y;
			y = t;
		}
		parents[y] = x;
		sizes[x] += sizes[y];
		return true;
	}
}
