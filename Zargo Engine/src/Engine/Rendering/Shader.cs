
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.IO;

namespace MiddleGames.Engine.Rendering
{
    public class Shader : IDisposable
    {

        private readonly int program; // tutorial handle deniyor
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexShaderSource;
            string fragmentShaderSource;

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
            program = GL.CreateProgram();

            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);

            GL.LinkProgram(program);

            Console.WriteLine("shader compiled sucsesfully");

            // clear memory
            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        public void Use()
        {
            GL.UseProgram(program);
        }

        public void Detach()
        {
            GL.UseProgram(0);
        }

        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(program, name);
        }

        // Uniforms

        public void SetInt(string name, int value){
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform1(location, value);
        }

        public void SetFloat(string name, float value){
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform1(location, value);
        }

        public void SetVector2(string name, Vector2 value){
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform2(location, value);
        }

        public void SetVector3(string name, Vector3 value){
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform3(location, value);
        }

        public void SetVector4(string name, Vector4 value){
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform4(location, value);
        }

        public void SetMatrix4(string name, Matrix4 value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.UniformMatrix4(location,true, ref value);
        }

        // disposing

        ~Shader()
        {
            GL.DeleteProgram(program);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue){
                GL.DeleteProgram(program);
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
