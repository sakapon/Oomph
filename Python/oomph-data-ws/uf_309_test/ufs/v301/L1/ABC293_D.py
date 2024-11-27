# https://atcoder.jp/contests/abc293/tasks/abc293_d

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, m = map(int, input().split())

c = 0
uf = UnionFind(n + 1)

for j in range(m):
    e = input().split()
    u = int(e[0])
    v = int(e[2])

    if not uf.union(u, v):
        c += 1

print(f"{c} {uf.groups_count - c - 1}")
