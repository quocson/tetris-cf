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
            return new Point((yScreen + 52) / Constant.d, xScreen / Constant.d);
        }

        public void lockedArr()
        {
            int i = (yScreen + 52) / Constant.d;
            int j = xScreen / Constant.d;
            if (Constant.onMap(i, j))
            {
                Constant.map[i, j] = color;
            }
        }

        public void drawBlock(Graphics gr)
        {
            gr.DrawImage(Constant.iColor,
                         new Rectangle(xScreen, yScreen, Constant.d - 1, Constant.d - 1),
                         new Rectangle((color - 1) * Constant.d, 0, Constant.d, Constant.d),
                         GraphicsUnit.Pixel);
        }

        public void drawGhostBlock(Graphics gr)
        {
            gr.DrawImage(Constant.iColor,
                         new Rectangle(xScreen, yScreen, Constant.d - 1, Constant.d - 1),
                         new Rectangle((color - 1) * Constant.d, Constant.d, Constant.d, Constant.d),
                         GraphicsUnit.Pixel);
        }

        public void eraserBlock(Graphics gr)
        {
            gr.DrawImage(Constant.iBorderGame,
                         new Rectangle(xScreen, yScreen, Constant.d, Constant.d),
                         new Rectangle(xScreen, yScreen, Constant.d, Constant.d),
                         GraphicsUnit.Pixel);
        }

        public bool rightPosition()
        {
            if ((xScreen >= 0 && xScreen <= Constant.yMax *  Constant.d - Constant.d) &&
                yScreen < Constant.xMax * Constant.d - 5 * Constant.d &&
                Constant.map[(yScreen + 52) / Constant.d, xScreen / Constant.d] == 0) 
                return true;

            return false;

        }

        public bool checkDown()
        {
            int currX = positionArr().X;
            int currY = positionArr().Y;
            if (yScreen < (Constant.xMax - 6 )* Constant.d &&
                Constant.map[currX + 1, currY] == 0)
                return true;
            return false;
        }

        public void moveLeft()
        {
            xScreen -= Constant.d;
        }

        public void moveRight()
        {
            xScreen += Constant.d;
        }

        public void moveDown()
        {
            yScreen += Constant.dyFallDown;
        }
    }
}
