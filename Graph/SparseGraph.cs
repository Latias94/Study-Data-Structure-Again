using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 稀疏图(Sparse Graph)- 邻接表
    /// </summary>
    public class SparseGraph : IGraph
    {
        // 图的点数和边数
        private int n, m;

        // 是否是有向图
        private bool directed;

        // 邻接表
        private List<List<int>> g;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="n">顶点数</param>
        /// <param name="directed">是否为有向图</param>
        public SparseGraph(int n, bool directed)
        {
            this.n = n;
            this.m = 0;
            this.directed = directed;
            g = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                g.Add(new List<int>());
            }
        }

        // 返回顶点数
        public int V()
        {
            return n;
        }

        // 返回边数
        public int E()
        {
            return m;
        }

        // 添加一条边，参数是两个顶点 允许有平行边
        public void AddEdge(int v, int w)
        {
            if (v < 0 || w < 0 || v >= n || w >= n) return;
            if (HasEdge(v, w)) return;
            g[v].Add(w);
            // 如果不是自环边且是无向图
            if (v != w && !directed)
            {
                g[w].Add(v);
            }

            m++;
        }

        public bool HasEdge(int v, int w)
        {
            if (v < 0 || w < 0 || v >= n || w >= n) return false;
            return g[v].Contains(w);
        }

        public void Show()
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write("vertex " + i + ":\t");
                int[] adjArr = Adj(i);
                for (int j = 0; j < adjArr.Length; j++)
                {
                    Console.Write(adjArr[j] + "\t");
                }

                Console.WriteLine();
            }
        }

        // 返回顶点 v 的所有邻边
        public int[] Adj(int v)
        {
            if (v >= n || v < 0) return new int[0];

            return g[v].ToArray();
        }
    }
}