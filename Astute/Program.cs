using System;
using Astute.Communication;
using Astute.Engine;
using Astute.Entity;
using NLog;

namespace Astute
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            Output.TcpOutput.OnNext(OutputConvertors.CommandToString(Command.Join));

            var outputSubscription = SubscriptionManager.CommandStringStream.Subscribe(Output.TcpOutput);
            var connection = SubscriptionManager.StateAndMessageStream.Connect();

            Console.ReadKey();

            outputSubscription.Dispose();
            connection.Dispose();
        }
    }
}