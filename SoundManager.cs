using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.IO;

namespace CatboobGGStream
{
    public class SoundManager
    {
        private MediaPlayer sound_media_player;

        public SoundManager()
        {
            sound_media_player = new MediaPlayer();
        }

        public void PlaySound(String sound_path)
        {
            if (File.Exists(sound_path))
            {
                sound_media_player.Open(new Uri(sound_path));
                sound_media_player.Play();
            }
        }

        public void StopSound()
        {
            sound_media_player.Stop();
        }
    }
}
