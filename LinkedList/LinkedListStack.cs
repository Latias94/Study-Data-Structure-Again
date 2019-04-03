using System.Text;
using StackAndQueues;

namespace LinkedList
{
    /// <summary>
    /// 基于链表来实现栈结构
    /// 注意：跨 project 调用 IStack 接口，需要在 csproj 中添加引用，本项目已添加引用
    /// </summary>
    public class LinkedListStack<T> : IStack<T>
    {
        private LinkedList<T> list;

        public LinkedListStack() {
            list = new LinkedList<T>();
        }
        public int GetSize()
        {
            return list.GetSize();
        }

        public bool IsEmpty()
        {
            return list.IsEmpty();
        }

        public void Push(T element)
        {
            list.AddFirst(element);
        }

        public T Pop()
        {
            return list.DeleteFirst();
        }

        public T Peek()
        {
            return list.GetFirst();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Stack: peek ");
            sb.Append(list);
            return sb.ToString();
        }
    }
}