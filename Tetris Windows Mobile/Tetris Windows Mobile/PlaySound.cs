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
        //private SoundPlayer amazing;
        //private SoundPlayer brillant;
        //private SoundPlayer clear;
        //private SoundPlayer excellent;
        //private SoundPlayer gameOver;
        //private SoundPlayer gameStart;
        //private SoundPlayer levelUp;
        //private SoundPlayer lockDown;
        //private SoundPlayer move;
        //private SoundPlayer moveFast;
        //private SoundPlayer rotate;
        //private SoundPlayer rotateFail;
        //private SoundPlayer veryGood;
        //private SoundPlayer wonderful;
        //private SoundPlayer wow;
        private SoundPlayer player;
        private SoundPlayer theme;

        public PlaySound()
        {
            player = new SoundPlayer(); 

            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream s = a.GetManifestResourceStream("Tetris_Windows_Mobile.Properties.Resources.theme.wav");
            theme = new SoundPlayer(s);
        }

        public void playSoundTheme()
        {
            theme.PlayLooping();
        }

        public void stopSoundTheme()
        {
            theme.Stop();
        }
    }
}
