using System;
using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class LifepackMessage : IMessage, IEquatable<LifepackMessage>
    {
        public LifepackMessage(Point location, int remainingTime)
        {
            Location = location;
            RemainingTime = remainingTime;
        }

        public Point Location { get; }
        public int RemainingTime { get; }

        public bool Equals(LifepackMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Location.Equals(other.Location) && (RemainingTime == other.RemainingTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is LifepackMessage && Equals((LifepackMessage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Location.GetHashCode()*397) ^ RemainingTime;
            }
        }

        public static bool operator ==(LifepackMessage left, LifepackMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LifepackMessage left, LifepackMessage right)
        {
            return !Equals(left, right);
        }
    }
}