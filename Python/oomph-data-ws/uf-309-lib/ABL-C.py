# https://atcoder.jp/contests/abl/tasks/abl_c

from unionfind306 import UnionFind

n, m = map(int, input().split())

count = n + 1
uf = UnionFind(count)

for i in range(m):
    a, b = map(int, input().split())
    if uf.union(a, b):
        count -= 1

print(count - 2)
