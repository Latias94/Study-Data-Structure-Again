namespace Graph
{
    public interface IGraph
    {
        int V();

        int E();

        void AddEdge(int v, int w);

        bool HasEdge(int v, int w);

        void Show();

        int[] Adj(int v);
    }
}