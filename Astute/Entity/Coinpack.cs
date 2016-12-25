using System;

namespace Astute.Entity
{
    public class Coinpack : IGridItem, ITimeVariant, IEquatable<Coinpack>
    {
        public Coinpack(Point location, int value, int timeToDisappear)
        {
            Location = location;
            CoinValue = value;
            TimeToDisappear = timeToDisappear;
        }

        public int CoinValue { get; }

        public bool Equals(Coinpack other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CoinValue == other.CoinValue && Location.Equals(other.Location) &&
                   TimeToDisappear == other.TimeToDisappear;
        }

        public Point Location { get; }
        public int TimeToDisappear { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Coinpack) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CoinValue;
                hashCode = (hashCode * 397) ^ Location.GetHashCode();
                hashCode = (hashCode * 397) ^ TimeToDisappear;
                return hashCode;
            }
        }

        public static bool operator ==(Coinpack left, Coinpack right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coinpack left, Coinpack right)
        {
            return !Equals(left, right);
        }
    }
}