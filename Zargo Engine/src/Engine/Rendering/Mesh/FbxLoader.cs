using System;
using System.IO;
using System.Numerics;
using UkooLabs.FbxSharpie;

namespace ZargoEngine.Rendering
{
    public static class FbxLoader
    {
        public static void LoadFbx(string path, ref Vector3[] positions,ref Vector3[] normals,ref Vector2[] texCoords,
                                   ref int[] vertexIndices)
        {
            var isBinary = FbxIO.IsBinaryFbx(path);
            var documentNode = FbxIO.Read(path);

            // Scale factor usually 1 or 2.54
            var scaleFactor = documentNode.GetScaleFactor();
            var geometryIds = documentNode.GetGeometryIds();

            var fbxIndexer = new FbxIndexer();

            foreach (var geometryId in geometryIds)
            {
                vertexIndices = documentNode.GetVertexIndices(geometryId);

                long[] normalLayerIndices = documentNode.GetLayerIndices(geometryId, FbxLayerElementType.Normal);
                long[] tangentLayerIndices = documentNode.GetLayerIndices(geometryId, FbxLayerElementType.Tangent);
                long[] binormalLayerIndices = documentNode.GetLayerIndices(geometryId, FbxLayerElementType.Binormal);
                long[] texCoordLayerIndices = documentNode.GetLayerIndices(geometryId, FbxLayerElementType.TexCoord);
                long[] materialLayerIndices = documentNode.GetLayerIndices(geometryId, FbxLayerElementType.Material);

                positions = documentNode.GetPositions(geometryId, vertexIndices);
                normals   = documentNode.GetNormals(geometryId, vertexIndices, normalLayerIndices[0]);
                texCoords = documentNode.GetTexCoords(geometryId, vertexIndices, texCoordLayerIndices[0]);
                int[] materials = documentNode.GetMaterials(geometryId, vertexIndices, materialLayerIndices[0]);

                bool hasNormals = documentNode.GetGeometryHasNormals(geometryId);
                bool hasTexCoords = documentNode.GetGeometryHasTexCoords(geometryId);
                bool hasTangents = documentNode.GetGeometryHasTangents(geometryId);
                bool hasBinormals = documentNode.GetGeometryHasBinormals(geometryId);
                bool hasMaterials = documentNode.GetGeometryHasMaterials(geometryId);

                Vector3[] tangents  = Array.Empty<Vector3>();
                Vector3[] binormals = Array.Empty<Vector3>();

                if (hasTangents){
                    tangents  = documentNode.GetTangents(geometryId, vertexIndices, tangentLayerIndices[0]);
                }

                if (hasBinormals){
                    binormals = documentNode.GetBinormals(geometryId, vertexIndices, binormalLayerIndices[0]);
                }

                for (var i = 0; i < positions.Length; i++)
                {
                    var vertex = new FbxVertex
                    {
                        Position = positions[i],
                        Normal = hasNormals ? normals[i] : new Vector3(),
                        Tangent = hasTangents ? tangents[i] : new Vector3(),
                        Binormal = hasBinormals ? binormals[i] : new Vector3(),
                        TexCoord = hasTexCoords ? texCoords[i] : new Vector2()
                    };
                    var materialId = hasMaterials ? materials[i] : 0;
                    fbxIndexer.AddVertex(vertex, materialId,0);
                }
            }

        }
    }
}
