
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UkooLabs.FbxSharpie;

namespace ZargoEngine.Rendering
{
    public class Mesh : IDisposable
    {
        private SubMesh subMesh;

        private readonly int vaoID, vboID, eboID;

        // comining
        public Mesh(string path)
        {
            if (path.EndsWith(".obj")){
                List<Tuple<FbxVertex, FbxVertex, FbxVertex>> faces = new List<Tuple<FbxVertex, FbxVertex, FbxVertex>>();
                ObjLoader.Load(path, ref faces);
                subMesh = new ObjMesh(faces);
            }
            else if (path.EndsWith(".fbx")){
                System.Numerics.Vector3[] positions = Array.Empty<System.Numerics.Vector3>();
                System.Numerics.Vector3[] normals   = Array.Empty<System.Numerics.Vector3>();
                System.Numerics.Vector2[] texCoords = Array.Empty<System.Numerics.Vector2>();
                int[] indices = Array.Empty<int>();

                FbxLoader.LoadFbx(path, ref positions, ref normals, ref texCoords,ref indices);
                subMesh = new FbxMesh(positions, normals, texCoords,indices);
            }
            LoadBuffers(ref vaoID,ref vboID,ref eboID);
        }

        // for custom mesh
        public Mesh(System.Numerics.Vector3[] positions, System.Numerics.Vector3[] normals, System.Numerics.Vector2[] texcoords,int[] indices)
        {
            subMesh = new FbxMesh(positions,normals,texcoords,indices); 
            LoadBuffers(ref vaoID, ref vboID, ref eboID);
        }

        public override string ToString()
        {
            var name = new StringBuilder();

            name.Append("Vertices\n");
            for (int i = 0; i < subMesh.Positions.Length; i++){
                name.Append(subMesh.Positions[i]);
                name.Append('\n');
            }
            
            name.Append("Texcoords\n");

            for (int i = 0; i < subMesh.Positions.Length; i++){
                name.Append(subMesh.TexCoords[i]);
                name.Append('\n');
            }
            
            name.Append("Normals\n");
            for (int i = 0; i < subMesh.Positions.Length; i++){
                name.Append(subMesh.Normals[i]);
                name.Append('\n');
            }

            for (int i = 0; i < subMesh.Indices.Length; i += 3){
                name.Append($"{subMesh.Indices[i]} {subMesh.Indices[i + 1]} {subMesh.Indices[i + 2]}");
                name.Append('\n');
            }

            return name.ToString();
        }

        private void LoadBuffers(ref int vaoID,ref int vboID, ref int eboID)
        { 
            vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);

            Debug.Log(GL.GetError());

            vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, subMesh.Positions.Length * Marshal.SizeOf<Vector3>(), subMesh.Positions.ToArray(), BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf<Vector3>(), 0);
            
            Debug.Log(GL.GetError());

            eboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, subMesh.Indices.Length * sizeof(int), subMesh.Indices, BufferUsageHint.StaticDraw);

            var uvBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, uvBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, subMesh.TexCoords.Length * Marshal.SizeOf<Vector2>(), subMesh.TexCoords.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            var normalBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, normalBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, subMesh.Normals.Length * Marshal.SizeOf<Vector3>(), subMesh.Normals.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            Debug.Log(GL.GetError());
        }

        
        public void Draw(){
            GL.BindVertexArray(vaoID);
            
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            GL.DrawElements(PrimitiveType.Triangles, subMesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
            
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);

            GL.BindVertexArray(0);
        }

        public void Dispose(){
            GL.DeleteVertexArray(vaoID);
            GL.DeleteBuffer(vboID);
            GL.DeleteBuffer(eboID);
            GC.SuppressFinalize(this);
        }
    }
}
