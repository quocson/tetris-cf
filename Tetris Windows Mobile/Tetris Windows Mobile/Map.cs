using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Tetris_Windows_Mobile
{
    class Map
    {
        public static int xMax, yMax;
        public static int[,] map;
        public static int startXscreen;
        public static int dyFallDown;
        public static int speedGame;
        public const int d = 13;

        static Map()
        {
            xMax = 18;
            yMax = 12;
            map = new int[xMax, yMax];
            startXscreen = 70;
            dyFallDown = Map.d;
            speedGame = 50;
        }

        public static bool onMap(int r,int c)
        {
            return (r >= 0 && r < xMax && c >= 0 && c < yMax);
        }

        public static int randShape(out int color,out int rotate)
        {
            Random rd = new Random(unchecked((int)DateTime.Now.Ticks));
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
                    if (i == 0)
                    {
                        map[i, j] = 0;
                    }
                    else   map[i, j] = map[i - 1, j];
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

        /*public static void showmap()
        {
            string mess = "";
            for (int i = 0; i < xMax; i++)
            {
                for (int j = 0; j < yMax; j++)
                {
                    mess += map[i, j] + " ";
                }
                mess += "\n";
            }
            MessageBox.Show(mess);
        }*/
    }
}
