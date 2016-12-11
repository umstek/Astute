using System;

namespace Astute.Entity
{
    public class Water : IGridItem, ICollidable, IEquatable<Water>
    {
        public Water(Point location)
        {
            Location = location;
        }

        public void Collide(Direction direction, Tank tank)
        {
            tank.Health--;
        }

        public bool Equals(Water other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Location.Equals(other.Location);
        }

        public Point Location { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Water) obj);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }

        public static bool operator ==(Water left, Water right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Water left, Water right)
        {
            return !Equals(left, right);
        }
    }
}