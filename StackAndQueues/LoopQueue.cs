using System;
using System.Text;

namespace StackAndQueues
{
    /// <summary>
    /// 循环队列
    /// </summary>
    public class LoopQueue<T> : IQueue<T>
    {
        // 这里用原生数组
        private T[] arr;

        /// <summary>
        /// 队首元素的位置
        /// </summary>
        private int front;

        /// <summary>
        /// 队尾元素的下一个位置
        /// </summary>
        private int tail;

        /// <summary>
        /// 循环队列中的元素数
        /// </summary>
        private int size;

        /// <summary>
        /// 循环队列的容量，记得要有一个要闲置从而可以区分判空和判满的条件，条件如下：
        /// 这里刻意浪费一个空间，这样判断当 tail == front 时队列为空(c 为队列长度)会更加容易，实现也更简洁。
        /// (tail + 1) % c == front 时队列满
        /// </summary>
        private int capacity;

        public LoopQueue(int capacity)
        {
            arr = new T[capacity + 1];
            front = 0;
            tail = 0;
            size = 0;
        }

        public LoopQueue() : this(10)
        {
        }

        public int GetSize()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return front == tail;
        }

        public void Enqueue(T element)
        {
            if ((tail + 1) % arr.Length == front)
            {
                // 循环队列满了就自动扩容
                Resize(GetCapacity() * 2);
            }

            arr[tail] = element;
            // 防止越界
            tail = (tail + 1) % arr.Length;
            size++;
        }

        private void Resize(int newCapacity)
        {
            capacity = newCapacity;
            T[] newArr = new T[newCapacity + 1];
            for (int i = 0; i < size; i++)
            {
                // 从 front 开始遍历数组
                newArr[i] = arr[(i + front) % arr.Length];
            }

            arr = newArr;
            front = 0;
            tail = size;
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new ArgumentException("队列不能为空！");
            }

            T value = arr[front];
            arr[front] = default(T);
            front = (front + 1) % arr.Length;
            size--;
            // 当数组中元素数小于容量的 1/4 时，自动缩容为原来的一半.之所以选 1/4 是为了防止频繁扩容和缩容引起性能下降
            if (size == GetCapacity() / 4 && GetCapacity() / 2 != 0)
            {
                Resize(GetCapacity() / 2);
            }

            return value;
        }

        public T GetFront()
        {
            if (IsEmpty())
            {
                throw new ArgumentException("队列不能为空！");
            }

            return arr[front];
        }

        public int GetCapacity()
        {
            return capacity;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append($"Queue: size = {size} , capacity = {GetCapacity()}\n");
            res.Append("front -> [");
            for (int i = front; i != tail; i = (i + 1) % arr.Length)
            {
                res.Append(arr[i]);
                if ((i + 1) % arr.Length != tail)
                {
                    res.Append(", ");
                }
            }

            res.Append("] <- tail");
            return res.ToString();
        }
    }
}