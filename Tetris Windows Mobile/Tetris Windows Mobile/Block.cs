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
            return new Point(yScreen / 13, xScreen / 13);
        }

        /*public void lockedArr()
        {
            int i = yScreen / 13;
            int j = yScreen / 13;
            if (Hang.onMap(i, j))
            {
                Hang.map[i, j] = color;
            }
        }*/

        #region Draw & Eraser this block onto ImageBuffer
        public void drawBlock(Graphics gr)
        {
            gr.DrawImage(Tetris_Windows_Mobile.Properties.Resources.colors,
                         new Rectangle(xScreen, yScreen, 13, 13),
                         new Rectangle(color * 13, 0, 13, 13),
                         GraphicsUnit.Pixel);
        }

        public void eraserBlock(Graphics gr)
        {
            gr.DrawImage(Tetris_Windows_Mobile.Properties.Resources.background,
                         new Rectangle(xScreen, yScreen, 13, 13),
                         new Rectangle(xScreen, yScreen, 13, 13),
                         GraphicsUnit.Pixel);
        }

        #endregion


        #region Check move & move this block

        public bool rightPosition()
        {
            if (xScreen < 0 || xScreen > MapManager.xMax *  13 - 13) 
                return false;

            if (yScreen <= -13) 
                return true;

            if (yScreen > -13 && yScreen < 0 && MapManager.map[yScreen / 13 + 1, xScreen / 13] == 0)
                return true;

            if (yScreen >= 0)
            {
                if (MapManager.onMap(yScreen / 13, xScreen / 13) && yScreen % 13 == 0 && MapManager.map[yScreen / 13, xScreen / 13] == 0) 
                    return true;

                if (yScreen % 13 != 0 && MapManager.onMap(yScreen / 13, xScreen / 13) && MapManager.onMap(yScreen / 13 + 1, xScreen / 13) &&
                    MapManager.map[yScreen / 13, xScreen / 13] == 0 && MapManager.map[yScreen / 13 + 1, xScreen / 13] == 0)
                    return true;

                return false;
            }

            return false;

        }

        public bool CheckDown()
        {
            int currX = positionArr().X;
            int currY = positionArr().Y;
            if (yScreen < 0)
            {
                if (yScreen >= -30 && MapManager.map[0, currY] == 0) 
                    return true;

                if (yScreen < -30) 
                    return true;

                return false;
            }
            if (currX < MapManager.xMax - 1 && yScreen >= 0 && MapManager.map[currX + 1, currY] == 0)
                return true;
            return false;
        }

        public void moveLeft()
        {
            xScreen -= 13;
        }

        public void moveRight()
        {
            yScreen += 13;
        }

        /*public void moveDown()
        {
            Yscreen += Hang.dyFallDown;
        }*/

        #endregion


    }
}
