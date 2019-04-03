using System;
using System.Text;
using StackAndQueues;

namespace LinkedList
{
    /// <summary>
    /// 基于链表来实现队列结构
    /// 注意：跨 project 调用 IQueue 接口，需要在 csproj 中添加引用，本项目已添加引用
    /// </summary>
    public class LinkedListQueue<T> : IQueue<T>
    {
        private class Node
        {
            public T element;
            public Node next;

            public Node(T element, Node next)
            {
                this.element = element;
                this.next = next;
            }

            public Node(T element) : this(element, null)
            {
            }

            public Node() : this(default(T), null)
            {
            }

            public override string ToString()
            {
                return $"{{element={element}}}";
            }
        }

        private Node head, tail;
        private int size;

        public LinkedListQueue()
        {
            head = null;
            tail = null;
            size = 0;
        }

        public int GetSize()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Enqueue(T element)
        {
            // 从队列尾部入队
            if (tail == null)
            {
                // tail 为 null 说明之前没有元素
                tail = new Node(element);
                head = tail;
            }
            else
            {
                tail.next = new Node(element);
                tail = tail.next;
            }

            size++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) {
                throw new ArgumentException("队列不能为空！");
            }
            // 从队头删除元素(出队)
            Node node = head;
            head = head.next;
            // 把原来的头结点置空
            node.next = null;
            if (head == null) {
                // 如果头结点指针指向 null，说明队列中没元素了，tail 也该为 null
                tail = null;
            }
            size--;
            return node.element;
        }

        public T GetFront()
        {
            if (IsEmpty()) {
                throw new ArgumentException("队列不能为空！");
            }
            return head.element;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Queue: front ");
            Node cur = head;
            while (cur != null) {
                sb.Append(cur + " -> ");
                cur = cur.next;
            }
            sb.Append("NULL tail");
            return sb.ToString();
        }
    }
}