using System;
using System.Text;

namespace LinkedList
{
    /// <summary>
    /// 链表
    /// </summary>
    public class LinkedList<T>
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

        /// <summary>
        /// 链表的伪头节点，这样循环时不用对 head 头节点做单独处理
        /// </summary>
        private Node dummyHead;

        /// <summary>
        /// 链表的头节点，有了伪头节点的引用，暂时就不需要头节点的引用了
        /// </summary>
//        private Node head;

        private int size;

        public LinkedList()
        {
            dummyHead = new Node(default(T), null);
            size = 0;
        }

        /// <summary>
        /// 获取链表节点数目
        /// </summary>
        public int GetSize()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        /// <summary>
        /// 链表头添加元素
        /// </summary>
        public void AddFirst(T element)
        {
            Insert(0, element);
        }

        /// <summary>
        /// 链表尾加入元素
        /// </summary>
        public void AddLast(T element)
        {
            Insert(size, element);
        }

        /// <summary>
        /// 在链表的 index(从 0 开始)位置添加新的元素 element
        /// </summary>
        public void Insert(int index, T element)
        {
            if (index < 0 || index > size)
            {
                throw new ArgumentException("插入失败！index 范围非法！");
            }

            Node prev = dummyHead;
            for (int i = 0; i < index; i++)
            {
                prev = prev.next;
            }

            prev.next = new Node(element, prev.next);
            size++;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentException("插入失败！index 范围非法！");
            }

            Node cur = dummyHead.next;
            for (int i = 0; i < index; i++)
            {
                cur = cur.next;
            }

            return cur.element;
        }

        /// <summary>
        /// 获取链表的第一个元素
        /// </summary>
        public T GetFirst()
        {
            return Get(0);
        }

        /// <summary>
        /// 获取链表的最后一个元素
        /// </summary>
        public T GetLast()
        {
            return Get(size - 1);
        }

        /// <summary>
        /// 更新链表的第 index 个位置的元素(从 0 开始计)为 element
        /// </summary>
        public void Update(int index, T element)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentException("更新失败！index范围违法！");
            }

            Node cur = dummyHead.next;
            for (int i = 0; i < index; i++)
            {
                cur = cur.next;
            }

            cur.element = element;
        }
        
        public T Delete(int index) {
            if (index < 0 || index > size) {
                throw new ArgumentException("插入失败！index 范围非法！");
            }
            Node prev = dummyHead;
            for (int i = 0; i < index; i++) {
                prev = prev.next;
            }
            Node node = prev.next;
            prev.next = node.next;
            node.next = null;
            size--;
            return node.element;
        }
        
        /// <summary>
        /// 删除链表头元素
        /// </summary>
        public T DeleteFirst() {
            return Delete(0);
        }

        /// <summary>
        /// 删除链表尾元素
        /// </summary>
        public T DeleteLast() {
            return Delete(size - 1);
        }

        /// <summary>
        /// 删除链表中元素 e
        /// </summary>
        public void DeleteElement(T element) {

            Node prev = dummyHead;
            while (prev.next != null) {
                if (prev.next.element.Equals(element)) {
                    break;
                }
                prev = prev.next;
            }

            if (prev.next != null) {
                Node node = prev.next;
                prev.next = node.next;
                node.next = null;
                size--;
            }
        }


        /// <summary>
        /// 查询链表中是否有元素 element
        /// </summary>
        public bool Contain(T element)
        {
            Node cur = dummyHead.next;
            while (cur != null)
            {
                if (cur.element.Equals(element))
                {
                    return true;
                }

                cur = cur.next;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node cur = dummyHead.next;
            while (cur != null)
            {
                sb.Append(cur + " -> ");
                cur = cur.next;
            }

            sb.Append("NULL");
            return sb.ToString();
        }
    }
}