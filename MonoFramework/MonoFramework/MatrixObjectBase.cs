using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoFramework
{
    public abstract class MatrixObjectBase : GameObjectBase
    {
        public MatrixObjectBase(GameHost game)
            : base(game)
        {
            Transformation = Matrix.Identity;
            Scale = Vector3.One;
            ObjectColor = Color.White;
        }

        public MatrixObjectBase(GameHost game, Vector3 position)
            : this(game)
        {
            Position = position;
        }

        public MatrixObjectBase(GameHost game, Vector3 position, Texture2D texture)
            : this(game, position)
        {
            ObjectTexture = texture;
        }

        // properties
        public virtual Texture2D ObjectTexture { get; set; }
        public virtual Matrix Transformation { get; set; }
        public virtual float PositionX { get; set; }
        public virtual float PositionY { get; set; }
        public virtual float PositionZ { get; set; }
        public virtual float AngleX { get; set; }
        public virtual float AngleY { get; set; }
        public virtual float AngleZ { get; set; }
        public virtual float ScaleX { get; set; }
        public virtual float ScaleY { get; set; }
        public virtual float ScaleZ { get; set; }
        public virtual Color ObjectColor { get; set; }
        public Vector3 Position
        {
            get
            {
                return new Vector3(PositionX, PositionY, PositionZ);
            }
            set
            {
                PositionX = value.X;
                PositionY = value.Y;
                PositionZ = value.Z;
            }
        }

        public Vector3 Angle
        {
            get
            {
                return new Vector3(AngleX, AngleY, AngleZ);
            }
            set
            {
                AngleX = value.X;
                AngleY = value.Y;
                AngleZ = value.Z;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return new Vector3(ScaleX, ScaleY, ScaleZ);
            }
            set
            {
                ScaleX = value.X;
                ScaleY = value.Y;
                ScaleZ = value.Z;
            }
        }

        public abstract void Draw(GameTime time, Effect effect);

        protected void ApplyStandardTransformations()
        {
            Matrix res;

            res = Transformation;
            if (!Position.Equals(Vector3.Zero))
            {
                res = Matrix.CreateTranslation(Position) * res;
            }

            if (AngleX != 0)
                res = Matrix.CreateRotationX(AngleX) * res;
            if (AngleY != 0)
                res = Matrix.CreateRotationY(AngleY) * res;
            if (AngleZ != 0)
                res = Matrix.CreateRotationZ(AngleZ) * res;

            if (Scale.Equals(Vector3.Zero))
            {
                res = Matrix.CreateScale(Scale) * res;
            }

            Transformation = res;
        }

        protected void SetIdentity()
        {
            Transformation = Matrix.Identity;
        }

        protected void ApplyTransformation(Matrix newTransformation)
        {
            Transformation = newTransformation * Transformation;
        }

        protected void PrepareEffect(Effect effect)
        {
            if (effect is BasicEffect)
            {
                PrepareEffect((BasicEffect)effect);
                return;
            }
            throw new NotSupportedException("Cannot prepare effects of type " + effect.GetType().Name + ".");
        }

        protected void PrepareEffect(BasicEffect effect)
        {
            if (ObjectTexture == null)
            {
                effect.TextureEnabled = false;
            }
            else
            {
                effect.TextureEnabled = true;
                if (ObjectTexture != effect.Texture)
                    effect.Texture = ObjectTexture;
            }
            effect.DiffuseColor = ObjectColor.ToVector3();
            effect.Alpha = (float)ObjectColor.A / 255.0f;
            effect.World = Transformation;
        }

    }
}
