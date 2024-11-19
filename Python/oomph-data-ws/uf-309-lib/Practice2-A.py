# https://atcoder.jp/contests/practice2/tasks/practice2_a

from unionfind306 import UnionFind

n, q = map(int, input().split())

uf = UnionFind(n)
r = []

for i in range(q):
    t, u, v = map(int, input().split())
    if t == 0:
        uf.union(u, v)
    else:
        r.append(int(uf.are_same(u, v)))

print("\n".join(map(str, r)))
