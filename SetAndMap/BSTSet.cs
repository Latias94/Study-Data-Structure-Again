using System;
using BST;

namespace SetAndMap
{
    /// <summary>
    /// 基于 BST 来实现集合结构
    /// 注意：跨 project 调用 BST 类，需要在 csproj 中添加引用，本项目已添加引用
    /// </summary>
    public class BSTSet<T> : ISet<T> where T : IComparable
    {
        private BST<T, T> bst;

        public BSTSet()
        {
            bst = new BST<T, T>();
        }

        public void Add(T element)
        {
            bst.Add(element, element);
        }

        public void Delete(T element)
        {
            bst.DeleteNode(element);
        }

        public bool Contain(T element)
        {
            return bst.Contain(element);
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