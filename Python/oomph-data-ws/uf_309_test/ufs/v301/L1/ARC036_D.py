# https://atcoder.jp/contests/arc036/tasks/arc036_d

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, qc = map(int, input().split())

r = []
uf = UnionFind(2 * n + 1)

for qi in range(qc):
    w, u, v, z = map(int, input().split())
    if w == 1:
        if z % 2 == 0:
            uf.union(u, v)
            uf.union(u + n, v + n)
        else:
            uf.union(u, v + n)
            uf.union(u + n, v)
    else:
        r.append(uf.are_same(u, v))

print("\n".join(map(lambda b: "YES" if b else "NO", r)))
