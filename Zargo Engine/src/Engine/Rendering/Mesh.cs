
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MiddleGames.Engine
{

    public struct Vertex
    {
        public Vector3 pos, norm;
        public static readonly int Stride = Marshal.SizeOf(default(Vertex));
    }

    struct Face
    {
        public int v1, v2, v3;    // vertex indices
		public int uv1, uv2, uv3; // uv indices
		public int vn1, vn2, vn3; // normal indices
    }

    public class Mesh
	{

		private const int POS_SIZE = 3;
		private const int TexCoord_SIZE = 2;

		private const int POS_OFFSET = 0;
		private const int TexCoord_OFFSET = POS_OFFSET + POS_SIZE * sizeof(float);

		private readonly List<int> indices = new List<int>();

		private readonly List<Vector2> textureVertices = new List<Vector2>();
		private readonly List<Vector3> vertices        = new List<Vector3>();
		private readonly List<Vector3> normals         = new List<Vector3>();
		private readonly List<Vertex>  modelData       = new List<Vertex>();

		private int iboId;
		private int vaoId;
		private int vboId;

		public Mesh(string filename)
		{
			Load(filename);
		}

		// Load and parse the .OBJ file
		private int Load(string filename)
		{
			int numVertices = 0;
			int numNormals = 0;
			int numFaces = 0;

			Console.Write("Loading model '" + filename + "'... ");

			string[] lines;

			using (StreamReader reader = new StreamReader(filename))
            {
				lines = reader.ReadToEnd().Split('\n');
            }

			foreach (string line in lines)
			{
				string[] tokens = line.Split(' ');

				switch (tokens[0])
				{
					case "v":
						vertices.Add(new Vector3(Convert.ToSingle(tokens[1]), Convert.ToSingle(tokens[2]), Convert.ToSingle(tokens[3])));
						numVertices++;
						break;
					case "vn":
						normals.Add(new Vector3(Convert.ToSingle(tokens[1]), Convert.ToSingle(tokens[2]), Convert.ToSingle(tokens[3])));
						numNormals++;
						break;
					case "vt":
						textureVertices.Add(new Vector2(float.Parse(tokens[1]), float.Parse(tokens[2])));
						break;
					case "f":
						string[] point0 = tokens[1].Split('/');
						string[] point1 = tokens[2].Split('/');
					    string[] point2 = tokens[3].Split('/');

						Face face = new Face()
						{
							v1  = Convert.ToInt32(point0[0]) - 1,
							uv1 = Convert.ToInt32(point0[1]) - 1,
							vn1 = Convert.ToInt32(point0[2]) - 1,

							v2  = Convert.ToInt32(point1[0]) - 1,
							uv2 = Convert.ToInt32(point0[1]) - 1,
							vn2 = Convert.ToInt32(point1[2]) - 1,

							v3  = Convert.ToInt32(point2[0]) - 1,
							uv3 = Convert.ToInt32(point0[1]) - 1,
							vn3 = Convert.ToInt32(point2[2]) - 1
						};

						modelData.Add(new Vertex() { norm = normals[face.vn1], pos = vertices[face.v1] });
						modelData.Add(new Vertex() { norm = normals[face.vn2], pos = vertices[face.v2] });
						modelData.Add(new Vertex() { norm = normals[face.vn3], pos = vertices[face.v3] });

						indices.Add(face.v1);
						indices.Add(face.v2);
						indices.Add(face.v3);

						numFaces += 3;

						break;
				}
			}

			loadVbo();

			Console.Write("done\n");

			Console.WriteLine("numVertices" + numVertices);
			Console.WriteLine("numNormals" + numNormals);
			Console.WriteLine("numFaces" + numFaces);

			return 0;
		}

		private void loadVbo()
		{
			// Vertex index data
			GL.GenBuffers(1, out iboId);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboId);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(int) * indices.Count), indices.ToArray(), BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboId);
			Console.WriteLine(GL.GetError());

			// Vertex position data
			GL.GenBuffers(1, out vboId);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.Stride * modelData.Count), modelData.ToArray(), BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
			Console.WriteLine(GL.GetError());

			// VAO
			GL.GenVertexArrays(1, out vaoId);
			GL.BindVertexArray(vaoId);

			const int LAYOUT_MAX_INDEX = 5 * sizeof(float); // VERTEX_SIZE_BYTES in largo engine
			
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
			GL.VertexAttribPointer(0, POS_SIZE, VertexAttribPointerType.Float, false, LAYOUT_MAX_INDEX, POS_OFFSET);     // layout 0
			GL.VertexAttribPointer(1, TexCoord_SIZE, VertexAttribPointerType.Float, false, LAYOUT_MAX_INDEX, TexCoord_OFFSET);// layout 1

			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.DisableVertexAttribArray(2);
			GL.DisableVertexAttribArray(3);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboId);

			GL.BindVertexArray(0);
			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);
			GL.EnableVertexAttribArray(3);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		}

		public void Draw()
		{
			GL.BindVertexArray(vaoId);

			GL.EnableVertexAttribArray(0); // position Layout
			GL.EnableVertexAttribArray(1); // TexCoord Layout

			GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt,indices.ToArray());

			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);

			GL.BindVertexArray(0);
		}
	}
}
