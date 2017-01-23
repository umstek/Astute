using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Astute.Communication;
using Astute.Communication.Messages;
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

            var subscription = Input.TcpInput
                .Retry()
                .Select(MessageFactory.GetMessage)
                .Scan<IMessage, Tuple<World, IMessage>>(null, TransformWorldAndCombine)
                .Scan<Tuple<World, IMessage>, IEnumerable<Tuple<World, Command?>>>(
                    new Tuple<World, Command?>[] {}, ComputeCommndAndBuffer
                )
                .Select(tuples => tuples.Last().Item2)
                .Where(command => command != null)
                .Select(command => command.GetValueOrDefault(Command.Shoot))
                .Select(OutputConvertors.CommandToString)
                .Do(commandString => Logger.Info(commandString))
                .Subscribe(Output.TcpOutput);

            Console.ReadKey();
            subscription.Dispose();
        }

        private static IEnumerable<Tuple<World, Command?>> ComputeCommndAndBuffer(
            IEnumerable<Tuple<World, Command?>> history, Tuple<World, IMessage> stateAndMessage)
        {
            var enumerable = history as Tuple<World, Command?>[] ?? history.ToArray();
            var command = Engine.Engine.ComputeCommand(enumerable, stateAndMessage.Item1, stateAndMessage.Item2);
            return enumerable.Concat(new[] {new Tuple<World, Command?>(stateAndMessage.Item1, command)});
        }

        private static Tuple<World, IMessage> TransformWorldAndCombine(Tuple<World, IMessage> tuple, IMessage message)
        {
            return new Tuple<World, IMessage>(World.FromMessage(tuple?.Item1, message), message);
        }
    }
}