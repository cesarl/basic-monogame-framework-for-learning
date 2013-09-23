using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFramework
{
    public class TextObject : SpriteObject
    {

        private SpriteFont font_;
        private string text_ = "";
        private TextAlignement horizontalAlign_ = TextAlignement.Manual;
        private TextAlignement verticalAlign_ = TextAlignement.Manual;

        public enum TextAlignement
        { 
            Manual,
            Near,
            Center,
            Far
        };

        public TextObject(GameHost game)
            : base(game)
        {
            Scale = new Vector2(1,1);
            SpriteColor = Color.White;
        }

        public TextObject(GameHost game, SpriteFont font)
            : this(game)
        {
            Font = font;
        }

        public TextObject(GameHost game, SpriteFont font, Vector2 position)
            : this(game, font)
        {
            Position = position;
        }

        public TextObject(GameHost game, SpriteFont font, Vector2 position, String text)
            : this(game, font, position)
        {
            Text = text;
        }

        public TextObject(GameHost game, SpriteFont font, Vector2 position, String text, TextAlignement horizontal, TextAlignement vertical)
            : this(game, font, position, text)
        {
            HorAlign = horizontal;
            VerAlign = vertical;
        }

        public SpriteFont Font
        {
            get {return font_;}
            set
            {
                if (font_ != value)
                {
                    font_ = value;
                    CalculateAlignementOrigin();
                }
            }
        }

        public String Text
        {
            get { return text_; }
            set
            {
                text_ = value;
                CalculateAlignementOrigin();
            }
        }

        public TextAlignement HorAlign
        {
            get { return horizontalAlign_; }
            set
            {
                if (horizontalAlign_ != value)
                {
                    horizontalAlign_ = value;
                    CalculateAlignementOrigin();
                }
            }
        }

        public TextAlignement VerAlign
        {
            get { return verticalAlign_; }
            set
            {
                if (verticalAlign_ != value)
                {
                    verticalAlign_ = value;
                    CalculateAlignementOrigin();
                }
            }
        }

        private void CalculateAlignementOrigin()
        {
            Vector2 size;
            if (HorAlign == TextAlignement.Manual && VerAlign == TextAlignement.Manual)
                return;
            if (Font == null || Text == null || Text.Length == 0)
                return;
         
            size = Font.MeasureString(Text);

            switch (HorAlign)
            {
                case TextAlignement.Near: OriginY = 0; break;
                case TextAlignement.Center: OriginY = size.Y / 2; break;
                case TextAlignement.Far: OriginY = size.Y; break;
            }
        }

        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            //base.Draw(time, spriteBatch);
            spriteBatch.DrawString(Font, Text, Position, SpriteColor);
        }
    }
}
