using OpenTK.Audio.OpenAL;
using StbSharp;
using System;
using System.IO;
using ZargoEngine.AssetManagement;

namespace ZargoEngine.Sound
{
    public class AudioClip : IDisposable
    {
        private int bufferID,sourceID;
        private string filePath;

        public bool isPlaying{
            get{
                AL.GetSource(sourceID, ALGetSourcei.SourceState, out int state);
                return (ALSourceState)state == ALSourceState.Playing;
            }
        }

        public AudioClip(string filePath, bool loops){

            if (!AssetManager.GetFileLocation(ref filePath)) return;
            
            this.filePath = filePath;

            var buffer = File.ReadAllBytes(this.filePath);

            var _vorbis = Vorbis.FromMemory(buffer);

            Debug.Log("vorbis buffer length: " + _vorbis.SongBuffer.Length);

            if (_vorbis.SongBuffer == null){
                Debug.Log("Audio loading from memory failed");
                return;
            }

            ALFormat format = _vorbis.Channels ==  1 ? ALFormat.Mono16 : ALFormat.Stereo16;

            bufferID = AL.GenBuffer();
            sourceID = AL.GenSource();

            AL.BufferData(bufferID, format, _vorbis.SongBuffer, _vorbis.SampleRate);
            
            AL.Source(sourceID, ALSourcei.Buffer, bufferID);
            AL.Source(sourceID, ALSourceb.Looping, loops);
            AL.Source(sourceID, ALSource3i.Position, 0 , 0 , 0);
            AL.Source(sourceID, ALSourcef.Gain, 1f);

            AudioManager.CheckErrors(AL.GetError());
            if (AL.GetError() == ALError.NoError){
                Debug.Log("sound Loaded sucsesfully");
            } 
        }

        public void Stop(){
            if (isPlaying){
                AL.SourceStop(sourceID);
            }
        }

        public void Play(){
            if (!isPlaying){
                AL.SourcePlay(sourceID);
                Debug.Log("playing audio");
            }
        }

        public void Dispose(){
            AL.DeleteSource(sourceID);
            AL.DeleteBuffer(bufferID);
            GC.SuppressFinalize(this);
        }
    }
}
