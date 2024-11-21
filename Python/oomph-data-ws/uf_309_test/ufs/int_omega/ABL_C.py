# https://atcoder.jp/contests/abl/tasks/abl_c

from ....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, m = map(int, input().split())

uf = UnionFind(n + 1)

for j in range(m):
    a, b = map(int, input().split())
    uf.union(a, b)

print(uf.groups_count - 2)
