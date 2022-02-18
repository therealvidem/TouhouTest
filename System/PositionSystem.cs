using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;

using TouhouTest.Component;

namespace TouhouTest.System
{
    public class PositionSystem : AEntitySetSystem<float>
    {
        public PositionSystem(World world, IParallelRunner runner) 
            : base(world.GetEntities().With<Position>().WithEither<Sprite>().Or<Hitbox>().AsSet(), runner)
        {
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            Position pos = entity.Get<Position>();

            if (entity.Has<Sprite>())
            {
                ref Sprite sprite = ref entity.Get<Sprite>();
                sprite.Position = pos.Value;
            }

            if (entity.Has<Hitbox>())
            {
                ref Hitbox collision = ref entity.Get<Hitbox>();
                collision.Value.Position = pos.Value;
            }
        }
    }
}
