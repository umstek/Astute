namespace Astute.Communication.Messages
{
    public sealed class JoinFailMessage : IMessage
    {
        public JoinFailMessage(JoinFailState state)
        {
            State = state;
        }

        public JoinFailState State { get; }
    }
}