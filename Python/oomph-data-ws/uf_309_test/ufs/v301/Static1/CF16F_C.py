# https://atcoder.jp/contests/cf16-final/tasks/codefestival_2016_final_c

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n, m = map(int, input().split())

uf = UnionFind(n + m)

for i in range(n):
    ls = list(map(int, input().split()))
    for l in ls[1:]:
        uf.union(i, n + l - 1)

rn = uf.find(0)
r = [uf.find(i) == rn for i in range(n)]
print("YES" if all(r) else "NO")
