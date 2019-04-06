using System;
using System.Diagnostics;

namespace UnionFind
{
    class Program
    {
        /// <summary>
        /// 测试不同实现的 UnionFind
        /// </summary>
        /// <param name="m">多少个数字</param>
        /// <returns></returns>
        private static TimeSpan TestUnionFind(IUnionFind uf, int m)
        {
            int size = uf.GetSize();
            Random random = new Random();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < m; i++)
            {
                int a = random.Next(size);
                int b = random.Next(size);
                uf.UnionElements(a, b);
            }

            for (int i = 0; i < m; i++)
            {
                int a = random.Next(size);
                int b = random.Next(size);
                uf.IsConnected(a, b);
            }

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        static void Main(string[] args)
        {
            // UnionFind1 慢于 UnionFind2
//            UnionFind1: 3290.1725 ms
//            UnionFind2: 1.6884 ms
//            int size = 100000;
//            int m = 10000;

            // UnionFind3 最快
//            UnionFind1: 35.1748929 s
//            UnionFind2: 16.4397501 s
//            UnionFind3: 0.02752 s
//            int size = 100000;
//            int m = 100000;

//            UnionFind3: 7.0488626 s
//            UnionFind4: 7.7999108 s
//            UnionFind5: 5.5249532 s
//            UnionFind6: 7.1775082 s
            int size = 10000000;
            int m = 10000000;
            TimeSpan ts;

//            UnionFind1 uf1 = new UnionFind1(size);
//            ts = TestUnionFind(uf1, m);
//            Console.WriteLine($"UnionFind1: {ts.TotalSeconds} s");
//
//            UnionFind2 uf2 = new UnionFind2(size);
//            ts = TestUnionFind(uf2, m);
//            Console.WriteLine($"UnionFind2: {ts.TotalSeconds} s");

            UnionFind3 uf3 = new UnionFind3(size);
            ts = TestUnionFind(uf3, m);
            Console.WriteLine($"UnionFind3: {ts.TotalSeconds} s");

            UnionFind4 uf4 = new UnionFind4(size);
            ts = TestUnionFind(uf4, m);
            Console.WriteLine($"UnionFind4: {ts.TotalSeconds} s");

            // 第五版的路径压缩是在循环遍历的时候实现的 所以整体性能更高
            UnionFind5 uf5 = new UnionFind5(size);
            ts = TestUnionFind(uf5, m);
            Console.WriteLine($"UnionFind5: {ts.TotalSeconds} s");
            
            // 压缩的时间会更高 但是查询会更快 近乎 O(1)
            UnionFind6 uf6 = new UnionFind6(size);
            ts = TestUnionFind(uf6, m);
            Console.WriteLine($"UnionFind6: {ts.TotalSeconds} s");
        }
    }
}