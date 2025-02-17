## Oomph.Data.UF09Lib
A laboratory of Union-Find.

### UFs
Union-Find Trees.

#### Int Alpha
- 100: no technique O(n)
- 101: path compression O(log n)
- 102: union by size O(log n)
- 103: path compression, union by size O(α(n))
- 112: union by rank O(log n)
- 113: path compression, union by rank O(α(n))
- 123: 103 + parents with sizes

#### Int Beta
- 200: array-based
- 201: node-based
- 221: data augmentation (relative, long)
- 251: short code, path compression

#### Int Omega
- 301: normal
- 302: undo
- 311: data augmentation
- 321: data augmentation (relative)

#### Typed Beta
- 401: normal
  - static vertexes, KeyNotFoundException
- 402: normal
  - dynamic vertexes, KeyNotFoundException
- 403: normal
  - dynamic vertexes, implicit nodes
- 404: normal
  - dynamic vertexes, implicit nodes (Union only)
- 411: data augmentation
  - static vertexes, KeyNotFoundException
- 412: data augmentation
  - dynamic vertexes, KeyNotFoundException
- 413: data augmentation
  - dynamic vertexes, implicit nodes
- 414: data augmentation
  - dynamic vertexes, implicit nodes (Union only)

#### Typed Omega
- 501: 403
- 511: 413
