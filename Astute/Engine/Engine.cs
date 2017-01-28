using System;
using System.Collections.Generic;
using System.Linq;
using Astute.Communication.Messages;
using Astute.Entity;

namespace Astute.Engine
{
    /// <summary>
    ///     Main Logic
    /// </summary>
    public static class Engine
    {
        /// <summary>
        ///     Calculates the command to be sent to the server for the current state of the world and the message which was
        ///     received just now.
        /// </summary>
        /// <param name="history">What states were there and which outputs were sent</param>
        /// <param name="state">The state of the world</param>
        /// <param name="message">Received message</param>
        /// <returns></returns>
        public static Command? ComputeCommand(IEnumerable<Tuple<World, Command?>> history, World state, IMessage message)
        {
            if (!history.Any()) // First time
                return Command.Join;
            if (message is BroadcastMessage)
                return (Command) new Random().Next(0, 4);

            return null;
        }

        /// <summary>
        ///     Calculates what command is to be sent to the server and stores the state and the output to be used in
        ///     future calculations.
        /// </summary>
        /// <param name="history">Old statuses of the world and corresponding outputs that were sent</param>
        /// <param name="stateAndMessage">New world and message correspondng to that</param>
        /// <returns>Current state of the world and the output to be sent to the server calculated from it</returns>
        public static IEnumerable<Tuple<World, Command?>> MakeHistory(
            IEnumerable<Tuple<World, Command?>> history, Tuple<World, IMessage> stateAndMessage
        )
        {
            var enumerable = history as Tuple<World, Command?>[] ?? history.ToArray();
            var command = ComputeCommand(enumerable, stateAndMessage.Item1, stateAndMessage.Item2);
            return enumerable.Concat(new[] {new Tuple<World, Command?>(stateAndMessage.Item1, command)});
        }

        /// <summary>
        ///     Updates a world according to the data received from a message.
        /// </summary>
        /// <param name="oldWorldAndMessage">Previous world and message</param>
        /// <param name="message">Received message</param>
        /// <returns>New world and the latest message which was used in the creation of the world</returns>
        public static Tuple<World, IMessage> BuildWorld(Tuple<World, IMessage> oldWorldAndMessage, IMessage message)
        {
            return new Tuple<World, IMessage>(World.FromMessage(oldWorldAndMessage?.Item1, message), message);
        }
    }
}