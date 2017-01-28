using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Astute.Entity;
using static Astute.Configuration.Configuration;

namespace UX.Renderers
{
    public static class GridRenderer
    {
        /// <summary>
        ///     Renders a complete grid.
        /// </summary>
        /// <param name="items">Grid items</param>
        /// <param name="gridSize">Size of the space allocated to the grid</param>
        /// <returns>Rendered grid</returns>
        public static StackPanel RenderGrid(IGridItem[,] items, int gridSize)
        {
            var slotCount = Config.GridSize;
            var slotSize = gridSize / slotCount;

            var grid = new StackPanel
            {
                Margin = new Thickness(0),
                Orientation = Orientation.Horizontal,
                Height = double.NaN,
                Width = double.NaN,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Enumerable.Range(0, slotCount)
                .Select(i => // Render a row
                {
                    var row = new StackPanel();

                    Enumerable.Range(0, slotCount)
                        .Select(j => SlotRenderer.RenderGridItem(items[i, j], slotSize))
                        .ForEach(rectangle => row.Children.Add(rectangle));

                    return row;
                })
                .ForEach(row => grid.Children.Add(row));


            return grid;
        }
    }
}