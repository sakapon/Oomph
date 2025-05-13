import java.util.*;

class Main {
	public static void main(String[] args) {
		Scanner sc = new Scanner(System.in);
		int n = sc.nextInt();
		int qc = sc.nextInt();

		UnionFind uf = new UnionFind(n + 1);
		while (qc-- > 0)
		{
			int t = sc.nextInt();
			int u = sc.nextInt();
			int v = sc.nextInt();

			if (t == 0)
			{
				uf.Union(u, v);
			}
			else
			{
				System.out.println(uf.AreSame(u, v) ? "Yes" : "No");
			}
		}
	}
}
