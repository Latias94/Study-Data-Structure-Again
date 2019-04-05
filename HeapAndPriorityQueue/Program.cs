﻿using System;
using System.Diagnostics;

namespace HeapAndPriorityQueue
{
    class Program
    {
        /// <summary>
        /// 提前对数组进行 Heapify 的操作的性能对比
        /// Heapify: 将任意数组整理成堆的形状
        ///     操作：从不是叶子节点的最后一个节点开始，一个一个进行 sift down
        ///          这样能少操作近乎占了一半的叶子结点
        /// public MaxHeap(T[] arr) 这个构造函数使用了 Heapify，时间复杂度为 O(n)
        /// 一个一个 Insert 插入元素到空堆 时间复杂度为 O(nlogn)
        /// </summary>
        private static double TestHeap(int[] testData, bool isHeapify)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            MaxHeap<int> maxHeap;
            if (isHeapify)
            {
                maxHeap = new MaxHeap<int>(testData);
            }
            else
            {
                maxHeap = new MaxHeap<int>(testData.Length);
                foreach (int num in testData)
                {
                    maxHeap.Insert(num);
                }
            }

            int[] arr = new int[testData.Length];
            for (int i = 0; i < testData.Length; i++)
            {
                arr[i] = maxHeap.ExtractMax();
            }

            for (int i = 1; i < testData.Length; i++)
            {
                if (arr[i - 1] < arr[i])
                {
                    throw new ArgumentException("Error");
                }
            }

            Console.WriteLine("Test MaxHeap completed.");

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            return ts.TotalMilliseconds;
        }

        static void Main(string[] args)
        {
            int n = 1000000;

            Random random = new Random();
            int[] testData = new int[n];
            for (int i = 0; i < n; i++)
            {
                testData[i] = random.Next(int.MaxValue);
            }

            double time1 = TestHeap(testData, false);
            Console.WriteLine($"Without heapify: {time1} ms");

            double time2 = TestHeap(testData, true);
            Console.WriteLine($"With heapify: {time2} ms");
        }
    }
}