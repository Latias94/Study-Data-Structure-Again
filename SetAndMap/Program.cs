using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SetAndMap
{
    class Program
    {
        private static double TestSet(ISet<string> set, string filename)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<string> list = new List<string>();
            if (FileOperation.ReadFromFile(filename, list))
            {
                foreach (string word in list)
                {
                    set.Add(word);
                }

                Console.WriteLine($"Total different words of {Path.GetFileName(filename)}: {set.GetSize()}");
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            return ts.TotalMilliseconds;
        }

        private static double TestMap(IMap<string, int> map, string filename)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<string> list = new List<string>();
            if (FileOperation.ReadFromFile(filename, list))
            {
                foreach (string word in list)
                {
                    if (map.Contains(word))
                    {
                        map.Set(word, map.Get(word) + 1);
                    }
                    else
                    {
                        map.Set(word, 1);
                    }
                }

                Console.WriteLine($"Total different words of {Path.GetFileName(filename)}: {map.GetSize()}");
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            return ts.TotalMilliseconds;
        }

        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            string filename = Path.Combine(projectDirectory, "SetAndMap/pride-and-prejudice.txt");

            Console.WriteLine("集合测试：");
            BSTSet<string> bstSet = new BSTSet<string>();
            double milliTime1 = TestSet(bstSet, filename);
            Console.WriteLine($"BST Set: {milliTime1} ms");

            LinkedListSet<string> linkedListSet = new LinkedListSet<string>();
            double milliTime2 = TestSet(linkedListSet, filename);
            Console.WriteLine($"Linked List Set: {milliTime2} ms");

            Console.WriteLine();
            Console.WriteLine("映射测试");
            BSTMap<string, int> bstMap = new BSTMap<string, int>();
            double milliTime3 = TestMap(bstMap, filename);
            Console.WriteLine($"BST Map: {milliTime3} ms");

            LinkedListMap<string, int> linkedListMap = new LinkedListMap<string, int>();
            double milliTime4 = TestMap(linkedListMap, filename);
            Console.WriteLine($"Linked List Map: {milliTime4} ms");
        }
    }
}