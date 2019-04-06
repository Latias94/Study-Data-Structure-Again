using System;
using System.Collections.Generic;

namespace BST
{
    /// <summary>
    /// 二分查找树（不平衡），平衡的二分查找树查看 AVLTree
    /// </summary>
    /// <typeparam name="TKey">Key 键值唯一、可比较</typeparam>
    /// <typeparam name="TValue">Value 无所谓</typeparam>
    public class BST<TKey, TValue> where TKey : IComparable
    {
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

            public Node(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                left = null;
                right = null;
            }

            public Node(Node node)
            {
                key = node.key;
                value = node.value;
                left = node.left;
                right = node.right;
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

        public BST()
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
        /// 向以 node 为根节点的二叉树搜索树中，插入节点（key,value）
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
                // 如果 key 相等就直接更新（注意二叉搜索树的键是唯一的）
                node.value = value;
            }

            return node;
        }

        /// <summary>
        /// 返回以 node 为根节点的二分搜索树中，key 所在的节点
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
        /// 对以 node 为根的二叉搜索树进行前序遍历
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
        /// 对以 node 为根的二叉搜索树进行中序遍历
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
        /// 对以 node 为根的二叉搜索树进行后序遍历
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
        /// 在以 node 为根的二叉搜索树中，返回键值最小的点
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
        /// 在以 node 为根的二叉搜索树中，返回键值最大的点
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
        /// 删除指定节点作为树的根的二叉搜索树中键值为 Key 的节点
        /// 删除最小节点后的替换值实际从删除节点的右子树中找最小节点即可
        /// 返回删除节点后的新的二分搜索树的根。
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
    }
}