using System;

namespace Astute.Entity
{
    public class Tank : IMovableGridItem, ICollidable, IShootable, IEquatable<Tank>
    {
        public Tank(Point location, int health, Direction direction, int points, int coins, int playerNumber,
            bool myTank = false)
        {
            Location = location;
            Direction = direction;
            Health = health;
            Points = points;
            Coins = coins;
            PlayerNumber = playerNumber;
            MyTank = myTank;
        }

        public int PlayerNumber { get; }
        public bool MyTank { get; }
        public Direction Direction { get; set; }
        public int Health { get; set; }
        public int Points { get; set; }
        public int Coins { get; set; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public bool Equals(Tank other)
        {
            return PlayerNumber == other.PlayerNumber;
        }

        public Point Location { get; set; }

        public bool Shoot()
        {
            return (Health -= 10) == 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Tank && Equals((Tank) obj);
        }

        public override int GetHashCode()
        {
            return PlayerNumber;
        }

        public static bool operator ==(Tank left, Tank right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Tank left, Tank right)
        {
            return !left.Equals(right);
        }
    }
}