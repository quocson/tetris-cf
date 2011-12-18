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
        private Shape ghostShape;
        private Bitmap imageBuffer;
        private int indexShape;
        private int color;
        private int indexRotate;

        public GameControl()
        {
            Location = new Point(0, 0);
            Size = new Size(170, 294);
            imageBuffer = new Bitmap(Constant.iBorderGame);
            indexShape = Constant.randShape(out color, out indexRotate);
            ghostShape = new Shape(indexShape, color, indexRotate);
        }

        public int Kind
        {
            get { return indexShape; }
        }

        public int Color
        {
            get { return color; }
        }

        public int Rotate
        {
            get { return indexRotate; }
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

        public void clearScreen()
        {
            imageBuffer.Dispose();
            imageBuffer = Constant.iBorderGame;
        }

        public void setGhostShape(int kind, int color, int rotate, bool stt)
        {
            Graphics gr = Graphics.FromImage(imageBuffer);
            if(stt)
                ghostShape.eraserShape(gr);
            ghostShape = new Shape(kind, color, rotate);
            ghostShape.xScreen = shape.xScreen;
            ghostShape.yScreen = shape.yScreen;
            while (ghostShape.canFallDown())
            {
                ghostShape.fallDown();
            }
            ghostShape.drawGhostShape(gr);
            gr.Dispose();
        }

        public void eraserGhostShape()
        {
            Graphics gr = Graphics.FromImage(imageBuffer);
            ghostShape.eraserShape(gr);
            gr.Dispose();
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
