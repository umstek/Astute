using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Astute.Communication;
using Astute.Communication.Messages;
using Astute.Entity;
using NLog;
using static Astute.Engine.Engine;

namespace Astute.Engine
{
    public static class SubscriptionManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     A message received and the calculated result from that message plus the previous worlds.
        ///     Decision taking process should connect here.
        ///     GUI Should connect here.
        /// </summary>
        public static IConnectableObservable<Tuple<World, IMessage>> StateAndMessageStream { get; } =
            Input.TcpInput
                .Retry()
                .Select(MessageFactory.GetMessage)
                .Scan<IMessage, Tuple<World, IMessage>>(null, BuildWorld)
                .Publish();

        /// <summary>
        ///     Returns the calculated command as a string.
        ///     TCP Output should connect here.
        /// </summary>
        public static IDisposable OutputSubscription { get; } =
            StateAndMessageStream
                .Scan<Tuple<World, IMessage>, IEnumerable<Tuple<World, Command?>>>(
                    new Tuple<World, Command?>[] {}, MakeHistory
                )
                .Select(tuples => tuples.Last().Item2)
                .Where(command => command != null)
                .Select(command => command.GetValueOrDefault(Command.Shoot))
                .Select(OutputConvertors.CommandToString)
                .Subscribe(Output.TcpOutput);
    }
}