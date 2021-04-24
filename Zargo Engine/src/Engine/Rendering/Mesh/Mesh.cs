
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ZargoEngine.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 position;
        public Vector2 texCoord;
        public Vector3 normal;

        public Vertex(Vector3 position, Vector2 texCoord, Vector3 normal){
            this.position = position;
            this.texCoord = texCoord;
            this.normal = normal;
        }
    }

    public class Mesh : IDisposable
    {
        private readonly List<Tuple<Vertex, Vertex, Vertex>> faces = new List<Tuple<Vertex, Vertex, Vertex>>();

        private Vector3[] _positions;
        public Vector3[] Vertices{
            get{
                if (_positions == null){
                    _positions = GetPositions();
                }
                return _positions;
            }
        }
        private Vector2[] _texCoords;
        public Vector2[] TexCoords
        {
            get{
                if (_texCoords == null) _texCoords = GetTextureCoords();
                return _texCoords;
            }
        }
        private Vector3[] _normals;
        public Vector3[] Normals
        {
            get
            {
                if (_normals == null) _normals = GetPositions();
                return _normals;
            }
        }

        private int[] _indices;
        public int[] indices
        {
            get
            {
                if (_indices == null) _indices = GetIndices();
                return _indices;
            }
        }

        private readonly int vaoID, vboID, eboID;

        // comining
        public Mesh(string path)
        {
            ObjLoader.Load(path, ref faces);
            LoadBuffers(ref vaoID,ref vboID,ref eboID);
        }

        public override string ToString()
        {
            var name = new StringBuilder();

            name.Append("Vertices\n");
            for (int i = 0; i < Vertices.Length; i++){
                name.Append(Vertices[i]);
                name.Append('\n');
            }
            
            name.Append("Texcoords\n");

            for (int i = 0; i < Vertices.Length; i++){
                name.Append(TexCoords[i]);
                name.Append('\n');
            }
            
            name.Append("Normals\n");
            for (int i = 0; i < Vertices.Length; i++){
                name.Append(Normals[i]);
                name.Append('\n');
            }

            for (int i = 0; i < indices.Length; i += 3){
                name.Append($"{indices[i]} {indices[i + 1]} {indices[i + 2]}");
                name.Append('\n');
            }

            return name.ToString();
        }

        private void LoadBuffers(ref int vaoID,ref int vboID, ref int eboID)
        { 
            vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);

            Console.WriteLine(GL.GetError());

            vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Marshal.SizeOf<Vector3>(), Vertices.ToArray(), BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf<Vector3>(), 0);
            
            Console.WriteLine(GL.GetError());

            eboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            var uvBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, uvBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, TexCoords.Length * Marshal.SizeOf<Vector2>(), TexCoords.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            var normalBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, normalBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, Normals.Length * Marshal.SizeOf<Vector3>(), Normals.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            Console.WriteLine(GL.GetError());
        }

        public Vector3[] GetPositions()
        {
            List<Vector3> coords = new List<Vector3>();

            foreach (var face in faces){
                coords.Add(face.Item1.position);
                coords.Add(face.Item2.position);
                coords.Add(face.Item3.position);
            }

            return coords.ToArray();
        }

        public Vector2[] GetTextureCoords(){
            List<Vector2> coords = new List<Vector2>();

            foreach (var face in faces){
                coords.Add(face.Item1.texCoord);
                coords.Add(face.Item2.texCoord);
                coords.Add(face.Item3.texCoord);
            }

            return coords.ToArray();
        }

        public int[] GetIndices(int offset = 0)
        {                                   // Indice count
            return Enumerable.Range(offset, faces.Count * 3).ToArray();
        }

        public void Draw(){
            GL.BindVertexArray(vaoID);
            
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            
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
