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

            Size = new Size(Map.xMax * 14, Map.yMax * 14);
            imageBuffer = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.background);

            indexShape = Map.randShape(out color, out indexRotate);
        }

        public void resetGame()
        {
            Map.resetMap();
            imageBuffer.Dispose();
            imageBuffer = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.background);
            Refresh();
        }

        public void gameInitObj(out int k,out int c,out int ro)
        {
          
            shape = new Shape(indexShape, color,indexRotate);
            k = Map.randShape(out c,out ro);
        }

        public void gameDeleteObj()
        {
            shape.Dispose();
        }
        public void setShape(int kind,int color,int rotate)
        {
            indexShape = kind;
            this.color = color;
            indexRotate = rotate;
        }

        public void locked()
        {
            shape.lockShape();
        }

        public void Buff(out Bitmap x)
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
                shape.EraserShape(gr);
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
            imageBuffer = Tetris_Windows_Mobile.Properties.Resources.background;
        }
        #region Override OnPaint & OnPaintBackground method

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(imageBuffer, 0, 0);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }
        #endregion    
    }
}
