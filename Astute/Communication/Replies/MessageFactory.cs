using System;
using System.Diagnostics;
using System.Linq;
using Astute.Entity;
using static Astute.Communication.InputConvertors;

namespace Astute.Communication.Replies
{
    public static class MessageFactory
    {
        public static IMessage GetMessage(string message)
        {
            if (message.Contains(":")) // Long message with information
            {
                var colonSplitted = SplitByColon(TrimHash(message)).ToArray();

                switch (colonSplitted[0])
                {
                    case "S": // Accepted join request reply
                        // S:P<num>:<player location x>,<player location y>:<direction>#
                        var playerNumberS = int.Parse(colonSplitted[1].Substring(1));
                        var locationS = SplitByComma(colonSplitted[2]).Select(int.Parse).ToArray();
                        var directionS = int.Parse(colonSplitted[3]);
                        return new JoinMessage(
                            playerNumberS,
                            new Point(locationS[0], locationS[1]),
                            (Direction) directionS);

                    case "I": // Initialized game message
                        // I:P<num>:<x>,<y>;<x>,<y>;<x>,<y>;...;<x>,<y>:<x>,<y>;<x>,<y>;<x>,<y>;...;<x>,<y>:<x>,<y>;<x>,<y>;<x>,<y>;...;<x>,<y>#
                        var playerNumberI = int.Parse(colonSplitted[1].Substring(1));
                        var bricks =
                            SplitBySemicolon(colonSplitted[2])
                                .Select(pointStr => SplitByComma(pointStr).Select(int.Parse).ToArray())
                                .ToArray();
                        var stones =
                            SplitBySemicolon(colonSplitted[3])
                                .Select(pointStr => SplitByComma(pointStr).Select(int.Parse).ToArray())
                                .ToArray();
                        var waters =
                            SplitBySemicolon(colonSplitted[4])
                                .Select(pointStr => SplitByComma(pointStr).Select(int.Parse).ToArray())
                                .ToArray();
                        return new InitiationMessage(
                            playerNumberI,
                            bricks.Select(b => new Point(b[0], b[1])).ToList(),
                            stones.Select(s => new Point(s[0], s[1])).ToList(),
                            waters.Select(w => new Point(w[0], w[1])).ToList());

                    case "G": // Global status update message
                        // G:P1;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>:...:P5;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>:<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;..;<x>,<y>,<damage-level>#
                        var playersDetails =
                            colonSplitted.Skip(1).Take(colonSplitted.Length - 1).Select(SplitBySemicolon).Select(
                                details =>
                                {
                                    var detailsArray = details.ToArray();

                                    var playerNumber = int.Parse(detailsArray[0].Substring(1));
                                    var location = SplitByComma(detailsArray[1]).Select(int.Parse).ToArray();
                                    var direction = int.Parse(detailsArray[2]);
                                    var isShot = detailsArray[3] == "1";
                                    var health = int.Parse(detailsArray[4]);
                                    var coins = int.Parse(detailsArray[5]);
                                    var points = int.Parse(detailsArray[6]);

                                    return new BroadcastMessage.PlayerDetails(
                                        playerNumber,
                                        new Point(location[0], location[1]),
                                        (Direction) direction,
                                        isShot,
                                        health,
                                        coins,
                                        points);
                                }).ToList();

                        var damageDetails = SplitBySemicolon(colonSplitted.Last()).Select(details =>
                        {
                            var detailsArray = SplitByComma(details).Select(int.Parse).ToArray();

                            return new BroadcastMessage.DamageDetails(
                                new Point(detailsArray[0], detailsArray[1]),
                                detailsArray[2]);
                        }).ToList();

                        return new BroadcastMessage(playersDetails, damageDetails);

                    case "L": // Lifepack appearance message
                        // L:<x>,<y>:<LT>#
                        var locationL = SplitByComma(colonSplitted[1]).Select(int.Parse).ToArray();

                        return new LifepackMessage(
                            new Point(locationL[0], locationL[1]),
                            int.Parse(colonSplitted[2]));
                    case "C": // Coinpack appearance message
                        // C:<x>,<y>:<LT>:<Val>#
                        var locationC = SplitByComma(colonSplitted[1]).Select(int.Parse).ToArray();

                        return new CoinpackMessage(
                            new Point(locationC[0], locationC[1]),
                            int.Parse(colonSplitted[2]),
                            int.Parse(colonSplitted[3]));
                    default: // Unknown message
                        Debug.Fail("Unknown message. ");
                        return null;
                }
            }

            // Short-code message
            var camelCaseCode = ScreamingSnakeCaseToCamelCase(TrimHash(message));
            if (Enum.GetNames(typeof(CommandFailState)).Contains(camelCaseCode))
                return new CommandFailMessage((CommandFailState) Enum.Parse(typeof(CommandFailState), camelCaseCode));
            if (Enum.GetNames(typeof(JoinFailState)).Contains(camelCaseCode))
                return new JoinFailMessage((JoinFailState) Enum.Parse(typeof(JoinFailState), camelCaseCode));

            Debug.Fail("Unknown message. ");
            return null;
        }
    }
}