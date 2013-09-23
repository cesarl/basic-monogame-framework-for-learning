using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    public class Asteroid : SpriteObject
    {
        private static string[] asteroidTextures = new string[] {"Rock1", "Rock2", "Rock3"};

        public Asteroid(GameHost game)
            : base(game)
        {
            RotateSpeed = GameHelper.RandomNext(-0.5f, 0.5f);
            Speed = GameHelper.RandomNext(-2f, 2f);
            Life = 3;
            do
            {
                Direction = new Vector2(GameHelper.RandomNext(0f, 1f), GameHelper.RandomNext(0f, 1f));
            } while (Direction == Vector2.Zero);
            SpriteTexture = GameHost.Textures[asteroidTextures[GameHelper.RandomNext(0, 2)]];
        }

        public Asteroid(GameHost game, Vector2 position)
            : this(game)
        {
            Position = position;
        }

        private Vector2 direction_ = new Vector2();

        public Vector2 Direction
        {
            get { return direction_; }
            set { direction_ = value; direction_.Normalize(); }
        }

        public float Speed
        { get; set; }

        public int Life
        { get; set; }

        public float RotateSpeed
        { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position += Direction * Speed;
            Angle += MathHelper.ToRadians(RotateSpeed);

            if (PositionX < OriginX)
            {
                PositionX = OriginX;
                Direction *= new Vector2(-1f, 1f);         
            }
            else if (PositionX > GameHost.GraphicsDevice.Viewport.Bounds.Width - OriginX)
            {
                PositionX = GameHost.GraphicsDevice.Viewport.Bounds.Width - OriginX;
                Direction *= new Vector2(-1f, 1f);         
            }
            else if (PositionY < OriginY)
            {
                PositionY = OriginY;
                Direction *= new Vector2(1f, -1f);         
            }
            else if (PositionY > GameHost.GraphicsDevice.Viewport.Bounds.Height - OriginY)
            {
                PositionY = GameHost.GraphicsDevice.Viewport.Bounds.Height - OriginY;
                Direction *= new Vector2(1f, -1f);         
            }

        }
    }
}
