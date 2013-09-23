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
    internal class Spaceship : SpriteObject
    {
        internal Spaceship(GameHost game, Texture2D texture, Vector2 position)
            : base(game, position, texture)
        {
            Origin = new Vector2(texture.Width, texture.Height);
            Scale = new Vector2(0.2f, 0.2f);
            ExplosionUpdateCount = 0;
        }

        internal bool IsExploding
        {
            get { return (ExplosionUpdateCount > 0);  }
        }

        private int ExplosionUpdateCount { get; set; }

        public override void Update(GameTime gameTime)
        {
            SpriteObject collisionObj;

            if (IsExploding)
                ExplosionUpdateCount -= 1;
            else
            {
                collisionObj = HasCollided();

                if (collisionObj is Asteroid)
                {
                    ((Asteroid)collisionObj).Damage();
                }

                if (collisionObj != null)
                {
                    Explode();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            if (!IsExploding)
                base.Draw(time, spriteBatch);
        }

        internal SpriteObject HasCollided()
        {
            SpriteObject spriteObj;
            Rectangle shipBox;
            float shipSize;
            float objectSize;
            float objectDist;

            shipBox = BoundingBox;
            shipSize = SpriteTexture.Width / 2.0f * ScaleX;

            foreach (GameObjectBase gameObj in GameHost.GameObjects)
            {
                if (gameObj is Asteroid)
                {
                    spriteObj = (SpriteObject)gameObj;
                    if (spriteObj.BoundingBox.Intersects(shipBox))
                    {
                        objectSize = spriteObj.SpriteTexture.Width / 2.0f * spriteObj.ScaleX;
                        objectDist = Vector2.Distance(Position, spriteObj.Position);
                        if (objectDist < shipSize + objectSize)
                            return spriteObj;
                    }
                }
            }
            return null;
        }

        private void Explode()
        {
            ExplosionUpdateCount = 1;
        }
    }
}
