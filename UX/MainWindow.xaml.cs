using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Astute.Communication;
using Astute.Communication.Replies;
using Astute.Engine;
using Astute.Entity;
using NLog;

namespace UX
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Engine _engine = new Engine();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            Observable.FromEventPattern<KeyEventHandler, KeyEventArgs>(
                    handler => KeyDown += handler,
                    handler => KeyDown -= handler)
                .Select(pattern =>
                {
                    switch (pattern.EventArgs.Key)
                    {
                        case Key.Enter:
                            Task.Run(() =>
                                Input.TcpInput.Retry().Select(MessageFactory.GetMessage)
                                    .Do(_engine.ReceiveMessage)
                                    .Subscribe(_ => UpdateGridUI()));
                            return Command.Join;
                        case Key.A: // Fallthrough
                        case Key.Left:
                            return Command.Left;
                        case Key.D: // Fallthrough
                        case Key.Right:
                            return Command.Right;
                        case Key.W: // Fallthrough
                        case Key.Up:
                            return Command.Up;
                        case Key.S: // Fallthrough
                        case Key.Down:
                            return Command.Down;
                        default:
                            return Command.Shoot;
                    }
                })
                .Select(OutputConvertors.CommandToString)
                .Subscribe(Output.TcpOutput);
        }

        private void UpdateGridUI()
        {
            if (Dispatcher.CheckAccess())
                for (var i = 0; i < 20; i++)
                    for (var j = 0; j < 20; j++)
                    {
                        var gi = _engine.State.GridItems[i, j];
                        if (gi is Tank)
                        {
                            GradientBrush gradientBrush;
                            switch ((gi as Tank).Direction)
                            {
                                case Direction.North:
                                    gradientBrush = new LinearGradientBrush(Colors.DarkGreen, Colors.WhiteSmoke, 90);
                                    break;
                                case Direction.East:
                                    gradientBrush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.DarkGreen, 0);
                                    break;
                                case Direction.South:
                                    gradientBrush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.DarkGreen, 90);
                                    break;
                                case Direction.West:
                                    gradientBrush = new LinearGradientBrush(Colors.DarkGreen, Colors.WhiteSmoke, 0);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = gradientBrush;
                        }
                        else if (gi is BrickWall)
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = Brushes.Brown;
                        else if (gi is StoneWall)
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = Brushes.Gray;
                        else if (gi is Coinpack)
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = Brushes.Gold;
                        else if (gi is Lifepack)
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = Brushes.Red;
                        else if (gi is Water)
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = Brushes.Blue;
                        else
                            ((Rectangle)((StackPanel)MainStack.Children[i]).Children[j]).Fill = Brushes.White;
                    }
            else
                Dispatcher.Invoke(UpdateGridUI);
        }
    }
}