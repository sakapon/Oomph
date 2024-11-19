class UnionFindWithData():
    """
    Represents a disjoint-set data structure with data augmentation, using union by size and path compression.

    - parents[root] = -size
    - parents[non-root] = parent

    time complexity: O(α(n))
    """

    def __init__(self, values: list, merge_values):
        self.n = len(values)
        self.parents = [-1] * self.n
        self.values = values
        self.merge_values = merge_values

    def find(self, x: int):
        if self.parents[x] < 0:
            return x
        else:
            self.parents[x] = self.find(self.parents[x])
            return self.parents[x]

    def are_same(self, x: int, y: int):
        return self.find(x) == self.find(y)

    def __getitem__(self, x: int):
        return self.values[self.find(x)]

    def __setitem__(self, x: int, value):
        self.values[self.find(x)] = value

    def union(self, x: int, y: int):
        x = self.find(x)
        y = self.find(y)
        if x == y:
            return False

        # 左右の順序を保って値をマージします。
        v = self.merge_values(self.values[x], self.values[y])

        if self.parents[x] > self.parents[y]:
            x, y = y, x
        self.parents[x] += self.parents[y]
        self.parents[y] = x
        self.values[x] = v
        return True
