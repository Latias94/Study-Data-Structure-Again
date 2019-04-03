namespace StackAndQueues
{
    public interface IQueue<T>
    {
        /// <summary>
        /// 获取 Queue 的元素数
        /// </summary>
        int GetSize();

        /// <summary>
        /// 判断 Queue 是否为空
        /// </summary>
        bool IsEmpty();

        /// <summary>
        /// 从队尾入队
        /// </summary>
        void Enqueue(T element);

        /// <summary>
        /// 从队首出队
        /// </summary>
        T Dequeue();

        /// <summary>
        /// 获取队头元素但是不弹出
        /// </summary>
        T GetFront();
    }
}