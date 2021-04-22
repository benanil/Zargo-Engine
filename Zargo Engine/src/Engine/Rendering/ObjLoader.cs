
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZargoEngine.Rendering
{
    public static class ObjLoader
    {
        public static void Load(string path, ref List<Tuple<Vertex,Vertex,Vertex>> faces)
        {
            List<string> lines = new List<string>(File.ReadAllText(path).Split('\n'));

            List<Tuple<TempVertex, TempVertex, TempVertex>> faceInds = new List<Tuple<TempVertex, TempVertex, TempVertex>>();

            List<Vector3> verts   = new List<Vector3>();
            List<Vector2> texs    = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            // Base values
            verts.Add(new Vector3());
            texs.Add(new Vector2());
            normals.Add(new Vector3());

            // Read file line by line
            foreach (String line in lines)
            {
                if (line.StartsWith("v ")) // Vertex definition
                {
                    // Cut off beginning of line
                    String temp = line[2..];

                    Vector3 vec = new Vector3();

                    if (temp.Trim().Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        String[] vertparts = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], out vec.X);
                        success     |= float.TryParse(vertparts[1], out vec.Y);
                        success     |= float.TryParse(vertparts[2], out vec.Z);

                        // If any of the parses failed, report the error
                        if (!success) Debug.LogError($"Error parsing vertex: {line}");
                    }
                    else{
                        Debug.LogError($"Error parsing vertex: {line}");
                    }

                    verts.Add(vec);
                }
                else if (line.StartsWith("vt ")) // Texture coordinate
                {
                    // Cut off beginning of line
                    String temp = line[2..];

                    Vector2 vec = new Vector2();

                    if (temp.Trim().Any((char c) => c == ' ')) // Check if there's enough elements for a vertex
                    {
                        String[] texcoordparts = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(texcoordparts[0], out vec.X);
                        success     |= float.TryParse(texcoordparts[1], out vec.Y);

                        // If any of the parses failed, report the error
                        if (!success) Debug.LogError($"Error parsing texture coordinate: {line}");
                    }
                    else
                    {
                        Debug.LogError($"Error parsing texture coordinate: {line}");
                    }

                    texs.Add(vec);
                }
                else if (line.StartsWith("vn ")) // Normal vector
                {
                    // Cut off beginning of line
                    String temp = line[2..];

                    Vector3 vec = new Vector3();

                    if (temp.Trim().Count((char c) => c == ' ') == 2) // Check if there's enough elements for a normal
                    {
                        String[] vertparts = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], out vec.X);
                        success |= float.TryParse(vertparts[1], out vec.Y);
                        success |= float.TryParse(vertparts[2], out vec.Z);

                        // If any of the parses failed, report the error
                        if (!success){
                            Console.WriteLine("Error parsing normal: {0}", line);
                        }
                    }
                    else{
                        Console.WriteLine("Error parsing normal: {0}", line);
                    }

                    normals.Add(vec);
                }
                else if (line.StartsWith("f ")) // Face definition
                {
                    // Cut off beginning of line
                    String temp = line[2..];

                    Tuple<TempVertex, TempVertex, TempVertex> face = new Tuple<TempVertex, TempVertex, TempVertex>(new TempVertex(), new TempVertex(), new TempVertex());

                    if (temp.Trim().Count((char c) => c == ' ') == 2) // Check if there's enough elements for a face
                    {
                        String[] faceparts = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        int t1, t2, t3;
                        int n1, n2, n3;

                        // Attempt to parse each part of the face
                        bool success = int.TryParse(faceparts[0].Split('/')[0], out int v1);
                        success     |= int.TryParse(faceparts[1].Split('/')[0], out int v2);
                        success     |= int.TryParse(faceparts[2].Split('/')[0], out int v3);

                        if (faceparts[0].Count((char c) => c == '/') >= 2){
                            success |= int.TryParse(faceparts[0].Split('/')[1], out t1);
                            success |= int.TryParse(faceparts[1].Split('/')[1], out t2);
                            success |= int.TryParse(faceparts[2].Split('/')[1], out t3);
                            success |= int.TryParse(faceparts[0].Split('/')[2], out n1);
                            success |= int.TryParse(faceparts[1].Split('/')[2], out n2);
                            success |= int.TryParse(faceparts[2].Split('/')[2], out n3);
                        }
                        else
                        {
                            if (texs.Count > v1 && texs.Count > v2 && texs.Count > v3){
                                t1 = v1;
                                t2 = v2;
                                t3 = v3;
                            }
                            else{
                                t1 = 0;
                                t2 = 0;
                                t3 = 0;
                            }

                            if (normals.Count > v1 && normals.Count > v2 && normals.Count > v3){
                                n1 = v1;
                                n2 = v2;
                                n3 = v3;
                            }
                            else{
                                n1 = 0;
                                n2 = 0;
                                n3 = 0;
                            }
                        }

                        // If any of the parses failed, report the error
                        if (!success) Debug.LogError($"Error parsing face: {line}");
                        else
                        {
                            TempVertex tv1 = new TempVertex(v1, n1, t1);
                            TempVertex tv2 = new TempVertex(v2, n2, t2);
                            TempVertex tv3 = new TempVertex(v3, n3, t3);
                            face = new Tuple<TempVertex, TempVertex, TempVertex>(tv1, tv2, tv3);
                            faceInds.Add(face);
                        }
                    }
                    else
                    {
                        Debug.LogError($"Error parsing face: {line}");
                    }
                }
            }

            foreach (var face in faceInds)
            {
                Vertex v1 = new Vertex(verts[face.Item1.Vertex],texs[face.Item1.Texcoord], normals[face.Item1.Normal]);
                Vertex v2 = new Vertex(verts[face.Item2.Vertex],texs[face.Item2.Texcoord], normals[face.Item2.Normal]);
                Vertex v3 = new Vertex(verts[face.Item3.Vertex],texs[face.Item3.Texcoord], normals[face.Item3.Normal]);

                faces.Add(new Tuple<Vertex, Vertex, Vertex>(v1, v2, v3));
            }
        }

        private struct TempVertex
        {
            public int Vertex;
            public int Normal;
            public int Texcoord;

            public TempVertex(int vert = 0, int norm = 0, int tex = 0)
            {
                Vertex = vert;
                Normal = norm;
                Texcoord = tex;
            }
        }
    }
}
