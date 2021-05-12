using System;
using System.Linq;
using OpenTK.Audio.OpenAL;

namespace ZargoEngine.Sound
{
    public class AudioManager
    {
        public static ALDevice AudioDevice;
        public static ALContext AudioContext;

        public AudioManager()
        {
            ALBase.RegisterOpenALResolver();

            string defaultDeviceName = ALC.GetString(AudioDevice, AlcGetString.AllDevicesSpecifier);
            int[] attributes = Array.Empty<int>();

            Debug.Log("default device name: " +  defaultDeviceName);

            AudioDevice  = ALC.OpenDevice(defaultDeviceName);
            AudioContext = ALC.CreateContext(AudioDevice,attributes);

            ALC.MakeContextCurrent(AudioContext);

            ALC.ProcessContext(AudioContext);

            var renderer = AL.Get(ALGetString.Renderer);
            var version  = AL.Get(ALGetString.Version);
            var vendor   = AL.Get(ALGetString.Vendor);

            Debug.Log("renderer: " + renderer);
            Debug.Log("version: "  + version);
            Debug.Log("vendor: "   + vendor);

            CheckErrors(AL.GetError());
            if (AL.GetError() == ALError.NoError){
                Debug.Log("audio device sucsesfully initialized");
            }
            else{
                throw new Exception("audio device initialization failed: " + AL.GetError());
            }
        }

        public static void CheckErrors(ALError error)
        {
            switch (error)
            {
                case ALError.NoError:
                    break;
                case ALError.InvalidName:
                    Debug.LogError("audio: InvalidName");
                    break;
                case ALError.IllegalEnum:
                    Debug.LogError("audio: IllegalEnum");
                    break;
                case ALError.InvalidValue:
                    Debug.LogError("audio: InvalidValue");
                    break;
                case ALError.IllegalCommand:
                    Debug.LogError("audio: IllegalCommand");
                    break;
                case ALError.OutOfMemory:
                    Debug.LogError("audio: OutOfMemory");
                    break;
            }
        }

        public static void Dispose(){
            ALC.DestroyContext(AudioContext);
            ALC.CloseDevice(AudioDevice);
        }
    }
}
