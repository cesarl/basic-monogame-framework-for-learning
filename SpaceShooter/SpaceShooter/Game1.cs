using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework;

namespace SpaceShooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : MonoFramework.GameHost
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Add("Rock1", Content.Load<Texture2D>("Rock1"));
            Textures.Add("Rock2", Content.Load<Texture2D>("Rock2"));
            Textures.Add("Rock3", Content.Load<Texture2D>("Rock3"));

            Fonts.Add("Default", Content.Load<SpriteFont>("Default"));

            // Initialize Benchmark

            GameObjects.Add(new Benchmark(this, Fonts["Default"], new Vector2(0f, 0f), Color.White));

            // Initialize asteroids

            for (int i = 0; i < 10; ++i)
            { 
                GameObjects.Add(new Asteroid(this, new Vector2(
                    GameHelper.RandomNext(0, GraphicsDevice.Viewport.Bounds.Right),
                    GameHelper.RandomNext(0, GraphicsDevice.Viewport.Bounds.Bottom)),
                    GameHelper.RandomNext(1, 4))
                );
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            UpdateAll(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            DrawSprites(gameTime, _spriteBatch);
            DrawText(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
