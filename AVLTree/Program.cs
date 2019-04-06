using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SetAndMap;
using BST;

namespace AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            string filename = Path.Combine(projectDirectory, "SetAndMap/pride-and-prejudice.txt");
            List<string> words = new List<string>();
            if (FileOperation.ReadFromFile(filename, words))
            {
                // 对比 BST 和 AVL 的性能
//                BST: 0.7879242 s
//                AVL Tree: 0.7134914 s

                // 对列表进行排序，让 BST 退化成链表
//                BST: 229.354414 s
//                AVL Tree: 0.8231674 s
//                words.Sort();

                BST<string, int> bst = new BST<string, int>();
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                foreach (string word in words)
                {
                    if (bst.Contain(word))
                    {
                        bst.Set(word, bst.Get(word) + 1);
                    }
                    else
                    {
                        bst.Add(word, 1);
                    }
                }

                foreach (string word in words)
                {
                    bst.Contain(word);
                }

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"BST: {ts.TotalSeconds} s");

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

                foreach (string word in words)
                {
                    avlTree.DeleteNode(word);
                    if (!avlTree.IsBalanced() || !avlTree.IsBST())
                        throw new Exception("Error");
                }

                Console.WriteLine("Congratulation. No error occur");
            }
        }
    }
}