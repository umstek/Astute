using System.Linq;
using Astute.Communication;
using NUnit.Framework;

namespace UnitTest.Communication
{
    [TestFixture]
    public class InputConvertorsTests
    {
//        private void PrintTestResults()
//        {
//            var result = TestContext.CurrentContext.Result;
//            TestContext.Out.WriteLine($@"
//Out of {result.PassCount + result.FailCount + result.SkipCount + result.InconclusiveCount} tests,
//    Passed:        {result.PassCount}
//    Failed:        {result.FailCount}
//    Inconclusive:  {result.InconclusiveCount}
//    Skipped:       {result.SkipCount}");
//        }

        [Test]
        public void ScreamingSnakeCaseToCamelCaseTests()
        {
            var someWords = InputConvertors.ScreamingSnakeCaseToCamelCase("SOME_WORDS");
            Assert.AreEqual("SomeWords", someWords);

            var someOtherWords = InputConvertors.ScreamingSnakeCaseToCamelCase("SOME_OTHER_WORDS");
            Assert.AreEqual("SomeOtherWords", someOtherWords);

            var giveAPoint = InputConvertors.ScreamingSnakeCaseToCamelCase("GIVE_A_POINT");
            Assert.AreEqual("GiveAPoint", giveAPoint);

            var endingUnderscore = InputConvertors.ScreamingSnakeCaseToCamelCase("WHAT_THE_");
            Assert.AreEqual("WhatThe", endingUnderscore);

            var startingUnderscore = InputConvertors.ScreamingSnakeCaseToCamelCase("_WHAT_THE");
            Assert.AreEqual("WhatThe", startingUnderscore);

            var wrappingUnderscores = InputConvertors.ScreamingSnakeCaseToCamelCase("_WHAT_THE_");
            Assert.AreEqual("WhatThe", wrappingUnderscores);
        }

        [Test]
        public void SplitByColonTests()
        {
            const string source =
                "G:P1;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>:P5;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>:<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level><x>,<y>,<damage-level>";
            var splitted = InputConvertors.SplitByColon(source);
            CollectionAssert.AreEqual(
                new[]
                {
                    "G",
                    "P1;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>",
                    "P5;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>",
                    "<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level><x>,<y>,<damage-level>"
                }, splitted.ToArray());
        }

        [Test]
        public void SplitByCommaTests()
        {
            const string source = "<player location x>,<player location y>";
            var splitted = InputConvertors.SplitByComma(source);
            CollectionAssert.AreEqual(new[] {"<player location x>", "<player location y>"}, splitted.ToArray());
        }

        [Test]
        public void SplitBySemicolonTests()
        {
            const string source =
                "P1;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>";
            var splitted = InputConvertors.SplitBySemicolon(source);
            CollectionAssert.AreEqual(
                new[]
                {
                    "P1",
                    "<player location x>,<player location y>",
                    "<direction>",
                    "<whether shot>",
                    "<health>",
                    "<coins>",
                    "<points>"
                }, splitted.ToArray());
        }

        [Test]
        public void TrimHashTests()
        {
            var allCapsHash = InputConvertors.TrimHash("ASDF#");
            Assert.AreEqual("ASDF", allCapsHash);

            var allCapsHashSpace = InputConvertors.TrimHash("ASDF# ");
            Assert.AreEqual("ASDF", allCapsHashSpace);

            var allCapsDoubleSpace = InputConvertors.TrimHash(" ASDF ");
            Assert.AreEqual("ASDF", allCapsDoubleSpace);

            var multiCaseMultiHash = InputConvertors.TrimHash("aBC_deF##");
            Assert.AreEqual("aBC_deF", multiCaseMultiHash);

            var preserveHashPrefix = InputConvertors.TrimHash("#asdf#zxc#");
            Assert.AreEqual("#asdf#zxc", preserveHashPrefix);
        }
    }
}