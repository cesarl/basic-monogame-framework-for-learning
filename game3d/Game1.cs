using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework;

namespace game3d
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : MonoFramework.GameHost
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private BasicEffect effect_;
        private MatrixModelObject cameleon_;

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
            Models.Add("Chameleon", Content.Load<Model>("Chameleon"));
            cameleon_ = new MatrixModelObject(this, new Vector3(0, 0, 4), Models["Chameleon"]);
            GameObjects.Add(cameleon_);

            float aspectRatio = (float)GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height;

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 0.1f, 1000.0f);

            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 6), Vector3.Zero, Vector3.Up);

            effect_ = new BasicEffect(GraphicsDevice);
            effect_.VertexColorEnabled = false;
            effect_.TextureEnabled = true;
            effect_.Projection = projection;
            effect_.View = view;
            effect_.World = Matrix.Identity;
            effect_.LightingEnabled = true;

            effect_.DirectionalLight0.Enabled = true;
            effect_.DirectionalLight0.Direction = new Vector3(0, 0, -1);
            effect_.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
            effect_.DirectionalLight0.SpecularColor = Color.White.ToVector3();

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

            // TODO: use this.Content to load your game content here
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
            cameleon_.AngleZ += 0.01f;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawObjects(gameTime, effect_);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
