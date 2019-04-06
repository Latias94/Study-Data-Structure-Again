using System;

namespace UnionFind
{
    /// <summary>
    /// 第一版 Union-Find 本质就是一个数组
    /// 查找 O(1)  合并 O(n)
    /// </summary>
    public class UnionFind1 : IUnionFind
    {
        private int[] id;

        public UnionFind1(int size)
        {
            id = new int[size];

            // 初始化, 每一个 id[i] 指向自己
            for (int i = 0; i < size; i++)
                id[i] = i;
        }

        /// <summary>
        /// 查找元素 p 所对应的集合编号，O(1)
        /// </summary>
        private int Find(int p)
        {
            if (p < 0 || p >= id.Length)
                throw new ArgumentException("p is out of bound.");

            return id[p];
        }

        public int GetSize()
        {
            return id.Length;
        }

        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// 合并元素 p 和元素 q 所属的集合 O(n)
        /// </summary>
        public void UnionElements(int p, int q)
        {
            int pId = Find(p);
            int qId = Find(q);

            if (pId == qId)
                return;
            // 合并过程需要遍历一遍所有元素, 将两个元素的所属集合编号合并
            for (int i = 0; i < id.Length; i++)
                if (id[i] == pId)
                    id[i] = qId;
        }
    }
}