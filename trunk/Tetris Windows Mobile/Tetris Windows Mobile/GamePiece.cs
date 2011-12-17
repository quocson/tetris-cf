using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    class GamePiece : Panel
    {
        private int piece;
        private Bitmap imgBuff;

        public GamePiece()
        {
            Location = new Point(0, 0);
            Size = new Size(0, 0);
            imgBuff = new Bitmap(Constant.iPiece);
        }

        public int Piece
        {
            get { return piece; }
            set { piece = value; }
        }

        public void drawPiece()
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

