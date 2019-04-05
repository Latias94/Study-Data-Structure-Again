using System;
using StackAndQueues;

namespace HeapAndPriorityQueue
{
    /// <summary>
    /// 基于二叉堆的优先队列
    /// 出队顺序和入队顺序无关，和优先级有关
    /// 基于堆的优先队列入队的时间复杂度为 O(logn) 出队的时间复杂度为 O(logn)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : IQueue<T> where T : IComparable
    {
        private MaxHeap<T> maxHeap;

        public PriorityQueue()
        {
            maxHeap = new MaxHeap<T>();
        }

        public int GetSize()
        {
            return maxHeap.Size();
        }

        public bool IsEmpty()
        {
            return maxHeap.IsEmpty();
        }

        public void Enqueue(T element)
        {
            maxHeap.Insert(element);
        }

        /// <summary>
        /// 出队 拿出最大元素
        /// </summary>
        /// <returns>最大元素</returns>
        public T Dequeue()
        {
            return maxHeap.ExtractMax();
        }

        public T GetFront()
        {
            return maxHeap.GetMax();
        }
    }
}