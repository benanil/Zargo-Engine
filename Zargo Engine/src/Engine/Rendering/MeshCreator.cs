
using OpenTK.Mathematics;
using ZargoEngine.Core;

namespace ZargoEngine.Rendering
{
    public class MeshCreator : NativeSingleton<MeshCreator>
    {
        readonly Vector3[] cubeVertexes = {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };
        
        readonly uint[] cubeIndices = {
              //front
              5, 3, 1,
              3, 8, 4,
              7, 6, 8,
              2, 8, 6,
              1, 4, 2,
              5, 2, 6,
              5, 7, 3,
              3, 7, 8,
              7, 5, 6,
              2, 4, 8,
              1, 3, 4,
              5, 1, 2
        };

        Vector2[] cubeCoords = new Vector2[] {
            new Vector2(0.0f, 0.0f),
            new Vector2(-1.0f, 1.0f),
            new Vector2(-1.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
 
            // back
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(-1.0f, 1.0f),
            new Vector2(-1.0f, 0.0f),
 
            // right
            new Vector2(-1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(-1.0f, 1.0f),
 
            // top
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(-1.0f, 0.0f),
            new Vector2(-1.0f, 1.0f),
 
            // front
            new Vector2(0.0f, 0.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
 
            // bottom
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(-1.0f, 1.0f),
            new Vector2(-1.0f, 0.0f)
        };

        /*
        readonly Vector3[] cubeVertexes = {
            new Vector3(-0.5f, -0.5f,  -0.5f),
            new Vector3( 0.5f,  0.5f,  -0.5f),
            new Vector3( 0.5f, -0.5f,  -0.5f),
            new Vector3(-0.5f,  0.5f,  -0.5f),
              //back
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f,  0.5f, -0.5f),
            new Vector3(0.5f,  0.5f,  0.5f),
            new Vector3(0.5f, -0.5f,  0.5f),
            //right
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
              //top
            new Vector3( 0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3( 0.5f, 0.5f,  0.5f),
            new Vector3(-0.5f, 0.5f,  0.5f),
            //front
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
              //bottom
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f,  -0.5f, -0.5f),
            new Vector3(0.5f,  -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
        };
        */
        

        readonly Vector3[] quadVertexes = {
            new Vector3( 0.5f,  0.5f, 0.0f),
            new Vector3( 0.5f, -0.5f, 0.0f),
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(-0.5f,  0.5f, 0.0f),
        };

        readonly Vector2[] quadCoords = new Vector2[]
        {
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,0),
            new Vector2(0,1),
        };

        readonly int[] quadIndices = {
             0, 1, 3, // first triangle
             1, 2, 3 // second triangle
        };

        int[] inds = {
            0, 2, 1, //face front
	        0, 3, 2,
            2, 3, 4, //face top
	        2, 4, 5,
            1, 2, 5, //face right
	        1, 5, 6,
            0, 7, 4, //face left
	        0, 4, 3,
            5, 4, 7, //face back
	        5, 7, 6,
            0, 6, 7, //face bottom
	        0, 1, 6
        };

        private Mesh cube;
        private Mesh quad;

        /// <summary>
        /// creates a Cube
        /// </summary>
        [System.Obsolete("dont use for now")]
        public static Mesh CreateCube()
        {
            if (instance.cube == null){
                //instance.cube = new Mesh(instance.cubeVertexes, instance.inds, instance.cubeCoords);
            }
            return instance.cube;
        }

        /// <summary>
        /// creates a quad
        /// </summary>
        [System.Obsolete("dont use for now")]
        public static Mesh CreateQuad()
        {
            if (instance.quad == null){
                //instance.quad = new Mesh(instance.quadVertexes, instance.quadIndices,instance.quadCoords);
            }

            return instance.quad;
        }

    }
}
