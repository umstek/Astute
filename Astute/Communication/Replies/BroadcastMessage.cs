using System.Collections.Generic;
using System.Windows;
using Astute.Entity;

namespace Astute.Communication.Replies
{
    public sealed class BroadcastMessage : IMessage
    {
        public BroadcastMessage(IEnumerable<PlayerDetails> playersDetails, IEnumerable<DamageDetails> damagesDetails)
        {
            PlayersDetails = playersDetails;
            DamagesDetails = damagesDetails;
        }

        public IEnumerable<PlayerDetails> PlayersDetails { get; }
        public IEnumerable<DamageDetails> DamagesDetails { get; }

        public sealed class PlayerDetails
        {
            public PlayerDetails(int playerNumber, Point location, Direction facingDirection, bool isShot, int health,
                int coins, int points)
            {
                PlayerNumber = playerNumber;
                Location = location;
                FacingDirection = facingDirection;
                IsShot = isShot;
                Health = health;
                Coins = coins;
                Points = points;
            }

            public int PlayerNumber { get; }
            public Point Location { get; }
            public Direction FacingDirection { get; }
            public bool IsShot { get; }
            public int Health { get; }
            public int Coins { get; }
            public int Points { get; }
        }

        public sealed class DamageDetails
        {
            public DamageDetails(Point location, int damageLevel)
            {
                Location = location;
                DamageLevel = damageLevel;
            }

            public Point Location { get; }
            public int DamageLevel { get; }
        }
    }
}