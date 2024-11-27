# https://atcoder.jp/contests/abc238/tasks/abc238_e

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, qc = map(int, input().split())

uf = UnionFind(n + 1)

for qi in range(qc):
    l, r = map(int, input().split())
    uf.union(l - 1, r)

print("Yes" if uf.are_same(0, n) else "No")
