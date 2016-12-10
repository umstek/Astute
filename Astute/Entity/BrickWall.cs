using System;

namespace Astute.Entity
{
    public class BrickWall : IGridItem, ICollidable, IShootable, IEquatable<BrickWall>
    {
        public BrickWall(int maxHealth, Point location)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Location = location;
        }

        public int MaxHealth { get; }
        public int Health { get; set; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public Point Location { get; }

        public bool Shoot() => Health-- == 0;

        public bool Equals(BrickWall other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MaxHealth == other.MaxHealth && Health == other.Health && Location.Equals(other.Location);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BrickWall) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = MaxHealth;
                hashCode = (hashCode*397) ^ Health;
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