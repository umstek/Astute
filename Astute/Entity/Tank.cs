using System;

namespace Astute.Entity
{
    public class Tank : IMovableGridItem, IEquatable<Tank>
    {
        public const int MaxHealth = 100;
        public const int InitialPoints = 0;
        public const int InitialCoins = 0;

        public Tank(Point location, int health, Direction direction, int points, int coins, int playerNumber, bool isFiring = false, bool myTank = false)
        {
            Location = location;
            Direction = direction;
            Health = health;
            Points = points;
            Coins = coins;
            PlayerNumber = playerNumber;
            IsFiring = isFiring;
            MyTank = myTank;
        }

        public Tank(Point location, Direction direction, int playerNumber, bool isFiring, bool myTank = false)
            : this(location, MaxHealth, direction, InitialPoints, InitialCoins, playerNumber, isFiring, myTank)
        {
        }

        public int PlayerNumber { get; }
        public bool MyTank { get; }
        public Direction Direction { get; set; }
        public int Health { get; set; }
        public int Points { get; set; }
        public int Coins { get; set; }
        public bool IsFiring { get; set; } 

        public bool Equals(Tank other)
        {
            return PlayerNumber == other?.PlayerNumber;
        }

        public Point Location { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is Tank && Equals((Tank) obj);
        }

        public override int GetHashCode()
        {
            return PlayerNumber;
        }

        public static bool operator ==(Tank left, Tank right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(Tank left, Tank right)
        {
            return left != null && !left.Equals(right);
        }
    }
}