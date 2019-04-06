using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AVLTree;
using SetAndMap;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // 只对比红黑树和 AVLTree 的添加元素和查询元素效率
//                Red Black Tree: 0.7943627 s
//                AVL Tree: 0.7095531 s
            // 性能不太占优的三个原因：
            // 1. 测试用例较小，更简单的算法，操作更少，可能更快
            // 2. 这里大多是查询操作 get & contain，因为红黑树本身不是一个严格的平衡树，高度最多可以达到 2*logn
            //     如果结合添加删除查询三个操作，整体红黑树会占优
            // 3. 这里实现的红黑树还有优化的余地，
            
//            Test1();

            // 只对比红黑树和 AVLTree 的添加随机元素的效率
//            Red Black Tree: 177.1346644 s
//            AVL Tree: 190.2319935 s
        
//            Test2();

            // 只对比红黑树和 AVLTree 的添加顺序的元素的效率
//            Red Black Tree: 34.0354543 s
//            AVL Tree: 46.6426262 s

            Test3();
        }

        private static void Test1()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            string filename = Path.Combine(projectDirectory, "SetAndMap/pride-and-prejudice.txt");
            List<string> words = new List<string>();
            if (FileOperation.ReadFromFile(filename, words))
            {
                RBTree<string, int> rbTree = new RBTree<string, int>();
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                foreach (string word in words)
                {
                    if (rbTree.Contain(word))
                    {
                        rbTree.Set(word, rbTree.Get(word) + 1);
                    }
                    else
                    {
                        rbTree.Add(word, 1);
                    }
                }

                foreach (string word in words)
                {
                    rbTree.Contain(word);
                }

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"Red Black Tree: {ts.TotalSeconds} s");

                AVLTree<string, int> avlTree = new AVLTree<string, int>();
                stopWatch.Restart();
                foreach (string word in words)
                {
                    if (avlTree.Contain(word))
                    {
                        avlTree.Set(word, avlTree.Get(word) + 1);
                    }
                    else
                    {
                        avlTree.Add(word, 1);
                    }
                }

                foreach (string word in words)
                {
                    avlTree.Contain(word);
                }

                stopWatch.Stop();
                ts = stopWatch.Elapsed;
                Console.WriteLine($"AVL Tree: {ts.TotalSeconds} s");
            }
        }

        private static void Test2()
        {
            int n = 20000000;
            List<int> list = new List<int>(n);
            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                list.Add(random.Next(int.MaxValue));
            }
            RBTree<int, int> rbTree = new RBTree<int, int>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (int num in list)
            {
                rbTree.Add(num, 0);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Red Black Tree: {ts.TotalSeconds} s");

            AVLTree<int, int> avlTree = new AVLTree<int, int>();
            stopWatch.Restart();
            
            foreach (int num in list)
            {
                avlTree.Add(num, 0);
            }

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine($"AVL Tree: {ts.TotalSeconds} s");
        }

        private static void Test3()
        {
            int n = 20000000;
            List<int> list = new List<int>(n);

            for (int i = 0; i < n; i++)
            {
                // 给定排好序的数字
                list.Add(i);
            }

            RBTree<int, int> rbTree = new RBTree<int, int>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (int num in list)
            {
                rbTree.Add(num, 0);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Red Black Tree: {ts.TotalSeconds} s");

            AVLTree<int, int> avlTree = new AVLTree<int, int>();
            stopWatch.Restart();

            foreach (int num in list)
            {
                avlTree.Add(num, 0);
            }

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine($"AVL Tree: {ts.TotalSeconds} s");
        }
    }
}