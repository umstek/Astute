using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Astute.Communication;
using Astute.Communication.Messages;
using Astute.Engine;
using Astute.Entity;
using MahApps.Metro.Controls;
using UX.Renderers;

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
                stateAndMessage => RenderWorld(stateAndMessage.Item1, stateAndMessage.Item2)
            );
        }

        private void RenderWorld(World world, IMessage message)
        {
            if (Dispatcher.CheckAccess()) // Accessing from UI thread. 
            {
                var grid = GridRenderer.RenderGrid(world.GridItems, (int)Math.Floor(MainGrid.ActualHeight));

                MainGrid.Children.Clear();
                MainGrid.Children.Add(grid);

                StatsStackPanel.Children.Clear();
                world.Tanks.Select(LabelRenderer.RenderTankProfile).ForEach(item => StatsStackPanel.Children.Add(item));
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