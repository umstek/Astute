using System;
using System.Reactive.Linq;
using Astute.Communication;
using Astute.Communication.Messages;

namespace Astute
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Input.TcpInput.Retry().Select(MessageFactory.GetMessage);
            Console.ReadKey();
        }
    }
}