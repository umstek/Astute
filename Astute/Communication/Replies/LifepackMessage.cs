using System.Windows;

namespace Astute.Communication.Replies
{
    public class LifepackMessage
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