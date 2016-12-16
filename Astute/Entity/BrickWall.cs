using System;

namespace Astute.Entity
{
    public class BrickWall : IGridItem, IEquatable<BrickWall>
    {
        private const int MaxHealth = 4;

        public BrickWall(int health, Point location)
        {
            Health = health;
            Location = location;
        }

        public BrickWall(Point location) : this(MaxHealth, location)
        {
        }

        public int Health { get; set; }

        public bool Equals(BrickWall other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (Health == other.Health) && Location.Equals(other.Location);
        }

        public Point Location { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BrickWall) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Health;
                hashCode = (hashCode*397) ^ Location.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(BrickWall left, BrickWall right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BrickWall left, BrickWall right)
        {
            return !Equals(left, right);
        }
    }
}