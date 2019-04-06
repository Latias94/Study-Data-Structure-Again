using System;
using SetAndMap;

namespace AVLTree
{
    public class AVLSet<T> : ISet<T> where T : IComparable
    {
        private AVLTree<T, T> avlTree;

        public AVLSet()
        {
            avlTree = new AVLTree<T, T>();
        }

        public void Add(T element)
        {
            avlTree.Add(element, element);
        }

        public void Delete(T element)
        {
            avlTree.DeleteNode(element);
        }

        public bool Contain(T element)
        {
            return avlTree.Contain(element);
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