namespace Astute.Entity
{
    public struct Tank : IMovableGridItem, ICollidable, IShootable
    {
        public Tank(Point location, int health, Direction direction, int points, int coins, int playerNumber,
            bool myTank = false)
        {
            Location = location;
            Direction = direction;
            Health = health;
            Points = points;
            Coins = coins;
            PlayerNumber = playerNumber;
            MyTank = myTank;
        }

        public int PlayerNumber { get; }
        public bool MyTank { get; }
        public Point Location { get; set; }
        public Direction Direction { get; set; }
        public int Health { get; set; }
        public int Points { get; set; }
        public int Coins { get; set; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public bool Shoot()
        {
            return false;
        }
    }
}