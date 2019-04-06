using System;
using System.Collections.Generic;

namespace RedBlackTree
{
    /// <summary>
    /// 红黑树
    /// 最大高度：2logn  因此查找相比 avl tree 会慢一点
    /// 但是添加元素和删除元素相比 avl tree 会快速一些
    /// 增删改查时间复杂度都是 O(logn)
    /// </summary>
    public class RBTree<TKey, TValue> where TKey : IComparable
    {
        private static readonly bool RED = true;
        private static readonly bool BLACK = false;

        private class Node
        {
            public TKey key;

            public TValue value;

            /// <summary>
            /// 左子节点
            /// </summary>
            public Node left;

            /// <summary>
            /// 右子节点
            /// </summary>
            public Node right;

            /// <summary>
            /// 棋子是红色还是黑色
            /// </summary>
            public bool color;

            public Node(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                left = null;
                right = null;
                color = RED;
            }

            public Node(Node node)
            {
                key = node.key;
                value = node.value;
                left = node.left;
                right = node.right;
                color = RED;
            }
        }

        /// <summary>
        /// 根节点
        /// </summary>
        private Node root;

        /// <summary>
        /// 节点数
        /// </summary>
        private int count;

        public RBTree()
        {
            root = null;
            count = 0;
        }


        public int Size()
        {
            return count;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public void Add(TKey key, TValue value)
        {
            root = Add(root, key, value);
            root.color = BLACK; // 根节点为黑色
        }

        public bool Contain(TKey key)
        {
            return GetNode(root, key) != null;
        }

        public TValue Get(TKey key)
        {
            Node node = GetNode(root, key);
            return node == null ? default(TValue) : node.value;
        }

        public void Set(TKey key, TValue newValue)
        {
            Node node = GetNode(root, key);
            if (node == null)
                throw new ArgumentException(key + " doesn't exist!");

            node.value = newValue;
        }

        /// <summary>
        /// 寻找最小的键值
        /// </summary>
        public TKey Min()
        {
            if (count == 0)
                throw new IndexOutOfRangeException("BST is empty");
            Node minNode = Min(root);
            return minNode.key;
        }

        /// <summary>
        /// 寻找最大的键值
        /// </summary>
        public TKey Max()
        {
            if (count == 0)
                throw new IndexOutOfRangeException("BST is empty");
            Node maxNode = Max(root);
            return maxNode.key;
        }

        /// <summary>
        /// 删除键值最小的节点
        /// </summary>
        public void DeleteMin()
        {
            if (root != null)
            {
                root = DeleteMin(root);
            }
        }

        /// <summary>
        /// 删除键值最大的节点
        /// </summary>
        public void DeleteMax()
        {
            if (root != null)
            {
                root = DeleteMax(root);
            }
        }

        public void DeleteNode(TKey key)
        {
            root = DeleteNode(root, key);
        }

        #region 辅助函数

        /// <summary>
        /// 向以 node 为根节点的红黑树中，插入节点（key,value）
        /// </summary>
        /// <param name="node">根节点</param>
        /// <param name="key">要更新的节点的 key</param>
        /// <param name="value">要更新的节点的 value</param>
        /// <returns></returns>
        private Node Add(Node node, TKey key, TValue value)
        {
            if (node == null)
            {
                count++;
                // 递归到叶子结点，把当前要插入的节点作为根节点返回
                // 一层层递归回去更新成相应的左子树或右子树
                return new Node(key, value);
            }

            if (key.CompareTo(node.key) < 0)
            {
                // 小于根节点的键，往左边子树插入
                node.left = Add(node.left, key, value);
            }
            else if (key.CompareTo(node.key) > 0)
            {
                // 大于根节点的键，往右边子树插入
                node.right = Add(node.right, key, value);
            }
            else
            {
                // 如果 key 相等就直接更新（注意红黑树的键是唯一的）
                node.value = value;
            }

            // 红黑树性质的维护
            if (IsRed(node.right) && !IsRed(node.left))
            {
                node = LeftRotate(node);
            }

            // 左旋转完成后，递归回去到父节点，再判断自己的 left left 是不是红色，要清楚旋转的根节点是哪个
            if (IsRed(node.left) && IsRed(node.left.left))
            {
                node = RightRotate(node);
            }

            if (IsRed(node.left) && IsRed(node.right))
            {
                FlipColors(node);
            }

            return node;
        }

        /// <summary>
        /// 返回以 node 为根节点的红黑树中，key 所在的节点
        /// </summary>
        private Node GetNode(Node node, TKey key)
        {
            if (node == null)
                return null;

            if (key.Equals(node.key))
                return node;
            if (key.CompareTo(node.key) < 0)
                return GetNode(node.left, key);
            return GetNode(node.right, key);
        }

        #endregion

        #region 前中后序遍历

        /// <summary>
        /// 前序遍历
        /// </summary>
        public void PreOrder()
        {
            PreOrder(root);
        }

        /// <summary>
        /// 中序遍历
        /// </summary>
        public void InOrder()
        {
            InOrder(root);
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        public void PostOrder()
        {
            PostOrder(root);
        }

        /// <summary>
        /// 层序遍历（Level Order）,又称广度优先遍历
        /// </summary>
        public void LevelOrder()
        {
            Queue<Node> queue = new Queue<Node>();
            // 先推入根节点
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                Node node = queue.Dequeue();
                Console.WriteLine(node.key);
                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
        }

        /// <summary>
        /// 对以 node 为根的红黑树进行前序遍历
        /// </summary>
        /// <param name="node">根节点</param>
        private void PreOrder(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.key);
                PreOrder(node.left);
                PreOrder(node.right);
            }
        }


        /// <summary>
        /// 对以 node 为根的红黑树进行中序遍历
        /// </summary>
        /// <param name="node">根节点</param>
        private void InOrder(Node node)
        {
            if (node != null)
            {
                InOrder(node.left);
                Console.WriteLine(node.key);
                InOrder(node.right);
            }
        }

        /// <summary>
        /// 对以 node 为根的红黑树进行后序遍历
        /// </summary>
        /// <param name="node">根节点</param>
        private void PostOrder(Node node)
        {
            if (node != null)
            {
                PostOrder(node.left);
                PostOrder(node.right);
                Console.WriteLine(node.key);
            }
        }

        /// <summary>
        /// 在以 node 为根的红黑树中，返回键值最小的点
        /// </summary>
        /// <param name="node">根节点</param>
        /// <returns>键值最小的节点</returns>
        private Node Min(Node node)
        {
            // 一直找左子树，没有左子节点的时候就到了最小点了
            if (node.left == null)
            {
                return node;
            }

            return Min(node.left);
        }

        /// <summary>
        /// 在以 node 为根的红黑树中，返回键值最大的点
        /// </summary>
        /// <param name="node">根节点</param>
        /// <returns>键值最大的节点</returns>
        private Node Max(Node node)
        {
            // 一直找右子树，没有左子节点的时候就到了最小点了
            if (node.right == null)
            {
                return node;
            }

            return Max(node.right);
        }

        /// <summary>
        /// 删除以 node 为根节点的子树中的最小的节点
        /// </summary>
        /// <param name="node">根节点</param>
        /// <returns>最小的节点</returns>
        private Node DeleteMin(Node node)
        {
            if (node.left == null)
            {
                // 没有左节点了，就要看看右节点是否存在，有则删除
                Node rightNode = node.right;
                node.right = null;
                count--;
                // 没有左节点的话，就移除它的父节点（也就是最小的节点）
                // 这样无论右节点为不为空 null，都可以返回作为上一个节点的左节点
                // 来替代原来被删除的父节点的位置
                return rightNode;
            }

            // 好好体验递归
            node.left = DeleteMin(node.left);
            return node;
        }

        /// <summary>
        /// 删除以 node 为根节点的子树中的最大的节点
        /// </summary>
        /// <param name="node">根节点</param>
        /// <returns>最小的节点</returns>
        private Node DeleteMax(Node node)
        {
            if (node.right == null)
            {
                // 没有右节点了，就要看看左节点是否存在，有则删除
                Node leftNode = node.left;
                node.left = null;
                count--;
                // 没有右节点的话，就移除它的父节点（也就是最大的节点）
                // 这样无论左节点为不为空 null，都可以返回作为上一个节点的右节点
                // 来替代原来被删除的父节点的位置
                return leftNode;
            }

            node.right = DeleteMax(node.right);
            return node;
        }

        /// <summary>
        /// 删除指定节点作为树的根的红黑树中键值为 Key 的节点
        /// 删除最小节点后的替换值实际从删除节点的右子树中找最小节点即可
        /// 返回删除节点后的新的红黑树的根。
        /// </summary>
        private Node DeleteNode(Node node, TKey key)
        {
            if (node == null)
            {
                return null;
            }

            if (key.CompareTo(node.key) < 0)
            {
                node.left = DeleteNode(node.left, key);
                return node;
            }

            if (key.CompareTo(node.key) > 0)
            {
                node.right = DeleteNode(node.right, key);
                return node;
            }

            // 找到要删除的节点
            if (node.left == null)
            {
                // 左子树为空，看右子树，删除节点后，把右子树接到节点原来的位置去
                Node rightNode = node.right;
                count--;
                return rightNode;
            }

            if (node.right == null)
            {
                // 右子树为空，看左子树，删除节点后，把左子树接到节点原来的位置去
                Node leftNode = node.left;
                count--;
                return leftNode;
            }

            // 左右子树都不为空，使用 Hibbard Deletion
            // 找到键值比节点键值大，且最相近的节点，也就是右子树的最小节点。(同理，或者左子树的最大节点)
            // 并把这个节点上提至节点本身的位置，更新左右子树
            Node successor = new Node(Min(node.right));
            successor.left = node.left;
            successor.right = DeleteMin(node.right);
            count++; // DeleteMin 中已经 count-- 了，所以要加回来

            node.left = node.right = null;
            count--; // 删除了键值相同的节点
            return successor;
        }

        #endregion

        #region 旋转相关的方法

        //   node                     x
        //  /   \     左旋转         /  \
        // T1   x   --------->   node   T3
        //     / \              /   \
        //    T2 T3            T1   T2
        private Node LeftRotate(Node node)
        {
            Node x = node.right;

            node.right = x.left;
            x.left = node;

            x.color = node.color;
            node.color = RED;
            return x;
        }

        //     node                   x
        //    /   \     右旋转       /  \
        //   x    T2   ------->   y   node
        //  / \                       /  \
        // y  T1                     T1  T2
        private Node RightRotate(Node node)
        {
            Node x = node.left;

            node.left = x.right;
            x.right = node;

            x.color = node.color;
            node.color = RED;
            return x;
        }

        /// <summary>
        /// 颜色翻转
        /// </summary>
        private void FlipColors(Node node)
        {
            node.color = RED;
            node.left.color = BLACK;
            node.right.color = BLACK;
        }

        /// <summary>
        /// 判断节点 node 的颜色
        /// </summary>
        private bool IsRed(Node node)
        {
            if (node == null)
                return BLACK;
            return node.color;
        }

        #endregion
    }
}