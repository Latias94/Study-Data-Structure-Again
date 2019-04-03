using Array;

namespace StackAndQueues
{
    /// <summary>
    /// 基于前面实现的动态数组 来实现队列结构
    /// 注意：跨 project 调用 Array 类，需要在 csproj 中添加引用，本项目已添加引用
    /// </summary>
    public class ArrayQueue<T> : IQueue<T>
    {
        private Array<T> arr;

        public ArrayQueue(int capacity)
        {
            arr = new Array<T>(capacity);
        }

        public ArrayQueue() : this(10)
        {
        }

        public int GetSize()
        {
            return arr.GetSize();
        }

        public bool IsEmpty()
        {
            return arr.IsEmpty();
        }

        public void Enqueue(T element)
        {
            arr.AddLast(element);
        }

        public T Dequeue()
        {
            return arr.RemoveFirst();
        }

        public T GetFront()
        {
            return arr.GetFirst();
        }

        /// <summary>
        /// 获取队列的容量
        /// </summary>
        public int GetCapacity()
        {
            return arr.GetCapacity();
        }

        public override string ToString()
        {
            return $"ArrayQueue: {arr}";
        }
    }
}