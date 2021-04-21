
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ZargoEngine.Rendering
{
    public class Mesh : IDisposable
    {
        private readonly List<Vector3> vertexes  = new ();
        private readonly List<Vector2> texCoords = new ();

        private readonly int[] indices = new int[0];

        private readonly int vaoID,vboID,eboID;

        // comining
        public Mesh(string path)
        {
            ObjLoader.Load(path, ref vertexes, ref texCoords, ref indices);
            LoadBuffers(ref vaoID,ref vboID,ref eboID);
        }

        public Mesh(in Vector3[] vertexes,in int[] indices,in Vector2[] texCoords){

            this.vertexes = vertexes.ToList();
            this.indices = indices;
            this.texCoords = texCoords.ToList();
            LoadBuffers(ref vaoID,ref vboID,ref eboID);
        }

        private void LoadBuffers(ref int vaoID,ref int vboID, ref int eboID)
        { 
            vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);

            Console.WriteLine(GL.GetError());

            vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf<Vector3>() * vertexes.Count, vertexes.ToArray(), BufferUsageHint.DynamicDraw);
            Console.WriteLine(GL.GetError());

            eboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf<Vector3>(), 0);
            GL.EnableVertexAttribArray(0);

            var uvBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, uvBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (texCoords.Count * Vector2.SizeInBytes), texCoords.ToArray(), BufferUsageHint.StaticDraw);
            
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            
            Console.WriteLine(GL.GetError());
        }

        public void Draw(){
            GL.BindVertexArray(vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

            GL.BindVertexArray(0);
        }

        public void Dispose(){
            GL.DeleteBuffer(vaoID);
            GL.DeleteBuffer(vboID);
            GL.DeleteBuffer(eboID);
            GC.SuppressFinalize(this);
        }
    }
}
