# https://atcoder.jp/contests/abc177/tasks/abc177_d

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, m = map(int, input().split())

uf = UnionFind(n + 1)

for j in range(m):
    a, b = map(int, input().split())
    uf.union(a, b)

r = max(uf.get_group_infoes(), key=lambda g: g.size).size
print(r)
