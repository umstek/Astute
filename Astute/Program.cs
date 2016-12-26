using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Astute.Communication;
using Astute.Communication.Messages;
using Astute.Engine;
using Astute.Entity;

namespace Astute
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Output.TcpOutput.OnNext(OutputConvertors.CommandToString(Command.Join));

            var subscription = Input.TcpInput
                .Retry()
                .Select(MessageFactory.GetMessage)
                .Aggregate<IMessage, Tuple<World, IMessage>>(null, TransformWorldAndCombine)
                .Aggregate<Tuple<World, IMessage>, IEnumerable<Tuple<World, Command>>>(
                    new Tuple<World, Command>[] { }, ComputeCommndAndBuffer)
                .Select(tuples => tuples.Last().Item2)
                .Select(OutputConvertors.CommandToString)
                //.Do(Console.WriteLine)
                .Subscribe(Output.TcpOutput);

            Console.ReadKey();
            subscription.Dispose();
        }

        private static IEnumerable<Tuple<World, Command>> ComputeCommndAndBuffer(
            IEnumerable<Tuple<World, Command>> history, Tuple<World, IMessage> stateAndMessage)
        {
            Console.WriteLine("-");
            var enumerable = history as Tuple<World, Command>[] ?? history.ToArray();
            var command = Engine.Engine.ComputeCommand(enumerable, stateAndMessage.Item1, stateAndMessage.Item2);
            return enumerable.Concat(new[] { new Tuple<World, Command>(stateAndMessage.Item1, command) });
        }

        private static Tuple<World, IMessage> TransformWorldAndCombine(Tuple<World, IMessage> tuple, IMessage message)
        {
            Console.WriteLine(DateTime.Now);
            return new Tuple<World, IMessage>(World.FromMessage(tuple?.Item1, message), message);
        }
    }
}