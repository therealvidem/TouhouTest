using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using System;

using TouhouTest.Component;
using TouhouTest.Structure;

namespace TouhouTest.System
{
    public class CollisionSystem : AEntitySetSystem<float>
    {
        public EntitySet _bullets;

        public CollisionSystem(World world, IParallelRunner runner)
            : base(world.GetEntities().With<Hitbox>().AsSet(), runner)
        {
            _bullets = world.GetEntities().With<Deadly>().With<Hitbox>().AsSet();
        }

        public static bool IsTouching(Circle c1, Circle c2)
        {
            return Math.Pow(c2.Position.X - c1.Position.X, 2f) + Math.Pow(c2.Position.Y - c1.Position.Y, 2f) <= Math.Pow(c2.Radius + c1.Radius, 2f);
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            
        }
    }
}
