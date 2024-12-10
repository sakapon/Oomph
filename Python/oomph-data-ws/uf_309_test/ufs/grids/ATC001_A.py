# https://atcoder.jp/contests/atc001/tasks/dfs_a

from ....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind
import itertools

h, w = map(int, input().split())
s0 = [input() for i in range(h)]

n = h * w
s = list(itertools.chain.from_iterable(s0))

sv = s.index('s')
gv = s.index('g')
uf = UnionFind(n)

for i in range(h):
    for j in range(w):
        v = w * i + j

        if j > 0:
            if s[v] != '#' and s[v - 1] != '#':
                uf.union(v, v - 1)

        if i > 0:
            if s[v] != '#' and s[v - w] != '#':
                uf.union(v, v - w)

print("Yes" if uf.are_same(sv, gv) else "No")
