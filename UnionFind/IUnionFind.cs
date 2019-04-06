namespace UnionFind
{
    public interface IUnionFind
    {
        int GetSize();

        bool IsConnected(int p, int q);

        void UnionElements(int p, int q);
    }
}