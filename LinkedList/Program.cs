using System;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            for (int i = 0; i < 5; i++)
            {
                linkedList.AddFirst(i);
                Console.WriteLine(linkedList);
            }

            linkedList.Insert(2, 100);
            Console.WriteLine(linkedList);
            linkedList.Delete(2);
            Console.WriteLine(linkedList);
            linkedList.DeleteFirst();
            Console.WriteLine(linkedList);

            Console.WriteLine();

            LinkedListStack<int> stack = new LinkedListStack<int>();
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i);
                Console.WriteLine(stack);
            }

            stack.Pop();
            Console.WriteLine(stack);

            Console.WriteLine();

            LinkedListQueue<int> queue = new LinkedListQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
                Console.WriteLine(queue);

                if (i % 3 == 2)
                {
                    queue.Dequeue();
                    Console.WriteLine(queue);
                }
            }
        }
    }
}