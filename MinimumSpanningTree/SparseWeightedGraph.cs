using System;
using System.Collections.Generic;

namespace MinimumSpanningTree
{
    /// <summary>
    /// 加权稀疏图 - 邻接表
    /// </summary>
    /// <typeparam name="TWeight">权值</typeparam>
    public class SparseWeightedGraph<TWeight> : IWeightedGraph<TWeight> where TWeight : struct, IConvertible, IComparable
    {
        // 图的点数和边数
        private int vertices, edges;

        // 是否是有向图
        private bool directed;

        // 邻接表
        private List<List<Edge<TWeight>>> adj;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="vertices">顶点数</param>
        /// <param name="directed">是否为有向图</param>
        public SparseWeightedGraph(int vertices, bool directed)
        {
            this.vertices = vertices;
            this.edges = 0;
            this.directed = directed;
            adj = new List<List<Edge<TWeight>>>();
            for (int i = 0; i < vertices; i++)
            {
                adj.Add(new List<Edge<TWeight>>());
            }
        }

        /// <summary>
        /// 返回顶点数
        /// </summary>
        public int V() => vertices;

        /// <summary>
        /// 返回边数
        /// </summary>
        public int E() => edges;

        /// <summary>
        /// 添加边，在 v 和 w 之间建立一条边
        /// </summary>
        public void AddEdge(Edge<TWeight> edge)
        {
            if (edge.V() < 0 || edge.W() < 0 || edge.V() >= vertices || edge.W() >= vertices) return;
            if (HasEdge(edge.V(), edge.W())) return;
            adj[edge.V()].Add(new Edge<TWeight>(edge));
            if (edge.V() != edge.W() && !directed)
            {
                // 无向图实际上是双向图，所以 w 到 v 也应该为true，如果是有向图这步就不用处理了
                // 注意这里 new 了一个 Edge 而不是直接传入 edge，是因为要加入地是 w-->v 的边。上面是 v->w 的边
                adj[edge.W()].Add(new Edge<TWeight>(edge.W(), edge.V(), edge.Weight()));
            }

            edges++;
        }

        /// <summary>
        /// 判断 v 到 w 之间是否存在边
        /// </summary>
        public bool HasEdge(int v, int w)
        {
            if (v < 0 || w < 0 || v >= vertices || w >= vertices) return false;
            foreach (var edge in adj[v])
            {
                if (edge.Other(v) == w) return true;
            }

            return false;
        }

        public void Show()
        {
            for (int i = 0; i < vertices; i++)
            {
                Console.Write("vertex " + i + ":\t");
                Edge<TWeight>[] adjArr = Adj(i);
                for (int j = 0; j < adjArr.Length; j++)
                {
                    Console.Write(adjArr[j] + "\t");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 返回顶点 v 的所有邻边
        /// </summary>
        public Edge<TWeight>[] Adj(int v)
        {
            if (v >= vertices || v < 0) return new Edge<TWeight>[0];

            return adj[v].ToArray();
        }
    }
}