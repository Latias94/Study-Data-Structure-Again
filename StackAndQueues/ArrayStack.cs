using Array;

namespace StackAndQueues
{
    /// <summary>
    /// 基于前面实现的动态数组 来实现栈结构
    /// 注意：跨 project 调用 Array 类，需要在 csproj 中添加引用
    /// </summary>
    public class ArrayStack<T> : IStack<T>
    {
        Array<T> arr;

        public int GetSize()
        {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new System.NotImplementedException();
        }

        public void Push(T element)
        {
            throw new System.NotImplementedException();
        }

        public T Pop()
        {
            throw new System.NotImplementedException();
        }

        public T Peek()
        {
            throw new System.NotImplementedException();
        }
    }
}