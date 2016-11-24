namespace Astute.Entity
{
    public interface ICollidable
    {
        void Collide(Direction direction, Tank tank);
        // TODO Combine with IGridItem. 
        // TODO Add new ICollider interface and/or combine it with IMovableGridItem interface. 
    }
}