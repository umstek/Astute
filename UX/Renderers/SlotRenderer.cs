using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Astute.Entity;
using MahApps.Metro.IconPacks;
using static UX.Renderers.Constants;

namespace UX.Renderers
{
    public static class SlotRenderer
    {
        /// <summary>
        ///     Identify the class of grid item and render as required.
        /// </summary>
        /// <param name="gridItem">Grid item</param>
        /// <param name="squareSize">Size of the square</param>
        /// <returns>Rendered view of grid item</returns>
        public static Rectangle RenderGridItem(IGridItem gridItem, int squareSize)
        {
            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull

            if (gridItem is Tank)
                return RenderTank((Tank) gridItem, squareSize);

            if (gridItem is Coinpack)
                return RenderCoinpack((Coinpack) gridItem, squareSize);

            if (gridItem is Lifepack)
                return RenderLifepack((Lifepack) gridItem, squareSize);

            if (gridItem is BrickWall)
                return RenderBrickWall((BrickWall) gridItem, squareSize);

            if (gridItem is StoneWall)
                return RenderStoneWall((StoneWall) gridItem, squareSize);

            if (gridItem is Water)
                return RenderWater((Water) gridItem, squareSize);

            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            return new Rectangle
            {
                Width = squareSize,
                Height = squareSize,
                Stroke = new SolidColorBrush(Colors.DimGray)
            };
        }

        /// <summary>
        ///     Render a bullet.
        /// </summary>
        /// <param name="tank">The tank which fired the bullet</param>
        /// <param name="squareSize">maximum size of the square</param>
        /// <returns>Rendered view of bullet</returns>
        public static Rectangle RenderBullet(Tank tank, int squareSize)
        {
            // <iconPacks:PackIconMaterial Kind="DotsVertical" />
            var angle = (int) tank.Direction * 90;
            var color = tank.MyTank ? Colors.White : TankColors[tank.PlayerNumber % TankColors.Length];
            ;

            return new Rectangle
            {
                Width = squareSize,
                Height = squareSize,
                Fill = new VisualBrush(
                    new PackIconMaterial
                    {
                        Kind = PackIconMaterialKind.DotsVertical,
                        Foreground = new SolidColorBrush(color),
                        Rotation = angle,
                        Effect = new DropShadowEffect
                        {
                            Color = color,
                            Direction = 0,
                            Opacity = 1,
                            ShadowDepth = 0
                        }
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };
        }

        /// <summary>
        ///     Render a tank.
        /// </summary>
        /// <param name="tank">Tanks model</param>
        /// <param name="squareSize">Maximum size of the tank</param>
        /// <returns>Rendered view of tank</returns>
        private static Rectangle RenderTank(Tank tank, int squareSize)
        {
            // Use a navigation icon for other tanks.
            // For our tank, use a tank-looking icon (Serial Port icon looks like a tank when rotated 180 degrees :/) 
            var kind = tank.MyTank ? PackIconMaterialKind.SerialPort : PackIconMaterialKind.Navigation;
            var color = tank.MyTank ? Colors.White : TankColors[tank.PlayerNumber % TankColors.Length];
            var angle = (90 * (int) tank.Direction + (tank.MyTank ? 180 : 0)) % 360;
            var opacity = tank.Health / 100.0;

            var rectangle = new Rectangle
            {
                Height = squareSize,
                Width = squareSize,
                Fill = new VisualBrush(
                    new PackIconMaterial
                    {
                        Kind = kind,
                        Foreground = new SolidColorBrush(color),
                        Rotation = angle,
                        Opacity = opacity,
                        Effect = new DropShadowEffect
                        {
                            Color = color,
                            Direction = 0,
                            Opacity = 1,
                            ShadowDepth = 0
                        }
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };

            return rectangle;
        }

        /// <summary>
        ///     Render a coinpack.
        /// </summary>
        /// <param name="coinpack">Coinpack model</param>
        /// <param name="squareSize">Maximum size of the containing square</param>
        /// <returns></returns>
        private static Rectangle RenderCoinpack(Coinpack coinpack, int squareSize)
        {
            var rectangle = new Rectangle
            {
                Height = squareSize,
                Width = squareSize,
                Fill = new VisualBrush(
                    new PackIconMaterial
                    {
                        Kind = PackIconMaterialKind.Coin,
                        Foreground = new SolidColorBrush(Colors.Gold),
                        Effect = new DropShadowEffect
                        {
                            Color = Colors.Gold,
                            Direction = 0,
                            Opacity = 1,
                            ShadowDepth = 0
                        }
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };

            return rectangle;
        }

        /// <summary>
        ///     Render a lifepack.
        /// </summary>
        /// <param name="lifepack">Lifepack model</param>
        /// <param name="squareSize">Maximum size of the containing square</param>
        /// <returns></returns>
        private static Rectangle RenderLifepack(Lifepack lifepack, int squareSize)
        {
            var rectangle = new Rectangle
            {
                Height = squareSize,
                Width = squareSize,
                Fill = new VisualBrush(
                    new PackIconFontAwesome
                    {
                        Kind = PackIconFontAwesomeKind.Medkit,
                        Foreground = new SolidColorBrush(Colors.Red),
                        Effect = new DropShadowEffect
                        {
                            Color = Colors.Red,
                            Direction = 0,
                            Opacity = 1,
                            ShadowDepth = 0
                        }
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };

            return rectangle;
        }

        /// <summary>
        ///     Render a brickwall.
        /// </summary>
        /// <param name="brickWall">BrickWall model</param>
        /// <param name="squareSize">Maximum size of the containing square</param>
        /// <returns></returns>
        private static Rectangle RenderBrickWall(BrickWall brickWall, int squareSize)
        {
            var rectangle = new Rectangle
            {
                Height = squareSize,
                Width = squareSize,
                Fill = new VisualBrush(
                    new PackIconMaterial
                    {
                        Kind = PackIconMaterialKind.ViewDashboard, // ViewDashboard icon looks like a brick wall
                        Rotation = 90, // when rotated by 90 degrees
                        Foreground = new SolidColorBrush(Colors.Brown),
                        Opacity = brickWall.Health * 25 / 100.0
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };

            return rectangle;
        }

        /// <summary>
        ///     Render a stonewall.
        /// </summary>
        /// <param name="stoneWall">StoneWall model</param>
        /// <param name="squareSize">Maximum size of the containing square</param>
        /// <returns></returns>
        private static Rectangle RenderStoneWall(StoneWall stoneWall, int squareSize)
        {
            var rectangle = new Rectangle
            {
                Height = squareSize,
                Width = squareSize,
                Fill = new VisualBrush(
                    new PackIconMaterial
                    {
                        Kind = PackIconMaterialKind.FormatLineStyle, // FormatLineStyle icon looks like a stone wall
                        Rotation = 180, // when rotated by 180 degrees
                        Foreground = new SolidColorBrush(Colors.Gray)
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };

            return rectangle;
        }

        /// <summary>
        ///     Render a water.
        /// </summary>
        /// <param name="water">Water pond model</param>
        /// <param name="squareSize">Maximum size of the containing square</param>
        /// <returns></returns>
        private static Rectangle RenderWater(Water water, int squareSize)
        {
            var rectangle = new Rectangle
            {
                Height = squareSize,
                Width = squareSize,
                Fill = new VisualBrush(
                    new PackIconMaterial
                    {
                        Kind = PackIconMaterialKind.Texture, // Looks like water
                        Foreground = new SolidColorBrush(Colors.DodgerBlue)
                    }
                ),
                Stroke = new SolidColorBrush(Colors.DimGray)
            };

            return rectangle;
        }
    }
}