using System;
using System.Collections.Generic;
using HeapAndPriorityQueue;

namespace MinimumSpanningTree
{
    /// <summary>
    /// 使用 Prim 算法求图的最小生成树 O(ElogE) E 为边数
    /// </summary>
    public class LazyPrimMST<TWeight> where TWeight : struct, IConvertible, IComparable
    {
        /// <summary>
        /// 图的引用
        /// </summary>
        private IWeightedGraph<TWeight> graph;

        /// <summary>
        /// 最小堆, 算法辅助数据结构
        /// </summary>
        private MinHeap<Edge<TWeight>> minHeap;

        /// <summary>
        /// 标记数组, 在算法运行过程中标记节点 i 是否被访问过
        /// </summary>
        private bool[] marked;

        /// <summary>
        /// 最小生成树所包含的所有边
        /// </summary>
        private List<Edge<TWeight>> mst;

        /// <summary>
        /// 最小生成树的权值
        /// </summary>
        private TWeight mstWeight;

        public LazyPrimMST(IWeightedGraph<TWeight> graph)
        {
            this.graph = graph;
            minHeap = new MinHeap<Edge<TWeight>>(graph.E());
            marked = new bool[graph.V()];
            mst = new List<Edge<TWeight>>();

            Visit(0);
            while (!minHeap.IsEmpty())
            {
                // 使用最小堆找出已经访问的边中权值最小的边
                Edge<TWeight> e = minHeap.ExtractMin();
                // 如果这条边的两端都已经访问过了, 则扔掉这条边
                if (marked[e.V()] == marked[e.W()])
                {
                    continue;
                }

                // 否则, 这条边则应该存在在最小生成树中
                mst.Add(e);

                // 访问和这条边连接的还没有被访问过的节点
                if (!marked[e.V()])
                    Visit(e.V());
                else
                    Visit(e.W());
            }

            // 计算最小生成树的权值
            mstWeight = mst[0].Weight();
            for (int i = 1; i < mst.Count; i++)
                mstWeight = Add(mstWeight, mst[i].Weight());
        }

        /// <summary>
        /// 访问节点 v
        /// </summary>
        private void Visit(int v)
        {
            if (marked[v])
            {
                throw new ArgumentException("v has already been visited");
            }

            marked[v] = true;

            // 将和节点 v 相连接的所有未访问的边放入最小堆中
            foreach (Edge<TWeight> e in graph.Adj(v))
                if (e.Other(v) > 0 && !marked[e.Other(v)])
                    minHeap.Insert(e);
        }

        // 返回最小生成树的所有边
        public List<Edge<TWeight>> MstEdges()
        {
            return mst;
        }

        // 返回最小生成树的权值
        public TWeight Result()
        {
            return mstWeight;
        }

        // 泛型相加 用了 C# 的动态类型
        private static T Add<T>(T number1, T number2)
        {
            dynamic dynamic1 = number1;
            dynamic dynamic2 = number2;
            return dynamic1 + dynamic2;
        }
    }
}