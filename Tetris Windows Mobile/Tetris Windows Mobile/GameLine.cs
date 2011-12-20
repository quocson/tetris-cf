using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    class GameLine : Panel
    {
        private int line;
        private Bitmap imgBuff;

        public GameLine()
        {
            Location = new Point(170, 190);
            Size = new Size(70, 50);
            imgBuff = new Bitmap(Constant.iLine);
        }

        public void destroy()
        {
            imgBuff.Dispose();
            Dispose();
            GC.SuppressFinalize(this);
        }

        public int Line
        {
            get { return line; }
            set { line = value; drawLine(); }
        }

        public void drawLine()
        {
            Graphics gr = Graphics.FromImage(imgBuff);
            int tmp = 0;
            for (int i = 0; i < 7; i++)
            {
                switch (i)
                {
                    case 0: tmp = line / 1000000; break;
                    case 1: tmp = line % 1000000 / 100000; break;
                    case 2: tmp = line % 100000 / 10000; break;
                    case 3: tmp = line % 10000 / 1000; break;
                    case 4: tmp = line % 1000 / 100; break;
                    case 5: tmp = line % 100 / 10; break;
                    case 6: tmp = line % 10; break;
                }
                gr.DrawImage(Constant.iNumber,
                             new Rectangle(10 + i * 8 + 1, 28, 8, 12),
                             new Rectangle(tmp * 8, 0, 8, 12),
                             GraphicsUnit.Pixel);
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
