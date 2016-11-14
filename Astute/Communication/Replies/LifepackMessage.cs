using System.Windows;

namespace Astute.Communication.Replies
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