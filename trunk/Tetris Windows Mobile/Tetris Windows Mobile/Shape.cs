using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris_Windows_Mobile
{
    class Shape : IDisposable
    {
        private int[,] statusArr;
        private int row, col;
        private int x, y;
        private Block[] cube;
        public int color;
        private int countRotate;

        public Shape(int kind, int color, int rotater)
        {
            int i, j, index;

            this.color = color;
            countRotate = rotater;

            statusArr = new int[4, 4];
            cube = new Block[4];
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    statusArr[i, j] = 0;
                }

            switch (kind)
            {
                case 15:
                    row = 4; col = 1;
                    x = Constant.startXscreen;
                    y = -54;
                    for (i = 0; i < row; i++)
                        statusArr[i, 0] = 1;
                    break;

                case 31:
                    row = col = 2;
                    x = Constant.startXscreen;
                    y = -28;
                    for (i = 0; i < row; i++)
                        for (j = 0; j < col; j++)
                            statusArr[i, j] = 1;
                    break;

                default:
                    row = 2;
                    col = 3;
                    x = Constant.startXscreen;
                    y = -28;
                    for (i = 0; i < row * col; i++)
                    {
                        statusArr[i / 3, i % 3] = (kind >> (5 - i)) & 1;
                    }
                    break;
            }

            for (i = 0; i < countRotate; i++)
            {
                if(row != col)
                    rotateArr();
            }

            if (countRotate == 1 || countRotate == 3)
                if (kind == 15)
                    y = -15;
                else
                    if (kind == 57 || kind == 60)
                        y = -54;

            index = 0;
            for (i = 0; i < row; i++)
                for (j = 0; j < col; j++)
                {
                    if (statusArr[i, j] == 1)
                    {
                        cube[index++] = new Block(x + j * Constant.d, y + i * Constant.d, color);
                    }
                }
        }

        public void Dispose()
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public int[,] getStatusArr(out int r, out int c)
        {
            r = row;
            c = col;
            return statusArr;
        }

        public Block[] getCube(ref int color)
        {
            color = this.color;
            return cube;
        }

        public int yScreen
        {
            get { return y; }
        }

        public int xScreen
        {
            get { return x; }
        }

        public void drawShape(Graphics gr)
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].drawBlock(gr);
            }
        }

        public void eraserShape(Graphics gr)
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].eraserBlock(gr);
            }
        }

        public bool canMoveLeft()
        {
            if (x < Constant.d || (y + Constant.d * row) == 0) 
                return false;

            int tmpX = x - Constant.d;
            int tmpY = y;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (statusArr[i, j] == 1)
                    {
                        Block tmp = new Block(tmpX + j * Constant.d, tmpY + i * Constant.d, color);
                        if (!tmp.rightPosition()) 
                            return false;
                    }
                }

            return true;

        }

        public bool canMoveRight()
        {
            if (x > Constant.xMax * Constant.d - Constant.d || (y + Constant.d * row) == 0) 
                return false;

            int tmpX = x + Constant.d;
            int tmpY = y;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (statusArr[i, j] == 1)
                    {
                        Block tmp = new Block(tmpX + j * Constant.d, tmpY + i * Constant.d, color);
                        if (!tmp.rightPosition()) 
                            return false;
                    }
                }
            return true;
        }

        public bool canRotate()
        {
            bool rotatable = true;
            int i, j;
            int tmpRow = col;
            int tmpCol = row;
            int[,] tmpArr = new int[4, 4];
            for (i = row - 1; i >= 0; i--)
                for (j = col - 1; j >= 0; j--)
                {
                    tmpArr[j, row - i - 1] = statusArr[i, j];
                    if (tmpArr[j, row - i - 1] == 1)
                    {
                        Block tmp = new Block(x + (row - i - 1) * Constant.d, y + j * Constant.d, color);
                        if (!tmp.rightPosition()) 
                            rotatable = false;
                    }
                }

            if (rotatable == true)
                return true;

            int dx = ((x + Constant.d * tmpCol - Constant.xMax * Constant.d) / Constant.d <= 0) ? 0 : (x + Constant.d * tmpCol - Constant.xMax) / Constant.d;
            int tmpX = x - Constant.d, tmpY = y;
            rotatable = true;
            do
            {
                for (i = 0; i < tmpRow; i++)
                    for (j = 0; j < tmpCol - dx; j++)
                    {
                        if (tmpArr[i, j] == 1)
                        {
                            Block tmp = new Block(tmpX + j * Constant.d, tmpY + i * Constant.d, color);
                            if (!tmp.rightPosition()) 
                                rotatable = false;
                        }
                    }
                if (!rotatable)
                    return false;

                if (rotatable)
                {
                    tmpX -= Constant.d;
                    dx--;
                }
            } while (dx > 0);
            x = tmpX + Constant.d;
            return true;
        }

        public bool canFallDown()
        {
            for (int i = 0; i < 4; i++)
            {
                if (!cube[i].checkDown())
                    return false;
            }
            return true;
        }

        public void moveLeft()
        {
            int i;
            for (i = 0; i < 4; i++)
            {
                cube[i].moveLeft();
            }
            x -= Constant.d;
        }

        public void moveRight()
        {
            int i;
            for (i = 0; i < 4; i++)
            {
                cube[i].moveRight();
            }
            x += Constant.d;
        }

        public void fallDown()
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].moveDown();
            }
            y += Constant.dyFallDown;
        }

        private void rotateArr()
        {
            int i, j;
            int tmpRow = col;
            int tmpCol = row;
            int[,] tmpArr = new int[4, 4];
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    tmpArr[i, j] = 0;

            for (i = row - 1; i >= 0; i--)
                for (j = col - 1; j >= 0; j--)
                {
                    tmpArr[j, row - i - 1] = statusArr[i, j];
                }
            row = tmpRow;
            col = tmpCol;
            for (i = 0; i < row; i++)
                for (j = 0; j < col; j++)
                {
                    statusArr[i, j] = tmpArr[i, j];
                }
        }

        public void rotate()
        {
            int i, j;
            int tmpRow = col;
            int tmpCol = row;
            int[,] tmpArr = new int[4, 4];
            for (i = row - 1; i >= 0; i--)
                for (j = col - 1; j >= 0; j--)
                {
                    tmpArr[j, row - i - 1] = statusArr[i, j];
                }
            if (!canRotate()) 
                return;
            if (y < 0) return;

            row = tmpRow;
            col = tmpCol;
            int index = 0;
            for (i = 0; i < row; i++)
                for (j = 0; j < col; j++)
                {
                    statusArr[i, j] = tmpArr[i, j];
                    if (statusArr[i, j] == 1)
                    {
                        cube[index].ScreenX = x + j * Constant.d;
                        cube[index].ScreenY = y + i * Constant.d;
                        index++;
                    }
                }
        }

        public void lockShape()
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].lockedArr();
            }
        }

        public Stack<int> checkFullLine()
        {
            Stack<int> fullLine = new Stack<int>();
            int rootLine = y / Constant.d;
            int dxLine = row;
            int i, j, counter;
            for (i = rootLine; i < rootLine + dxLine; i++)
            {
                counter = 0;
                for (j = 0; j < Constant.yMax; j++)
                {
                    if (Constant.onMap(i, j) && Constant.map[i, j] != 0) 
                        counter++;
                }
                if (counter == Constant.yMax)
                {
                    fullLine.Push(i);
                }
            }
            return fullLine;
        }

        public bool checkGameOver()
        {
            int i;
            for (i = 0; i < 4; i++)
            {
                if (cube[i].ScreenY < 0) 
                    return true;
            }
            return false;
        }
    }
}
