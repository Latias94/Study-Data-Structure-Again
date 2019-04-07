# 图

## 分类

### 无权图（Unweighted Graph）

![](https://i.loli.net/2019/04/07/5ca94bcacee69.png)

### 有权图（Weighted Graph）

![](https://i.loli.net/2019/04/07/5ca94bd109feb.png)

## 简单图

### 两个概念

![](https://i.loli.net/2019/04/07/5ca94bd5bf7b9.png)

- 平行边和自环边都不会改变连通性
- 最小生成树和最短路径算法中，自环边可忽略，平行边则选择距离最短那条边即可

简单图**没有平行边和自环边**

## 图的表示

### 邻接矩阵（Adjacency Matrix）

无向图：

![](https://i.loli.net/2019/04/07/5ca94bdb30235.png)

有向图：

![](https://i.loli.net/2019/04/07/5ca94bdfe4a7f.png)

### 邻接表（Adjacency Lists）

无向图：

![](https://i.loli.net/2019/04/07/5ca94be4b8a2a.png)

有向图：

![](https://i.loli.net/2019/04/07/5ca94be9ceb4a.png)

- 邻接表适合表示稀疏图（Sparse Graph）
- 邻接矩阵适合表示

## 连通分量

![](https://i.loli.net/2019/04/07/5ca94bf00621d.png)

## 图数据库

相对于关系型数据库来说，图数据库更支持这种数据结构，例如下图中能以 O(1) 的效率获取 Cindy 的朋友。

![](https://i.loli.net/2019/04/07/5ca94bf5b2087.png)

### 开源图数据库

- [Neo4j](https://neo4j.com/)
- [ArangoDB](https://www.arangodb.com/)
- [Apache Giraph](http://giraph.apache.org/)

## Dijkstra 算法

解决最短路径问题的算法有好几种，其中一种有趣的算法是由 Edsger Dijkstra（念为“dike’ struh”）于 1959 年发现的。该算法也很自然地被称为 Dijkstra 算法。

Dijkstra 算法的规则如下：

1. 以起步的顶点为当前顶点。
2. 检查当前顶点的所有邻接点，计算起点到所有已知顶点的权重，并记录下来。
3. 从未访问过（未曾作为当前顶点）的邻接点中，选取一个起点能到达的总权重最小的顶点，作为下一个当前顶点。
4. 重复前 3 步，直至图中所有顶点都被访问过。