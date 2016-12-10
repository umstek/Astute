using Astute.Communication;
using Astute.Entity;
using NUnit.Framework;

namespace UnitTest.Communication
{
    [TestFixture]
    public class OutputConvertorsTests
    {
        [Test]
        public void CommandToStringTests()
        {
            var shoot = OutputConvertors.CommandToString(Command.Shoot);
            Assert.AreEqual("SHOOT#", shoot);

            var left = OutputConvertors.CommandToString(Command.Left);
            Assert.AreEqual("LEFT#", left);

            var right = OutputConvertors.CommandToString(Command.Right);
            Assert.AreEqual("RIGHT#", right);

            var up = OutputConvertors.CommandToString(Command.Up);
            Assert.AreEqual("UP#", up);

            var down = OutputConvertors.CommandToString(Command.Down);
            Assert.AreEqual("DOWN#", down);

            var join = OutputConvertors.CommandToString(Command.Join);
            Assert.AreEqual("JOIN#", join);
        }
    }
}