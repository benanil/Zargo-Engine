
using OpenTK.Mathematics;
using ZargoEngine.Editor;

namespace ZargoEngine
{
    public class FirstBehaviour : MonoBehaviour
    {
        public int denemeInt;
        public float denemeFloat;
        public string deneme;
        public bool denemeBool;
        [Drag(1,0,50)]
        public Vector3 denemeVector;
        public Color4 denemeColor;

        public FirstBehaviour()
        {
            name = "First Behaviour";
        }
    }
}