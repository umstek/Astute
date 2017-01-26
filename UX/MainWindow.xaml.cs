using System;
using Astute.Communication.Messages;
using Astute.Engine;
using Astute.Entity;
using MahApps.Metro.Controls;

namespace UX
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            SubscriptionManager.StateAndMessageStream.Subscribe(
                stateAndMessage => RenderGrid(stateAndMessage.Item1, stateAndMessage.Item2)
            );
        }

        private void RenderGrid(World world, IMessage message)
        {
            if (Dispatcher.CheckAccess()) // Accessing from UI thread. 
                foreach (var slot in world.GridItems)
                    switch (slot.GetType().Name)
                    {
                        case nameof(Tank):
                            break;
                        case nameof(BrickWall):
                            break;
                        case nameof(StoneWall):
                            break;
                        case nameof(Coinpack):
                            break;
                        case nameof(Lifepack):
                            break;
                        case nameof(Water):
                            break;
                        default:
                            break;
                    }
            else
                Dispatcher.Invoke(() => RenderGrid(world, message));
        }

        //{

        //private void UpdateGridUI()
        //    if (Dispatcher.CheckAccess())
        //        for (var i = 0; i < 20; i++)
        //        for (var j = 0; j < 20; j++)
        //        {
        //            var gi = _engine.State.GridItems[i, j];
        //            if (gi is Tank)
        //            {
        //                GradientBrush gradientBrush;
        //                switch ((gi as Tank).Direction)
        //                {
        //                    case Direction.North:
        //                        gradientBrush = new LinearGradientBrush(Colors.DarkGreen, Colors.WhiteSmoke, 90);
        //                        break;
        //                    case Direction.East:
        //                        gradientBrush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.DarkGreen, 0);
        //                        break;
        //                    case Direction.South:
        //                        gradientBrush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.DarkGreen, 90);
        //                        break;
        //                    case Direction.West:
        //                        gradientBrush = new LinearGradientBrush(Colors.DarkGreen, Colors.WhiteSmoke, 0);
        //                        break;
        //                    default:
        //                        throw new ArgumentOutOfRangeException();
        //                }
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = gradientBrush;
        //            }
        //            else if (gi is BrickWall)
        //            {
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = Brushes.Brown;
        //            }
        //            else if (gi is StoneWall)
        //            {
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = Brushes.Gray;
        //            }
        //            else if (gi is Coinpack)
        //            {
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = Brushes.Gold;
        //            }
        //            else if (gi is Lifepack)
        //            {
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = Brushes.Red;
        //            }
        //            else if (gi is Water)
        //            {
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = Brushes.Blue;
        //            }
        //            else
        //            {
        //                ((Rectangle) ((StackPanel) MainStack.Children[i]).Children[j]).Fill = Brushes.White;
        //            }
        //        }
        //    else
        //        Dispatcher.Invoke(UpdateGridUI);
        //}
    }
}