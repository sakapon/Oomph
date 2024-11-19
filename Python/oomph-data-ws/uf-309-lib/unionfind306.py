class UnionFind():
    """
    Represents a disjoint-set data structure, using union by size and path compression.

    - parents[root] = -size
    - parents[non-root] = parent

    time complexity: O(α(n))
    """

    def __init__(self, n: int):
        self.n = n
        self.parents = [-1] * n

    def find(self, x: int):
        if self.parents[x] < 0:
            return x
        else:
            self.parents[x] = self.find(self.parents[x])
            return self.parents[x]

    def are_same(self, x: int, y: int):
        return self.find(x) == self.find(y)

    def union(self, x: int, y: int):
        x = self.find(x)
        y = self.find(y)
        if x == y:
            return False

        if self.parents[x] > self.parents[y]:
            x, y = y, x
        self.parents[x] += self.parents[y]
        self.parents[y] = x
        return True
