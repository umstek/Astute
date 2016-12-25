using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Astute.Communication.Messages;
using Astute.Entity;

namespace Astute.Engine
{
    public class World
    {
        private HashSet<BrickWall> _brickWalls = new HashSet<BrickWall>();
        private HashSet<Coinpack> _coinpacks = new HashSet<Coinpack>();
        private HashSet<Lifepack> _lifepacks = new HashSet<Lifepack>();
        private HashSet<StoneWall> _stoneWalls = new HashSet<StoneWall>();
        private HashSet<Tank> _tanks = new HashSet<Tank>();
        private HashSet<Water> _waters = new HashSet<Water>();

        /// <summary>
        ///     Creates an empty world.
        /// </summary>
        /// <param name="playerNumber">The number of the player which the client is assigned to. </param>
        public World(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        ///     All the GridItems as a 2D array to be accessed by (specially) the User Interface.
        /// </summary>
        public IGridItem[,] GridItems { get; } = new IGridItem[20, 20];

        public int PlayerNumber { get; }

        /// <summary>
        ///     BrickWalls currently in the game.
        /// </summary>
        public HashSet<BrickWall> BrickWalls
        {
            get { return _brickWalls; }
            set
            {
                _brickWalls = value;
                foreach (var brickWall in _brickWalls)
                    GridItems[brickWall.Location.X, brickWall.Location.Y] = brickWall;
            }
        }

        /// <summary>
        ///     StoneWalls currently in the game.
        /// </summary>
        public HashSet<StoneWall> StoneWalls
        {
            get { return _stoneWalls; }
            set
            {
                _stoneWalls = value;
                foreach (var stoneWall in _stoneWalls)
                    GridItems[stoneWall.Location.X, stoneWall.Location.Y] = stoneWall;
            }
        }

        /// <summary>
        ///     Water ponds currently in the game.
        /// </summary>
        public HashSet<Water> Waters
        {
            get { return _waters; }
            set
            {
                _waters = value;
                foreach (var water in _waters)
                    GridItems[water.Location.X, water.Location.Y] = water;
            }
        }

        /// <summary>
        ///     Tanks currently in the game.
        /// </summary>
        public HashSet<Tank> Tanks
        {
            get { return _tanks; }
            set
            {
                _tanks = value;
                foreach (var tank in _tanks)
                    GridItems[tank.Location.X, tank.Location.Y] = tank;
            }
        }

        /// <summary>
        ///     Lifepacks currently in the game.
        /// </summary>
        public HashSet<Lifepack> Lifepacks
        {
            get { return _lifepacks; }
            set
            {
                _lifepacks = value;
                foreach (var lifepack in _lifepacks)
                    GridItems[lifepack.Location.X, lifepack.Location.Y] = lifepack;
            }
        }

        /// <summary>
        ///     Coinpacks currently in the game.
        /// </summary>
        public HashSet<Coinpack> Coinpacks
        {
            get { return _coinpacks; }
            set
            {
                _coinpacks = value;
                foreach (var coinpack in _coinpacks)
                    GridItems[coinpack.Location.X, coinpack.Location.Y] = coinpack;
            }
        }

        /// <summary>
        ///     Creates a new world using the details given in the InitiationMessage object.
        /// </summary>
        /// <param name="message">Initiation message is the first message received by the client. </param>
        /// <returns>New world based on the InitiationMessage. </returns>
        public static World FromInitiationMessage(InitiationMessage message)
        {
            var brickWalls = message.Bricks.ToArray().Select(point => new BrickWall(point));
            var stoneWalls = message.Stones.ToArray().Select(point => new StoneWall(point));
            var waters = message.Water.ToArray().Select(point => new Water(point));

            return new World(message.PlayerNumber)
            {
                BrickWalls = new HashSet<BrickWall>(brickWalls),
                StoneWalls = new HashSet<StoneWall>(stoneWalls),
                Waters = new HashSet<Water>(waters)
            };
        }

        /// <summary>
        ///     Creates a world using the world created by the first message InitiationMessage and
        ///     data received in the JoinMessage.
        /// </summary>
        /// <param name="oldWorld">The old world created using the InitiationMessage. </param>
        /// <param name="message">JoinMessage is the message received when the game starts. </param>
        /// <returns>New world based on both the old world and the JoinMessage. </returns>
        public static World FromJoinMessage(World oldWorld, JoinMessage message)
        {
            return new World(message.PlayerNumber /* == oldWorld.PlayerNumber? */)
            {
                BrickWalls = oldWorld.BrickWalls,
                StoneWalls = oldWorld.StoneWalls,
                Waters = oldWorld.Waters,
                Tanks = new HashSet<Tank>
                {
                    new Tank(message.Location, message.FacingDirection, message.PlayerNumber, true)
                }
            };
        }

        /// <summary>
        ///     Broadcast message can be any message received after the join message, in 1 second intervals.
        /// </summary>
        /// <param name="oldWorld">The current state of the world. </param>
        /// <param name="message">The BroadcastMessage received. </param>
        /// <returns>New world based on both the old world and the BroadcastMessage. </returns>
        public static World FromBroadcastMessage(World oldWorld, BroadcastMessage message)
        {
            var brickWalls = message.DamagesDetails.ToArray().Select(details =>
            {
                var oldBrickWall = oldWorld.GridItems[details.Location.X, details.Location.Y] as BrickWall;
                // Assumption: BrickWall is the only thing which gets damaged.
                Debug.Assert(oldBrickWall != null);
                Trace.Assert(oldBrickWall != null);
                var newBrickWall = new BrickWall(oldBrickWall.Health, oldBrickWall.Location);
                return newBrickWall;
            });

            var tanks = message.PlayersDetails.ToArray()
                .Select(
                    details =>
                        new Tank(details.Location, details.Health, details.FacingDirection, details.Points,
                            details.Coins, details.PlayerNumber, details.PlayerNumber == oldWorld.PlayerNumber));

            return new World(oldWorld.PlayerNumber)
            {
                BrickWalls = new HashSet<BrickWall>(brickWalls),
                StoneWalls = oldWorld.StoneWalls,
                Waters = oldWorld.Waters,
                Tanks = new HashSet<Tank>(tanks),

                // Acts as a clock for TimeVarient items. 
                // TODO Check accuracy of this calculation ( < 1 thing).
                // TODO Confirm that we don't need an actual clock for this.
                // TODO Identify what the server sends (does it actually sends time to disappear?).
                Coinpacks =
                    new HashSet<Coinpack>(
                        oldWorld.Coinpacks.ToList()
                            .Where(coinpack => coinpack.TimeToDisappear > 1)
                            .Select(
                                coinpack =>
                                    new Coinpack(coinpack.Location, coinpack.CoinValue, coinpack.TimeToDisappear - 1))),
                Lifepacks =
                    new HashSet<Lifepack>(
                        oldWorld.Lifepacks.ToList()
                            .Where(lifepack => lifepack.TimeToDisappear > 1)
                            .Select(
                                lifepack =>
                                    new Lifepack(lifepack.Location, lifepack.HealthValue, lifepack.TimeToDisappear)))
            };
        }

        /// <summary>
        ///     Adds a new coinpack to the world.
        /// </summary>
        /// <param name="oldWorld">Current state of the world. </param>
        /// <param name="message">The CoinpackMessage received. </param>
        /// <returns>New world based on both the old world and the CoinpackMessage. </returns>
        public static World FromCoinpackMessage(World oldWorld, CoinpackMessage message)
        {
            var coinpack = new Coinpack(message.Location, message.CoinValue, message.RemainingTime);
            return new World(oldWorld.PlayerNumber)
            {
                BrickWalls = oldWorld.BrickWalls,
                StoneWalls = oldWorld.StoneWalls,
                Waters = oldWorld.Waters,
                Tanks = oldWorld.Tanks,
                Coinpacks = new HashSet<Coinpack>(oldWorld.Coinpacks.Concat(new[] {coinpack})),
                Lifepacks = oldWorld.Lifepacks
            };
        }

        /// <summary>
        ///     Adds a new lifepack to the world.
        /// </summary>
        /// <param name="oldWorld">Current state of the world. </param>
        /// <param name="message">The LifepackMessage received. </param>
        /// <returns>New world based on both the old world and the LifepackMessage. </returns>
        public static World FromLifepackMessage(World oldWorld, LifepackMessage message)
        {
            var lifepack = new Lifepack(message.Location, message.RemainingTime);
            return new World(oldWorld.PlayerNumber)
            {
                BrickWalls = oldWorld.BrickWalls,
                StoneWalls = oldWorld.StoneWalls,
                Waters = oldWorld.Waters,
                Tanks = oldWorld.Tanks,
                Coinpacks = oldWorld.Coinpacks,
                Lifepacks = new HashSet<Lifepack>(oldWorld.Lifepacks.Concat(new[] {lifepack}))
            };
        }

        public static World FromMessage(World oldWorld, IMessage message)
        {
            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull

            if (message is InitiationMessage) // 1
            {
                var messageEx = (InitiationMessage) message;
                return FromInitiationMessage(messageEx);
            }

            if (message is JoinMessage) // 2
            {
                var messageEx = (JoinMessage) message;
                return FromJoinMessage(oldWorld, messageEx);
            }

            if (message is JoinFailMessage)
            {
                var messageEx = (JoinFailMessage) message;
                return oldWorld;
            }

            if (message is BroadcastMessage) // 3
            {
                var messageEx = (BroadcastMessage) message;
                return FromBroadcastMessage(oldWorld, messageEx);
            }

            if (message is LifepackMessage)
            {
                var messageEx = (LifepackMessage) message;
                return FromLifepackMessage(oldWorld, messageEx);
            }

            if (message is CoinpackMessage)
            {
                var messageEx = (CoinpackMessage) message;
                return FromCoinpackMessage(oldWorld, messageEx);
            }

            if (message is CommandFailMessage)
            {
                var messageEx = (CommandFailMessage) message;
                return oldWorld;
            }

            Debug.Fail("Unknown message. ");

            return oldWorld;
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull
        }
    }
}