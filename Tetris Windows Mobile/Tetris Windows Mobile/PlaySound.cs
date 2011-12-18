using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Media;

namespace Tetris_Windows_Mobile
{
    class PlaySound
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
            theme.Load();
            while (!theme.IsLoadCompleted) ;
            theme.PlayLooping();
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
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundBrilliant()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.brilliant.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundClear()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.clear.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundExcellent()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.excellent.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundGameOver()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.gameover.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundLevelUp()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.levelup.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundLockDown()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.lockdown.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundMove()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.move.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundMoveFast()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.movefast.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundRotate()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.rotate.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundRotateFail()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.rotatefail.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundVeryGood()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.verygood.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundWonderful()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.wonderful.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }

        public void playSoundWow()
        {
            player.Dispose();
            s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Resources.wow.wav");
            player = new SoundPlayer(s);
            player.Load();
            while (!player.IsLoadCompleted) ;
            player.PlaySync();
        }
    }
}
