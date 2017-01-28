using System;

namespace Astute.Communication.Messages
{
    public class DeathMessage : IMessage, IEquatable<DeathMessage>
    {
        public DeathMessage(DeathState deathState)
        {
            State = State;
        }

        public DeathState State { get; }

        public bool Equals(DeathMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return State == other.State;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is DeathMessage && Equals((DeathMessage) obj);
        }

        public override int GetHashCode()
        {
            return (int) State;
        }

        public static bool operator ==(DeathMessage left, DeathMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DeathMessage left, DeathMessage right)
        {
            return !Equals(left, right);
        }
    }
}