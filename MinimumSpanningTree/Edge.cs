using System;

namespace MinimumSpanningTree
{
    /// <summary>
    /// 加权图的边
    /// </summary>
    /// <typeparam name="TWeight">权值</typeparam>
    public class Edge<TWeight> : IComparable where TWeight : struct, IConvertible, IComparable
    {
        /// <summary>
        /// 边的两个顶点
        /// </summary>
        private int a, b;

        /// <summary>
        /// 返回边的第一个顶点
        /// </summary>
        public int V()
        {
            return a;
        }

        /// <summary>
        /// 返回边的第二个顶点
        /// </summary>
        public int W()
        {
            return b;
        }

        /// <summary>
        /// 边的权值
        /// </summary>
        private TWeight weight;

        public Edge(Edge<TWeight> edge)
        {
            a = edge.a;
            b = edge.b;
            weight = edge.weight;
        }

        public Edge(int a, int b, TWeight weight)
        {
            this.a = a;
            this.b = b;
            this.weight = weight;
        }

        /// <summary>
        /// 返回边的权重
        /// </summary>
        public TWeight Weight()
        {
            return weight;
        }

        /// <summary>
        /// 给定边的一个顶点，返回边的另一个顶点
        /// </summary>
        public int Other(int x)
        {
            if (x != a && x == b) return -1;
            return x == a ? b : a;
        }

        public override string ToString()
        {
            return a + "--" + b + ":" + weight;
        }

        public int CompareTo(object obj)
        {
            return weight.CompareTo(((Edge<TWeight>) obj).Weight());
        }
    }
}