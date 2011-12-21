using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Media;

namespace Tetris_Windows_Mobile
{
    class PlaySound : IDisposable
    {
        private SoundPlayer player;
        private SoundPlayer theme;
        private System.Reflection.Assembly a;
        private System.IO.Stream s;
        public PlaySound()
        {
            player = new SoundPlayer();
            a = System.Reflection.Assembly.GetExecutingAssembly();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.theme.wav");
            theme = new SoundPlayer(s);
        }

        public void playSoundTheme()
        {
            try
            {
                theme.LoadAsync();
                theme.PlayLooping();
            }
            catch (Exception )
            {
                
            }
        }

        public void Dispose()
        {
            player.Dispose();
            theme.Dispose();
            GC.SuppressFinalize(this);
        }

        public void stopSoundTheme()
        {
            theme.Stop();
        }

        public void stopSoundPlayer()
        {
            player.Stop();
        }

        public void playSoundAmazing()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.amazing.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundBrilliant()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.brilliant.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundGameOver()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.gameover.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundLevelUp()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.levelup.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundVeryGood()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.verygood.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundWonderful()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.wonderful.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundGameWin()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.gamewin.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception )
            {
                
            }
            player.Play();
        }

        public void playSoundExcellent()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.excellent.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception)
            {

            }
            player.Play();
        }

        public void playSoundClear()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.clear.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception)
            {

            }
            player.Play();
        }

        public void playSoundWow()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.wow.wav");
            player = new SoundPlayer(s);
            try
            {
                player.LoadAsync();
            }
            catch (Exception)
            {

            }
            player.Play();
        }
    }
}
