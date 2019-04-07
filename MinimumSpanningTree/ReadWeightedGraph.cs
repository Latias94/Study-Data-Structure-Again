using System;
using System.IO;

namespace MinimumSpanningTree
{
    public static class ReadWeightedGraph
    {
        public static void ReadFromFile(IWeightedGraph<double> graph, string filename)
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
                    double[] arr = ToNumberArr(line);
                    n = (int) arr[0];
                    if (n < 0) throw new ArgumentException("number of vertices in a Graph must be nonnegative");
                    if (n != graph.V())
                        throw new ArgumentException(
                            "number of vertices in a Graph must match with the vertices in file");

                    int m = (int) arr[1];
                    if (m < 0) throw new ArgumentException("number of edges in a Graph must be nonnegative");
                }

                while ((line = file.ReadLine()) != null)
                {
                    double[] arr = ToNumberArr(line);
                    int v = (int) arr[0];
                    int w = (int) arr[1];
                    double weight = arr[2];
                    if (v < 0 || w < 0 || v >= n || w >= n) return;
                    graph.AddEdge(new Edge<double>(v, w, weight));
                }
            }
            catch (IOException ioe)
            {
                throw new ArgumentException("Could not open " + filename, ioe);
            }
        }

        private static double[] ToNumberArr(string line)
        {
            double[] result = new double[3];
            string[] lineArr = line.Split(" ");

            result[0] = int.Parse(lineArr[0]);
            result[1] = int.Parse(lineArr[1]);
            if (lineArr.Length > 2)
            {
                result[2] = double.Parse(lineArr[2]);
            }

            return result;
        }
    }
}