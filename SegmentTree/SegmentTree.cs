using System;

namespace SegmentTree
{
    /// <summary>
    /// 线段树对于更新和查找都是 O(logn)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SegmentTree<T>
    {
        private T[] tree;
        private T[] data;
        /// <summary>
        /// 根据业务特点决定每个节点的值合并方式是 sum、max、min、avg 等
        /// 即综合两段子树来得到父节点的数据
        /// </summary>
        private Func<T, T, T> merger;

        public SegmentTree(T[] arr, Func<T, T, T> merger)
        {
            data = new T[arr.Length];
            this.merger = merger;
            // 数组复制
            Array.Copy(arr, data, arr.Length);
            // 因为线段树是平衡二叉树而不是完全二叉树，叶子节点可能在倒数第二层，也可能在倒数第一层
            // 叶子节点的个数就是 arr 的长度，所以倒数第二层之前可以看做需要 arr.Length - 1 的空间
            // 倒数第二层的叶子节点需要 arr.Length 的空间，倒数第一层则需要大约 2 * arr.Length 的空间
            // 线段树不考虑添加元素的话，使用四倍 arr.Length 即可
            tree = new T[4 * arr.Length];
            BuildSegmentTree(0, 0, data.Length - 1);
        }

        /// <summary>
        /// 创建线段树：在treeIndex的位置创建区间 [l...r] 的线段树
        /// </summary>
        /// <param name="treeIndex">要创建的线段树的根节点</param>
        /// <param name="l">区间左边界</param>
        /// <param name="r">区间右边界</param>
        private void BuildSegmentTree(int treeIndex, int l, int r)
        {
            if (l > r)
                throw new ArgumentException("Left edge index should smaller than right edge index");
            // 当只有一个元素的时候
            if (l == r)
            {
                tree[treeIndex] = data[l];
                return;
            }

            // 获取左孩子的索引
            int leftTreeIndex = LeftChild(treeIndex);
            int rightTreeIndex = RightChild(treeIndex);
            // 确定左右区间的分界点，[leftTreeIndex, mid] [mid+1, rightTreeIndex]
            int mid = l + (r - l) / 2;
            BuildSegmentTree(leftTreeIndex, l, mid);
            BuildSegmentTree(rightTreeIndex, mid + 1, r);

            // 这里根据业务特点决定每个节点的值是 sum、max、min、avg 等.综合两段子树来得到父节点的数据
            tree[treeIndex] = merger(tree[leftTreeIndex], tree[rightTreeIndex]);
        }

        /// <summary>
        /// 查询指定区间 [queryL,queryR] 的 merge 结果
        /// </summary>
        /// <param name="queryL">区间左边界</param>
        /// <param name="queryR">区间右边界</param>
        /// <returns>返回得到的 merge 结果</returns>
        public T Query(int queryL, int queryR)
        {
            if (queryL > queryR)
                throw new ArgumentException("Left edge index should smaller than right edge index");
            if (queryL < 0 || queryL >= data.Length || queryR < 0 || queryR >= data.Length)
            {
                throw new ArgumentException("Index is illegal");
            }

            return Query(0, 0, data.Length - 1, queryL, queryR);
        }

        /// <summary>
        /// 递归查找
        /// </summary>
        /// <param name="treeIndex">根节点索引</param>
        /// <param name="l">开始查找的根节点</param>
        /// <param name="r">区间右边界</param>
        /// <param name="queryL">要查询的左边界</param>
        /// <param name="queryR">要查询的右边界</param>
        /// <returns>查询并 Merge 后的结果</returns>
        private T Query(int treeIndex, int l, int r, int queryL, int queryR)
        {
            // 当区间完全重合的时候，直接返回根节点的值就行
            if (l == queryL && r == queryR)
            {
                return tree[treeIndex];
            }

            // 获取左孩子的索引
            int leftTreeIndex = LeftChild(treeIndex);
            int rightTreeIndex = RightChild(treeIndex);
            int mid = l + (r - l) / 2;
            if (queryL >= mid + 1)
            {
                // 只在右侧找
                return Query(rightTreeIndex, mid + 1, r, queryL, queryR);
            }

            if (queryR <= mid)
            {
                // 只在左侧找
                return Query(leftTreeIndex, l, mid, queryL, queryR);
            }

            // 要查询的区间在左右两侧各有一部分，要分开查，然后合并
            T leftResult = Query(leftTreeIndex, l, mid, queryL, mid);
            T rightResult = Query(rightTreeIndex, mid + 1, r, mid + 1, queryR);
            return merger(leftResult, rightResult);
        }

        /// <summary>
        /// 更新指定位置的元素
        /// </summary>
        /// <param name="index">所在索引</param>
        /// <param name="element">更新后的值</param>
        public void Update(int index, T element)
        {
            if (index < 0 || index >= data.Length)
            {
                throw new ArgumentException("Index is illegal");
            }

            data[index] = element;
            // 下面是更新线段树
            Update(0, 0, data.Length - 1, index, element);
        }

        /// <summary>
        /// 递归更新值
        /// </summary>
        private void Update(int treeIndex, int l, int r, int index, T element)
        {
            if (l == r)
            {
                tree[treeIndex] = element;
                return;
            }

            int leftTreeIndex = LeftChild(treeIndex);
            int rightTreeIndex = RightChild(treeIndex);
            int mid = l + (r - l) / 2;
            if (index >= mid + 1)
            {
                // 右子树中查找
                Update(rightTreeIndex, mid + 1, r, index, element);
            }
            else
            {
                // 左子树中查找
                Update(leftTreeIndex, l, mid, index, element);
            }

            tree[treeIndex] = merger(tree[leftTreeIndex], tree[rightTreeIndex]);
        }


        public T Get(int index)
        {
            if (index < 0 || index >= data.Length)
            {
                throw new ArgumentException("Index is illegal");
            }

            return data[index];
        }


        public int GetSize()
        {
            return data.Length;
        }

        /// <summary>
        /// 返回二叉树的数组表示中的一个索引所表示的元素的左孩子节点的索引
        /// </summary>
        /// <param name="index">要查询的节点索引</param>
        /// <returns>左孩子节点索引</returns>
        public int LeftChild(int index)
        {
            return 2 * index + 1;
        }

        /// <summary>
        /// 返回二叉树的数组表示中的一个索引所表示的元素的右孩子节点的索引
        /// </summary>
        /// <param name="index">要查询的节点索引</param>
        /// <returns>右孩子节点索引</returns>
        public int RightChild(int index)
        {
            return 2 * index + 2;
        }

        public override string ToString()
        {
            return $"SegmentTree{{tree=[{string.Join(", ", tree)}]}}";
        }
    }
}