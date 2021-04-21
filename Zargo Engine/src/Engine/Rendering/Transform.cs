﻿
using OpenTK.Mathematics;

namespace ZargoEngine.Rendering
{
    public class Transform
    {
        public Vector3 position = Vector3.Zero;

        private Vector3 _eulerAngels;
        public Vector3 eulerAngles
        {
            get
            {
                return _eulerAngels;
            }
            set
            {
                _eulerAngels = value;
                rotation = Quaternion.FromEulerAngles(value);
            }
        }

        public Quaternion rotation;

        public float scale = 1;

        public Transform(Vector3 position = new Vector3(), Vector3 rotation = new Vector3(), float scale = 1)
        {
            this.position = position;
            this.eulerAngles = rotation;
            this.scale = scale;
        }

        public Matrix4 GetTranslation()
        {
            return Matrix4.Transpose(Matrix4.CreateScale(scale) *
                   Matrix4.CreateTranslation(position)          *
                   Matrix4.CreateRotationX  (eulerAngles.X)     *
                   Matrix4.CreateRotationY  (eulerAngles.Y)     *
                   Matrix4.CreateRotationZ  (eulerAngles.Z));
        }
    }
}
