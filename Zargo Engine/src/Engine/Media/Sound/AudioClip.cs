using System;
using System.IO;
using ZargoEngine.Media.Codecs;
using ZargoEngine.Media.OpenAL;

namespace ZargoEngine.Sound
{
    public class AudioClip : IDisposable
    {
        AudioPlayer player;

        public AudioClip(string filePath, bool loops){
            if (!File.Exists(filePath)) return;

            player = new AudioPlayer();

            if (!Path.HasExtension(filePath)){
                Debug.LogError("sound file has no extension");
                return;
            }

            string extension = Path.GetExtension(filePath);

            Debug.Log("sound file extension: " + extension);

            if (extension.Equals(".wav"))      player.Init(new WavCodec(filePath));
            else if (extension.Equals(".raw")) player.Init(new RawCodec(filePath));
            else if (extension.Equals(".mp3")) player.Init(new MP3Codec(filePath));
            else Debug.LogError("sound file extension does not supported");
        }

        public void Stop(){
            player.Play();
        }

        public void Play(){
            player.Stop();
        }

        public void Dispose(){
            player.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
