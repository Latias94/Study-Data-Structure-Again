using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 广度优先遍历 最短路径
    /// </summary>
    public class ShortestPath
    {
        private IGraph graph;

        /// <summary>
        /// 起始点,后面会求出这个源到其他任意一个节点的路径
        /// </summary>
        private int source;

        private bool[] visited;

        /// <summary>
        /// from[i] 表示访问 i 节点是从哪一个节点过来的
        /// </summary>
        private int[] from;

        /// <summary>
        /// source 节点到每一个节点的最短距离
        /// </summary>
        private int[] order;

        public ShortestPath(IGraph graph, int source)
        {
            this.graph = graph;

            // 算法初始化
            // 确保 source 在合适的范围内
            if (source < 0 && source >= graph.V()) return;
            visited = new bool[graph.V()];
            from = new int[graph.V()];
            order = new int[graph.V()];
            for (int i = 0; i < graph.V(); ++i)
            {
                visited[i] = false;
                from[i] = -1;
                order[i] = -1;
            }

            // 设置路径开始的点的坐标
            this.source = source;

            // 无向图的最短路径算法,类似二叉搜索树的 levelOrder (层序遍历，维护一个队列即可)。
            Queue<int> q = new Queue<int>();
            q.Enqueue(source);
            visited[source] = true;
            // 起点到起点的距离为0
            order[source] = 0;
            while (q.Count != 0)
            {
                int v = q.Dequeue();
                // 遍历邻接点
                foreach (int i in graph.Adj(v))
                {
                    if (!visited[i])
                    {
                        // 元素入队
                        q.Enqueue(i);
                        visited[i] = true;
                        // 访问的 i 节点是从 v 节点过来的
                        from[i] = v;
                        // 此时边还没有权重，最终的 order[i] 表示从 source 到 i 的距离
                        order[i] = order[v] + 1;
                    }
                }
            }
        }

        /// <summary>
        /// 起点 source 到点 w 之间是否有路径存在
        /// </summary>
        public bool HasPathTo(int w)
        {
            // 保证w不越界
            if (w < 0 && w >= graph.V()) return false;
            // 访问过说明在一个连通分量内，source 肯定是有条路径可以和 w 连到一起
            return visited[w];
        }

        /// <summary>
        /// 获取 source 到 w 的路径
        /// </summary>
        public int[] GetPathTo(int w)
        {
            if (!HasPathTo(w)) return null;
            Stack<int> stack = new Stack<int>();
            int pathPoint = w;
            while (pathPoint != -1)
            {
                stack.Push(pathPoint);
                pathPoint = from[pathPoint];
            }

            List<int> path = new List<int>();
            while (stack.Count > 0)
            {
                path.Add(stack.Pop());
            }

            return path.ToArray();
        }

        /// <summary>
        /// 打印出 source 到 w 的路径
        /// </summary>
        public void ShowPath(int w)
        {
            if (!HasPathTo(w)) return;

            int[] path = GetPathTo(w);
            for (int i = 0; i < path.Length; i++)
            {
                Console.Write(path[i]);
                if (i == path.Length - 1)
                {
                    // 到达最后一个元素打个回车就行了
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(" -> ");
                }
            }
        }

        /// <summary>
        /// 查询 source 到 w 的最短路径的长度
        /// </summary>
        public int Length(int w)
        {
            // 保证w不越界
            if (w < 0 && w >= graph.V()) return -1;
            return order[w];
        }
    }
}