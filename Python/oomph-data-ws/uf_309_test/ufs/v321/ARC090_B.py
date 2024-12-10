# https://atcoder.jp/contests/abc087/tasks/arc090_b

from ....uf_309_lib.ufs.int_omega.unionfind_321 import UnionFind

n, m = map(int, input().split())

ok = True
uf = UnionFind(n + 1, 0)

for i in range(m):
    l, r, d = map(int, input().split())
    if uf.union(l, r, d) or uf.verify(l, r, d):
        continue
    ok = False
    break

print("Yes" if ok else "No")
