using System.Numerics;
using ZargoEngine.Helper;

namespace ZargoEngine.Rendering
{
    public class FbxMesh : SubMesh
    {
        public FbxMesh(Vector3[] positions,Vector3[] normals,Vector2[] texCoords,int[] indices)
        {
            this.Indices = indices;
            this.Positions = positions.ToOpenTk();
            this.Normals = normals.ToOpenTk();
            this.TexCoords = texCoords.ToOpenTk();
        }

        public override OpenTK.Mathematics.Vector3[] GetPositions()
        {
            return Positions;
        }

        public override OpenTK.Mathematics.Vector2[] GetTextureCoords()
        {
            return TexCoords;
        }

        public override int[] GetIndices(int offset = 0)
        {
            return Indices;
        }
    }
}
