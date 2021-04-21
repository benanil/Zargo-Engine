
using OpenTK.Mathematics;
using ZargoEngine.Rendering;

namespace ZargoEngine.Core
{
    public class MeshCreator : NativeSingleton<MeshCreator>
    {
        /*
        readonly Vertex[] cubeVertexes = {
            new Vertex(new Vector3( 0.5f,  0.5f, 0.0f), new Vector2(1, 1)),
            new Vertex(new Vector3( 0.5f, -0.5f, 0.0f), new Vector2(1, 0)),
            new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector2(0, 0)),
            new Vertex(new Vector3(-0.5f,  0.5f, 0.0f), new Vector2(0, 1)),

            new Vertex(new Vector3( 0.5f,  0.5f, 0.5f), new Vector2(1, 1)),
            new Vertex(new Vector3( 0.5f, -0.5f, 0.5f), new Vector2(1, 0)),
            new Vertex(new Vector3(-0.5f, -0.5f, 0.5f), new Vector2(0, 0)),
            new Vertex(new Vector3(-0.5f,  0.5f, 0.5f), new Vector2(0, 1)),
        };
        */

        readonly Vertex[] cubeVertexes = {
            new Vertex(new Vector3(-0.8f, -0.8f, -0.8f), new Vector2(0, 0)),
            new Vertex(new Vector3( 0.8f, -0.8f, -0.8f), new Vector2(0, 0)),
            new Vertex(new Vector3( 0.8f,  0.8f, -0.8f), new Vector2(0, 0)),
            new Vertex(new Vector3(-0.8f,  0.8f, -0.8f), new Vector2(0, 0)),
          //                                                            
            new Vertex(new Vector3(-0.8f, -0.8f,  0.8f), new Vector2(0, 0)),
            new Vertex(new Vector3( 0.8f, -0.8f,  0.8f), new Vector2(0, 0)),
            new Vertex(new Vector3( 0.8f,  0.8f,  0.8f), new Vector2(0, 0)),
            new Vertex(new Vector3(-0.8f,  0.8f,  0.8f), new Vector2(0, 0)),
        };

        readonly Vertex[] quadVertexes = {
            new Vertex(new Vector3( 0.5f,  0.5f, 0.0f), new Vector2(1, 1)),
            new Vertex(new Vector3( 0.5f, -0.5f, 0.0f), new Vector2(1, 0)),
            new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector2(0, 0)),
            new Vertex(new Vector3(-0.5f,  0.5f, 0.0f), new Vector2(0, 1)),
        };

        readonly uint[] quadIndices = {
             0, 1, 3, // first triangle
             1, 2, 3 // second triangle
        };

        readonly uint[] cubeIndices = {
              //front
              0, 7, 3,
              0, 4, 7,
              //back
              1, 2, 6,
              6, 5, 1,
              //left
              0, 2, 1,
              0, 3, 2,
              //right
              4, 5, 6,
              6, 7, 4,
              //top
              2, 3, 6,
              6, 3, 7,
              //bottom
              0, 1, 5,
              0, 5, 4
        };

        private Mesh cube;
        private Mesh quad;

        /// <summary>
        /// creates a Cube
        /// </summary>
        public static Mesh CreateCube()
        {
            if (instance.cube == null){
                instance.cube = new Mesh(instance.cubeVertexes, instance.cubeIndices);
            }
            return instance.cube;
        }

        /// <summary>
        /// creates a quad
        /// </summary>
        public static Mesh CreateQuad()
        {
            if (instance.quad == null){
                instance.quad = new Mesh(instance.quadVertexes, instance.quadIndices);
            }

            return instance.quad;
        }

    }
}
