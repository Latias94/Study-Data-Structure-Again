using System;
using SetAndMap;

namespace AVLTree
{
    public class AVLMap<TKey, TValue> : IMap<TKey, TValue> where TKey : IComparable
    {
        private AVLTree<TKey, TValue> avlTree;

        public AVLMap()
        {
            avlTree = new AVLTree<TKey, TValue>();
        }

        public void Set(TKey key, TValue value)
        {
            avlTree.Add(key, value);
        }

        public void Delete(TKey key)
        {
            avlTree.DeleteNode(key);
        }

        public bool Contains(TKey key)
        {
            return avlTree.Contain(key);
        }

        public TValue Get(TKey key)
        {
            return avlTree.Get(key);
        }

        public int GetSize()
        {
            return avlTree.Size();
        }

        public bool IsEmpty()
        {
            return avlTree.IsEmpty();
        }
    }
}