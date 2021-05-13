
using OpenTK.Mathematics;
using ZargoEngine.AssetManagement;
using ZargoEngine.Editor;
using ZargoEngine.Sound;

namespace ZargoEngine
{
    public class FirstBehaviour : MonoBehaviour
    {
        [Slider()]
        public int denemeInt;
        public float denemeFloat;
        public string deneme;
        public bool denemeBool;
        [Drag(1,0,50)]
        public Vector3 denemeVector;
        public Color4 denemeColor;

        public AudioClip sound;

        public FirstBehaviour()
        {
            sound = new AudioClip(AssetManager.GetFileLocation("Sounds/Car Engine start.wav"), false);
            name = "First Behaviour";
        }

        [Button()]
        public void FirstMethod()
        {
            Debug.Log("button test");
            sound.Play();
        }

    }
}