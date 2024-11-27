# https://atcoder.jp/contests/abc214/tasks/abc214_d

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n = int(input())
es = [list(map(int, input().split())) for j in range(n - 1)]

r = 0
uf = UnionFind(n + 1)

es.sort(key=lambda e: e[2])
for u, v, w in es:
    r += uf.find(u).size * uf.find(v).size * w
    uf.union(u, v)

print(r)
