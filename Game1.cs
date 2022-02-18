using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using TouhouTest.System;
using TouhouTest.Level;

namespace TouhouTest
{
    public class Game1 : Game
    {
        const int DEFAULT_WINDOW_WIDTH = 960;
        const int DEFAULT_WINDOW_HEIGHT = 720;
        const int DEFAULT_TARGET_WIDTH = 1280;
        const int DEFAULT_TARGET_HEIGHT = 720;

        public RenderTarget2D renderTarget;
        public float scale = 0.44444f;

        private SpriteBatch _batch;
        private GraphicsDeviceManager _graphics;
        private World _world;
        private DefaultParallelRunner _runner;
        private ISystem<float> _system;

        Texture2D playerSprite;
        Texture2D bulletSprite;

        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _graphics.PreferredBackBufferWidth = DEFAULT_WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = DEFAULT_WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            _batch = new SpriteBatch(GraphicsDevice);

            _world = new World();
            _runner = new DefaultParallelRunner(Environment.ProcessorCount);
            _system = new SequentialSystem<float>(
                new PlayerSystem(_world, Window),
                new VelocitySystem(_world, _runner),
                new PositionSystem(_world, _runner),
                new RenderSystem(_batch, _world)
            );

            Entity player = Level1.CreatePlayer(_world, playerSprite, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 1.2f));
            Level1.Load(_world, bulletSprite);
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            renderTarget = new RenderTarget2D(GraphicsDevice, DEFAULT_TARGET_WIDTH, DEFAULT_TARGET_HEIGHT);

            playerSprite = Content.Load<Texture2D>("Sprites/character_sprites");
            bulletSprite = Content.Load<Texture2D>("Sprites/objects_and_projectiles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            scale = 1f / (DEFAULT_TARGET_HEIGHT / _graphics.GraphicsDevice.Viewport.Height);

            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _system.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            GraphicsDevice.SetRenderTarget(null);

            _batch.Begin();
            _batch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            _batch.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            _system.Dispose();
            _runner.Dispose();
            _world.Dispose();
            _batch.Dispose();
            playerSprite.Dispose();
            _graphics.Dispose();

            base.Dispose(disposing);
        }
    }
}
