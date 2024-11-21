class UnionFind:

    class Node:

        def __init__(self, key: int):
            self.key = key
            self.parent = None
            self.size = 1

    def __init__(self, n: int):
        self.nodes = [None] * n
        for i in range(n):
            self.nodes[i] = self.Node(i)
        self.items_count = n
        self.groups_count = n

    def find_rec(self, n: Node) -> Node:
        if n.parent is None:
            return n
        else:
            n.parent = self.find_rec(n.parent)
            return n.parent

    def find(self, x: int):
        return self.find_rec(self.nodes[x])

    def are_same(self, x: int, y: int):
        return self.find(x) == self.find(y)

    def union(self, x: int, y: int):
        gx = self.find(x)
        gy = self.find(y)
        if gx == gy:
            return False

        if gx.size < gy.size:
            gx, gy = gy, gx
        gy.parent = gx
        gx.size += gy.size
        self.groups_count = self.groups_count - 1
        return True

    def get_group_infoes(self):
        return filter(lambda n: n.parent is None, self.nodes)
