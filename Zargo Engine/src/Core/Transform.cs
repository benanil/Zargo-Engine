

using OpenTK.Mathematics;

namespace ZargoEngine
{
    public class Transform
    {
        public Matrix4 Translation;
        public Vector3 position;
        public Quaternion rotation;

        public Transform(Matrix4 translation)
        {
            Translation = translation;
            this.position = new Vector3(Translation.Column0.X,
                                        Translation.Column0.Y,
                                        Translation.Column0.Z);

            this.rotation = Quaternion.Identity; // arono
        }

        public Transform(Vector3 position, Quaternion rotation)
        {
            Matrix4.CreateTranslation(position, out Translation);

            this.position = position;
            this.rotation = rotation; // arono
        }

        public Transform()
        {
            Translation = Matrix4.Identity;
            this.position = Vector3.Zero;
            this.rotation = Quaternion.Identity; // arono
        }

    }
}
