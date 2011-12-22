using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    public class Constant
    {
        public static int xMax, yMax;
        public static int[,] map;
        public static int startXscreen;
        public const int dyFallDown = 13;
        public static Bitmap iBorderGame;
        public static Bitmap iNext;
        public static Bitmap iScore;
        public static Bitmap iLevel;
        public static Bitmap iLine;
        public static Bitmap iPiece;
        public static Bitmap iColor;
        public static Bitmap iNumber;
        public static Random rd;
        public static string fPath;
        public const int d = 13;

        public static XmlSave saver;
        static Constant()
        {

            fPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\highscore.xml";
            saver = new XmlSave();
            xMax = 24;
            yMax = 13;
            map = new int[xMax, yMax];
            startXscreen = 65;


            iBorderGame = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.border);
            iNext = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.next);
            iScore = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.score);
            iLevel = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.level);
            iLine = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.line);
            iPiece = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.piece);
            iColor = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.colors);
            iNumber = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.number);
            rd = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        public static bool onMap(int r,int c)
        {
            return (r >= 0 && r < xMax && c >= 0 && c < yMax);
        }

        public static int randShape(out int color,out int rotate)
        {
            int index = rd.Next(0, 7);
            color = rd.Next(1, 8);
            rotate = rd.Next(0,5);

            switch (index)
            {
                case 0: index = 15; break;
                case 1: index = 31; break;
                case 2: index = 51; break;
                case 3: index = 30; break;
                case 4: index = 58; break;
                case 5: index = 57; break;
                case 6: index = 60; break;
            }
            return index;
        }

        public static void updateMap(int rol,ref int dxLine)
        {
            int i, j;
            for (i = rol; i >= 0; i--)
                for (j = 0; j < yMax; j++)
                {
                    if (i != 0)
                     map[i, j] = map[i - 1, j];
                }
            dxLine++;
        }

        public static void resetMap()
        {
            for (int i = 0; i < xMax; i++)
            {
                for (int j = 0; j < yMax; j++)
                {
                    map[i, j] = 0;
                }
            }
        }
    }
}
