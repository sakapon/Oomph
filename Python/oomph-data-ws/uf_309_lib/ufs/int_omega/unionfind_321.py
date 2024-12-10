class UnionFind:

    class Node:
        def __init__(self, key: int, value):
            self.key = key
            self.parent = None
            self.size = 1
            self.value = value

    def __init__(self, n: int, v0):
        self.nodes = [None] * n
        for i in range(n):
            self.nodes[i] = self.Node(i, v0)
        self.items_count = n
        self.groups_count = n

    def find_rec(self, n: Node) -> Node:
        if n.parent is None:
            return n
        else:
            r = self.find_rec(n.parent)
            n.value = n.value + n.parent.value
            n.parent = r
            return r

    def find(self, x: int):
        return self.find_rec(self.nodes[x])

    def are_same(self, x: int, y: int):
        return self.find(x) == self.find(y)

    def union(self, x: int, y: int, x2y):
        gx = self.find(x)
        gy = self.find(y)
        if gx == gy:
            return False

        if gx.size < gy.size:
            gx, gy = gy, gx
            x, y = y, x
            x2y = -x2y
        gy.parent = gx
        gx.size += gy.size
        self.groups_count -= 1
        gy.value = -self.nodes[y].value + x2y + self.nodes[x].value
        return True

    def get_group_infoes(self):
        return filter(lambda n: n.parent is None, self.nodes)

    def get_x2y(self, x: int, y: int):
        if not self.are_same(x, y):
            raise RuntimeError()
        return self.nodes[y].value - self.nodes[x].value

    def verify(self, x: int, y: int, x2y):
        return self.are_same(x, y) and self.nodes[y].value == x2y + self.nodes[x].value
