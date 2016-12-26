using System;
using System.Collections.Generic;
using System.Linq;
using Astute.Communication.Messages;
using Astute.Entity;

namespace Astute.Engine
{
    public static class Engine
    {
        public static Command ComputeCommand(IEnumerable<Tuple<World, Command>> history, World state, IMessage message)
        {
            if (!history.Any()) // First time
                return Command.Join;
            return (Command) new Random().Next(0, 4);
            return Command.Shoot;
        }
    }
}