using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Astute.Communication;
using Astute.Communication.Messages;
using Astute.Engine;
using Astute.Entity;
using MahApps.Metro.Controls;
using UX.Renderers;
using static Astute.Configuration.Configuration;
using Point = Astute.Entity.Point;

namespace UX
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //
        // XXX Start XXX This violates programming style
        private List<Tuple<Point, Rectangle>> _backups = new List<Tuple<Point, Rectangle>>();
        //
        private List<Tuple<Point, Direction>> _bullets = new List<Tuple<Point, Direction>>();
        // XXX End XXX

        public MainWindow()
        {
            InitializeComponent();

            SubscriptionManager.StateAndMessageStream.Subscribe(
                stateAndMessage => RenderWorld(stateAndMessage.Item1, stateAndMessage.Item2)
            );
        }

        // XXX Start XXX This violates programming style
        private void DrawBullets(World world)
        {
            // Bullets fired this second
            var newBullets = world.Tanks
                .Where(tank => tank.IsFiring)
                .Select(tank => new Tuple<Point, Direction>(tank.Location, tank.Direction))
                .ToList();

            // Constructing new lists
            var bulletsCopy = new List<Tuple<Point, Direction>>();
            var backupsCopy = new List<Tuple<Point, Rectangle>>();

            // Propagate old bullets
            _bullets.ForEach(tuple =>
                {
                    Point nextCell; // Where the bullet is going to propagate
                    switch (tuple.Item2) // Direction
                    {
                        case Direction.North:
                            nextCell = new Point(tuple.Item1.X, tuple.Item1.Y - 1);
                            break;
                        case Direction.East:
                            nextCell = new Point(tuple.Item1.X + 1, tuple.Item1.Y);
                            break;
                        case Direction.South:
                            nextCell = new Point(tuple.Item1.X, tuple.Item1.Y + 1);
                            break;
                        case Direction.West:
                            nextCell = new Point(tuple.Item1.X - 1, tuple.Item1.Y);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    // Cannot draw here
                    if (nextCell.X < 0 ||
                        nextCell.Y < 0 ||
                        nextCell.X >= Config.GridSize ||
                        nextCell.Y >= Config.GridSize)
                        return;
                    if (world.GridItems[nextCell.X, nextCell.Y] is BrickWall ||
                        world.GridItems[nextCell.X, nextCell.Y] is StoneWall)
                        return;

                    // If there was something in this cell before being occupied by the bullet
                    var backup = _backups.Where(tuple1 => tuple1.Item1 == tuple.Item1).ToList();
                    if (backup.Any())
                    {
                        // Re-place it there 
                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[tuple.Item1.X])
                            .Children.RemoveAt(tuple.Item1.Y);
                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[tuple.Item1.X])
                            .Children.Insert(tuple.Item1.Y, backup.First().Item2);
                    }

                    var rect = // If there is currently something in the rectangle to be replaced
                        (Rectangle)
                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X]).Children[nextCell.Y];
                    if (rect != null) // Backup it
                        backupsCopy.Add(new Tuple<Point, Rectangle>(nextCell, rect));

                    // Place the bullet
                    var bullet = SlotRenderer.RenderBullet(tuple.Item2,
                        (int) Math.Floor(MainGrid.ActualHeight / Config.GridSize));
                    ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                        .Children.RemoveAt(nextCell.Y);
                    ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                        .Children.Insert(nextCell.Y, bullet);

                    bulletsCopy.Add(new Tuple<Point, Direction>(nextCell, tuple.Item2));
                }
            );

            // Draw new bullets
            newBullets.ForEach(tuple =>
                {
                    Point nextCell; // Where the bullet is going to propagate
                    switch (tuple.Item2) // Direction
                    {
                        case Direction.North:
                            nextCell = new Point(tuple.Item1.X, tuple.Item1.Y - 1);
                            break;
                        case Direction.East:
                            nextCell = new Point(tuple.Item1.X + 1, tuple.Item1.Y);
                            break;
                        case Direction.South:
                            nextCell = new Point(tuple.Item1.X, tuple.Item1.Y + 1);
                            break;
                        case Direction.West:
                            nextCell = new Point(tuple.Item1.X - 1, tuple.Item1.Y);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    // Cannot draw here
                    if (nextCell.X < 0 ||
                        nextCell.Y < 0 ||
                        nextCell.X >= Config.GridSize ||
                        nextCell.Y >= Config.GridSize)
                        return;
                    if (world.GridItems[nextCell.X, nextCell.Y] is BrickWall ||
                        world.GridItems[nextCell.X, nextCell.Y] is StoneWall)
                        return;

                    var rect = // If there is currently something in the rectangle to be replaced
                        (Rectangle)
                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                        .Children[nextCell.Y];
                    if (rect != null) // Backup it
                        backupsCopy.Add(new Tuple<Point, Rectangle>(tuple.Item1, rect));

                    // Place the bullet
                    var bullet = SlotRenderer.RenderBullet(tuple.Item2,
                        (int) Math.Floor(MainGrid.ActualHeight / Config.GridSize));
                    ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                        .Children.RemoveAt(nextCell.Y);
                    ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                        .Children.Insert(nextCell.Y, bullet);

                    bulletsCopy.Add(new Tuple<Point, Direction>(nextCell, tuple.Item2));
                }
            );

            _bullets = bulletsCopy;
            _backups = backupsCopy;

            Observable.Timer(TimeSpan.FromSeconds(1.0 / 3)).Merge(Observable.Timer(TimeSpan.FromSeconds(2.0 / 3)))
                .Subscribe(l => Dispatcher.Invoke(() =>
                        {
                            // Constructing new lists
                            var bulletsCopy2 = new List<Tuple<Point, Direction>>();
                            var backupsCopy2 = new List<Tuple<Point, Rectangle>>();

                            // Propagate old bullets
                            _bullets.ForEach(tuple =>
                                {
                                    Point nextCell; // Where the bullet is going to propagate
                                    switch (tuple.Item2) // Direction
                                    {
                                        case Direction.North:
                                            nextCell = new Point(tuple.Item1.X, tuple.Item1.Y - 1);
                                            break;
                                        case Direction.East:
                                            nextCell = new Point(tuple.Item1.X + 1, tuple.Item1.Y);
                                            break;
                                        case Direction.South:
                                            nextCell = new Point(tuple.Item1.X, tuple.Item1.Y + 1);
                                            break;
                                        case Direction.West:
                                            nextCell = new Point(tuple.Item1.X - 1, tuple.Item1.Y);
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }

                                    // Cannot draw here
                                    if (nextCell.X < 0 ||
                                        nextCell.Y < 0 ||
                                        nextCell.X >= Config.GridSize ||
                                        nextCell.Y >= Config.GridSize)
                                        return;
                                    if (world.GridItems[nextCell.X, nextCell.Y] is BrickWall ||
                                        world.GridItems[nextCell.X, nextCell.Y] is StoneWall)
                                        return;

                                    // If there was something in this cell before being occupied by the bullet
                                    var backup = _backups.Where(tuple1 => tuple1.Item1 == tuple.Item1).ToList();
                                    if (backup.Any())
                                    {
                                        // Re-place it there 
                                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[tuple.Item1.X])
                                            .Children.RemoveAt(tuple.Item1.Y);
                                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[tuple.Item1.X])
                                            .Children.Insert(tuple.Item1.Y, backup.First().Item2);
                                    }

                                    var rect = // If there is currently something in the rectangle to be replaced
                                        (Rectangle)
                                        ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X]).Children
                                        [
                                            nextCell.Y];
                                    if (rect != null) // Backup it
                                        backupsCopy2.Add(new Tuple<Point, Rectangle>(nextCell, rect));

                                    // Place the bullet
                                    var bullet = SlotRenderer.RenderBullet(tuple.Item2,
                                        (int) Math.Floor(MainGrid.ActualHeight / Config.GridSize));
                                    ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                                        .Children.RemoveAt(nextCell.Y);
                                    ((StackPanel) ((StackPanel) MainGrid.Children[0]).Children[nextCell.X])
                                        .Children.Insert(nextCell.Y, bullet);

                                    bulletsCopy2.Add(new Tuple<Point, Direction>(nextCell, tuple.Item2));
                                }
                            );

                            _bullets = bulletsCopy2;
                            _backups = backupsCopy2;
                        }
                    )
                );
        }

        // XXX End XXX

        private void RenderWorld(World world, IMessage message)
        {
            if (Dispatcher.CheckAccess()) // Accessing from UI thread. 
            {
                var grid = GridRenderer.RenderGrid(world.GridItems, (int) Math.Floor(MainGrid.ActualHeight));

                MainGrid.Children.Clear();
                MainGrid.Children.Add(grid);

                StatsStackPanel.Children.Clear();
                world.Tanks.Select(LabelRenderer.RenderTankProfile).ForEach(item => StatsStackPanel.Children.Add(item));

                // XXX Start XXX This violates programming style
                DrawBullets(world);
                // XXX End XXX
            }
            else
            {
                Dispatcher.Invoke(() => RenderWorld(world, message));
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Output.TcpOutput.OnNext(OutputConvertors.CommandToString(Command.Join));
            Task.Run(() => SubscriptionManager.StateAndMessageStream.Connect());
        }
    }
}