using System;
using System.Reactive.Linq;
using Astute.Communication;

namespace Astute
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(l => l < 1 ? "JOIN#" : (l < 13 ? "RIGHT#" : "SHOOT#"))
                .Subscribe(Output.TcpOutput);
            Input.TcpInput.Retry().Subscribe(Console.WriteLine);
            Console.ReadKey();
        }
    }
}