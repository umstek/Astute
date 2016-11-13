using System.Windows;

namespace Astute.Communication.Replies
{
    public class CoinpackMessage
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