using System;
using System.Linq;
using Astute.Communication.Exceptions;
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

                switch (colonSplitted[0])
                {
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

                    case "S": // Accepted join request reply
                        // S:P<num>:<player location x>,<player location y>:<direction>#
                        // BUG Actual format returned is: S:P0;0,0;0:P1;0,19;0:P2;19,0;0#

                        var tanksDetails =
                            colonSplitted.Skip(1)
                                .Select(InputConvertors.SplitBySemicolon)
                                .Select(details =>
                                {
                                    var detailsArray = details.ToArray();

                                    var playerNumberS = int.Parse(detailsArray[0].Substring(1));
                                    var locationS =
                                        InputConvertors.SplitByComma(detailsArray[1]).Select(int.Parse).ToArray();
                                    var directionS = int.Parse(detailsArray[2]);

                                    return new JoinMessage.TankDetails(
                                        playerNumberS,
                                        new Point(locationS[0], locationS[1]),
                                        (Direction) directionS
                                    );
                                });

                        return new JoinMessage(tanksDetails);

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
                                            points
                                        );
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
                            // BUG What server sends is two coin values:
                            // the value at the beginning and the value at the end.
                            // The time that when the lifepack disappears should be a constant. 
                            int.Parse(colonSplitted[2]),
                            int.Parse(colonSplitted[3]));
                    default: // Unknown message
                        throw new UnknownMessageException(message);
                    // return null;
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
            // TODO Add PITFALL# plus all death messages. 
            throw new UnknownMessageException(message);
            // return null;
        }
    }
}