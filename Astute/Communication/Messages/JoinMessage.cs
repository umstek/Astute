using System;
using System.Collections.Generic;
using System.Linq;
using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class JoinMessage : IMessage, IEquatable<JoinMessage>
    {
        public JoinMessage(IEnumerable<TankDetails> tanksDetails)
        {
            TanksDetails = tanksDetails;
        }

        public IEnumerable<TankDetails> TanksDetails { get; }

        public bool Equals(JoinMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TanksDetails.Count() == other.TanksDetails.Count() &&
                            TanksDetails.All(other.TanksDetails.Contains);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is JoinMessage && Equals((JoinMessage)obj);
        }

        public override int GetHashCode()
        {
            return TanksDetails.Aggregate(0, (i, details) => i ^ details.GetHashCode());
        }

        public static bool operator ==(JoinMessage left, JoinMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JoinMessage left, JoinMessage right)
        {
            return !Equals(left, right);
        }

        public sealed class TankDetails : IEquatable<TankDetails>
        {
            public TankDetails(int playerNumber, Point location, Direction facingDirection)
            {
                PlayerNumber = playerNumber;
                Location = location;
                FacingDirection = facingDirection;
            }

            public int PlayerNumber { get; }
            public Point Location { get; }
            public Direction FacingDirection { get; }

            public bool Equals(TankDetails other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return PlayerNumber == other.PlayerNumber && Location.Equals(other.Location) &&
                       FacingDirection == other.FacingDirection;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                return obj is TankDetails && Equals((TankDetails)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = PlayerNumber;
                    hashCode = (hashCode * 397) ^ Location.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int)FacingDirection;
                    return hashCode;
                }
            }

            public static bool operator ==(TankDetails left, TankDetails right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(TankDetails left, TankDetails right)
            {
                return !Equals(left, right);
            }
        }

    }
}