
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZargoEngine.Rendering
{
    public static class ObjLoader
    {
        public static void Load(string path, ref List<Vector3> verts, ref List<Vector2> texs, ref int[] indices)
        {
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();

            List<String> lines = new List<string>(File.ReadAllText(path).Split('\n'));

            foreach (String line in lines)
            {
                if (line.StartsWith("v ")) // Vertex definition
                {
                    // Cut off beginning of line
                    String temp = line[2..];

                    Vector3 vec = new Vector3();

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        String[] vertparts = temp.Split(' ');

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], out vec.X);
                        success &= float.TryParse(vertparts[1], out vec.Y);
                        success &= float.TryParse(vertparts[2], out vec.Z);

                        // Dummy color/texture coordinates for now
                        texs.Add(new Vector2((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));

                        // If any of the parses failed, report the error
                        if (!success) Console.WriteLine("Error parsing vertex: {0}", line);
                    }

                    verts.Add(vec);
                }
                else if (line.StartsWith("f ")) // Face definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a face
                    {
                        String[] faceparts = temp.Split(' ');


                        // Attempt to parse each part of the face
                        bool success = int.TryParse(faceparts[0], out int i1);
                        success &= int.TryParse(faceparts[1],     out int i2);
                        success &= int.TryParse(faceparts[2],     out int i3);

                        // If any of the parses failed, report the error
                        if (!success) Console.WriteLine("Error parsing face: {0}", line);
                        else{
                            // Decrement to get zero-based vertex numbers
                            face = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                            faces.Add(face);
                        }
                    }
                }
            }
            indices = GetIndices(faces);
        }
        
        /// <summary>
        /// Get indices to draw this object
        /// </summary>
        /// <param name="offset">Number of vertices buffered before this object</param>
        /// <returns>Array of indices with offset applied</returns>
        private static int[] GetIndices(List<Tuple<int, int, int>> faces, int offset = 0)
        {
            List<int> temp = new List<int>();

            foreach (var face in faces){
                temp.Add(face.Item1 + offset);
                temp.Add(face.Item2 + offset);
                temp.Add(face.Item3 + offset);
            }

            return temp.ToArray();
        }
    }
}
