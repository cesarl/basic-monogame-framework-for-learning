using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoFramework
{
    public class Benchmark : TextObject
    {
        private double lastTimeUpdate_;
        private int drawCount_;
        private int lastDrawCount_;
        private int lastUpdateCount_;

        private StringBuilder strBuilder_ = new StringBuilder();

        public Benchmark(GameHost game, SpriteFont font, Vector2 position, Color color)
            : base(game, font, position)
        {
            SpriteColor = color;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int newDrawCount;
            int newUpdateCount;
            double newElapsedTime;

            if (gameTime.TotalGameTime.TotalMilliseconds > lastTimeUpdate_ + 1000)
            {
                newDrawCount = drawCount_ - lastDrawCount_;
                newUpdateCount = UpdateCount - lastUpdateCount_;
                newElapsedTime = gameTime.TotalGameTime.TotalMilliseconds - lastTimeUpdate_;
            }

            strBuilder_.Length = 0;
            strBuilder_.AppendLine("Object count : " + GameHost.GameObjects.Count.ToString());
            //strBuilder_.AppendLine("Frames per second: " + ((float)newDrawCount / newElapsedTime * 1000).ToString("0.0"));
            //strBuilder_.AppendLine("Updates per second: " + ((float)newUpdateCount / newElapsedTime * 1000).ToString("0.0"));
            Text = strBuilder_.ToString();
        }
    }
}