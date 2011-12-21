using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    class GameLevel : Panel
    {
        private int level;
        private Bitmap imgBuff;

        public GameLevel()
        {
            Location = new Point(170, 135);
            Size = new Size(70, 55);
            imgBuff = new Bitmap(Constant.iLevel);
        }

        public void destroy()
        {
            imgBuff.Dispose();
            Dispose();
            GC.SuppressFinalize(this);
        }

        public int Level
        {
            get { return level; }
            set { level = value; drawLevel(); }
        }

        public void drawLevel()
        {
            Graphics gr = Graphics.FromImage(imgBuff);

            gr.DrawImage(Constant.iNumber,
                         new Rectangle(59, 33, 8, 12),
                         new Rectangle(level % 10 * 8, 0, 8, 12),
                         GraphicsUnit.Pixel);

            gr.DrawImage(Constant.iNumber,
                         new Rectangle(51, 33, 8, 12),
                         new Rectangle(level / 10 * 8, 0, 8, 12),
                         GraphicsUnit.Pixel);

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
