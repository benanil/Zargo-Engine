using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using UkooLabs.FbxSharpie;
using ZargoEngine.Helper;

namespace ZargoEngine.Rendering
{
    public class ObjMesh : SubMesh
    {
        public List<Tuple<FbxVertex, FbxVertex, FbxVertex>> faces = new();

        public ObjMesh(List<Tuple<FbxVertex, FbxVertex, FbxVertex>> faces)
        {
            this.faces = faces;
        }

        public override Vector3[] GetPositions()
        {
            List<Vector3> coords = new List<Vector3>();

            foreach (var face in faces)
            {
                coords.Add(face.Item1.Position.ToOpenTK());
                coords.Add(face.Item2.Position.ToOpenTK());
                coords.Add(face.Item3.Position.ToOpenTK());
            }

            return coords.ToArray();
        }

        public override Vector2[] GetTextureCoords()
        {
            List<Vector2> coords = new List<Vector2>();

            foreach (var face in faces)
            {
                coords.Add(face.Item1.TexCoord.ToOpenTK());
                coords.Add(face.Item2.TexCoord.ToOpenTK());
                coords.Add(face.Item3.TexCoord.ToOpenTK());
            }

            return coords.ToArray();
        }

        public override int[] GetIndices(int offset = 0)
        {                                   // Indice count
            return Enumerable.Range(offset, faces.Count * 3).ToArray();
        }
    }
}
