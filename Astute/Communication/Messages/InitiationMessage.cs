using System;
using System.Collections.Generic;
using System.Linq;
using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class InitiationMessage : IMessage, IEquatable<InitiationMessage>
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

        public bool Equals(InitiationMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            // Refer BroadcastMessage for comments on how Equals method is implemented. 
            var bricksEquality = (Bricks.Count() == other.Bricks.Count()) && Bricks.All(other.Bricks.Contains);
            var stonesEquality = (Stones.Count() == other.Stones.Count()) && Stones.All(other.Stones.Contains);
            var waterEquality = (Water.Count() == other.Water.Count()) && Water.All(other.Water.Contains);
            return (PlayerNumber == other.PlayerNumber) && bricksEquality && stonesEquality && waterEquality;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is InitiationMessage && Equals((InitiationMessage) obj);
        }

        public override int GetHashCode()
        {
            var bricksHash = Bricks.Aggregate(0, (i, details) => i ^ details.GetHashCode());
            var stonesHash = Stones.Aggregate(0, (i, details) => i ^ details.GetHashCode());
            var waterHash = Water.Aggregate(0, (i, details) => i ^ details.GetHashCode());

            unchecked
            {
                return ((Bricks.Count()*397) ^ bricksHash) + ((Stones.Count()*397) ^ stonesHash) +
                       ((Water.Count()*397) ^ waterHash);
            }
        }

        public static bool operator ==(InitiationMessage left, InitiationMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InitiationMessage left, InitiationMessage right)
        {
            return !Equals(left, right);
        }
    }
}