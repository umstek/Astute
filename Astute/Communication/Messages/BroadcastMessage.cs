using System;
using System.Collections.Generic;
using System.Linq;
using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class BroadcastMessage : IMessage, IEquatable<BroadcastMessage>
    {
        public BroadcastMessage(IEnumerable<PlayerDetails> playersDetails, IEnumerable<DamageDetails> damagesDetails)
        {
            PlayersDetails = playersDetails;
            DamagesDetails = damagesDetails;
        }

        public IEnumerable<PlayerDetails> PlayersDetails { get; }
        public IEnumerable<DamageDetails> DamagesDetails { get; }

        public bool Equals(BroadcastMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            // http://stackoverflow.com/questions/3669970/compare-two-listt-objects-for-equality-ignoring-order
            // Items are unique - so lists like {x, x, y} and {x, y, y} are not checked. 
            // So, checking item count for equality and checking all items in one list for existance in the other is enough. 
            var playerDetailsEquality = (PlayersDetails.Count() == other.PlayersDetails.Count()) &&
                                        PlayersDetails.All(other.PlayersDetails.Contains);
            var damageDetailsEquality = (DamagesDetails.Count() == other.DamagesDetails.Count()) &&
                                        DamagesDetails.All(other.DamagesDetails.Contains);
            return playerDetailsEquality && damageDetailsEquality;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is BroadcastMessage && Equals((BroadcastMessage) obj);
        }

        public override int GetHashCode()
        {
            // http://stackoverflow.com/questions/670063/getting-hash-of-a-list-of-strings-regardless-of-order
            // Items are unique - probability of getting the same hashcode is lower. 
            var playerDetailsHash = PlayersDetails.Aggregate(0, (i, details) => i ^ details.GetHashCode());
            var damageDetailshash = DamagesDetails.Aggregate(0, (i, details) => i ^ details.GetHashCode());

            return unchecked(((PlayersDetails.Count()*397) ^ playerDetailsHash) +
                             ((DamagesDetails.Count()*397) ^ damageDetailshash));
        }

        public static bool operator ==(BroadcastMessage left, BroadcastMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BroadcastMessage left, BroadcastMessage right)
        {
            return !Equals(left, right);
        }

        public sealed class PlayerDetails : IEquatable<PlayerDetails>
        {
            public PlayerDetails(int playerNumber, Point location, Direction facingDirection, bool isShot, int health,
                int coins, int points)
            {
                PlayerNumber = playerNumber;
                Location = location;
                FacingDirection = facingDirection;
                IsShot = isShot;
                Health = health;
                Coins = coins;
                Points = points;
            }

            public int PlayerNumber { get; }
            public Point Location { get; }
            public Direction FacingDirection { get; }
            // ReSharper disable once MemberCanBePrivate.Global
            public bool IsShot { get; }
            public int Health { get; }
            public int Coins { get; }
            public int Points { get; }

            public bool Equals(PlayerDetails other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return (PlayerNumber == other.PlayerNumber) && Location.Equals(other.Location) &&
                       (FacingDirection == other.FacingDirection) && (IsShot == other.IsShot) &&
                       (Health == other.Health) && (Coins == other.Coins) && (Points == other.Points);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                return obj is PlayerDetails && Equals((PlayerDetails) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = PlayerNumber;
                    hashCode = (hashCode*397) ^ Location.GetHashCode();
                    hashCode = (hashCode*397) ^ (int) FacingDirection;
                    hashCode = (hashCode*397) ^ IsShot.GetHashCode();
                    hashCode = (hashCode*397) ^ Health;
                    hashCode = (hashCode*397) ^ Coins;
                    hashCode = (hashCode*397) ^ Points;
                    return hashCode;
                }
            }

            public static bool operator ==(PlayerDetails left, PlayerDetails right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(PlayerDetails left, PlayerDetails right)
            {
                return !Equals(left, right);
            }
        }

        public sealed class DamageDetails : IEquatable<DamageDetails>
        {
            public DamageDetails(Point location, int damageLevel)
            {
                Location = location;
                DamageLevel = damageLevel;
            }

            public Point Location { get; }
            public int DamageLevel { get; }

            public bool Equals(DamageDetails other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Location.Equals(other.Location) && (DamageLevel == other.DamageLevel);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                return obj is DamageDetails && Equals((DamageDetails) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Location.GetHashCode()*397) ^ DamageLevel;
                }
            }

            public static bool operator ==(DamageDetails left, DamageDetails right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(DamageDetails left, DamageDetails right)
            {
                return !Equals(left, right);
            }
        }
    }
}