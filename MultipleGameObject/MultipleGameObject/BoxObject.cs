using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework;

namespace MultipleGameObject
{
    internal class BoxObject : SpriteObject
    {
        //private Game1 game_;

        private float vel_;
        private float rotateSpeed_;

        internal BoxObject(Game1 game, Texture2D texture)
            : base(game, Vector2.Zero, texture)
        {
            //game_ = game;
            Position = new Vector2(GameHelper.RandomNext(0, game.GraphicsDevice.Viewport.Bounds.Width),
                               GameHelper.RandomNext(0, game.GraphicsDevice.Viewport.Bounds.Height));
            Origin = new Vector2(texture.Width, texture.Height) / 2;
            SpriteColor = new Color(GameHelper.RandomNext(0, 256), GameHelper.RandomNext(0, 256), GameHelper.RandomNext(0, 256), GameHelper.RandomNext(0.3f, 1.0f));
            vel_ = GameHelper.RandomNext(2.0f) + 2;
            rotateSpeed_ = GameHelper.RandomNext(-0.5f, 5.0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            PositionY += vel_;
            if (PositionY > GameHost.GraphicsDevice.Viewport.Bounds.Bottom)
            {
                PositionY = -SpriteTexture.Height;
            }

            Angle += MathHelper.ToRadians(rotateSpeed_);
        }
    }
}
