using System;
using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class CoinpackMessage : IMessage, IEquatable<CoinpackMessage>
    {
        public CoinpackMessage(Point location, int remainingTime, int coinValue)
        {
            Location = location;
            RemainingTime = remainingTime;
            CoinValue = coinValue;
        }

        public Point Location { get; }
        public int RemainingTime { get; }
        public int CoinValue { get; }

        public bool Equals(CoinpackMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Location.Equals(other.Location) && RemainingTime == other.RemainingTime &&
                   CoinValue == other.CoinValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is CoinpackMessage && Equals((CoinpackMessage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Location.GetHashCode();
                hashCode = (hashCode * 397) ^ RemainingTime;
                hashCode = (hashCode * 397) ^ CoinValue;
                return hashCode;
            }
        }

        public static bool operator ==(CoinpackMessage left, CoinpackMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CoinpackMessage left, CoinpackMessage right)
        {
            return !Equals(left, right);
        }
    }
}