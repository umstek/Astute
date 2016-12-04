using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class LifepackMessage : IMessage
    {
        public LifepackMessage(Point location, int remainingTime)
        {
            Location = location;
            RemainingTime = remainingTime;
        }

        public Point Location { get; }
        public int RemainingTime { get; }
    }
}