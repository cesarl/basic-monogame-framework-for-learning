using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoFramework
{
    public abstract class GameObjectBase
    {
        public GameObjectBase(Game game)
        {
            Game = game;
        }

        protected Game Game { get; set; }

        public int UpdateCount { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            UpdateCount += 1;
        }
    }
}
