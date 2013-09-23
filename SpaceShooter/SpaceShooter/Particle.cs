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
    public class Particle : SpriteObject
    {
        internal bool IsActive { get; set; }
        internal Vector2 Direction { get; set; }
        internal float Speed { get; set; }
        internal float Inertia { get; set; }
        internal byte FadeAmount { get; set; }

        public Particle(GameHost game)
            : base(game)
        { }

        public override void Update(GameTime gameTime)
        {
            if (!IsActive)
                return;

            base.Update(gameTime);
            Position += Direction * Speed;
            Speed *= Inertia;
            SpriteColor= new Color(SpriteColor.R, SpriteColor.G, SpriteColor.B, SpriteColor.A - FadeAmount);
            if (SpriteColor.A <= 0)
            {
                IsActive = false;
            }
        }

        public override void Draw(GameTime time, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (!IsActive)
                return;
            base.Draw(time, spriteBatch);
        }

        public void Reset(Vector2 position, Texture2D texture)
        {
            Position = position;
            SpriteTexture = texture;
            Origin = new Vector2(SpriteTexture.Width / 2, SpriteTexture.Height / 2);
            Angle = MathHelper.ToRadians(GameHelper.RandomNext(360f));
            IsActive = true;
            FadeAmount = 3;
            do
            {
                Direction = new Vector2(GameHelper.RandomNext(-1.0f, 1.0f));
            } while (Direction == Vector2.Zero);
            Direction.Normalize();
            Inertia = 1.0f;
        }

    }
}
