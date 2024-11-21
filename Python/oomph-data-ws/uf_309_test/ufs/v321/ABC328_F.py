# https://atcoder.jp/contests/abc328/tasks/abc328_f

from ....uf_309_lib.ufs.int_omega.unionfind_321 import UnionFind

n, qc = map(int, input().split())

r = []
uf = UnionFind(n + 1, 0)

for i in range(qc):
    a, b, d = map(int, input().split())
    if uf.union(b, a, d) or uf.verify(b, a, d):
        r.append(i + 1)

print(*r)
