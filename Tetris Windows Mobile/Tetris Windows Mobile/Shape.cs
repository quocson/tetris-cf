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

        #region Constructor
        public Shape(int kind, int color, int rotater)
        {
            int i, j, index;

            this.color = color;
            this.countRotate = rotater;

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
                    x = Map.startXscreen;
                    y = -56;
                    for (i = 0; i < row; i++)
                        statusArr[i, 0] = 1;
                    break;

                case 31:
                    row = col = 2;
                    x = Map.startXscreen;
                    y = -28;
                    for (i = 0; i < row; i++)
                        for (j = 0; j < col; j++)
                            statusArr[i, j] = 1;
                    break;

                default:
                    row = 2;
                    col = 3;
                    x = Map.startXscreen;
                    y = -28;
                    for (i = 0; i < row * col; i++)
                    {
                        statusArr[i / 3, i % 3] = (kind >> (5 - i)) & 1;
                    }
                    break;
            }

            for (i = 0; i <= countRotate; i++)
            {
                rotateArr();
            }

            index = 0;
            for (i = 0; i < row; i++)
                for (j = 0; j < col; j++)
                {
                    if (statusArr[i, j] == 1)
                    {
                        cube[index++] = new Block(x + j * 14, y + i * 14, color);
                    }
                }
        }
        #endregion

        public void Dispose()
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public int[,] getStatsArr(out int r, out int c)
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

        #region Draw& Eraser shape
        public void drawShape(Graphics gr)
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].drawBlock(gr);
            }
        }
        public void EraserShape(Graphics gr)
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].eraserBlock(gr);
            }
        }
        #endregion


        #region Check & Move shape

        public bool canMoveLeft()
        {
            if (x < 14 || (y + 14 * row) == 0) 
                return false;

            int tmpX = x - 14;
            int tmpY = y;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (statusArr[i, j] == 1)
                    {
                        Block tmp = new Block(tmpX + j * 14, tmpY + i * 14, color);
                        if (!tmp.rightPosition()) 
                            return false;
                    }
                }

            return true;

        }

        public bool canMoveRight()
        {
            if (x > Map.xMax * 14 - 14 || (y + 14 * row) == 0) 
                return false;

            int tmpX = x + 14;
            int tmpY = y;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (statusArr[i, j] == 1)
                    {
                        Block tmp = new Block(tmpX + j * 14, tmpY + i * 14, color);
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
                        Block tmp = new Block(x + (row - i - 1) * 14, y + j * 14, color);
                        if (!tmp.rightPosition()) 
                            rotatable = false;
                    }
                }

            if (rotatable == true)
                return true;

            int dx = ((x + 14 * tmpCol - Map.xMax * 14) / 14 <= 0) ? 0 : (x + 14 * tmpCol - Map.xMax) / 14;
            int tmpX = x - 14, tmpY = y;
            rotatable = true;
            do
            {
                for (i = 0; i < tmpRow; i++)
                    for (j = 0; j < tmpCol - dx; j++)
                    {
                        if (tmpArr[i, j] == 1)
                        {
                            Block tmp = new Block(tmpX + j * 14, tmpY + i * 14, color);
                            if (!tmp.rightPosition()) 
                                rotatable = false;
                        }
                    }
                if (!rotatable)
                    return false;

                if (rotatable)
                {
                    tmpX -= 14;
                    dx--;
                }
            } while (dx > 0);
            x = tmpX + 14;
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
            x -= 14;
        }

        public void moveRight()
        {
            int i;
            for (i = 0; i < 4; i++)
            {
                cube[i].moveRight();
            }
            x += 14;
        }

        public void fallDown()
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].moveDown();
            }
            y += Map.dyFallDown;
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
                        cube[index].ScreenX = x + j * 14;
                        cube[index].ScreenY = y + i * 14;
                        index++;
                    }
                }
        }

        #endregion

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
            int rootLine = y / 14;
            int dxLine = row;
            int i, j, counter;
            for (i = rootLine; i < rootLine + dxLine; i++)
            {
                counter = 0;
                for (j = 0; j < Map.yMax; j++)
                {
                    if (Map.onMap(i, j) && Map.map[i, j] != 0) 
                        counter++;
                }
                if (counter == Map.yMax)
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
