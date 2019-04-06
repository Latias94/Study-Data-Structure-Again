using System;

namespace BST
{
    class Program
    {
        static void Main(string[] args)
        {
            BST<int, int> bst = new BST<int, int>();

            // 取n个取值范围在 [0...m) 的随机整数放进二分搜索树中
            int n = 10;
            int m = 100;
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int key = random.Next(m);
                // 为了后续测试方便,这里 value 值取和 key 值一样
                bst.Add(key, key);
                Console.Write(key + " ");
            }

            Console.WriteLine();

            // 测试二分搜索树的 size()
            Console.WriteLine("size: " + bst.Size());
            Console.WriteLine();

            // 测试二分搜索树的前序遍历 preOrder
            Console.WriteLine("preOrder: ");
            bst.PreOrder();
            Console.WriteLine();

            // 测试二分搜索树的中序遍历 inOrder 中序排序可用于排序(升序)
            Console.WriteLine("inOrder: ");
            bst.InOrder();
            Console.WriteLine();

            // 测试二分搜索树的后序遍历 postOrder  后序遍历可用于释放二叉搜索树
            Console.WriteLine("postOrder: ");
            bst.PostOrder();
            Console.WriteLine();

            // 层序遍历
            Console.WriteLine("levelOrder: ");
            bst.LevelOrder();
            Console.WriteLine();

            // 测试 removeMin
            // 输出的元素应该是从小到大排列的
            Console.WriteLine("Test removeMin: ");
            while (!bst.IsEmpty())
            {
                Console.Write("min: " + bst.Min() + " , ");
                bst.DeleteMin();
                Console.WriteLine("After removeMin, size = " + bst.Size());
            }

            Console.WriteLine();


            for (int i = 0; i < n; i++)
            {
                int key = random.Next(m);
                // 为了后续测试方便,这里value值取和key值一样
                bst.Add(key, key);
            }
            // 注意, 由于随机生成的数据有重复, 所以bst中的数据数量大概率是小于n的

            // 测试 removeMax
            // 输出的元素应该是从大到小排列的
            Console.WriteLine("Test removeMax: ");
            while (!bst.IsEmpty())
            {
                Console.Write("max: " + bst.Max() + " , ");
                bst.DeleteMax();
                Console.WriteLine("After removeMax, size = " + bst.Size());
            }

            // order数组中存放[0...n)的所有元素
            int[] order = new int[n];
            for (int i = 0; i < n; i++)
            {
                order[i] = i;
            }

            for (int i = 0; i < n; i++)
            {
                int key = random.Next(n);
                // 为了后续测试方便,这里 value 值取和 key 值一样.
                // 注意这里因为 key 是随机产生的，所以可能会出现 key 相同覆盖的情况,
                // 因此所有的键合并起来一定是 order 数组的子集，所以下面的删除最后一定是 0
                bst.Add(key, key);
            }

            // 打乱 order 数组的顺序
            Shuffle(order);
            Console.WriteLine("Test delete: ");
            Console.WriteLine(bst.Size());
            // 乱序删除 [0...n) 范围里的所有元素
            for (int i = 0; i < n; i++)
            {
                if (bst.Contain(order[i]))
                {
                    bst.DeleteNode(order[i]);
                    Console.WriteLine("After remove " + order[i] + " size = " + bst.Size());
                }
            }

            // 最终整个二分搜索树应该为空
            Console.WriteLine(bst.Size());
        }

        /// <summary>
        /// 打乱数组顺序
        /// </summary>
        public static void Shuffle(int[] arr)
        {
            Random random = new Random();
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int pos = random.Next(i + 1);
                int t = arr[pos];
                arr[pos] = arr[i];
                arr[i] = t;
            }
        }
    }
}