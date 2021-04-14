
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.IO;

namespace MiddleGames.Engine.Rendering
{
    public class Shader : IDisposable
    {
        private readonly int program; // tutorialda handle deniyor

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexShaderSource;
            string fragmentShaderSource;

            using (StreamReader reader = new(vertexPath)){ 
                 vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new(fragmentPath)){
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

        public void Use(){
            GL.UseProgram(program);
        }

        public static void Detach(){
            GL.UseProgram(0);
        }

        public int GetAttribLocation(string name) => GL.GetAttribLocation(program, name);

        // property to id
        public static int PoropertyToId(int program, string name) => GL.GetUniformLocation(program, name);
        public int        PoropertyToId(string name)              => GL.GetUniformLocation(program, name);

        // Uniforms
        public static void SetInt    (int location, int     value) => GL.Uniform1(location, value);
        public static void SetFloat  (int location, float   value) => GL.Uniform1(location, value);
        public static void SetVector2(int location, Vector2 value) => GL.Uniform2(location, value);
        public static void SetVector3(int location, Vector3 value) => GL.Uniform3(location, value);
        public static void SetVector4(int location, Vector4 value) => GL.Uniform4(location, value);
        public static void SetColor  (int location, Color4  value) => GL.Uniform4(location, value);
        public static void SetMatrix4(int location, Matrix4 value) => GL.UniformMatrix4(location, true, ref value);

        public void SetInt    (string name, int     value)  => GL.Uniform1(GL.GetUniformLocation(program, name), value);
        public void SetFloat  (string name, float   value)  => GL.Uniform1(GL.GetUniformLocation(program, name), value);
        public void SetVector2(string name, Vector2 value)  => GL.Uniform2(GL.GetUniformLocation(program, name), value);
        public void SetVector3(string name, Vector3 value)  => GL.Uniform3(GL.GetUniformLocation(program, name), value);
        public void SetVector4(string name, Vector4 value)  => GL.Uniform4(GL.GetUniformLocation(program, name), value);
        public void SetColor  (string name, Color4  value)  => GL.Uniform4(GL.GetUniformLocation(program, name), value);
        public void SetMatrix4(string name, Matrix4 value)  => GL.UniformMatrix4(GL.GetUniformLocation(program, name), true, ref value);

        // disposing

        public void Dispose(){
            GL.DeleteProgram(program);
            GC.SuppressFinalize(this);
        }
    }
}
