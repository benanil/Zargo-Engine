
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace ZargoEngine.Rendering
{
    public struct Vertex
    {
        public Vector3 position;
        public Vector2 TexCoord;

        public Vertex(Vector3 position, Vector2 texCoord)
        {
            this.position = position;
            TexCoord = texCoord;
        }
    }
    
    public class Mesh : IDisposable
    {
        private readonly Vertex[] vertexes;
        private readonly uint[] indices;

        private readonly int vaoID;
        private readonly int vboID;
        private readonly int eboID;

        // comining
        public Mesh(string path)
        {

        }

        public Mesh(Vertex[] vertexes, uint[] indices){
            this.vertexes = vertexes;
            this.indices = indices;

            vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);

            Console.WriteLine(GL.GetError());

            vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf<Vertex>() * this.vertexes.Length, this.vertexes, BufferUsageHint.DynamicDraw);
            Console.WriteLine(GL.GetError());

            eboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf<Vertex>(), Marshal.OffsetOf<Vertex>(nameof(Vertex.position)));
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf<Vertex>(), Marshal.OffsetOf<Vertex>(nameof(Vertex.TexCoord)));
            GL.EnableVertexAttribArray(1);

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
