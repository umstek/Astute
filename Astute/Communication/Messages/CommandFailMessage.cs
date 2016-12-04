namespace Astute.Communication.Messages
{
    public sealed class CommandFailMessage : IMessage
    {
        public CommandFailMessage(CommandFailState state)
        {
            State = state;
        }

        public CommandFailState State { get; }
    }
}