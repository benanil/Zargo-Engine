using OpenTK.Mathematics;

namespace ZargoEngine.Rendering
{
    public class SubMesh
    {

        private Vector3[] _positions;
        public Vector3[] Positions
        {
            get{
                if (_positions == null) _positions = GetPositions();
                return _positions;
            }
            set{
                _positions = value;
            }
        }

        private Vector2[] _texCoords;
        public Vector2[] TexCoords
        {
            get{
                if (_texCoords == null) _texCoords = GetTextureCoords();
                return _texCoords;
            }
            set{
                _texCoords = value;
            }
        }

        private Vector3[] _normals;
        public Vector3[] Normals
        {
            get{
                if (_normals == null) _normals = GetPositions();
                return _normals;
            }
            set{
                _normals = value;
            }
        }

        private int[] _indices;

        public int[] Indices
        {
            get{
                if (_indices == null) _indices = GetIndices();
                return _indices;
            }
            set{
                _indices = value;
            }
        }
        
        public virtual Vector3[] GetPositions(){
            return Positions;
        }

        public virtual Vector2[] GetTextureCoords(){
            return TexCoords;
        }

        public virtual int[] GetIndices(int offset = 0){// Indice count
            return Indices;
        }
    }
}
