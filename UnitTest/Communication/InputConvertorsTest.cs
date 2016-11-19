using System.Linq;
using Astute.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Communication
{
    [TestClass]
    public class InputConvertorsTest
    {
        [TestMethod]
        public void TestTrimHash()
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

        [TestMethod]
        public void TestScreamingSnakeCaseToCamelCase()
        {
            var someWords = InputConvertors.ScreamingSnakeCaseToCamelCase("SOME_WORDS");
            Assert.AreEqual("SomeWords", someWords);

            var someOtherWords = InputConvertors.ScreamingSnakeCaseToCamelCase("SOME_OTHER_WORDS");
            Assert.AreEqual("SomeOtherWords", someOtherWords);

            var giveAPoint = InputConvertors.ScreamingSnakeCaseToCamelCase("GIVE_A_POINT");
            Assert.AreEqual("GiveAPoint", giveAPoint);

            var endingUnderscore = InputConvertors.ScreamingSnakeCaseToCamelCase("WHAT_THE_");
            Assert.AreEqual("WhatThe", endingUnderscore);
        }

        [TestMethod]
        public void TestSplitByColon()
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

        [TestMethod]
        public void TestSplitBySemicolon()
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

        [TestMethod]
        public void TestSplitByComma()
        {
            const string source = "<player location x>,<player location y>";
            var splitted = InputConvertors.SplitByComma(source);
            CollectionAssert.AreEqual(new[] {"<player location x>", "<player location y>"}, splitted.ToArray());
        }
    }
}