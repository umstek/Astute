using System.Windows.Controls;
using System.Windows.Media;
using Astute.Entity;
using static UX.Renderers.Constants;

namespace UX.Renderers
{
    public static class LabelRenderer
    {
        /// <summary>
        ///     Renders information about a tank as a stacked set of labels.
        /// </summary>
        /// <param name="tank">Tank model</param>
        /// <returns>Stacked set of labels carrying information</returns>
        public static StackPanel RenderTankProfile(Tank tank)
        {
            var color = tank.MyTank ? Colors.White : TankColors[tank.PlayerNumber % TankColors.Length];
            var brush = new SolidColorBrush(color);

            return new StackPanel
            {
                Children =
                {
                    new Label
                    {
                        Content = $"Player {tank.PlayerNumber}",
                        Foreground = brush,
                        FontSize = 24
                    },
                    new Label
                    {
                        Content = $"Health:\t{tank.Health}",
                        Foreground = brush
                    },
                    new Label
                    {
                        Content = $"Coins:\t{tank.Coins}",
                        Foreground = brush
                    },
                    new Label
                    {
                        Content = $"Points:\t{tank.Points}",
                        Foreground = brush
                    }
                }
            };
        }
    }
}