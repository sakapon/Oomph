import time
import random
from unionfind306 import UnionFind

n = 200000
es = []

for i in range(n):
    x = random.randint(0, n - 1)
    y = random.randint(0, n - 1)
    es.append((x, y))

start_time = time.time()
uf = UnionFind(n)
for a, b in es:
    uf.union(a, b)
print(time.time() - start_time)

print("end")
