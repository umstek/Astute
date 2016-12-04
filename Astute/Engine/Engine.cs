using System.Diagnostics;
using Astute.Communication.Messages;
using Astute.Entity;

namespace Astute.Engine
{
    public class Engine
    {
        public State State { get; set; }

        public void ReceiveMessage(IMessage message)
        {
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            if (message is InitiationMessage) // 1
            {
                var messageEx = (InitiationMessage) message;
                State = new State(messageEx.Bricks, messageEx.Stones, messageEx.Water);
            }
            else if (message is JoinMessage) // 2
            {
                var messageEx = (JoinMessage) message;
                var myTank = new Tank(messageEx.Location, 100, messageEx.FacingDirection, 0, 0, messageEx.PlayerNumber,
                    true);
                State.SetMyTank(myTank);
            }
            else if (message is JoinFailMessage)
            {
                var messageEx = (JoinFailMessage) message;
                // TODO handle condition
            }
            else if (message is BroadcastMessage) // 3
            {
                var messageEx = (BroadcastMessage) message;
                State.Update(messageEx.PlayersDetails, messageEx.DamagesDetails);
            }
            else if (message is LifepackMessage)
            {
                var messageEx = (LifepackMessage) message;
                State.ShowLifepack(messageEx.Location, messageEx.RemainingTime);
            }
            else if (message is CoinpackMessage)
            {
                var messageEx = (CoinpackMessage) message;
                State.ShowCoinpack(messageEx.Location, messageEx.CoinValue, messageEx.RemainingTime);
            }
            else if (message is CommandFailMessage)
            {
                var messageEx = (CommandFailMessage) message;
                // TODO handle condition
            }
            else
            {
                Debug.Fail("Unknown message. 1");
            }
        }
    }
}