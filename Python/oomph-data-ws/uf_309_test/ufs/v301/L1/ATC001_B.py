# https://atcoder.jp/contests/atc001/tasks/unionfind_a
# https://atcoder.jp/contests/practice2/tasks/practice2_a

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, qc = map(int, input().split())

r = []
uf = UnionFind(n)

for qi in range(qc):
    t, u, v = map(int, input().split())
    if t == 0:
        uf.union(u, v)
    else:
        r.append("Yes" if uf.are_same(u, v) else "No")
        # r.append("1" if uf.are_same(u, v) else "0")

print("\n".join(r))
