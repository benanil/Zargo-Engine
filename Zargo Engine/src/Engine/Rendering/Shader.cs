
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.IO;

namespace MiddleGames.Engine.Rendering
{
    public class Shader : IDisposable
    {
        public readonly int program; // tutorialda handle deniyor

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
                Game.instance.Close();
                return;
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
                Game.instance.Close();
                return;
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
        public static int PoropertyToId(in int program, string name) => GL.GetUniformLocation(program, name);
        public int        PoropertyToId(in string name)              => GL.GetUniformLocation(program, name);

        // Uniforms
        public static void SetInt    (in int location, in int     value) => GL.Uniform1(location, value);
        public static void SetFloat  (in int location, in float   value) => GL.Uniform1(location, value);
        public static void SetColor  (in int location, in Color4  value) => GL.Uniform4(location, value);
        public static void SetVector2(in int location, in Vector2 value) => GL.Uniform2(location, value);
        public static void SetVector3(in int location, in Vector3 value) => GL.Uniform3(location, value);
        public static void SetVector4(in int location, in Vector4 value) => GL.Uniform4(location, value);
        public static void SetMatrix4(in int location, Matrix4 value) => GL.UniformMatrix4(location, true, ref value);
        
        public void SetInt    (in string name, in int     value)  => GL.Uniform1(GL.GetUniformLocation(program, name), value);
        public void SetFloat  (in string name, in float   value)  => GL.Uniform1(GL.GetUniformLocation(program, name), value);
        public void SetColor  (in string name, in Color4  value)  => GL.Uniform4(GL.GetUniformLocation(program, name), value);
        public void SetVector2(in string name, in Vector2 value)  => GL.Uniform2(GL.GetUniformLocation(program, name), value);
        public void SetVector3(in string name, in Vector3 value)  => GL.Uniform3(GL.GetUniformLocation(program, name), value);
        public void SetVector4(in string name, in Vector4 value)  => GL.Uniform4(GL.GetUniformLocation(program, name), value);
        public void SetMatrix4(in string name, Matrix4 value)  => GL.UniformMatrix4(GL.GetUniformLocation(program, name), true, ref value);

        // disposing

        public void Dispose(){
            GL.DeleteProgram(program);
            GC.SuppressFinalize(this);
        }
    }
}
