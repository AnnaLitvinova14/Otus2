using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus2
{
    public class Calculator
    {
        public static long ClassicSum(int[] array)
        {
            long sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        public static long ParallelThread(int[] array)
        {
            long generalSum = 0;
            int threadsCount = Environment.ProcessorCount;
            // чтобы глушить доступ к общей сумме
            object _lock = new object();
            List<Thread> threads = new List<Thread>(threadsCount);
            var results = new long[threadsCount];
            // делим массив по частям
            int partArray = array.Length / threadsCount;
            for(int i = 0; i < threadsCount; i++)
            {
                int threadIndex = i;
                // создаем потоки
                Thread thread = new Thread(() =>
                {
                    // сумма конкретного потока
                    long localSum = 0;
                    // индексы для каждого потока
                    int startIndex = threadIndex * partArray;
                    int endIndex = startIndex + partArray;
                    // сумма для каждого потока
                    for (int j = startIndex; j < endIndex; j++)
                    {
                        localSum += array[j];
                    }
                    // в общую сумму плюсуем локальную
                    lock (_lock)
                    {
                        generalSum += localSum;
                    }
                });
                threads.Add(thread);
                // запускаем поток
                thread.Start();
            }
            // ожидаем завершения потоков
            foreach (var thread in threads)
            {
                thread.Join();
            }
            return generalSum;
        }
        public static long ParallelLinq(int[] array)
        {
            return array.AsParallel().Sum();
        }
    }
}
