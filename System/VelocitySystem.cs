using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;

using TouhouTest.Component;

namespace TouhouTest.System
{
    public class VelocitySystem : AEntitySetSystem<float>
    {
        public VelocitySystem(World world, IParallelRunner runner)
            : base(world.GetEntities().With<Velocity>().With<Position>().AsSet(), runner)
        {
        }

        public static Vector2 CalculatePosition(float elapsedTime, Vector2 pos, Vector2 vel)
        {
            return pos + (vel * elapsedTime);
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            Position pos = entity.Get<Position>();
            Velocity vel = entity.Get<Velocity>();
            entity.Set(new Position
            {
                Value = CalculatePosition(elapsedTime, pos.Value, vel.Value)
            });
        }
    }
}
