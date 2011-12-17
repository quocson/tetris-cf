using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    class NextShape : Panel
    {
        private Bitmap imgBuff;

        public NextShape()
        {
            Location = new Point(0,0);
            Size = new Size(0, 0);
            imgBuff = new Bitmap(Constant.iNext);
        }

        public void drawNextShape(int kind, int color, int rotate)
        {
            int r, c;
            Shape tmpShape = new Shape(kind, color, rotate);
            int[,] tmpArr = tmpShape.getStatusArr(out r, out c);

            Graphics gr = Graphics.FromImage(imgBuff);

            if(r == 4 && c == 1)
            {
                for (int i = 0; i < r; i++)
                {
                    Block tmp = new Block(0, 0, color);
                    tmp.drawBlock(gr);
                }
            }
            if(r == 1 && c == 4)
            {
                for (int i = 0; i < c; i++)
                {
                    Block tmp = new Block(0, 0, color);
                    tmp.drawBlock(gr);
                }
            }
            if(r == 2 && c == 2)
            {
                for (int i = 0; i < r; i++)
                    for (int j = 0; j < c; j++)
                    {
                        if (tmpArr[i, j] == 1)
                        {
                            Block tmp = new Block(0, 0,color);
                            tmp.drawBlock(gr);
                        }
                    }
            }
            if (r == 2 && c == 3)
            {
                for (int i = 0; i < r; i++)
                    for (int j = 0; j < c; j++)
                    {
                        if (tmpArr[i, j] == 1)
                        {
                            Block tmp = new Block(0, 0, color);
                            tmp.drawBlock(gr);
                        }
                    }
            }
            if(r == 3 && c == 2)
            {
                for (int i = 0; i < r; i++)
                    for (int j = 0; j < c; j++)
                    {
                        if (tmpArr[i, j] == 1)
                        {
                            Block tmp = new Block(0, 0, color);
                            tmp.drawBlock(gr);
                        }
                    }
            }
            Refresh();
            gr.Dispose();            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(imgBuff, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}