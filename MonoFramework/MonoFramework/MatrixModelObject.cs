using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoFramework
{
    public class MatrixModelObject : MatrixObjectBase
    {
        public MatrixModelObject(GameHost game)
            : base(game)
        {
        }

        public MatrixModelObject(GameHost game, Vector3 position, Model model)
            : this(game)
        {
            Position = position;
            ObjectModel = model;
        }

        public virtual Model ObjectModel { get; set; }
        public override Texture2D ObjectTexture
        {
            get
            {
                if (base.ObjectTexture != null)
                    return base.ObjectTexture;
                if (ObjectModel == null || ObjectModel.Meshes.Count == 0 || ObjectModel.Meshes[0].MeshParts.Count == 0)
                    return null;
                return ((BasicEffect)ObjectModel.Meshes[0].MeshParts[0].Effect).Texture;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SetIdentity();
            ApplyStandardTransformations();
        }

        public override void Draw(GameTime time, Effect effect)
        {
            PrepareEffect(effect);
            DrawModel((BasicEffect)effect);
        }

        protected virtual void DrawModel(BasicEffect effect)
        {
            Matrix initialWorld;
            Matrix[] boneTrandforms;

            if (ObjectModel == null) return;

            initialWorld = effect.World;

            boneTrandforms = new Matrix[ObjectModel.Bones.Count];
            ObjectModel.CopyAbsoluteBoneTransformsTo(boneTrandforms);

            foreach (ModelMesh mesh in ObjectModel.Meshes)
            {
                effect.World = boneTrandforms[mesh.ParentBone.Index] * initialWorld;

                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    if (base.ObjectTexture != null)
                        effect.Texture = base.ObjectTexture;
                    else
                        effect.Texture = ((BasicEffect)part.Effect).Texture;
                    effect.GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
                    effect.GraphicsDevice.Indices = part.IndexBuffer;

                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        effect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                    }
                }
            }
            effect.World = initialWorld;
        }
    }
}
