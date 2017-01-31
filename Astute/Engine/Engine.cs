using System;
using System.Collections.Generic;
using System.Linq;
using Astute.Communication.Messages;
using Astute.Entity;

namespace Astute.Engine
{
    /// <summary>
    ///     Main Logic
    /// </summary>

    public static class Engine
    {
        /// <summary>
        ///     Calculates the command to be sent to the server for the current state of the world and the message which was
        ///     received just now.
        /// </summary>
        /// <param name="history">What states were there and which outputs were sent</param>
        /// <param name="state">The state of the world</param>
        /// <param name="message">Received message</param>
        /// <returns></returns>

        private static string dir;
        private static int k = 0;
        private static int count = 1;
        private static int count2 = -1;

        private static Command? ComputeCommand(IEnumerable<Tuple<World, Command?>> history, World state,
            IMessage message)
        {

            //  var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();

            if (!history.Any()) // First time
                return Command.Join;
            if (message is BroadcastMessage)
            {
                count++;
                var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();
                //ourTank.Direction==Direction.North

                System.Diagnostics.Debug.WriteLine(ourTank.Location.X + " ; " + ourTank.Location.Y);
                System.Diagnostics.Debug.WriteLine("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");


                // return (Command)new Random().Next(0, 4);

                if (k != 0)
                {
                    if (ourTank.Direction == Direction.West)
                    {
                        k--;
                        return (Command)3;
                    }

                    else if (ourTank.Direction == Direction.North)
                    {
                        k--;
                        return (Command)0;
                    }
                    else if (ourTank.Direction == Direction.East)
                    {
                        k--;
                        return (Command)2;
                    }
                    if (ourTank.Direction == Direction.South)
                    {
                        k--;
                        return (Command)1;
                    }


                }
                else {
                    if (count % 20 == 0 || count2 >= 0)
                    {
                        
                        System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA   ");
                        System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA   ");
                        if (count % 20 == 0) {
                            count2 = 4;
                        }

                        count++;
                        System.Diagnostics.Debug.WriteLine(count.ToString());
                        System.Diagnostics.Debug.WriteLine(count2.ToString());
                        count2--;
                        System.Diagnostics.Debug.WriteLine(count2.ToString());
                        return (Command)avoidWaterStone1(state);
                    }
                    else {
                        System.Diagnostics.Debug.WriteLine("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBb   ");
                        System.Diagnostics.Debug.WriteLine(count.ToString());
                        count++;
                        count2 = -1;
                        return (Command)avoidWaterStone2(state);
                    }
                }
            }

            return null;

        }
     
        private static void takeCoinsLifePacks(World state) {

            var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();

          //  if (ourTank.Location.X== ) { }

        }

        private static void whatISee(World state) /// LEFT RIGHT UP DOWN BRICK OR TANK   "MUST IMPLEMENT"
        { }


        private static int a(World state, int b)
        {
            var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();

          

                if ((ourTank.Location.X- 1) >= 0 &&   ( (state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is Coinpack) || (state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is Lifepack)))
                {
                    if (ourTank.Direction == Direction.West)
                    {
                        return 3;
                    }
                    else {
                        k++;
                        return 3;
                    }
                }


                else if ((ourTank.Location.X + 1) < 10 && ( (state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is Coinpack) || (state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is Lifepack)))
                {
                    if (ourTank.Direction == Direction.East)
                    {
                        return 2;
                    }
                    else {
                        k++;
                        return 2;
                    }
                }
                else if ((ourTank.Location.Y + 1) < 10 && ( (state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is Coinpack) || (state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is Lifepack)))
                {
                    if (ourTank.Direction == Direction.South)
                    {
                        return 1;
                    }
                    else {
                        k++;
                        return 1;
                    }
                }

                else if ((ourTank.Location.Y -1) >= 0 && ((state.GridItems[ourTank.Location.X, ourTank.Location.Y - 1] is Coinpack) || (state.GridItems[ourTank.Location.X, ourTank.Location.Y - 1] is Lifepack)))
                {
                    if (ourTank.Direction == Direction.North)
                    {
                        return 0;
                    }
                    else {
                        k++;
                        return 0;
                    }
                }


                else {
                    return b;
                }
            

        }




        private static int canGo(World state)
        {
            var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();
            //if (ourTank.Direction.Equals(dir)) {            }
            if (ourTank.Direction == Direction.West) {
                if ((ourTank.Location.X - 1) >= 0 && (!(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is Water) && !(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is StoneWall)))
                {

                }

                }


            return 2;
        }

        private static int avoidWaterStone1(World state)
        {
            //int[] n1 = new int[5] { 0, 1, 2, 3 ,4};
            var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();
            try
            {
                // Configuration.Configuration.Config.GridSize.
                 if ((ourTank.Location.X - 1) >= 0 && (!(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is BrickWall) &&   !(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is Water) && !(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("MMMMMMMMMMMMMMMMMMMMM   ");
                    return a(state, 3);
                }

                else if ((ourTank.Location.Y + 1) < 10 && (!(state.GridItems[ourTank.Location.X, ourTank.Location.Y+1] is BrickWall) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is Water) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("KKKKKKKKKKKKKKKKK   " + (ourTank.Location.X + 1)+ (state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is Water));
                    System.Diagnostics.Debug.WriteLine(123.ToString());
                    int y = a(state, 1);
                    System.Diagnostics.Debug.WriteLine(y.ToString());
                    System.Diagnostics.Debug.WriteLine(333.ToString());
                    return y;
                }

                else if ((ourTank.Location.X + 1) < 10 && (!(state.GridItems[ourTank.Location.X +1, ourTank.Location.Y] is BrickWall) && !(state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is Water) && !(state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("YYYYYYYYYYYYYYYYYYYYY   ");
                    
                    int y=  a(state,2);
                    System.Diagnostics.Debug.WriteLine(y);
                    return y;
                }
                else if ((ourTank.Location.Y - 1) >= 0 && (!(state.GridItems[ourTank.Location.X , ourTank.Location.Y-1] is BrickWall) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y - 1] is Water) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y - 1] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("RRRRRRRRRRRRRRRRRRR   ");
                    return a(state,0);
                }



            }
            catch (Exception e) { }


            //    if (state.GridItems[4, 6] is Water == true)
            //  {

            //}
            return 4 ;
        }


        private static int avoidWaterStone2(World state)
        {
            //int[] n1 = new int[5] { 0, 1, 2, 3 ,4};
            var ourTank = state.Tanks.Where(tank => tank.PlayerNumber == state.PlayerNumber).First();
            try
            {
                 if ((ourTank.Location.Y - 1) >= 0 && (!(state.GridItems[ourTank.Location.X , ourTank.Location.Y-1] is BrickWall) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y - 1] is Water) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y - 1] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("RRRRRRRRRRRRRRRRRRR   ");
                    return a(state, 0);
                }

                else if ((ourTank.Location.X + 1) < 10 && (!(state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is BrickWall) && !(state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is Water) && !(state.GridItems[ourTank.Location.X + 1, ourTank.Location.Y] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("YYYYYYYYYYYYYYYYYYYYY   ");

                    int y = a(state, 2);
                    System.Diagnostics.Debug.WriteLine(y);
                    return y;
                }

                else if ((ourTank.Location.Y + 1) < 10 && (!(state.GridItems[ourTank.Location.X, ourTank.Location.Y+1] is BrickWall) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is Water) && !(state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("KKKKKKKKKKKKKKKKK   " + (ourTank.Location.X + 1) + (state.GridItems[ourTank.Location.X, ourTank.Location.Y + 1] is Water));
                    System.Diagnostics.Debug.WriteLine(123.ToString());
                    int y = a(state, 1);
                    System.Diagnostics.Debug.WriteLine(y.ToString());
                    System.Diagnostics.Debug.WriteLine(333.ToString());
                    return y;
                }
                // Configuration.Configuration.Config.GridSize.
                else if ((ourTank.Location.X - 1) >= 0 && (!(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is BrickWall) && !(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is Water) && !(state.GridItems[ourTank.Location.X - 1, ourTank.Location.Y] is StoneWall)))
                {
                    System.Diagnostics.Debug.WriteLine("MMMMMMMMMMMMMMMMMMMMM   ");
                    return a(state, 3);
                }

    

            }
            catch (Exception e) { }


            //    if (state.GridItems[4, 6] is Water == true)
            //  {

            //}
            return 4;
        }













        /// <summary>
        ///     Calculates what command is to be sent to the server and stores the state and the output to be used in
        ///     future calculations.
        /// </summary>
        /// <param name="history">Old statuses of the world and corresponding outputs that were sent</param>
        /// <param name="stateAndMessage">New world and message correspondng to that</param>
        /// <returns>Current state of the world and the output to be sent to the server calculated from it</returns>
        public static IEnumerable<Tuple<World, Command?>> MakeHistory(
            IEnumerable<Tuple<World, Command?>> history, Tuple<World, IMessage> stateAndMessage
        )
        {
            var enumerable = history as Tuple<World, Command?>[] ?? history.ToArray();
            var command = ComputeCommand(enumerable, stateAndMessage.Item1, stateAndMessage.Item2);
            return enumerable.Concat(new[] {new Tuple<World, Command?>(stateAndMessage.Item1, command)});
        }

        /// <summary>
        ///     Updates a world according to the data received from a message.
        /// </summary>
        /// <param name="oldWorldAndMessage">Previous world and message</param>
        /// <param name="message">Received message</param>
        /// <returns>New world and the latest message which was used in the creation of the world</returns>
        public static Tuple<World, IMessage> BuildWorld(Tuple<World, IMessage> oldWorldAndMessage, IMessage message)
        {
            return new Tuple<World, IMessage>(World.FromMessage(oldWorldAndMessage?.Item1, message), message);
        }
    }
}
