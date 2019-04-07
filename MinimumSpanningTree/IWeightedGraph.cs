using System;

namespace MinimumSpanningTree
{
    public interface IWeightedGraph<TWeight> where TWeight : struct, IConvertible, IComparable
    {
        int V();

        int E();

        void AddEdge(Edge<TWeight> edge);

        bool HasEdge(int v, int w);

        void Show();

        Edge<TWeight>[] Adj(int v);
    }
}