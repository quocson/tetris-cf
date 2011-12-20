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
            ghostShape = null;
            shape = null;
        }

        public Shape Shape
        {
            get { return shape; }
        }

        public void resetGame()
        {
            Constant.resetMap();
            imageBuffer.Dispose();
            imageBuffer = new Bitmap(Constant.iBorderGame);
            Refresh();
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

        public bool gameObjFall(bool g)
        {            
            if (shape.canFallDown())
            {
                Graphics gr = Graphics.FromImage(imageBuffer);
                shape.eraserShape(gr);
                shape.fallDown();
                if (g)
                {
                    ghostShape = new Shape(shape.StatusArr, shape.Color, Shape.xScreen, shape.yScreen, shape.Row, shape.Col);
                    ghostShape.goToEnd();
                    ghostShape.drawGhostShape(gr);
                }
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
            shape.goToEnd();
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

        public void deleteLine(int line)
        {
            Graphics g = Graphics.FromImage(imageBuffer);
            g.DrawImage(Constant.iBorderGame, new Rectangle(0, 0, Constant.yMax * Constant.d, (line - 5 + 2) * Constant.d),
                new Rectangle(0, 0, Constant.yMax * Constant.d, (line - 5 + 2) * Constant.d), GraphicsUnit.Pixel);
            Refresh();
            for (int i = line  - 1; i >= 0; i--)
                for (int j = 0; j < Constant.yMax; j++) 
                {
                    
                    if (Constant.map[i, j] != 0)
                    {
                        g.DrawImage(Constant.iColor,
                        new Rectangle(j * Constant.d, (i - 5 + 2 ) * Constant.d, Constant.d - 1, Constant.d - 1),
                        new Rectangle((Constant.map[i, j] - 1) * Constant.d, 0, Constant.d, Constant.d),
                        GraphicsUnit.Pixel);
                        Refresh();
                    }
                }
            g.Dispose();
        }

        public void eraserGhostShape()
        {
            Graphics gr = Graphics.FromImage(imageBuffer);
            if(ghostShape != null)
                ghostShape.eraserShape(gr);
            shape.drawShape(gr);
            Refresh();
            gr.Dispose();
        }

        public void drawGhostShape()
        {
            Graphics gr = Graphics.FromImage(imageBuffer);
            if (shape != null)
            {
                ghostShape = new Shape(shape.StatusArr, shape.Color, Shape.xScreen, shape.yScreen, shape.Row, shape.Col);
                ghostShape.goToEnd();
                ghostShape.drawGhostShape(gr);
                shape.drawShape(gr);
            }
            Refresh();
            gr.Dispose();
        }

        public void setGhostNull()
        {
            ghostShape = null;
        }

        public void keyDown(KeyEventArgs e, PlaySound sound, bool enableSound, ref int tempScore, bool g)
        {
            Graphics gr = Graphics.FromImage(imageBuffer);
            shape.eraserShape(gr);
            if (e.KeyValue == (int)System.Windows.Forms.Keys.Left && shape.canMoveLeft())
            {
                if (ghostShape != null && g)
                    ghostShape.eraserShape(gr);
                shape.moveLeft();
            }
            else
            if (e.KeyValue == (int)System.Windows.Forms.Keys.Up && shape.canRotate())
            {
                if (ghostShape != null && g)
                    ghostShape.eraserShape(gr);
                shape.rotate();
            }
            else
            if (e.KeyValue == (int)System.Windows.Forms.Keys.Up && !shape.canRotate())
            {
            }
            else
            if (e.KeyValue == (int)System.Windows.Forms.Keys.Right && shape.canMoveRight())
            {
                if (ghostShape != null)
                    ghostShape.eraserShape(gr);
                shape.moveRight();
            }
            else
            if (e.KeyValue == (int)System.Windows.Forms.Keys.Down && shape.canFallDown())
            {
                shape.fallDown();
                tempScore += 2;
            }
            else
            if (e.KeyValue == (int)System.Windows.Forms.Keys.Enter && shape.canFallDown())
            {
                gameObjFallToEnd();
                tempScore += 10;
            }
            if (g)
            {
                ghostShape = new Shape(shape.StatusArr, shape.Color, Shape.xScreen, shape.yScreen, shape.Row, shape.Col);
                ghostShape.goToEnd();
                ghostShape.drawGhostShape(gr);
            }
            shape.drawShape(gr);
            Refresh();
            gr.Dispose();
        }

        public bool check()
        {
            if (ghostShape != null || shape != null)
                return true;
            return false;
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
