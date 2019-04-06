using System;
using System.Collections.Generic;

namespace AVLTree
{
    /// <summary>
    /// 二分查找树
    /// </summary>
    /// <typeparam name="TKey">Key 键值唯一、可比较</typeparam>
    /// <typeparam name="TValue">Value 无所谓</typeparam>
    public class AVLTree<TKey, TValue> where TKey : IComparable
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

            /// <summary>
            /// 节点高度
            /// </summary>
            public int height;

            public Node(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                left = null;
                right = null;
                height = 1;
            }

            public Node(Node node)
            {
                key = node.key;
                value = node.value;
                left = node.left;
                right = node.right;
                height = 1;
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

        public AVLTree()
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

        public void Set(TKey key, TValue newValue)
        {
            Node node = GetNode(root, key);
            if (node == null)
                throw new ArgumentException(key + " doesn't exist!");

            node.value = newValue;
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

        /// <summary>
        /// 判断该二叉树是否是一棵二分搜索树
        /// </summary>
        public bool IsBST()
        {
            List<TKey> keys = new List<TKey>();
            // 二分搜索树的中序遍历可以用来升序排序，用这个性质来检查是否是一个二分搜索树
            InOrder(root, keys);
            for (int i = 1; i < keys.Count; i++)
            {
                if (keys[i - 1].CompareTo(keys[i]) > 0)
                {
                    // 升序排列中出现一个地方前面的元素大于后面的元素就可以否定整个二叉搜索树了
                    return false;
                }
            }

            return true;
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

        public void DeleteNode(TKey key)
        {
            root = DeleteNode(root, key);
        }

        /// <summary>
        /// 判断二叉树是否是一棵平衡二叉树
        /// </summary>
        public bool IsBalanced()
        {
            return IsBalanced(root);
        }

        #region 辅助函数

        /// <summary>
        /// 判断二叉树是否是一棵平衡二叉树
        /// </summary>
        /// <param name="node">递归开始的子树根节点</param>
        private bool IsBalanced(Node node)
        {
            if (node == null)
            {
                // 递归到底仍未发现不平衡的左右子树，说明整个二叉树是平衡地
                return true;
            }

            int balancedFactor = GetBalanceFactor(node);
            if (Math.Abs(balancedFactor) > 1)
            {
                return false;
            }

            // 递归查看左右子树
            return IsBalanced(node.left) && IsBalanced(node.right);
        }

        /// <summary>
        /// 向以 node 为根节点的二叉树搜索树中，插入节点（key,value）
        /// 同时维护高度值
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

            // 添加节点后 需要更新高度值
            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
            int balanceFactor = GetBalanceFactor(node);

            // 插入的元素在不平衡的节点的左侧的左侧 LL
            if (balanceFactor > 1 && GetBalanceFactor(node.left) >= 0)
            {
                return RightRotate(node);
            }

            // 插入的元素在不平衡的节点的右侧的右侧 RR
            if (balanceFactor < -1 && GetBalanceFactor(node.right) <= 0)
            {
                return LeftRotate(node);
            }

            // 插入的元素在不平衡的节点的左侧的右侧 LR
            if (balanceFactor > 1 && GetBalanceFactor(node.left) < 0)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }

            // 插入的元素在不平衡的节点的右侧的左侧 RL
            if (balanceFactor < -1 && GetBalanceFactor(node.right) > 0)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
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

        private int GetHeight(Node node)
        {
            return node == null ? 0 : node.height;
        }

        private int GetBalanceFactor(Node node)
        {
            return node == null ? 0 : GetHeight(node.left) - GetHeight(node.right);
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
        /// 对以 node 为根的二叉搜索树进行中序遍历,结果存放到 list 中
        /// </summary>
        private void InOrder(Node node, List<TKey> keys)
        {
            if (node != null)
            {
                InOrder(node.left, keys);
                keys.Add(node.key);
                InOrder(node.right, keys);
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

            Node returnNode;
            if (key.CompareTo(node.key) < 0)
            {
                node.left = DeleteNode(node.left, key);
                returnNode = node;
            }
            else if (key.CompareTo(node.key) > 0)
            {
                node.right = DeleteNode(node.right, key);
                returnNode = node;
            }
            else
            {
                // 键值相等的时候，找到要删除的节点
                if (node.left == null)
                {
                    // 左子树为空，看右子树，删除节点后，把右子树接到节点原来的位置去
                    Node rightNode = node.right;
                    count--;
                    returnNode = rightNode;
                }
                else if (node.right == null)
                {
                    // 右子树为空，看左子树，删除节点后，把左子树接到节点原来的位置去
                    Node leftNode = node.left;
                    count--;
                    returnNode = leftNode;
                }
                else
                {
                    // 左右子树都不为空，使用 Hibbard Deletion
                    // 找到键值比节点键值大，且最相近的节点，也就是右子树的最小节点。(同理，或者左子树的最大节点)
                    // 并把这个节点上提至节点本身的位置，更新左右子树
                    Node successor = new Node(Min(node.right));
                    successor.left = node.left;
                    // 替换 DeleteMin，递归调用 DeleteNode 保证平衡性
                    successor.right = DeleteNode(node.right, successor.key);
                    count++; // DeleteMin 中已经 count-- 了，所以要加回来

                    node.left = node.right = null;
                    count--; // 删除了键值相同的节点
                    returnNode = successor;
                }
            }

            if (returnNode == null)
                return null;

            // 进行平衡
            returnNode.height = 1 + Math.Max(GetHeight(returnNode.left), GetHeight(returnNode.right));
            int balanceFactor = GetBalanceFactor(returnNode);

            // 插入的元素在不平衡的节点的左侧的左侧 LL
            if (balanceFactor > 1 && GetBalanceFactor(returnNode.left) >= 0)
            {
                return RightRotate(returnNode);
            }

            // 插入的元素在不平衡的节点的右侧的右侧 RR
            if (balanceFactor < -1 && GetBalanceFactor(returnNode.right) <= 0)
            {
                return LeftRotate(returnNode);
            }

            // 插入的元素在不平衡的节点的左侧的右侧 LR
            if (balanceFactor > 1 && GetBalanceFactor(returnNode.left) < 0)
            {
                returnNode.left = LeftRotate(returnNode.left);
                return RightRotate(returnNode);
            }

            // 插入的元素在不平衡的节点的右侧的左侧 RL
            if (balanceFactor < -1 && GetBalanceFactor(returnNode.right) > 0)
            {
                returnNode.right = RightRotate(returnNode.right);
                return LeftRotate(returnNode);
            }

            return returnNode;
        }

        #endregion

        #region 实现平衡的旋转方式

        // 对节点y进行向右旋转操作，返回旋转后新的根节点x
        //        y                              x
        //       / \                           /   \
        //      x   T4     向右旋转 (y)        z     y
        //     / \       - - - - - - - ->    / \   / \
        //    z   T3                       T1  T2 T3 T4
        //   / \
        // T1   T2
        private Node RightRotate(Node y)
        {
            Node x = y.left;
            y.left = x.right;
            x.right = y;

            y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));
            x.height = 1 + Math.Max(GetHeight(x.left), GetHeight(x.right));
            return x;
        }

        // 对节点y进行向左旋转操作，返回旋转后新的根节点x
        //    y                             x
        //  /  \                          /   \
        // T1   x      向左旋转 (y)       y     z
        //     / \   - - - - - - - ->   / \   / \
        //   T2  z                     T1 T2 T3 T4
        //      / \
        //     T3 T4
        private Node LeftRotate(Node y)
        {
            Node x = y.right;
            y.right = x.left;
            x.left = y;

            y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));
            x.height = 1 + Math.Max(GetHeight(x.left), GetHeight(x.right));
            return x;
        }

        #endregion
    }
}