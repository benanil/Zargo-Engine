
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MiddleGames.Engine
{
	[StructLayout(LayoutKind.Explicit)]
    public struct Vertex
    {
		[FieldOffset(0)]
		public Vector3 aPosition;
		[FieldOffset(1)]
		public Vector3 aNormal;
		[FieldOffset(2)]
		public Vector2 aTexCoord;
    }

    public class Mesh
	{
 		private struct Face
		{
			public int vp1, vp2, vp3;  // vertex indices
			public int uv1, uv2, uv3; // uv indices
			public int vn1, vn2, vn3; // normal indices
		}

		private const int POS_SIZE = 3;
		private const int NORM_SIZE = 3;
		private const int TEXCOORD_SIZE = 2;

		private const int POS_OFFSET = 0;
		private const int NORMAL_OFFSET = POS_OFFSET + POS_SIZE * sizeof(float);
		private const int TEXCOORD_OFFSET = NORMAL_OFFSET + NORM_SIZE * sizeof(float); 

		private readonly List<int> indices = new List<int>();

		private readonly List<Vector2> textureVertices = new List<Vector2>();
		private readonly List<Vector3> vertices        = new List<Vector3>();
		private readonly List<Vector3> normals         = new List<Vector3>();
		private readonly List<Vertex>  modelData       = new List<Vertex>();

		private int eboID;
		private int vaoId;
		private int vboId;

		public Mesh(string filename)
		{
			int numTriangle = 0;

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
						break;
					case "vn":
						normals.Add(new Vector3(Convert.ToSingle(tokens[1]), Convert.ToSingle(tokens[2]), Convert.ToSingle(tokens[3])));
						break;
					case "vt":
						textureVertices.Add(new Vector2(Convert.ToSingle(tokens[1]), Convert.ToSingle(tokens[2])));
			
						break;
					case "f":
						string[] point0 = tokens[1].Split('/');
						string[] point1 = tokens[2].Split('/');
						string[] point2 = tokens[3].Split('/');

						Face face = new ()
						{
							vp1  = Convert.ToInt32(point0[0]) - 1,
							uv1 = Convert.ToInt32(point0[1]) - 1,
							vn1 = Convert.ToInt32(point0[2]) - 1,
							 
							vp2  = Convert.ToInt32(point1[0]) - 1,
							uv2 = Convert.ToInt32(point1[1]) - 1,
							vn2 = Convert.ToInt32(point1[2]) - 1,

							vp3  = Convert.ToInt32(point2[0]) - 1,
							uv3 = Convert.ToInt32(point2[1]) - 1,
							vn3 = Convert.ToInt32(point2[2]) - 1
						};

						modelData.Add(new Vertex() { aNormal = normals[face.vn1], aPosition = vertices[face.vp1], aTexCoord = textureVertices[face.uv1]});
						modelData.Add(new Vertex() { aNormal = normals[face.vn2], aPosition = vertices[face.vp2], aTexCoord = textureVertices[face.uv2]});
						modelData.Add(new Vertex() { aNormal = normals[face.vn3], aPosition = vertices[face.vp3], aTexCoord = textureVertices[face.uv3]});

						indices.Add(face.vp1);
						indices.Add(face.vp2);
						indices.Add(face.vp3);

						numTriangle += 1;

						break;
				}
			}

			Console.Write("done\n");

			LoadVbo();
			DebugMesh();
		}

		public void DebugMesh()
		{
			Console.Write("Vertices\n");
			Console.Write("------------------\n");
			for (int i = 0; i < vertices.Count; i++)
			{
				Console.WriteLine(vertices[i]);
			}

			Console.Write("normals\n");
			Console.Write("------------------\n");
			for (int i = 0; i < normals.Count; i++)
			{
				Console.WriteLine(normals[i]);
			}

			Console.Write("indices\n");
			Console.Write("------------------\n");
			for (int i = 0; i < indices.Count; i++)
			{
				int queue = 0;
				if (i != 0)
					queue = i % 3;

				if (queue == 0)
				{
					Console.Write("\n");
				}
				Console.Write(indices[i].ToString() + " ");
			}
			Console.Write("\n");

			Console.Write("texture vertices\n");
			Console.Write("------------------\n");
			for (int i = 0; i < textureVertices.Count; i++)
			{
				Console.WriteLine(textureVertices[i].ToString() );
			}
		}

		// Loops the value t, so that it is never larger than length and never smaller than 0.
		public static float Repeat(float t, float length)
		{
			return Math.Clamp(t - MathF.Floor(t / length) * length, 0.0f, length);
		}

		private void LoadVbo()
		{
			// VAO
			GL.GenVertexArrays(1, out vaoId);
			GL.BindVertexArray(vaoId);
			Console.WriteLine(GL.GetError());

			// Create and upload indices buffer
			GL.GenBuffers(1, out eboID);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(int) * indices.Count), indices.ToArray(), BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
			Console.WriteLine(GL.GetError());

			// Allocate space for vertexes
			GL.GenBuffers(1, out vboId);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf(default(Vertex)) * modelData.Count), modelData.ToArray(), BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
			Console.WriteLine(GL.GetError());

			const int LAYOUT_MAX_INDEX = POS_SIZE + NORM_SIZE + TEXCOORD_SIZE * sizeof(float); // VERTEX_SIZE_BYTES in largo engine

			// Enable the buffer attribute pointers
			GL.VertexAttribPointer(0, POS_SIZE, VertexAttribPointerType.Float, false, LAYOUT_MAX_INDEX, POS_OFFSET);          // layout 0
			GL.EnableVertexAttribArray(0);

			GL.VertexAttribPointer(1, NORM_SIZE, VertexAttribPointerType.Float, false, LAYOUT_MAX_INDEX, NORMAL_OFFSET);      // layout 1
			GL.EnableVertexAttribArray(1);

			GL.VertexAttribPointer(2, TEXCOORD_SIZE, VertexAttribPointerType.Float, false, LAYOUT_MAX_INDEX, TEXCOORD_OFFSET);// layout 2
			GL.EnableVertexAttribArray(2);

			// unbind
			GL.BindVertexArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		}

		public void Draw()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
			GL.BindVertexArray(vaoId);

			GL.EnableVertexAttribArray(0); // position in
			GL.EnableVertexAttribArray(1); // normal   in
			GL.EnableVertexAttribArray(2); // TexCoord in

			//GL.DrawElements(PrimitiveType.Triangles,indices.Count, DrawElementsType.UnsignedInt,indices.ToArray());

			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);
			GL.DisableVertexAttribArray(2);

			// unbind
			GL.BindVertexArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		}
	}
}
