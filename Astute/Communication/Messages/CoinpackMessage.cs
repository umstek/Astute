using Astute.Entity;

namespace Astute.Communication.Messages
{
    public sealed class CoinpackMessage : IMessage
    {
        public CoinpackMessage(Point location, int remainingTime, int coinValue)
        {
            Location = location;
            RemainingTime = remainingTime;
            CoinValue = coinValue;
        }

        public Point Location { get; }
        public int RemainingTime { get; }
        public int CoinValue { get; }
    }
}