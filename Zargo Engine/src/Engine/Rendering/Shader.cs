
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.IO;

namespace ZargoEngine.Rendering
{
    public class Shader : IDisposable
    {
        private readonly int program;

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexSource = string.Empty;
            string fragmentSource = string.Empty;

            using (StreamReader reader = new StreamReader(vertexPath)){
                vertexSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(fragmentPath)){
                fragmentSource = reader.ReadToEnd();
            }

            int vertexID = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vertexID, vertexSource);
            GL.CompileShader(vertexID);
            Console.WriteLine(GL.GetError());

            GL.GetShader(vertexID, ShaderParameter.CompileStatus, out int log);

            if (log == 0){
                GL.GetShaderInfoLog(vertexID, out string infoLog);
                Console.WriteLine(infoLog);
                return;
            }

            int fragmentID = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(fragmentID, fragmentSource);
            GL.CompileShader(fragmentID);

            Console.WriteLine(GL.GetError());

            GL.GetShader(vertexID, ShaderParameter.CompileStatus, out log);

            if (log == 0){
                GL.GetShaderInfoLog(fragmentID, out string infoLog);
                Console.WriteLine(infoLog);
                return;
            }

            program = GL.CreateProgram();
            GL.AttachShader(program, vertexID);
            GL.AttachShader(program, fragmentID);

            GL.LinkProgram(program);

            Console.WriteLine(GL.GetError());

            GL.GetShader(program,ShaderParameter.CompileStatus,out log);

            if (log == 0){
                GL.GetShaderInfoLog(program, out string infoLog);
                Console.WriteLine(infoLog);
                return;
            }

            Console.WriteLine("Shader compiled sucsesfully");

            GL.DetachShader(program, vertexID);
            GL.DetachShader(program, fragmentID);
            GL.DeleteShader(vertexID);
            GL.DeleteShader(fragmentID);
        }

        public void Use()
        {
            GL.UseProgram(program);
        }
        
        public static void DetachShader()
        {
            GL.UseProgram(0);
        }

        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(program, name);
        }

        // Uniforms

        public int GetUniformLocation( string name)
        {
            return GL.GetUniformLocation(program, name);
        }

        public static int GetUniformLocation(int program,string name)
        {
            return GL.GetUniformLocation(program, name);
        }

        public void SetInt    (string name, int value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform1(location, value);
        }
        public void SetFloat  (string name, float value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform1(location, value);
        }
        public void SetVector2(string name, Vector2 value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform2(location, value);
        }
        public void SetVector3(string name, Vector3 value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform3(location, value);
        }
        public void SetVector4(string name, Vector4 value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform4(location, value);
        }
        public void SetMatrix4(string name, Matrix4 value)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.UniformMatrix4(location, true, ref value);
        }
        
        public static void SetInt(int location, int value)         => GL.Uniform1(location, value);
        public static void SetFloat(int location, float value)     => GL.Uniform1(location, value);
        public static void SetVector2(int location, Vector2 value) => GL.Uniform2(location, value);
        public static void SetVector3(int location, Vector3 value) => GL.Uniform3(location, value);
        public static void SetVector4(int location, Vector4 value) => GL.Uniform4(location, value);

        public static void SetMatrix4(int location, Matrix4 value, bool transpose = true) => GL.UniformMatrix4(location, transpose, ref value);

        public void Dispose()
        {
            GL.DeleteShader(program);
            GC.SuppressFinalize(this);
        }
    }
}
