using System;
using System.IO;

namespace MinimumSpanningTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // 得到当前 solution 的路径
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            // 使用两种图的存储方式读取graph1.txt文件
            string filename = Path.Combine(projectDirectory, "Algorithms2019\\MinimumSpanningTree\\graph1.txt");
            SparseWeightedGraph<double> g1 = new SparseWeightedGraph<double>(8, false);
            ReadWeightedGraph.ReadFromFile(g1, filename);
            Console.WriteLine("graph1 in Sparse WeightedGraph:");
            g1.Show();
            Console.WriteLine();
                
            DenseWeightedGraph<double> g2 = new DenseWeightedGraph<double>(8, false);
            ReadWeightedGraph.ReadFromFile(g2, filename);
            Console.WriteLine("graph1 in Dense WeightedGraph:");
            g2.Show();
            Console.WriteLine();
        }
    }
}