using System;

namespace Astute.Entity
{
    public class StoneWall : IGridItem, ICollidable, IEquatable<StoneWall>
    {
        public StoneWall(Point location)
        {
            Location = location;
        }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public bool Equals(StoneWall other)
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
            return Equals((StoneWall) obj);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }

        public static bool operator ==(StoneWall left, StoneWall right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StoneWall left, StoneWall right)
        {
            return !Equals(left, right);
        }
    }
}