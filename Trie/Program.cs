using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SetAndMap;

namespace Trie
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
                Console.WriteLine($"Total words: {words.Count}");
            }

            // 对比 BST Set 和 Trie
            BSTSet<string> bstSet = new BSTSet<string>();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (string word in words)
            {
                bstSet.Add(word);
            }

            // 查询单词
            foreach (string word in words)
            {
                bstSet.Contain(word);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Total different words of {Path.GetFileName(filename)}: {bstSet.GetSize()}");
            Console.WriteLine($"BST Set: {ts.TotalMilliseconds} ms");

            Trie trie = new Trie();
            stopWatch.Restart();
            foreach (string word in words)
            {
                trie.Insert(word);
            }

            foreach (string word in words)
            {
                trie.Contain(word);
            }

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine($"Total different words of {Path.GetFileName(filename)}: {bstSet.GetSize()}");
            Console.WriteLine($"Trie: {ts.TotalMilliseconds} ms");
        }
    }
}