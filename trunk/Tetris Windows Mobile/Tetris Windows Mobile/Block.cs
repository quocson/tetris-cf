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
            return new Point(yScreen / Constant.d, xScreen / Constant.d);
        }

        public void lockedArr()
        {
            int i = yScreen / Constant.d;
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

        public void eraserBlock(Graphics gr)
        {
            gr.DrawImage(Constant.iBackground,
                         new Rectangle(xScreen, yScreen, Constant.d, Constant.d),
                         new Rectangle(xScreen, yScreen, Constant.d, Constant.d),
                         GraphicsUnit.Pixel);
        }

        public bool rightPosition()
        {
            if (xScreen < 0 || xScreen > Constant.xMax *  Constant.d - Constant.d) 
                return false;

            if (yScreen <= -Constant.d) 
                return true;

            if (yScreen > -Constant.d && yScreen < 0 && Constant.map[yScreen / Constant.d + 1, xScreen / Constant.d] == 0)
                return true;

            if (yScreen >= 0)
            {
                if (Constant.onMap(yScreen / Constant.d, xScreen / Constant.d) && yScreen % Constant.d == 0 && Constant.map[yScreen / Constant.d, xScreen / Constant.d] == 0) 
                    return true;

                if (yScreen % Constant.d != 0 && Constant.onMap(yScreen / Constant.d, xScreen / Constant.d) && Constant.onMap(yScreen / Constant.d + 1, xScreen / Constant.d) &&
                    Constant.map[yScreen / Constant.d, xScreen / Constant.d] == 0 && Constant.map[yScreen / Constant.d + 1, xScreen / Constant.d] == 0)
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
                if (yScreen >= -Constant.d && Constant.map[0, currY] == 0) 
                    return true;

                if (yScreen < -Constant.d) 
                    return true;

                return false;
            }
            if (currX < Constant.xMax - 1 && yScreen >= 0 && Constant.map[currX + 1, currY] == 0)
                return true;
            return false;
        }

        public void moveLeft()
        {
            xScreen -= Constant.d;
        }

        public void moveRight()
        {
            yScreen += Constant.d;
        }

        public void moveDown()
        {
            yScreen += Constant.dyFallDown;
        }
    }
}
