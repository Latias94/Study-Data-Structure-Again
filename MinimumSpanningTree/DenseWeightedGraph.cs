using System;
using System.Collections.Generic;

namespace MinimumSpanningTree
{
    /// <summary>
    /// 加权稠密图 - 邻接矩阵
    /// </summary>
    /// <typeparam name="TWeight">权值</typeparam>
    public class DenseWeightedGraph<TWeight> : IWeightGraph<TWeight> where TWeight : struct, IConvertible, IComparable
    {
        // 图的点数和边数
        private int vertices, edges;

        // 是否是有向图
        private bool directed;

        // 邻接矩阵 二维矩阵
        private Edge<TWeight>[,] adj;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="vertices">顶点数</param>
        /// <param name="directed">是否为有向图</param>
        public DenseWeightedGraph(int vertices, bool directed)
        {
            this.vertices = vertices;
            this.edges = 0;
            this.directed = directed;
            adj = new Edge<TWeight>[vertices, vertices];
            // g 初始化为 vertices * vertices 的布尔矩阵, 每一个 g[i][j] 均为null, 表示没有任和边
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    adj[i, j] = null;
                }
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
        /// 添加一条边，参数是两个顶点
        /// </summary>
        public void AddEdge(Edge<TWeight> edge)
        {
            if (edge.V() < 0 || edge.W() < 0 || edge.V() >= vertices || edge.W() >= vertices) return;
            if (HasEdge(edge.V(), edge.W())) return;
            adj[edge.V(), edge.W()] = new Edge<TWeight>(edge);
            if (edge.V() != edge.W() && !directed)
            {
                // 无向图实际上是双向图，所以 w 到 v 也应该为true，如果是有向图这步就不用处理了
                // 注意这里 new 了一个 Edge 而不是直接传入 edge，是因为要加入地是 w-->v 的边。上面是 v->w 的边
                adj[edge.W(), edge.V()] = new Edge<TWeight>(edge);
            }

            edges++;
        }

        public bool HasEdge(int v, int w)
        {
            if (v < 0 || w < 0 || v >= vertices || w >= vertices) return false;
            return adj[v, w] != null;
        }

        public void Show()
        {
            for (int i = 0; i < vertices; i++)
            {
                Console.Write("vertex " + i + ":\t");
                Edge<TWeight>[] adjArr = Adj(i);
                for (int j = 0; j < adjArr.Length; j++)
                {
                    if (adjArr[j] != null)
                    {
                        Console.Write(adjArr[j] + "\t");
                    }
                    else
                    {
                        Console.Write("  NULL  \t");
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 返回顶点 v 的所有邻边（包括空边）
        /// </summary>
        public Edge<TWeight>[] Adj(int v)
        {
            if (v >= vertices || v < 0) return new Edge<TWeight>[0];
            List<Edge<TWeight>> result = new List<Edge<TWeight>>();
            for (int i = 0; i < vertices; i++)
            {
                result.Add(adj[v, i]);
            }

            return result.ToArray();
        }
    }
}