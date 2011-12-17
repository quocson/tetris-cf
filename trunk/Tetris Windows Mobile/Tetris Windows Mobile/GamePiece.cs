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
            Location = new Point(170, 240);
            Size = new Size(70, 54);
            imgBuff = new Bitmap(Constant.iPiece);
        }

        public int Piece
        {
            get { return piece; }
            set { piece = value; drawPiece(); }
        }

        public void drawPiece()
        {
            Graphics gr = Graphics.FromImage(imgBuff);
            int tmp = 0;
            for (int i = 0; i < 7; i++)
            {
                switch (i)
                {
                    case 0: tmp = piece / 1000000; break;
                    case 1: tmp = piece % 1000000 / 100000; break;
                    case 2: tmp = piece % 100000 / 10000; break;
                    case 3: tmp = piece % 10000 / 1000; break;
                    case 4: tmp = piece % 1000 / 100; break;
                    case 5: tmp = piece % 100 / 10; break;
                    case 6: tmp = piece % 10; break;
                }
                gr.DrawImage(Constant.iNumber,
                             new Rectangle(10 + i * 8 + 1, 31, 8, 12),
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

