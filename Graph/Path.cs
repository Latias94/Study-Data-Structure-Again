using System;
using System.Collections.Generic;

namespace Graph
{
    public class Path
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
        /// 从 v 开始遍历连通分量内的所有点
        /// </summary>
        private void Dfs(int v)
        {
            visited[v] = true;
            int[] arr = graph.Adj(v);
            foreach (int i in arr)
            {
                if (!visited[i])
                {
                    from[i] = v;
                    Dfs(i);
                }
            }
        }

        public Path(IGraph graph, int source)
        {
            this.graph = graph;
            // 算法初始化
            // 确保 source 在合适的范围内
            if (source < 0 || source >= graph.V()) return;
            visited = new bool[graph.V()];
            from = new int[graph.V()];
            for (int i = 0; i < graph.V(); ++i)
            {
                visited[i] = false;
                from[i] = -1;
            }

            // 设置路径开始的点的坐标
            this.source = source;

            // 寻路算法，稍加改动dfs就行了
            Dfs(source);
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
    }
}