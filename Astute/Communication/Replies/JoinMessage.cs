using Astute.Entity;

namespace Astute.Communication.Replies
{
    public sealed class JoinMessage : IMessage
    {
        public JoinMessage(int playerNumber, Point location, Direction facingDirection)
        {
            PlayerNumber = playerNumber;
            Location = location;
            FacingDirection = facingDirection;
        }

        public int PlayerNumber { get; }
        public Point Location { get; }
        public Direction FacingDirection { get; }
    }
}