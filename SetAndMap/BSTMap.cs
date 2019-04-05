using System;
using BST;

namespace SetAndMap
{
    /// <summary>
    /// 基于二分查找树实现的映射结构
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    public class BSTMap<TKey, TValue> : IMap<TKey, TValue> where TKey : IComparable
    {
        private BST<TKey, TValue> bst;

        public BSTMap()
        {
            bst = new BST<TKey, TValue>();
        }

        public void Set(TKey key, TValue value)
        {
            bst.Insert(key, value);
        }

        public void Delete(TKey key)
        {
            bst.DeleteNode(key);
        }

        public bool Contains(TKey key)
        {
            return bst.Contain(key);
        }

        public TValue Get(TKey key)
        {
            return bst.Search(key);
        }

        public int GetSize()
        {
            return bst.Size();
        }

        public bool IsEmpty()
        {
            return bst.IsEmpty();
        }
    }
}