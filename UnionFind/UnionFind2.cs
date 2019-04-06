using System;

namespace UnionFind
{
    /// <summary>
    /// 把每个元素看作一个树节点，不一样的是这里节点指向父节点
    /// 查找 O(h)  合并 O(h)   h 为树的高度
    /// </summary>
    public class UnionFind2 : IUnionFind
    {
        /// <summary>
        /// parent[i] 表示第 i 个元素所指向的父节点
        /// </summary>
        private int[] parent;

        public UnionFind2(int size)
        {
            parent = new int[size];

            for (int i = 0; i < size; i++)
                parent[i] = i;
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

            parent[pRoot] = qRoot;
        }
    }
}