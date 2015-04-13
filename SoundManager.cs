using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;
using System.IO;

namespace CatboobGGStream
{
    public class SoundManager
    {
        public MediaPlayer SoundMediaPlayer;

        public SoundManager()
        {
            SoundMediaPlayer = new MediaPlayer();
        }

        public void SetVolume(double volume_level)
        {
            if (volume_level >= 0.0 && volume_level <= 1.0)
                SoundMediaPlayer.Volume = volume_level;
        }

        public void PlaySound(String sound_path)
        {
            if (File.Exists(sound_path))
            {                
                SoundMediaPlayer.Open(new Uri(sound_path));
                SoundMediaPlayer.Play();
            }
        }

        public void StopSound()
        {
            SoundMediaPlayer.Stop();
            SoundMediaPlayer.Close();
        }
    }
}
