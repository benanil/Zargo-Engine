
using OpenTK.Mathematics;
using ZargoEngine.Core;

namespace ZargoEngine.Rendering
{
    public class MeshCreator : NativeSingleton<MeshCreator>
    {
        readonly Vector3[] cubeVertexes = {
            new Vector3(-0.5f, -0.5f,  -0.5f),
            new Vector3(0.5f, 0.5f,  -0.5f),
            new Vector3(0.5f, -0.5f,  -0.5f),
            new Vector3(-0.5f, 0.5f,  -0.5f),
 
            //back
            new Vector3(0.5f, -0.5f,  -0.5f),
            new Vector3(0.5f, 0.5f,  -0.5f),
            new Vector3(0.5f, 0.5f,  0.5f),
            new Vector3(0.5f, -0.5f,  0.5f),
 
            //right
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3(0.5f, -0.5f,  0.5f),
            new Vector3(0.5f, 0.5f,  0.5f),
            new Vector3(-0.5f, 0.5f,  0.5f),
 
            //top
            new Vector3(0.5f, 0.5f,  -0.5f),
            new Vector3(-0.5f, 0.5f,  -0.5f),
            new Vector3(0.5f, 0.5f,  0.5f),
            new Vector3(-0.5f, 0.5f,  0.5f),
 
            //front
            new Vector3(-0.5f, -0.5f,  -0.5f),
            new Vector3(-0.5f, 0.5f,  0.5f),
            new Vector3(-0.5f, 0.5f,  -0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
 
            //bottom
            new Vector3(-0.5f, -0.5f,  -0.5f),
            new Vector3(0.5f, -0.5f,  -0.5f),
            new Vector3(0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f)
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

        //f 5/1/1  3/2/1  1/3/1
        //f 3/2/2  8/4/2  4/5/2
        //f 7/6/3  6/7/3  8/8/3
        //f 2/9/4  8/10/4 6/11/4
        //f 1/3/5  4/5/5  2/9/5
        //f 5/12/6 2/9/6  6/7/6
        //f 5/1/1  7/13/1 3/2/1
        //f 3/2/2  7/14/2 8/4/2
        //f 7/6/3  5/12/3 6/7/3
        //f 2/9/4  4/5/4  8/10/4
        //f 1/3/5  3/2/5  4/5/5
        //f 5/12/6 1/3/6  2/9/6

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
            //left
            0,1,2,0,3,1,
 
            //back
            4,5,6,4,6,7,
 
            //right
            8,9,10,8,10,11,
 
            //top
            13,14,12,13,15,14,
 
            //front
            16,17,18,16,19,17,
 
            //bottom
            20,21,22,20,22,23
        };

        private Mesh cube;
        private Mesh quad;

        /// <summary>
        /// creates a Cube
        /// </summary>
        public static Mesh CreateCube()
        {
            if (instance.cube == null){
                instance.cube = new Mesh(instance.cubeVertexes, instance.inds, instance.cubeCoords);
            }
            return instance.cube;
        }

        /// <summary>
        /// creates a quad
        /// </summary>
        public static Mesh CreateQuad()
        {
            if (instance.quad == null){
                instance.quad = new Mesh(instance.quadVertexes, instance.quadIndices,instance.quadCoords);
            }

            return instance.quad;
        }

    }
}
