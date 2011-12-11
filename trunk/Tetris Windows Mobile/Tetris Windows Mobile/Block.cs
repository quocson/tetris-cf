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

        #region Constructor

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
        #endregion

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
            return new Point(yScreen / 14, xScreen / 14);
        }

        public void lockedArr()
        {
            int i = yScreen / 14;
            int j = yScreen / 14;
            if (Map.onMap(i, j))
            {
                Map.map[i, j] = color;
            }
        }

        #region Draw & Eraser this block onto ImageBuffer
        public void drawBlock(Graphics gr)
        {
            gr.DrawImage(Tetris_Windows_Mobile.Properties.Resources.colors,
                         new Rectangle(xScreen, yScreen, 14, 14),
                         new Rectangle(color * 14, 0, 14, 14),
                         GraphicsUnit.Pixel);
        }

        public void eraserBlock(Graphics gr)
        {
            gr.DrawImage(Tetris_Windows_Mobile.Properties.Resources.background,
                         new Rectangle(xScreen, yScreen, 14, 14),
                         new Rectangle(xScreen, yScreen, 14, 14),
                         GraphicsUnit.Pixel);
        }

        #endregion


        #region Check move & move this block

        public bool rightPosition()
        {
            if (xScreen < 0 || xScreen > Map.xMax *  14 - 14) 
                return false;

            if (yScreen <= -14) 
                return true;

            if (yScreen > -14 && yScreen < 0 && Map.map[yScreen / 14 + 1, xScreen / 14] == 0)
                return true;

            if (yScreen >= 0)
            {
                if (Map.onMap(yScreen / 14, xScreen / 14) && yScreen % 14 == 0 && Map.map[yScreen / 14, xScreen / 14] == 0) 
                    return true;

                if (yScreen % 14 != 0 && Map.onMap(yScreen / 14, xScreen / 14) && Map.onMap(yScreen / 14 + 1, xScreen / 14) &&
                    Map.map[yScreen / 14, xScreen / 14] == 0 && Map.map[yScreen / 14 + 1, xScreen / 14] == 0)
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
                if (yScreen >= -30 && Map.map[0, currY] == 0) 
                    return true;

                if (yScreen < -30) 
                    return true;

                return false;
            }
            if (currX < Map.xMax - 1 && yScreen >= 0 && Map.map[currX + 1, currY] == 0)
                return true;
            return false;
        }

        public void moveLeft()
        {
            xScreen -= 14;
        }

        public void moveRight()
        {
            yScreen += 14;
        }

        public void moveDown()
        {
            yScreen += Map.dyFallDown;
        }

        #endregion


    }
}
