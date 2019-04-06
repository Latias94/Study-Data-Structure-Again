using System;

namespace SegmentTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = {-2, 0, 3, -5, 2, -1};
            // 用于求和的线段树
            // 可以根据需求修改 lambda 函数以实现平均数、最大值、最小值等等功能
            SegmentTree<int> segmentTree = new SegmentTree<int>(nums, (a, b) => a + b);
            // 1. 查看树
            Console.WriteLine(segmentTree);
            // 2. 查询指定区间的和
            Console.WriteLine(segmentTree.Query(0, 2)); // -2 + 0 + 3
            Console.WriteLine(segmentTree.Query(2, 5)); // 3 + -5 + 2 + -1
            Console.WriteLine(segmentTree.Query(0, 5));
        }
    }
}