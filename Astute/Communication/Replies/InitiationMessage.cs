using System.Collections.Generic;
using System.Windows;

namespace Astute.Communication.Replies
{
    public sealed class InitiationMessage
    {
        public InitiationMessage(int playerNumber, IList<Point> bricks, IList<Point> stones, IList<Point> water)
        {
            PlayerNumber = playerNumber;
            Bricks = bricks;
            Stones = stones;
            Water = water;
        }

        public int PlayerNumber { get; }
        public IList<Point> Bricks { get; }
        public IList<Point> Stones { get; }
        public IList<Point> Water { get; }
    }
}