using OpenTK.Graphics.OpenGL4;
using System;
using ZargoEngine.AssetManagement;

namespace ZargoEngine.Rendering
{
    public class Skybox : IDisposable
    {
        private readonly string[] textureLocations =
        {
            "Images/skybox/right.jpg",
            "Images/skybox/left.jpg",
            "Images/skybox/top.jpg",
            "Images/skybox/bottom.jpg",
            "Images/skybox/front.jpg",
            "Images/skybox/back.jpg"
        };

        private static readonly float SIZE = 1000f;

        private static readonly float[] vertices = {
            -SIZE,  SIZE, -SIZE,
            -SIZE, -SIZE, -SIZE,
             SIZE, -SIZE, -SIZE,
             SIZE, -SIZE, -SIZE,
             SIZE,  SIZE, -SIZE,
            -SIZE,  SIZE, -SIZE,

            -SIZE, -SIZE,  SIZE,
            -SIZE, -SIZE, -SIZE,
            -SIZE,  SIZE, -SIZE,
            -SIZE,  SIZE, -SIZE,
            -SIZE,  SIZE,  SIZE,
            -SIZE, -SIZE,  SIZE,

             SIZE, -SIZE, -SIZE,
             SIZE, -SIZE,  SIZE,
             SIZE,  SIZE,  SIZE,
             SIZE,  SIZE,  SIZE,
             SIZE,  SIZE, -SIZE,
             SIZE, -SIZE, -SIZE,

            -SIZE, -SIZE,  SIZE,
            -SIZE,  SIZE,  SIZE,
             SIZE,  SIZE,  SIZE,
             SIZE,  SIZE,  SIZE,
             SIZE, -SIZE,  SIZE,
            -SIZE, -SIZE,  SIZE,

            -SIZE,  SIZE, -SIZE,
             SIZE,  SIZE, -SIZE,
             SIZE,  SIZE,  SIZE,
             SIZE,  SIZE,  SIZE,
            -SIZE,  SIZE,  SIZE,
            -SIZE,  SIZE, -SIZE,

            -SIZE, -SIZE, -SIZE,
            -SIZE, -SIZE,  SIZE,
             SIZE, -SIZE, -SIZE,
             SIZE, -SIZE, -SIZE,
            -SIZE, -SIZE,  SIZE,
             SIZE, -SIZE,  SIZE
        };

        readonly uint[] indices = {
            0, 1, 3,
            3, 2, 0,
            0, 1, 7,
            7, 6, 1,
            1, 3, 6,
            6, 4, 3,
            3, 2, 4,
            4, 2, 5,
            5, 4, 6,
            6, 5, 7,
            7, 5, 2,
            2, 0, 7
        };

        private readonly CubeMap cubeMap;
        private readonly Shader skyBoxShader;

        private readonly int vaoID, vboID, eboID;

        public Skybox()
        {
            skyBoxShader = AssetManager.GetShader("Shaders/Skybox/SkyBoxVert.hlsl", "Shaders/Skybox/SkyBoxFrag.hlsl");
            cubeMap = new CubeMap(textureLocations);

            vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);

            vboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), IntPtr.Zero);
            GL.EnableVertexAttribArray(0);

            eboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void Use(Camera camera)
        {
            GL.DepthMask(false);
            
            skyBoxShader.Use();
            skyBoxShader.SetMatrix4("projection", camera.GetProjectionMatrix());
            skyBoxShader.SetMatrix4("view", camera.GetViewMatrix());
            
            GL.BindVertexArray(vaoID);
            cubeMap.Bind();

            GL.EnableVertexAttribArray(0);
            GL.DrawElements(PrimitiveType.TriangleStrip, 36, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(0);

            GL.DepthMask(true);

            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(eboID);
            GL.DeleteVertexArray(vaoID);
            GL.DeleteBuffer(vboID);
            GC.SuppressFinalize(this);
        }

    }
}
