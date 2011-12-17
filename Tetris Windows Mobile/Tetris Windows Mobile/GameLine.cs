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
            Location = new Point(0, 0);
            Size = new Size(0, 0);
            imgBuff = new Bitmap(Constant.iLine);
        }

        public int Line
        {
            get { return line; }
            set { line = value; drawLine(); }
        }

        public void drawLine()
        {
            Graphics gr = Graphics.FromImage(imgBuff);

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
