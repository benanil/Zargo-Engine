

using OpenTK.Mathematics;

namespace ZargoEngine
{
    public class Transform
    {
        public Matrix4 Translation
        {
            get
            {
                return Matrix4.CreateScale(scale) * Matrix4.CreateTranslation(position);
            }
        }

        public Vector3 position;
        public Quaternion rotation;
        public float scale = 2;

        public Transform(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation; // arono
        }

        public Transform(Vector3 position, Quaternion rotation,float scale)
        {
            this.position = position;
            this.rotation = rotation; // arono
            this.scale = scale;
        }

        public Transform()
        {
            this.position = Vector3.Zero;
            this.rotation = Quaternion.Identity; // arono
        }

    }
}
