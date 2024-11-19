# https://atcoder.jp/contests/abc277/tasks/abc277_c

from unionfind506 import UnionFindWithData

n = int(input())
edges = [tuple(int(s) for s in input().split()) for _ in range(n)]

# compression
set = set()
set.add(1)
for a, b in edges:
    set.add(a)
    set.add(b)
f = [x for x in set]
f.sort()
m = len(f)

map = {}
for i in range(m):
    map[f[i]] = i

# max2 = lambda x, y: x if x >= y else y
uf = UnionFindWithData(f, max)
for a, b in edges:
    uf.union(map[a], map[b])
print(uf[0])
