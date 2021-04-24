
using OpenTK.Mathematics;

namespace ZargoEngine.Rendering
{
    public abstract class Light : Component
    {
        public float range = 10;
        public float intensity = 5;
        public Vector3 position = new Vector3(0,200,100);
        public Color4 color = new (byte.MaxValue, byte.MaxValue,0,byte.MaxValue);

        protected Light()
        {
            name = "Light";
        }

        protected Light(float range, float intensity, Vector3 position, Color4 color)
        {
            name = "Light";
            this.range = range;
            this.intensity = intensity;
            this.position = position;
            this.color = color;
        }
    }
}
