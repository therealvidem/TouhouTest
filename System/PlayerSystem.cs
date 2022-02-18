using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using TouhouTest.Component;

namespace TouhouTest.System
{
    public enum PlayerSpriteDirection
    {
        NONE,
        UP,
        LEFT,
        DOWN,
        RIGHT,
    }

    public class PlayerSystem : AEntitySetSystem<float>
    {
        private static readonly float SPEED = 300f;
        
        public static readonly Dictionary<PlayerSpriteDirection, Rectangle> PLAYER_SPRITE_DIRECTIONS = new Dictionary<PlayerSpriteDirection, Rectangle>()
        {
            [PlayerSpriteDirection.NONE] = new Rectangle(17, 17, 31, 47),
            [PlayerSpriteDirection.UP] = new Rectangle(145, 17, 31, 47),
            [PlayerSpriteDirection.LEFT] = new Rectangle(243, 64, 31, 47),
            [PlayerSpriteDirection.DOWN] = new Rectangle(17, 112, 31, 47),
            [PlayerSpriteDirection.RIGHT] = new Rectangle(243, 112, 31, 47),
        };

        private readonly GameWindow _window;

        private KeyboardState _state;
        private Entity _hitbox;

        public PlayerSystem(World world, GameWindow window)
            : base(world.GetEntities().With<PlayerInput>().With<Position>().With<Velocity>().AsSet())
        {
            _window = window;
            _hitbox = world.CreateEntity();
            _hitbox.Set<Hitbox>(default);
            _hitbox.Set<Killable>(default);
            _hitbox.Disable();
        }

        protected override void PreUpdate(float elapsedTime)
        {
            _state = Keyboard.GetState();
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            Vector2 newVel = Vector2.Zero;
            PlayerSpriteDirection newSpriteDirection = PlayerSpriteDirection.NONE;

            // Add Speed component ?
            if (_state.IsKeyDown(Keys.W) || _state.IsKeyDown(Keys.Up))
            {
                newVel += new Vector2(0f, -SPEED);
                newSpriteDirection = PlayerSpriteDirection.UP;
            }
            else if (_state.IsKeyDown(Keys.S) || _state.IsKeyDown(Keys.Down))
            {
                newVel += new Vector2(0f, SPEED);
                newSpriteDirection = PlayerSpriteDirection.DOWN;
            }

            if (_state.IsKeyDown(Keys.A) || _state.IsKeyDown(Keys.Left))
            {
                newVel += new Vector2(-SPEED, 0f);
                newSpriteDirection = PlayerSpriteDirection.LEFT;
            }
            else if (_state.IsKeyDown(Keys.D) || _state.IsKeyDown(Keys.Right))
            {
                newVel += new Vector2(SPEED, 0f);
                newSpriteDirection = PlayerSpriteDirection.RIGHT;
            }

            if (_state.IsKeyDown(Keys.LeftShift))
            {
                newVel *= 0.5f;
                _hitbox.Enable<Sprite>();
            }
            else
            {

            }

            entity.Set(new Velocity
            {
                Value = newVel
            });

            if (entity.Has<Sprite>())
            {
                ref Sprite sprite = ref entity.Get<Sprite>();
                sprite.Source = PLAYER_SPRITE_DIRECTIONS[newSpriteDirection];
            }
        }
    }
}
