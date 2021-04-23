
using ZargoEngine.Core;
using ZargoEngine.AssetManagement;

namespace ZargoEngine.Rendering
{
    public class MeshCreator : NativeSingleton<MeshCreator>
    {
        private Mesh cube;
        private Mesh quad;

        /// <summary>
        /// creates a Cube
        /// </summary>
        public static Mesh CreateCube()
        {
            if (instance.cube == null){
                instance.cube = AssetManager.GetMesh("Models/cube.obj");
            }
            return instance.cube;
        }

        /// <summary>
        /// creates a quad
        /// </summary>
        public static Mesh CreateQuad()
        {
            if (instance.quad == null){
                instance.quad = AssetManager.GetMesh("Models/quad.obj");
            }

            return instance.quad;
        }

    }
}
