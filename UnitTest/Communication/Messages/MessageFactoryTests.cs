using Astute.Communication.Messages;
using Astute.Entity;
using NUnit.Framework;

namespace UnitTest.Communication.Messages
{
    [TestFixture]
    public class MessageFactoryTests
    {
        [Test]
        public void GetBroadcastMessageTests()
        {
            var broadcastMessageString =
                "G:P0;0,0;0;0;100;0;0:13,17,0;19,16,0;3,7,0;14,11,0;5,2,0;9,6,0;17,4,0;11,8,0;12,6,0#";
            var broadcastMessage = MessageFactory.GetMessage(broadcastMessageString);
            var playersDetails = new[]
                {new BroadcastMessage.PlayerDetails(0, new Point(0, 0), Direction.North, false, 100, 0, 0)};
            var damagesDetails = new[]
            {
                new BroadcastMessage.DamageDetails(new Point(13, 17), 0),
                new BroadcastMessage.DamageDetails(new Point(19, 16), 0),
                new BroadcastMessage.DamageDetails(new Point(3, 7), 0),
                new BroadcastMessage.DamageDetails(new Point(14, 11), 0),
                new BroadcastMessage.DamageDetails(new Point(5, 2), 0),
                new BroadcastMessage.DamageDetails(new Point(9, 6), 0),
                new BroadcastMessage.DamageDetails(new Point(17, 4), 0),
                new BroadcastMessage.DamageDetails(new Point(11, 8), 0),
                new BroadcastMessage.DamageDetails(new Point(12, 6), 0)
            };
            var broadcastMessageExpected = new BroadcastMessage(playersDetails, damagesDetails);

            Assert.NotNull(broadcastMessage);
            Assert.IsInstanceOf<BroadcastMessage>(broadcastMessage);
            Assert.AreEqual(broadcastMessageExpected, broadcastMessage);

            var broadcastMessageCast = broadcastMessage as BroadcastMessage;
            Assume.That(broadcastMessageCast, Is.Not.Null);

            CollectionAssert.AreEquivalent(playersDetails, broadcastMessageCast?.PlayersDetails);
            CollectionAssert.AreEquivalent(damagesDetails, broadcastMessageCast?.DamagesDetails);

            var broadcastMessageUnexpected = new BroadcastMessage(
                new[]
                {
                    new BroadcastMessage.PlayerDetails(0, new Point(0, 0), Direction.South /* here */, false, 100, 0, 0)
                },
                damagesDetails);

            Assert.AreNotEqual(broadcastMessageUnexpected, broadcastMessageCast);
            Assert.True(broadcastMessageCast != broadcastMessageUnexpected);

            Assert.True(broadcastMessageExpected.GetHashCode() == broadcastMessageCast?.GetHashCode());
            Assert.False(broadcastMessageExpected.GetHashCode() == broadcastMessageUnexpected.GetHashCode());
        }

        [Test]
        public void GetCoinpackMessageTests()
        {
            var coinpackMessageString = "C:5,4:14384:1056#";
            var coinpackMessage = MessageFactory.GetMessage(coinpackMessageString);
            var coinpackMessageExpected = new CoinpackMessage(new Point(5, 4), 14384, 1056);

            Assert.NotNull(coinpackMessage);
            Assert.IsInstanceOf<CoinpackMessage>(coinpackMessage);
            Assert.AreEqual(coinpackMessageExpected, coinpackMessage);

            var coinpackMessageCast = coinpackMessage as CoinpackMessage;
            Assume.That(coinpackMessageCast, Is.Not.Null);

            Assert.AreEqual(new Point(5, 4), coinpackMessageCast?.Location);
            Assert.AreEqual(14384, coinpackMessageCast?.RemainingTime);
            Assert.AreEqual(1056, coinpackMessageCast?.CoinValue);

            var coinpackMessageUnexpected = new CoinpackMessage(new Point(5, 4), 12345 /* here */, 1056);

            Assert.AreNotEqual(coinpackMessageUnexpected, coinpackMessageCast);
            Assert.True(coinpackMessageCast != coinpackMessageUnexpected);

            Assert.True(coinpackMessageExpected.GetHashCode() == coinpackMessageCast?.GetHashCode());
            Assert.False(coinpackMessageExpected.GetHashCode() == coinpackMessageUnexpected.GetHashCode());
        }

        [Test]
        public void GetCommandFailMessageTests()
        {
            var commandFailMessageString = "TOO_QUICK#";
            var commandFailMessage = MessageFactory.GetMessage(commandFailMessageString);
            var commandFailMessageExpected = new CommandFailMessage(CommandFailState.TooQuick);

            Assert.NotNull(commandFailMessage);
            Assert.IsInstanceOf<CommandFailMessage>(commandFailMessage);
            Assert.AreEqual(commandFailMessageExpected, commandFailMessage);

            var commandFailMessageCast = commandFailMessage as CommandFailMessage;
            Assume.That(commandFailMessageCast, Is.Not.Null);

            Assert.AreEqual(CommandFailState.TooQuick, commandFailMessageCast?.State);

            var commandFailMessageUnexpected = new CommandFailMessage(CommandFailState.CellOccupied /* here */);

            Assert.AreNotEqual(commandFailMessageUnexpected, commandFailMessageCast);
            Assert.True(commandFailMessageCast != commandFailMessageUnexpected);

            Assert.True(commandFailMessageExpected.GetHashCode() == commandFailMessageCast?.GetHashCode());
            Assert.False(commandFailMessageExpected.GetHashCode() == commandFailMessageUnexpected.GetHashCode());
        }

        [Test]
        public void GetInitiationMessageTests()
        {
            var initiationMessageString =
                "I:P0:14,1;9,5;13,16;4,8;15,11;19,6;3,6;1,17;5,12;9,16:12,7;4,11;18,15;2,6;13,17;4,1;8,5;19,16;3,7;14,11:18,5;2,9;16,13;0,17;8,15;3,14;1,8;12,9;16,3;0,7#";
            var initiationMessage = MessageFactory.GetMessage(initiationMessageString);
            var initiationMessageExpected = new InitiationMessage(0,
                new[]
                {
                    new Point(14, 1), new Point(9, 5), new Point(13, 16), new Point(4, 8), new Point(15, 11),
                    new Point(19, 6), new Point(3, 6), new Point(1, 17), new Point(5, 12), new Point(9, 16)
                },
                new[]
                {
                    new Point(12, 7), new Point(4, 11), new Point(18, 15), new Point(2, 6), new Point(13, 17),
                    new Point(4, 1), new Point(8, 5), new Point(19, 16), new Point(3, 7), new Point(14, 11)
                },
                new[]
                {
                    new Point(18, 5), new Point(2, 9), new Point(16, 13), new Point(0, 17), new Point(8, 15),
                    new Point(3, 14), new Point(1, 8), new Point(12, 9), new Point(16, 3), new Point(0, 7)
                });

            Assert.NotNull(initiationMessage);
            Assert.IsInstanceOf<InitiationMessage>(initiationMessage);
            Assert.AreEqual(initiationMessageExpected, initiationMessage);

            var initiationMessageCast = initiationMessage as InitiationMessage;
            Assume.That(initiationMessageCast, Is.Not.Null);

            Assert.AreEqual(initiationMessageCast?.PlayerNumber, 0);
            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Point(14, 1), new Point(9, 5), new Point(13, 16), new Point(4, 8), new Point(15, 11),
                    new Point(19, 6), new Point(3, 6), new Point(1, 17), new Point(5, 12), new Point(9, 16)
                },
                initiationMessageCast?.Bricks);
            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Point(12, 7), new Point(4, 11), new Point(18, 15), new Point(2, 6), new Point(13, 17),
                    new Point(4, 1), new Point(8, 5), new Point(19, 16), new Point(3, 7), new Point(14, 11)
                },
                initiationMessageCast?.Stones);
            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Point(18, 5), new Point(2, 9), new Point(16, 13), new Point(0, 17), new Point(8, 15),
                    new Point(3, 14), new Point(1, 8), new Point(12, 9), new Point(16, 3), new Point(0, 7)
                },
                initiationMessageCast?.Water);

            var initiationMessageUnexpected = new InitiationMessage(0,
                new[]
                {
                    new Point(14, 1), new Point(9, 5), new Point(13, 16), new Point(4, 8), new Point(15, 11),
                    new Point(19, 6), new Point(3, 6), new Point(1, 17), new Point(5, 12), new Point(9, 16)
                },
                new[]
                {
                    new Point(12, 7), new Point(4, 11), new Point(18, 15), new Point(2, 6), new Point(13, 17),
                    new Point(4, 1), new Point(8, 5), new Point(19, 16), new Point(3, 7), new Point(14, 11)
                },
                new[]
                {
                    new Point(18, 5), new Point(4 /* here */, 9), new Point(16, 13), new Point(0, 17), new Point(8, 15),
                    new Point(3, 14), new Point(1, 8), new Point(12, 9), new Point(16, 3), new Point(0, 7)
                });

            Assert.AreNotEqual(initiationMessageUnexpected, initiationMessageCast);
            Assert.IsTrue(initiationMessageCast != initiationMessageUnexpected);

            Assert.True(initiationMessageExpected.GetHashCode() == initiationMessageCast?.GetHashCode());
            Assert.False(initiationMessageExpected.GetHashCode() == initiationMessageUnexpected.GetHashCode());
        }

        //[Test]
        //public void GetDeathMessageTests()
        //{
        //    var deathMessageString = "PITFALL#";
        //    var deathMessage = MessageFactory.GetMessage(deathMessageString);
        //    var deathMessageExpected = new DeathMessage();
        //}

        [Test]
        public void GetJoinFailMessageTests()
        {
            var joinFailMessageString = "GAME_ALREADY_STARTED#";
            var joinFailMessage = MessageFactory.GetMessage(joinFailMessageString);
            var joinFailMessageExpected = new JoinFailMessage(JoinFailState.GameAlreadyStarted);

            Assert.NotNull(joinFailMessage);
            Assert.IsInstanceOf<JoinFailMessage>(joinFailMessage);
            Assert.AreEqual(joinFailMessageExpected, joinFailMessage);

            var joinFailMessageCast = joinFailMessage as JoinFailMessage;
            Assume.That(joinFailMessageCast, Is.Not.Null);

            Assert.AreEqual(JoinFailState.GameAlreadyStarted, joinFailMessageCast?.State);

            var joinFailMessageUnexpected = new JoinFailMessage(JoinFailState.PlayersFull /* here */);

            Assert.AreNotEqual(joinFailMessageUnexpected, joinFailMessageCast);
            Assert.True(joinFailMessageCast != joinFailMessageUnexpected);

            Assert.True(joinFailMessageExpected.GetHashCode() == joinFailMessageCast?.GetHashCode());
            Assert.False(joinFailMessageExpected.GetHashCode() == joinFailMessageUnexpected.GetHashCode());
        }

        [Test]
        public void GetJoinMessageTests()
        {
            var joinMessageString = "S:P0;0,0;0#";
            var joinMessage = MessageFactory.GetMessage(joinMessageString);
            var joinMessageExpected = new JoinMessage(0, new Point(0, 0), Direction.North);

            Assert.NotNull(joinMessage);
            Assert.IsInstanceOf<JoinMessage>(joinMessage);
            Assert.AreEqual(joinMessageExpected, joinMessage);

            var joinMessageCast = joinMessage as JoinMessage;
            Assume.That(joinMessageCast, Is.Not.Null);

            Assert.AreEqual(new Point(0, 0), joinMessageCast?.Location);
            Assert.AreEqual(Direction.North, joinMessageCast?.FacingDirection);
            Assert.AreEqual(0, joinMessageCast?.PlayerNumber);

            var joinMessageUnexpected = new JoinMessage(0, new Point(1, 1), Direction.North);

            Assert.AreNotEqual(joinMessageUnexpected, joinMessageCast);
            Assert.True(joinMessageCast != joinMessageUnexpected);

            Assert.True(joinMessageExpected.GetHashCode() == joinMessageCast?.GetHashCode());
            Assert.False(joinMessageExpected.GetHashCode() == joinMessageUnexpected.GetHashCode());
        }

        [Test]
        public void GetLifepackMessageTests()
        {
            var lifepackMessageString = "L:0,1:74546#";
            var lifepackMessage = MessageFactory.GetMessage(lifepackMessageString);
            var lifepackMessageExpected = new LifepackMessage(new Point(0, 1), 74546);

            Assert.NotNull(lifepackMessage);
            Assert.IsInstanceOf<LifepackMessage>(lifepackMessage);
            Assert.AreEqual(lifepackMessageExpected, lifepackMessage);

            var lifepackMessageCast = lifepackMessage as LifepackMessage;
            Assume.That(lifepackMessageCast, Is.Not.Null);

            Assert.AreEqual(new Point(0, 1), lifepackMessageCast?.Location);
            Assert.AreEqual(74546, lifepackMessageCast?.RemainingTime);

            var lifepackMessageUnexpected = new LifepackMessage(new Point(0, 1), 78986 /* here */);

            Assert.AreNotEqual(lifepackMessageUnexpected, lifepackMessageCast);
            Assert.True(lifepackMessageCast != lifepackMessageUnexpected);

            Assert.True(lifepackMessageExpected.GetHashCode() == lifepackMessageCast?.GetHashCode());
            Assert.False(lifepackMessageExpected.GetHashCode() == lifepackMessageUnexpected.GetHashCode());
        }
    }
}