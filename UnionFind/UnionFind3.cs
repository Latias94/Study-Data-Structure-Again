using System;

namespace UnionFind
{
    /// <summary>
    /// UnionFind2 中每次合并，都会把新元素当作根节点，这样树会很高 
    /// UnionFind3 的解决方案就是要考虑当前树有多少个节点
    /// 如果树有太多节点，新元素可以指向原来的根节点来减少高度
    /// 查找 O(h)  合并 O(h)   h 为树的高度 
    /// </summary>
    public class UnionFind3 : IUnionFind
    {
        /// <summary>
        /// parent[i] 表示第 i 个元素所指向的父节点
        /// </summary>
        private int[] parent;

        /// <summary>
        /// 表示以 i 为根的集合中元素个数
        /// </summary>
        private int[] sz;

        public UnionFind3(int size)
        {
            parent = new int[size];
            sz = new int[size];

            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                sz[i] = 1;
            }
        }

        public int GetSize()
        {
            return parent.Length;
        }

        /// <summary>
        /// 查找过程, 查找元素 p 所对应的集合编号。O(h)，h 为树的高度
        /// </summary>
        private int Find(int p)
        {
            if (p < 0 || p >= parent.Length)
                throw new ArgumentException("p is out of bound.");

            // 不断去查询自己的父亲节点, 直到到达根节点
            // 根节点的特点: parent[p] == p
            while (p != parent[p])
                p = parent[p];
            return p;
        }

        /// <summary>
        /// 查看元素 p 和元素 q 是否所属一个集合。O(h)，h 为树的高度
        /// </summary>
        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// 合并元素 p 和元素 q 所属的集合。O(h)，h 为树的高度
        /// </summary>
        public void UnionElements(int p, int q)
        {
            int pRoot = Find(p);
            int qRoot = Find(q);

            if (pRoot == qRoot)
                return;

            // 如果 qRoot 的树节点比较多，就让 pRoot 的树接到 qRoot 上去
            if (sz[pRoot] < sz[qRoot])
            {
                parent[pRoot] = qRoot;
                sz[qRoot] += sz[pRoot];
            }
            else
            {
                parent[qRoot] = pRoot;
                sz[pRoot] += sz[qRoot];
            }
        }
    }
}