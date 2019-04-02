using System;
using System.IO;

namespace Graph
{
    /// <summary>
    /// 读取文件
    /// </summary>
    public static class ReadGraph
    {
        public static void ReadFromFile(IGraph graph, string filename)
        {
            if (filename == null) return;
            try
            {
                StreamReader file = new StreamReader(filename);
                string line;
                int n = -1; // 顶点数
                // 第一行是点数和边数
                if ((line = file.ReadLine()) != null)
                {
                    int[] arr = ToNumberArr(line);
                    n = arr[0];
                    if (n < 0) throw new ArgumentException("number of vertices in a Graph must be nonnegative");
                    if (n != graph.V())
                        throw new ArgumentException(
                            "number of vertices in a Graph must match with the vertices in file");

                    int m = arr[1];
                    if (m < 0) throw new ArgumentException("number of edges in a Graph must be nonnegative");
                }

                while ((line = file.ReadLine()) != null)
                {
                    int[] arr = ToNumberArr(line);
                    int v = arr[0];
                    int w = arr[1];
                    if (v < 0 || w < 0 || v >= n || w >= n) return;
                    graph.AddEdge(v, w);
                }
            }
            catch (IOException ioe)
            {
                throw new ArgumentException("Could not open " + filename, ioe);
            }
        }

        private static int[] ToNumberArr(string line)
        {
            int[] result = new int[2];
            string[] lineArr = line.Split(" ");

            result[0] = int.Parse(lineArr[0]);
            result[1] = int.Parse(lineArr[1]);
            return result;
        }
    }
}