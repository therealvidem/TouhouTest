using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework.Graphics;

using TouhouTest.Component;

namespace TouhouTest.System
{
    public class RenderSystem : AComponentSystem<float, Sprite>
    {
        private readonly SpriteBatch _batch;

        public RenderSystem(SpriteBatch spriteBatch, World world)
            : base(world)
        {
            _batch = spriteBatch;
        }

        protected override void PreUpdate(float state)
        {
            _batch.Begin();
        }

        protected override void Update(float state, ref Sprite component)
        {
            _batch.Draw(
                component.Texture,
                component.Position,
                component.Source,
                component.Color,
                component.Rotation,
                component.Origin,
                component.Scale,
                component.Effects,
                component.LayerDepth
            );
        }

        protected override void PostUpdate(float state)
        {
            _batch.End();
        }
    }
}
