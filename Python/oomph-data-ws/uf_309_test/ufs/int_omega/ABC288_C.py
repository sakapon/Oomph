# https://atcoder.jp/contests/abc288/tasks/abc288_c

from ....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, m = map(int, input().split())

r = 0
uf = UnionFind(n + 1)

for j in range(m):
    a, b = map(int, input().split())
    if not uf.union(a, b):
        r = r + 1

print(r)
