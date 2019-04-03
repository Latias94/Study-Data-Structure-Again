using System;
namespace Array
{
    class Program
    {
        static void Main(string[] args)
        {
            Array<int> arr = new Array<int>(20);
            for (int i = 0; i < 10; i++)
            {
                arr.AddLast(i);
            }

            // 向下标为 1 的地方插入元素，原来的 1 及后面的元素统一后移一位
            arr.Insert(1, 100);
            Console.WriteLine(arr);
            // 向队首添加元素
            arr.AddFirst(-1);
            Console.WriteLine(arr);
            // 删除索引为 2 的元素
            arr.Remove(2);
            Console.WriteLine(arr);
            arr.RemoveFirst();
            Console.WriteLine(arr);
            
            Array<Student> studentArray = new Array<Student>();
            studentArray.AddLast(new Student("皮卡丘", 3));
            studentArray.AddLast(new Student("杰尼龟", 4));
            studentArray.AddLast(new Student("小火龙", 5));
            Console.WriteLine(studentArray);
            
            arr = new Array<int>();
            for (int i = 0; i < 10; i++) {
                arr.AddLast(i);
            }
            Console.WriteLine("Array: "+arr);
            Console.WriteLine("向下标为 1 的地方插入元素 100，超出数组容量会自动扩容");
            arr.Insert(1, 100);
            Console.WriteLine("Array: "+arr);
            Console.WriteLine("连续减少两个元素，元素数小于容量一半的时候，不缩容，得等到 1/4 的时候再扩容，可以防止反复扩容缩容引起性能震荡");
            arr.RemoveFirst();
            arr.RemoveFirst();
            Console.WriteLine("Array: "+arr);
            Console.WriteLine("再弹出四个，容量到 1/4，才会自动缩容");
            arr.RemoveFirst();
            arr.RemoveFirst();
            arr.RemoveFirst();
            arr.RemoveFirst();
            Console.WriteLine("Array: "+arr);
        }
    }
}