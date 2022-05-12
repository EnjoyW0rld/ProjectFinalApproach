using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class SoundManager : GameObject
    {
        static SoundManager _Instance;
        SoundChannel sc;
        Dictionary<int, Sound> sounds = new Dictionary<int, Sound>();

        public static SoundManager Instance()
        {
            if(_Instance == null)
            {
                _Instance = new SoundManager();
            }
            return _Instance;
        }
        public SoundManager()
        {
            LoadSounds();
            sc = new SoundChannel(0);
        }
        void LoadSounds()
        {
            /*            string[] soundList = Directory.GetFiles("Assets/Sound/");
                        for (int i = 0; i < soundList.Length; i++)
                        {
                            sounds.Add(i, new Sound(soundList[i]));
                        }*/

            //sounds.Add(1, new Sound("Assets/wavs/ButtonCLick.ogg"));
            sounds.Add(0, new Sound("Assets/Sound/scenetrans3.wav"));
            sounds.Add(1, new Sound("Assets/Sound/portal.wav"));
            sounds.Add(2, new Sound("Assets/Sound/select.wav"));
            sounds.Add(3, new Sound("Assets/Sound/thrust.ogg"));
            sounds.Add(4, new Sound("Assets/Sound/crash.mp3"));
            sounds.Add(5, new Sound("Assets/Sound/gameover.wav"));
            sounds.Add(6, new Sound("Assets/wavs/drumandspace.wav",true));

        }
        public void PlaySound(int i)
        {
            
            Sound sound;
            
            sounds.TryGetValue(i, out sound);
            //sound.Play(false, 0);
            sc = sound.Play();
       
        }
        public void StopAllSounds()
        {
            sc.Stop();
        }
    }
}
