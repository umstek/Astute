using System;
using System.Diagnostics;
using System.Linq;
using Astute.Entity;
using NLog;

namespace Astute.Communication.Messages
{
    public static class MessageFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IMessage GetMessage(string message)
        {
            Logger.Info(message);

            if (message.Contains(":")) // Long message with information
            {
                var colonSplitted = InputConvertors.SplitByColon(InputConvertors.TrimHash(message)).ToArray();
                Debug.WriteLine(message);

                switch (colonSplitted[0])
                {
                    case "S": // Accepted join request reply
                        // S:P<num>:<player location x>,<player location y>:<direction>#
                        // BUG Actual format returned is:
                        // BUG S:P0;0,0;0#
                        var semicolonSplitted = InputConvertors.SplitBySemicolon(colonSplitted[1]).ToArray();
                        // var playerNumberS = int.Parse(colonSplitted[1].Substring(1));
                        var playerNumberS = int.Parse(semicolonSplitted[0].Substring(1));
                        // var locationS = SplitByComma(colonSplitted[2]).Select(int.Parse).ToArray();
                        var locationS = InputConvertors.SplitByComma(semicolonSplitted[1]).Select(int.Parse).ToArray();
                        // var directionS = int.Parse(colonSplitted[3]);
                        var directionS = int.Parse(semicolonSplitted[2]);
                        return new JoinMessage(
                            playerNumberS,
                            new Point(locationS[0], locationS[1]),
                            (Direction) directionS);

                    case "I": // Initialized game message
                        // I:P<num>:<x>,<y>;<x>,<y>;<x>,<y>;...;<x>,<y>:<x>,<y>;<x>,<y>;<x>,<y>;...;<x>,<y>:<x>,<y>;<x>,<y>;<x>,<y>;...;<x>,<y>#
                        var playerNumberI = int.Parse(colonSplitted[1].Substring(1));
                        var bricks =
                            InputConvertors.SplitBySemicolon(colonSplitted[2])
                                .Select(pointStr => InputConvertors.SplitByComma(pointStr).Select(int.Parse).ToArray())
                                .ToArray();
                        var stones =
                            InputConvertors.SplitBySemicolon(colonSplitted[3])
                                .Select(pointStr => InputConvertors.SplitByComma(pointStr).Select(int.Parse).ToArray())
                                .ToArray();
                        var waters =
                            InputConvertors.SplitBySemicolon(colonSplitted[4])
                                .Select(pointStr => InputConvertors.SplitByComma(pointStr).Select(int.Parse).ToArray())
                                .ToArray();
                        return new InitiationMessage(
                            playerNumberI,
                            bricks.Select(b => new Point(b[0], b[1])).ToList(),
                            stones.Select(s => new Point(s[0], s[1])).ToList(),
                            waters.Select(w => new Point(w[0], w[1])).ToList());

                    case "G": // Global status update message
                        // G:P1;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>:...:P5;<player location x>,<player location y>;<direction>;<whether shot>;<health>;<coins>;<points>:<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;<x>,<y>,<damage-level>;..;<x>,<y>,<damage-level>#
                        var playersDetails =
                            colonSplitted.Skip(1)
                                .Take(colonSplitted.Length - 2)
                                .Select(InputConvertors.SplitBySemicolon)
                                .Select(
                                    details =>
                                    {
                                        var detailsArray = details.ToArray();

                                        var playerNumber = int.Parse(detailsArray[0].Substring(1));
                                        var location =
                                            InputConvertors.SplitByComma(detailsArray[1]).Select(int.Parse).ToArray();
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

                        var damageDetails = InputConvertors.SplitBySemicolon(colonSplitted.Last()).Select(details =>
                        {
                            var detailsArray = InputConvertors.SplitByComma(details).Select(int.Parse).ToArray();

                            return new BroadcastMessage.DamageDetails(
                                new Point(detailsArray[0], detailsArray[1]),
                                detailsArray[2]);
                        }).ToList();

                        return new BroadcastMessage(playersDetails, damageDetails);

                    case "L": // Lifepack appearance message
                        // L:<x>,<y>:<LT>#
                        var locationL = InputConvertors.SplitByComma(colonSplitted[1]).Select(int.Parse).ToArray();

                        return new LifepackMessage(
                            new Point(locationL[0], locationL[1]),
                            int.Parse(colonSplitted[2]));
                    case "C": // Coinpack appearance message
                        // C:<x>,<y>:<LT>:<Val>#
                        var locationC = InputConvertors.SplitByComma(colonSplitted[1]).Select(int.Parse).ToArray();

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
            // BUG For obstacle, the actual format is OBSTACLE;25#
            // var camelCaseCode = ScreamingSnakeCaseToCamelCase(TrimHash(message));
            var camelCaseCode =
                InputConvertors.ScreamingSnakeCaseToCamelCase(
                    InputConvertors.SplitBySemicolon(InputConvertors.TrimHash(message)).First());
            if (Enum.GetNames(typeof(CommandFailState)).Contains(camelCaseCode))
                return new CommandFailMessage((CommandFailState) Enum.Parse(typeof(CommandFailState), camelCaseCode));
            if (Enum.GetNames(typeof(JoinFailState)).Contains(camelCaseCode))
                return new JoinFailMessage((JoinFailState) Enum.Parse(typeof(JoinFailState), camelCaseCode));

            Debug.Fail("Unknown message. ");
            return null;
        }
    }
}