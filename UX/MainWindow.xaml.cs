using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Engine Engine = new Engine();

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
                                    .Do(Engine.ReceiveMessage)
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
            Logger.Info("Update.");
        }
    }
}