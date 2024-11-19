# https://atcoder.jp/contests/atc001/tasks/unionfind_a

from unionfind306 import UnionFind

n, q = map(int, input().split())

uf = UnionFind(n)
r = []

for i in range(q):
    t, u, v = map(int, input().split())
    if t == 0:
        uf.union(u, v)
    else:
        r.append("Yes" if uf.are_same(u, v) else "No")

print("\n".join(r))
