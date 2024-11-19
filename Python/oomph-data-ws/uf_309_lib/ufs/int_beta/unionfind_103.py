class UnionFind:
    """
    Represents a disjoint-set data structure, using union by size and path compression.

    time complexity: O(α(n))
    """

    def __init__(self, n: int):
        self.parents = [-1] * n
        self.sizes = [1] * n

    def find(self, x: int):
        if self.parents[x] == -1:
            return x
        else:
            self.parents[x] = self.find(self.parents[x])
            return self.parents[x]

    def are_same(self, x: int, y: int):
        return self.find(x) == self.find(y)

    def get_group_size(self, x: int):
        return self.sizes[self.find(x)]

    def union(self, x: int, y: int):
        x = self.find(x)
        y = self.find(y)
        if x == y:
            return False

        if self.sizes[x] < self.sizes[y]:
            x, y = y, x
        self.parents[y] = x
        self.sizes[x] += self.sizes[y]
        return True
