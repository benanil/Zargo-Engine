
using OpenTK.Graphics.ES30;
using System;
using System.IO;

namespace MiddleGames.Engine.Rendering
{
    public class Shader : IDisposable
    {

        private readonly int Handle;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexShaderSource;
            string fragmentShaderSource;
            string shaderLog;

            using (StreamReader reader = new StreamReader(vertexPath))
            {
                 vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(fragmentPath))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            // create vertex shader
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int isCompiled);
            
            // check errors
            if (isCompiled == 0){
                GL.GetShaderInfoLog(vertexShader, out string info);
                Console.WriteLine("failed to compile vertex shader: " + info);
            }

            // create Fragment shader
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out isCompiled);

            // check errors
            if (isCompiled == 0){
                GL.GetShaderInfoLog(vertexShader, out string info);
                Console.WriteLine("failed to compile vertex shader: " + info);
            }

            // link Shaders
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);

            // clear memory
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        // disposing

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue){
                GL.DeleteProgram(Handle);
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
