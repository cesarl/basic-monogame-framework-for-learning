using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework
{
    public static class GameHelper
    {
        private static Random rand_;

        public static int RandomNext(int maxValue)
        {
            return RandomNext(0, maxValue);
        }

        public static int RandomNext(int min, int max)
        {
            if (rand_ == null)
                rand_ = new Random();
            return rand_.Next(min, max);
        }

        public static float RandomNext(float max)
        {
            return RandomNext(0.0f, max);
        }

        public static float RandomNext(float min, float max)
        { 
            if (rand_ == null)
                rand_ = new Random();
            return (float)rand_.NextDouble() * (max - min) + min;

        }
    }
}
