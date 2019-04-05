using System;
using Array;

namespace HeapAndPriorityQueue
{
    /// <summary>
    /// 基于之前实现的动态扩容数组来实现最大堆（二叉堆）结构
    /// 注意：跨 project 调用 Array 类，需要在 csproj 中添加引用，本项目已添加引用
    /// </summary>
    public class MaxHeap<T> where T : IComparable
    {
        private Array<T> data;

        public MaxHeap(int capacity)
        {
            data = new Array<T>(capacity);
        }

        public MaxHeap()
        {
            data = new Array<T>();
        }

        /// <summary>
        /// Heapify: 将任意数组整理成堆的形状
        ///     操作：从不是叶子节点的最后一个节点开始，一个一个进行 sift down，
        ///          这样能少操作近乎占了一半的叶子结点
        ///     时间复杂度为 O(n)
        /// </summary>
        public MaxHeap(T[] arr)
        {
            data = new Array<T>(arr);
            for (int i = Parent(arr.Length - 1); i >= 0; i--)
            {
                SiftDown(i);
            }
        }

        public int Size()
        {
            return data.GetSize();
        }

        public bool IsEmpty()
        {
            return data.IsEmpty();
        }

        /// <summary>
        /// 索引所表示的节点 的父节点的索引
        /// </summary>
        /// <param name="index">要查询的节点索引</param>
        /// <returns>父节点索引</returns>
        private int Parent(int index)
        {
            if (index == 0)
            {
                throw new ArgumentException("index-0 doesn't have parent.");
            }

            // 二叉堆性质
            return (index - 1) / 2;
        }

        /// <summary>
        /// 返回完全二叉树的数组表示中，一个索引所表示的元素的左孩子节点的索引
        /// </summary>
        /// <param name="index">要查询的节点索引</param>
        /// <returns>左孩子节点的索引</returns>
        private int LeftChild(int index)
        {
            return index * 2 + 1;
        }

        /// <summary>
        /// 返回完全二叉树的数组表示中，一个索引所表示的元素的右孩子节点的索引
        /// </summary>
        /// <param name="index">要查询的节点索引</param>
        /// <returns>右孩子节点的索引</returns>
        private int RightChild(int index)
        {
            return index * 2 + 2;
        }

        public void Insert(T element)
        {
            data.AddLast(element);
            SiftUp(data.GetSize() - 1);
        }

        private void SiftUp(int k)
        {
            // 只要节点比父节点小，就不断与父节点交换位置
            while (k > 0 && data.Get(Parent(k)).CompareTo(data.Get(k)) < 0)
            {
                data.Swap(k, Parent(k));
                k = Parent(k);
            }
        }
        
        private void SiftDown(int k) {
            while (LeftChild(k) < data.GetSize()) {
                int j = LeftChild(k);
                if (j + 1 < data.GetSize() &&
                    data.Get(j + 1).CompareTo(data.Get(j)) > 0) {
                    // j 记录 k 左右子树中找比较大的值的索引
                    j++;
                }
                // 根据最大堆的性质，父节点要比左右子树节点都大
                // 如果 k 这个父节点比较大的子节点还大，就不用再操作了
                if (data.Get(k).CompareTo(data.Get(j)) >= 0) {
                    break;
                }
                // 将 k 父节点和较大值的节点交换位置
                data.Swap(k, j);
                k = j;
            }
        }


        /// <summary>
        /// 查看堆中的最大元素
        /// </summary>
        /// <returns>最大的元素</returns>
        public T GetMax()
        {
            if (data.GetSize() == 0)
            {
                throw new ArgumentException("Can not getMax when heap is empty.");
            }

            return data.Get(0);
        }
        
        /// <summary>
        /// 弹出出堆中最大元素
        /// </summary>
        /// <returns>最大的元素</returns>
        public T ExtractMax() {
            T max = GetMax();
            // 把最小的元素放到根节点，然后再端 sift down 还原堆结构
            data.Swap(0, data.GetSize() - 1);
            data.RemoveLast();
            SiftDown(0);
            return max;
        }
        
        /// <summary>
        /// 取出堆中的最大元素，并用新元素替换它
        /// </summary>
        /// <param name="element">新元素</param>
        /// <returns>取出的最大的元素</returns>
        public T Replace(T element) {
            T ret = GetMax();
            data.Set(0, element);
            SiftDown(0);
            return ret;
        }
    }
}