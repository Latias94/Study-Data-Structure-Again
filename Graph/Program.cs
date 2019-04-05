using System;
using System.IO;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // 得到当前 solution 的路径
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            // 使用两种图的存储方式读取graph1.txt文件
            string filename = System.IO.Path.Combine(projectDirectory, "Graph/graph1.txt");
            SparseGraph g1 = new SparseGraph(13, false);
            ReadGraph.ReadFromFile(g1, filename);
            Console.WriteLine("graph1 in Sparse Graph:");
            g1.Show();
            Component component1 = new Component(g1);
            Console.WriteLine("graph1.txt, Components Count: " + component1.Count());
            Path path1 = new Path(g1, 0);
            Console.Write("DFS path of 0 to 3: ");
            path1.ShowPath(3);
            Console.WriteLine();
            ShortestPath shortestPath1 = new ShortestPath(g1, 0);
            Console.Write("BFS shortest path of 0 to 3: ");
            shortestPath1.ShowPath(3);

//
//            DenseGraph g2 = new DenseGraph(13, false);
//            ReadGraph.ReadFromFile(g2, filename);
//            Console.WriteLine("graph1 in Dense Graph:");
//            g2.show();
//            component1 = new Component(g2);
//            Console.WriteLine("graph1.txt, Components Count: " + component1.Count());
//            Path path2 = new Path(g2, 0);
//            Console.Write("DFS path of 0 to 3: ");
//            path2.ShowPath(3);
//            Console.WriteLine();

            // 使用两种图的存储方式读取graph2.txt文件
            filename = System.IO.Path.Combine(projectDirectory, "Graph/graph2.txt");
            SparseGraph g3 = new SparseGraph(7, false);
            ReadGraph.ReadFromFile(g3, filename);
            Console.WriteLine("graph2 in Sparse Graph:");
            g3.Show();
            Component component2 = new Component(g3);
            Console.WriteLine("graph2.txt, Components Count: " + component2.Count());
            Path path3 = new Path(g3, 0);
            Console.Write("DFS path of 0 to 6: ");
            path3.ShowPath(6);
            Console.WriteLine();
            ShortestPath shortestPath3 = new ShortestPath(g1, 0);
            Console.Write("BFS shortest path of 0 to 6: ");
            shortestPath3.ShowPath(6);

            DenseGraph g4 = new DenseGraph(7, false);
            ReadGraph.ReadFromFile(g4, filename);
            Console.WriteLine("graph2 in Dense Graph:");
            g4.Show();
            component2 = new Component(g4);
            Console.WriteLine("graph2.txt, Components Count: " + component2.Count());
            Path path4 = new Path(g4, 0);
            Console.Write("DFS path of 0 to 6: ");
            path4.ShowPath(6);
            Console.WriteLine();
        }
    }
}