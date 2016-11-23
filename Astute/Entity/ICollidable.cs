namespace Astute.Entity
{
    public interface ICollidable
    {
        void Collide(Direction direction, Tank tank);
    }
}