namespace Graph
{
    /// <summary>
    /// 获取图的连通分量
    /// </summary>
    public class Component
    {
        private IGraph graph;

        /// <summary>
        /// visited[i]表示i处的点是否已被访问
        /// </summary>
        private bool[] visited;

        /// <summary>
        /// 连通分量的个数
        /// </summary>
        private int ccount;

        private int[] id;

        public Component(IGraph graph)
        {
            this.graph = graph;
            visited = new bool[graph.V()];
            id = new int[graph.V()];
            ccount = 0;
            for (int i = 0; i < graph.V(); ++i)
            {
                // 初始化的时候每一个点都没有被访问过
                visited[i] = false;
                id[i] = -1;
            }

            for (int i = 0; i < graph.V(); ++i)
            {
                if (!visited[i])
                {
                    Dfs(i);
                    ccount++;
                }
            }
        }

        private void Dfs(int v)
        {
            // 上来先把 v 设置为已访问
            visited[v] = true;
            // 同一个连通分量内，其 parent(即为 id[i] 的值)设置为当前连通分量的顺序号即可
            id[v] = ccount;
            int[] arr = graph.Adj(v);
            // 遍历邻接点
            foreach (int i in arr)
            {
                if (!visited[i])
                {
                    Dfs(i);
                }
            }
        }
        
        /// <summary>
        /// 返回连通分量的个数
        /// </summary>
        public int Count()
        {
            return ccount;
        }

        /// <summary>
        /// 判断两点之间是否连通
        /// </summary>
        public bool IsConnected(int v, int w)
        {
            // 检测元素是否过界
            if (v < 0 || v > graph.V()) return false;
            if (w < 0 || w > graph.V()) return false;
            return id[v] == id[w];
        }
    }
}