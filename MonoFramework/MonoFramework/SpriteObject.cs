using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFramework
{
    public class SpriteObject : GameObjectBase
    {
        public SpriteObject(Game game)
            : base(game)
        {
            Scale = new Vector2(1,1);
            SpriteColor = Color.White;
        }

        public SpriteObject(Game game, Vector2 position)
            : this(game)
        {
            Position = position;
        }

        public SpriteObject(Game game, Vector2 position, Texture2D texture)
            : this(game, position)
        {
            SpriteTexture = texture;
        }

        public virtual float PositionX { get; set; }
        public virtual float PositionY { get; set; }
        public virtual float OriginX { get; set; }
        public virtual float OriginY { get; set; }
        public virtual float ScaleX { get; set; }
        public virtual float ScaleY { get; set; }

        public virtual Vector2 Origin
        {
            get
            {
                return new Vector2(OriginX, OriginY);
            }
            set
            {
                OriginX = value.X;
                OriginY = value.Y;
            }
        }

        public virtual Vector2 Position
        {
            get
            {
                return new Vector2(PositionX, PositionY);
            }
            set
            {
                PositionX = value.X;
                PositionY = value.Y;
            }
        }

        public virtual Vector2 Scale
        {
            get
            {
                return new Vector2(ScaleX, ScaleY);
            }
            set
            {
                ScaleX = value.X;
                ScaleY = value.Y;
            }
        }


        public virtual Texture2D SpriteTexture { get; set; }
        public virtual Color SpriteColor { get; set; }
        public virtual float Angle { get; set; }
        public virtual Rectangle Rect { get; set; }
        public virtual float Depth { get; set; }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            if (SpriteTexture == null)
                return;
            if (Rect.IsEmpty)
                spriteBatch.Draw(SpriteTexture, Position, null, SpriteColor, Angle, Origin, Scale, SpriteEffects.None, Depth);
            else
                spriteBatch.Draw(SpriteTexture, Position, Rect, SpriteColor, Angle, Origin, Scale, SpriteEffects.None, Depth);
        }

    }

}
