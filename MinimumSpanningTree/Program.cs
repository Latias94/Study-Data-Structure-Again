using System;
using System.Collections.Generic;
using System.IO;

namespace MinimumSpanningTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            // 使用两种图的存储方式读取graph1.txt文件
            string filename = Path.Combine(projectDirectory, "MinimumSpanningTree/graph1.txt");
            SparseWeightedGraph<double> g1 = new SparseWeightedGraph<double>(8, false);
            ReadWeightedGraph.ReadFromFile(g1, filename);
            Console.WriteLine("graph1 in Sparse WeightedGraph:");
            g1.Show();
            Console.WriteLine();

//            DenseWeightedGraph<double> g2 = new DenseWeightedGraph<double>(8, false);
//            ReadWeightedGraph.ReadFromFile(g2, filename);
//            Console.WriteLine("graph1 in Dense WeightedGraph:");
//            g2.Show();
//            Console.WriteLine();

            Console.WriteLine("Test Lazy Prim MST:");
            LazyPrimMST<double> lazyPrimMST = new LazyPrimMST<Double>(g1);
            List<Edge<Double>> mst = lazyPrimMST.MstEdges();
            for (int i = 0; i < mst.Count; i++)
                Console.WriteLine(mst[i]);
            Console.WriteLine($"The MST weight is: {lazyPrimMST.Result()}");

            Console.WriteLine();
        }
    }
}