# https://atcoder.jp/contests/past202004-open/tasks/past202004_e

from .....uf_309_lib.ufs.int_omega.unionfind_301 import UnionFind

n = int(input())
a = list(map(int, input().split()))

uf = UnionFind(n)

for i in range(n):
    uf.union(i, a[i] - 1)

r = [uf.find(i).size for i in range(n)]
print(" ".join(map(str, r)))
