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
        Spaceship spaceShip_;

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
            Textures.Add("Spaceship", Content.Load<Texture2D>("Spaceship"));
            Textures.Add("Smoke", Content.Load<Texture2D>("SmokeParticle"));

            Fonts.Add("Default", Content.Load<SpriteFont>("Default"));

            // Add Spaceship
            spaceShip_ = new Spaceship(this, Textures["Spaceship"], new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            GameObjects.Add(spaceShip_);

            // Initialize Benchmark

            GameObjects.Add(new Benchmark(this, Fonts["Default"], new Vector2(0f, 0f), Color.White));

            // Initialize asteroids

            for (int i = 0; i < 150; ++i)
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

        public Vector2 Prout { get; set; }

        public void SetParticles(int n, Vector2 position)
        {
            int rest = n;
            GameObjectBase obj;

            for (int i = 0, mi = GameObjects.Count; i < mi && rest > 0; ++i)
            { 
                obj = GameObjects[i];
                if (!(obj is Particle) || ((Particle)obj).IsActive)
                    continue;
                ((Particle)obj).Reset(position, Textures["Smoke"]);
                --rest;
            }
            while (rest > 0)
            {
                GameObjects.Add(new Particle(this, position, Textures["Smoke"]));
                --rest;
            }
        }
    }
}
