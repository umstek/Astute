using System;

namespace Astute.Communication.Messages
{
    public sealed class CommandFailMessage : IMessage, IEquatable<CommandFailMessage>
    {
        public CommandFailMessage(CommandFailState state)
        {
            State = state;
        }

        public CommandFailState State { get; }

        public bool Equals(CommandFailMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return State == other.State;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is CommandFailMessage && Equals((CommandFailMessage) obj);
        }

        public override int GetHashCode()
        {
            return (int) State;
        }

        public static bool operator ==(CommandFailMessage left, CommandFailMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CommandFailMessage left, CommandFailMessage right)
        {
            return !Equals(left, right);
        }
    }
}