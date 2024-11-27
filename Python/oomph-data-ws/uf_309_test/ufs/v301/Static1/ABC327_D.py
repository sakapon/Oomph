# https://atcoder.jp/contests/abc327/tasks/abc327_d
# bipartite matching, fake vertexes

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, m = map(int, input().split())
a = list(map(int, input().split()))
b = list(map(int, input().split()))

uf = UnionFind(2 * n + 1)

for j in range(m):
    u, v = a[j], b[j]
    uf.union(u, v + n)
    uf.union(u + n, v)

r = True
for v in range(1, n + 1):
    if uf.are_same(v, v + n):
        r = False
        break
print("Yes" if r else "No")
