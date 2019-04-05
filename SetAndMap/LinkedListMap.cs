namespace SetAndMap
{
    /// <summary>
    /// 基于链表实现的映射结构
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    public class LinkedListMap<TKey, TValue> : IMap<TKey, TValue>
    {
        private class Node
        {
            public TKey Key;
            public TValue Value;
            public Node Next;

            public Node(TKey key, TValue value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }

            public Node(TKey key, TValue value) : this(key, value, null)
            {
            }

            public Node() : this(default(TKey), default(TValue), null)
            {
            }

            public override string ToString()
            {
                return $"{{{Key} : {Value}}}";
            }
        }

        private Node dummyHead;
        private int size;

        public LinkedListMap()
        {
            dummyHead = new Node();
            size = 0;
        }

        private Node GetNode(TKey key)
        {
            Node cur = dummyHead.Next;
            while (cur != null)
            {
                if (cur.Key.Equals(key))
                {
                    return cur;
                }

                cur = cur.Next;
            }

            return null;
        }


        public void Set(TKey key, TValue value)
        {
            Node node = GetNode(key);
            // 找不到的话，就新建一个新节点加在在伪头结点后面
            if (node == null)
            {
                dummyHead.Next = new Node(key, value, dummyHead.Next);
                size++;
            }
            else
            {
                // 找到的话就更新
                node.Value = value;
            }
        }

        public void Delete(TKey key)
        {
            Node prev = dummyHead;
            while (prev.Next != null)
            {
                if (prev.Next.Key.Equals(key))
                {
                    break;
                }

                prev = prev.Next;
            }

            if (prev.Next != null)
            {
                Node deleteNode = prev.Next;
                prev.Next = deleteNode.Next;
                deleteNode.Next = null;
                size--;
            }
        }

        public bool Contains(TKey key)
        {
            return GetNode(key) != null;
        }

        public TValue Get(TKey key)
        {
            Node node = GetNode(key);
            return node == null ? default(TValue) : node.Value;
        }

        public int GetSize()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }
    }
}