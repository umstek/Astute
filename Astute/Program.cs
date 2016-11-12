using System;
using System.Reactive.Linq;

namespace Astute
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Observable.Interval(TimeSpan.FromMilliseconds(333)).Select(l => "SHOOT#").Subscribe(Output.TcpOutput);
            Input.TcpInput.Retry().Subscribe(Console.WriteLine);
            Console.ReadKey();
        }
    }
}