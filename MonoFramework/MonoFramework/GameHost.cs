using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using MonoFramework;
using System;

namespace MonoFramework
{
    public class GameHost : Microsoft.Xna.Framework.Game
    {
        private GameObjectBase[] objectArray_;

        public GameHost()
        {
            Textures = new Dictionary<string, Texture2D>();
            Fonts = new Dictionary<string, SpriteFont>();
            GameObjects = new List<GameObjectBase>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Songs = new Dictionary<string, Song>();
            
        }

        public Dictionary<string, Texture2D> Textures { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public List<GameObjectBase> GameObjects { get; set; }
        public Dictionary<string, SoundEffect> SoundEffects { get; set; }
        public Dictionary<string, Song> Songs { get; set; }

        protected virtual void UpdateAll(GameTime time)
        {

            int objectCount;

            if (objectArray_ == null)
            {
                objectArray_ = new GameObjectBase[(int)MathHelper.Max(20, GameObjects.Count * 1.2f)];
            }
            else if (GameObjects.Count > objectArray_.Length)
            {
                objectArray_ = new GameObjectBase[(int)(GameObjects.Count * 1.2f)];
            }
            objectCount = GameObjects.Count;

            for (int i = 0; i < objectArray_.Length; ++i)
            {
                if (i < objectCount)
                {
                    objectArray_[i] = GameObjects[i];
                }
                else
                {
                    objectArray_[i] = null;
                }
            }

            for (int i = 0; i < objectCount; ++i)
            {
                objectArray_[i].Update(time);
            }
        }

        public void DrawSprites(GameTime time, SpriteBatch spriteBatch)
        {
            DrawSprites(time, spriteBatch, null);
        }

        public void DrawSprites(GameTime time, SpriteBatch spriteBatch, Texture2D restrictToTexture)
        {
            GameObjectBase obj;
            int objCount;

            objCount = objectArray_.Length;
            for (int i = 0; i < objCount; ++i)
            {
                obj = objectArray_[i];
                if (obj is SpriteObject && !(obj is TextObject))
                {
                    if (restrictToTexture == null || ((SpriteObject)obj).SpriteTexture == restrictToTexture)
                    {
                        ((SpriteObject)obj).Draw(time, spriteBatch);
                    }
                }
            }
        }

        public virtual void DrawText(GameTime time, SpriteBatch spriteBatch)
        {
            int objCount;
            GameObjectBase obj;

            objCount = objectArray_.Length;
            for (int i = 0; i < objCount; ++i)
            {
                obj = objectArray_[i];
                if (obj is TextObject)
                {
                    ((TextObject)obj).Draw(time, spriteBatch);
                }
            }
        }

        public SpriteObject[] GetSpritesAtPoint(Vector2 pos)
        {
            SpriteObject obj;
            // looks like a little bit dirty -> can result in huge allocation
            // to verify
            SpriteObject[] sel = new SpriteObject[GameObjects.Count];
            int hitCount = 0;

            foreach (GameObjectBase o in GameObjects)
            {
                if (!(o is SpriteObject))
                    continue;
                obj = (SpriteObject)o;
                if (obj.IsPointInObject(pos))
                {
                    sel[hitCount] = obj;
                    ++hitCount;
                }
            }
            Array.Resize(ref sel, hitCount);
            return sel;
        }
        
        public SpriteObject GetSpriteAtPoint(Vector2 pos)
        {
            SpriteObject obj;
            SpriteObject sel = null;
            float lowest = float.MaxValue;

            foreach (GameObjectBase o in GameObjects)
            {
                if (!(o is SpriteObject))
                    continue;
                obj = (SpriteObject)o;
                if (obj.Depth <= lowest && obj.IsPointInObject(pos))
                {
                    sel = obj;
                    lowest = obj.Depth;
                }
            }
            return sel;
        }

    }
}
