using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

using TouhouTest.Component;
using TouhouTest.System;

namespace TouhouTest.Level
{
    public static class Level1
    {
        private static void CreateBullet(World world, Texture2D texture, Rectangle source, Color color, Vector2 initialPos, Vector2 initialVel)
        {
            Entity bullet = world.CreateEntity();
            bullet.Set(new Position
            {
                Value = initialPos,
            });
            bullet.Set(new Velocity
            {
                Value = initialVel,
            });
            bullet.Set(new Sprite
            {
                Texture = texture,
                Position = initialPos,
                Source = source,
                Color = color,
                Rotation = 0f,
                Origin = Vector2.Zero,
                Scale = Vector2.One,
                Effects = SpriteEffects.None,
                LayerDepth = 0,
            });
            bullet.Set<Deadly>(default);
        }

        public static Entity CreatePlayer(World world, Texture2D texture, Vector2 initialPos)
        {
            Entity player = world.CreateEntity();
            player.Set(new Position
            {
                Value = initialPos,
            });
            player.Set(new Velocity
            {
                Value = Vector2.Zero,
            });
            player.Set(new Sprite
            {
                Texture = texture,
                Position = initialPos,
                Source = PlayerSystem.PLAYER_SPRITE_DIRECTIONS[PlayerSpriteDirection.UP],
                Color = Color.White,
                Rotation = 0f,
                Origin = Vector2.Zero,
                Scale = Vector2.One,
                Effects = SpriteEffects.None,
                LayerDepth = 0,
            });
            player.Set<PlayerInput>(default);
            player.Set(new Hitbox
            {
                Value = new Structure.Circle(initialPos, 2f)
            });
            return player;
        }

        // Loads level 1 into `world`
        public static void Load(World world, Texture2D bulletTexture)
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Vector2 initialPos = new Vector2(rand.Next(50), 0f);
                Vector2 initialVel = new Vector2(0f, 250f);
                CreateBullet(
                    world,
                    bulletTexture, new Rectangle(10, 64, 16, 16),
                    new Color(rand.Next(255), rand.Next(255), rand.Next(255)),
                    initialPos,
                    initialVel
                );
            }
        }
    }
}
