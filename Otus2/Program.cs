using Otus2;
using System.Diagnostics;
using static Otus2.Calculator;
using System.Drawing;

Console.WriteLine($"ОС: {Environment.OSVersion}");
Console.WriteLine($"Процессор: {Environment.ProcessorCount} ядер");
Console.WriteLine($"Платформа: {(Environment.Is64BitProcess ? "x64" : "x86")}");
Console.WriteLine($"Версия .NET: {Environment.Version}");
Console.WriteLine();

long result;
int[] sizes = [100000, 1000000, 10000000];
foreach (int size in sizes)
{
    Console.WriteLine("==================================");
    Console.WriteLine($"Размер массива: {size}");
    int[] array = GenerateArray(size);
    var stopwatch = Stopwatch.StartNew();
    result = Calculator.ClassicSum(array);
    stopwatch.Stop();
    Console.WriteLine($"Последовательная сумма: {result}, время: {stopwatch.ElapsedMilliseconds} мс");

    stopwatch.Restart();
    result = Calculator.ParallelThread(array); 
    stopwatch.Stop();
    Console.WriteLine($"Параллельная сумма (Thread): {result}, время: {stopwatch.ElapsedMilliseconds} мс");

    stopwatch.Restart();
    result = Calculator.ParallelLinq(array);
    stopwatch.Stop();
    Console.WriteLine($"Параллельная сумма (Linq): {result}, время: {stopwatch.ElapsedMilliseconds} мс");
}
Console.ReadLine();

int[] GenerateArray(int size)
{
    Random rand = new Random();
    int[] array = new int[size];
    for (int i = 0; i < size; i++)
    {
        array[i] = rand.Next(1, 100);
    }
    return array;
}