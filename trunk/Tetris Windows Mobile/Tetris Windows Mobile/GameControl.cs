using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    class GameControl:Panel
    {
        private Shape shape;
        private Bitmap imageBuffer;
        private int indexShape;
        private int color;
        private int indexRotate;

        public GameControl()
        {
            Location = new Point(0, 0);
            Size = new Size(240, 294);
            imageBuffer = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.border);
            indexShape = Constant.randShape(out color, out indexRotate);
        }

        public void resetGame()
        {
            Constant.resetMap();
            imageBuffer.Dispose();
            imageBuffer = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.border);
            Refresh();
        }

        public void saveGame()
        {
        }

        public void loadGame()
        {
        }

        public void gameInitObj(out int k,out int c,out int ro)
        {
            shape = new Shape(indexShape, color, indexRotate);
            k = Constant.randShape(out c, out ro);
        }

        public void gameDeleteObj()
        {
            shape.Dispose();
        }

        public void setShape(int kind, int color, int rotate)
        {
            indexShape = kind;
            this.color = color;
            indexRotate = rotate;
        }

        public void locked()
        {
            shape.lockShape();
        }

        public void buff(out Bitmap x)
        {
            x = imageBuffer;
        }

        public bool isEndGame()
        {
            return shape.checkGameOver();
        }

        public bool gameObjFall()
        {            
            if (shape.canFallDown())
            {
                Graphics gr = Graphics.FromImage(imageBuffer);
                shape.eraserShape(gr);
                shape.fallDown();
                shape.drawShape(gr);
                gr.Dispose();
                return true;
            }
            return false;
        }

        public void drawPanel()
        {
            Refresh();      
        }

        void gameObjFallToEnd()
        {
            while (shape.canFallDown())
            {
                shape.fallDown();
            }
        }

        public Stack<int> fullLine()
        {
            return shape.checkFullLine();
        }

        public void ClearScreen()
        {
            imageBuffer.Dispose();
            imageBuffer = Tetris_Windows_Mobile.Properties.Resources.border;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(imageBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }   
    }
}
