using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    public class Block : IDisposable
    {
        private int xScreen, yScreen;

        private int color;

        public Block()
        {
            xScreen = yScreen = color = 0;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Block(int iScreen, int jScreen, int iColor)
        {
            xScreen = iScreen;
            yScreen = jScreen;
            color = iColor;
        }

        public int ScreenX
        {
            get { return xScreen; }
            set { xScreen = value; }
        }

        public int ScreenY
        {
            get { return yScreen; }
            set { yScreen = value; }
        }

        public int Color
        {
            get { return color; }
            set { color = value; }
        }

        public Point positionArr()
        {
            return new Point(yScreen / Map.d, xScreen / Map.d);
        }

        public void lockedArr()
        {
            int i = yScreen / Map.d;
            int j = xScreen / Map.d;
            if (Map.onMap(i, j))
            {
                Map.map[i, j] = color;
            }
        }

        public void drawBlock(Graphics gr)
        {
            gr.DrawImage(Map.iColor,
                         new Rectangle(xScreen, yScreen, Map.d - 1, Map.d - 1),
                         new Rectangle(color * Map.d, 0, Map.d, Map.d),
                         GraphicsUnit.Pixel);
        }

        public void eraserBlock(Graphics gr)
        {
            gr.DrawImage(Map.iBackground,
                         new Rectangle(xScreen, yScreen, Map.d, Map.d),
                         new Rectangle(xScreen, yScreen, Map.d, Map.d),
                         GraphicsUnit.Pixel);
        }

        public bool rightPosition()
        {
            if (xScreen < 0 || xScreen > Map.xMax *  Map.d - Map.d) 
                return false;

            if (yScreen <= -Map.d) 
                return true;

            if (yScreen > -Map.d && yScreen < 0 && Map.map[yScreen / Map.d + 1, xScreen / Map.d] == 0)
                return true;

            if (yScreen >= 0)
            {
                if (Map.onMap(yScreen / Map.d, xScreen / Map.d) && yScreen % Map.d == 0 && Map.map[yScreen / Map.d, xScreen / Map.d] == 0) 
                    return true;

                if (yScreen % Map.d != 0 && Map.onMap(yScreen / Map.d, xScreen / Map.d) && Map.onMap(yScreen / Map.d + 1, xScreen / Map.d) &&
                    Map.map[yScreen / Map.d, xScreen / Map.d] == 0 && Map.map[yScreen / Map.d + 1, xScreen / Map.d] == 0)
                    return true;

                return false;
            }

            return false;

        }

        public bool checkDown()
        {
            int currX = positionArr().X;
            int currY = positionArr().Y;
            if (yScreen < 0)
            {
                if (yScreen >= -Map.d && Map.map[0, currY] == 0) 
                    return true;

                if (yScreen < -Map.d) 
                    return true;

                return false;
            }
            if (currX < Map.xMax - 1 && yScreen >= 0 && Map.map[currX + 1, currY] == 0)
                return true;
            return false;
        }

        public void moveLeft()
        {
            xScreen -= Map.d;
        }

        public void moveRight()
        {
            yScreen += Map.d;
        }

        public void moveDown()
        {
            yScreen += Map.dyFallDown;
        }
    }
}
