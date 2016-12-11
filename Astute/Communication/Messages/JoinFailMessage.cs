using System;

namespace Astute.Communication.Messages
{
    public sealed class JoinFailMessage : IMessage, IEquatable<JoinFailMessage>
    {
        public JoinFailMessage(JoinFailState state)
        {
            State = state;
        }

        public JoinFailState State { get; }

        public bool Equals(JoinFailMessage other)
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
            return obj is JoinFailMessage && Equals((JoinFailMessage) obj);
        }

        public override int GetHashCode()
        {
            return (int) State;
        }

        public static bool operator ==(JoinFailMessage left, JoinFailMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JoinFailMessage left, JoinFailMessage right)
        {
            return !Equals(left, right);
        }
    }
}