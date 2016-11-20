using System.Collections.Generic;
using Astute.Entity;

namespace Astute.Communication.Replies
{
    public sealed class InitiationMessage : IMessage
    {
        public InitiationMessage(int playerNumber, IEnumerable<Point> bricks, IEnumerable<Point> stones,
            IEnumerable<Point> water)
        {
            PlayerNumber = playerNumber;
            Bricks = bricks;
            Stones = stones;
            Water = water;
        }

        public int PlayerNumber { get; }
        public IEnumerable<Point> Bricks { get; }
        public IEnumerable<Point> Stones { get; }
        public IEnumerable<Point> Water { get; }
    }
}