using System;

namespace StackAndQueues
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayStack<int> stack = new ArrayStack<int>();
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i);
                Console.WriteLine(stack);
            }

            Console.WriteLine();

            ArrayQueue<int> arrayQueue = new ArrayQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                arrayQueue.Enqueue(i);
                Console.WriteLine(arrayQueue);
                if (i % 3 == 2)
                {
                    arrayQueue.Dequeue();
                    Console.WriteLine(arrayQueue);
                }
            }

            Console.WriteLine();

            LoopQueue<int> loopQueue = new LoopQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                loopQueue.Enqueue(i);
                Console.WriteLine(loopQueue);
                if (i % 3 == 2)
                {
                    loopQueue.Dequeue();
                    Console.WriteLine(loopQueue);
                }
            }
        }
    }
}