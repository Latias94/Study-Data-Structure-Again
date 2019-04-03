namespace StackAndQueues
{
    public interface IStack<T>
    {
        /// <summary>
        /// 获取Stack的元素数量
        /// </summary>
        int GetSize();

        /// <summary>
        /// 判断 Stack 是否为空
        /// </summary>
        bool IsEmpty();

        /// <summary>
        /// 向 Stack 中添加元素
        /// </summary>
        /// <param name="element">待添加元素</param>
        void Push(T element);

        /// <summary>
        /// 弹出栈顶元素
        /// </summary>
        /// <returns>栈顶元素</returns>
        T Pop();

        /// <summary>
        /// 获取栈顶元素但是不弹出
        /// </summary>
        /// <returns>栈顶元素</returns>
        T Peek();
    }
}