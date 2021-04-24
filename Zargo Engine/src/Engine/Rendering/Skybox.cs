using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using ZargoEngine.AssetManagement;

namespace ZargoEngine.Rendering
{
    public class Skybox : IDisposable
    {
        private const string texture0 = nameof(texture0);

        private readonly string[] textureLocations =
        {
            "Images/skybox/right.jpg",
            "Images/skybox/left.jpg",
            "Images/skybox/top.jpg",
            "Images/skybox/bottom.jpg",
            "Images/skybox/front.jpg",
            "Images/skybox/back.jpg"
        };

        private static readonly float[] vertices = {
            -1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,
        //                  
            -1.0f, -1.0f,  1.0f,
            -1.0f, -1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f,  1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,
        //                  
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f,  1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
    //                      
            -1.0f, -1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,
    //                      
            -1.0f,  1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f, -1.0f,
          //                
            -1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f,  1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f,  1.0f,
             1.0f, -1.0f,  1.0f

        };

        private readonly CubeMap cubeMap;
        private readonly Shader skyBoxShader;

        private readonly int vaoID, vboID;

        public Skybox()
        {
            skyBoxShader = AssetManager.GetShader("Shaders/Skybox/SkyboxVert.hlsl", "Shaders/Skybox/SkyboxFrag.hlsl");
            cubeMap = new CubeMap(textureLocations);

            vaoID = GL.GenVertexArray();

            GL.BindVertexArray(vaoID);

            vboID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), IntPtr.Zero);

            Debug.Log(GL.GetError());
        }

        public void Use(Camera camera)
        {
            GL.DepthMask(false);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Blend);

            skyBoxShader.Use();
            skyBoxShader.SetInt(texture0, 0);

            skyBoxShader.SetMatrix4("model", Matrix4.CreateTranslation(Vector3.Zero) * Matrix4.CreateScale(1000),false);
            skyBoxShader.SetMatrix4("projection", camera.GetProjectionMatrix(),false);
            skyBoxShader.SetMatrix4("view", camera.GetViewMatrix(),true);
            
            GL.BindVertexArray(vaoID);
            cubeMap.Bind();

            GL.EnableVertexAttribArray(0);

            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 36);

            GL.DisableVertexAttribArray(0);

            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.DepthMask(true);

            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(vaoID);
            GL.DeleteBuffer(vboID);
            GC.SuppressFinalize(this);
        }

    }
}
