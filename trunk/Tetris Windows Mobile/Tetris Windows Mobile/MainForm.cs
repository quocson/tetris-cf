using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Tetris_Windows_Mobile
{
    public enum ModeGame {Ready, Playing, MenuFocus, Paused };

    public partial class MainForm : Form
    {
        private GameControl gameControl;
        private NextShape nextShape;
        private GameScore gameScore;
        private GameLevel gameLevel;
        private GameLine gameLine;
        private GamePiece gamePiece;
        private ModeGame modeGame;
        private int shapeNext;
        private int colorNext;
        private int rotaterNext;
        private int tempScore;
        private bool bGhost;
        private bool bSound;
        public int stt;
        private PlaySound playSound;
        Stack<int> full;
        

        public MainForm()
        {
            InitializeComponent();
            timer.Interval = Constant.speedGame;
            gameControl = new GameControl();
            nextShape = new NextShape();
            gameScore = new GameScore();
            gameLevel = new GameLevel();
            gameLine = new GameLine();
            gamePiece = new GamePiece();
            Controls.Add(gameControl);
            Controls.Add(nextShape);
            Controls.Add(gameScore);
            Controls.Add(gameLevel);
            Controls.Add(gameLine);
            Controls.Add(gamePiece);
            bGhost = true;
            bSound = true;
            tempScore = 0;
            full = new Stack<int>();
            playSound = new PlaySound();
            changeMode(ModeGame.Ready);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (modeGame == ModeGame.Playing)
                Constant.saver.saveRecords(gameScore.Score);
            gameControl.resetGame();

            gameScore.Score = 0;
            gameLevel.Level = 1;
            gameLine.Line = 0;
            gamePiece.Piece = 0;
            

            gameControl.gameInitObj(out  shapeNext, out  colorNext, out  rotaterNext);
            gameControl.setShape(shapeNext, colorNext, rotaterNext);
            nextShape.drawNextShape(shapeNext, colorNext, rotaterNext);
            gameControl.drawGhostShape(bGhost);
            gamePiece.Piece++;

            menuItem2.Enabled = true;
            menuItem9.Enabled = true;
            menuItem10.Enabled = true;
            menuItem2.Text = "Pause";
            if (bSound)
                playSound.playSoundTheme();
            changeMode(ModeGame.Playing);
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            changeMode(ModeGame.MenuFocus);
            Help help = new Help(this);
            help.ShowDialog();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            changeMode(ModeGame.MenuFocus);
            TopScores highScores = new TopScores(this);
            highScores.ShowDialog();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            changeMode(ModeGame.MenuFocus);
            About about = new About(this);
            about.ShowDialog();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (modeGame == ModeGame.Paused)
            {
                stt = 1;
                menuItem2.Text = "Pause";
                menuItem9.Enabled = true;
                menuItem10.Enabled = true;
                if (bSound)
                    playSound.playSoundTheme();
                changeMode(ModeGame.Playing);
            }
            else
            {
                stt = 2;
                menuItem2.Text = "Resume";
                menuItem9.Enabled = false;
                menuItem10.Enabled = false;
                playSound.stopSoundTheme();
                changeMode(ModeGame.Paused);
            }
        }

        private void MainForm_LostFocus(object sender, EventArgs e)
        {
        }

        private void MainForm_GotFocus(object sender, EventArgs e)
        {
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        public  void changeMode(ModeGame mode)
        {
            modeGame = mode;
            if (mode == ModeGame.MenuFocus)
            {
                if (bSound)
                    playSound.stopSoundTheme();
                timer.Enabled = false;
            }
            if (mode == ModeGame.Playing)
            {
                stt = 1;
                if (bSound)
                    playSound.playSoundTheme();
                timer.Enabled = true;
            }
            if (mode == ModeGame.Ready)
                stt = 0;
            if (mode == ModeGame.Paused)
                if (bSound)
                    playSound.stopSoundTheme();
                
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch(modeGame)
            {
                    
                case  ModeGame.Playing:
                    gameControl.drawPanel();
                    if  (!gameControl.gameObjFall())
                    {
                        gameControl.locked();
                        gameControl.drawPanel();
                        gameControl.setGhostNull();
                        if (gameControl.isEndGame())
                        {
                            this.timer.Enabled = false;
                            bool newgame = false;
                            if (bSound)
                                playSound.playSoundGameOver();
                            int rank = Constant.saver.saveRecords(gameScore.Score);
                            if ( rank > 0)
                            {
                                if (MessageBox.Show("New High Score: " + gameScore.Score + "\nRank: " + rank  + "\nDo you want to play again?",
                                    "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    newgame = true;
                                }
                            }

                            else
                                if (MessageBox.Show("Your score: " + gameScore.Score + "\nDo you want to play again?",
                                    "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    newgame = true;
                                }
                            if (newgame)
                            {
                                gameControl.resetGame();

                                gameScore.Score = 0;
                                gameLevel.Level = 1;
                                gameLine.Line = 0;
                                gamePiece.Piece = 0;
                                gameControl.gameInitObj(out  shapeNext, out  colorNext, out  rotaterNext);
                                gameControl.setShape(shapeNext, colorNext, rotaterNext);
                                nextShape.drawNextShape(shapeNext, colorNext, rotaterNext);
                                gamePiece.Piece++;

                                menuItem2.Enabled = true;
                                menuItem2.Text = "Pause";
                                if (bSound)
                                    playSound.playSoundTheme();
                                changeMode(ModeGame.Playing);
                                timer.Enabled = true;
                            }
                            else
                            {
                                changeMode(ModeGame.Ready);
                                playSound.stopSoundTheme();
                                timer.Enabled = true;
                                menuItem2.Enabled = false;
                                return;
                            }
                        }   
                        int val, i = 0;
                        bool isfull;
                        isfull = ((full = gameControl.fullLine()).Count > 0);

                       
                        tempScore += (full.Count / 4) * 100;
                        gameLine.Line += full.Count;
                        int c = 0;
                        int numRow = full.Count;
                        while (full.Count > 0)
                        {
                            c++;
                            val = full.Pop() + i;
                            tempScore += c * (Constant.yMax / Constant.d) * (24 - val);
                            gameControl.deleteLine(val);
                            Constant.updateMap(val, ref i);
                        }
                        if (bSound)
                            switch (numRow)
                            {
                                case 1: playSound.playSoundAmazing(); break;
                                case 2: playSound.playSoundVeryGood(); break;
                                case 3: playSound.playSoundBrilliant(); break;
                                case 4: playSound.playSoundWonderful(); break;
                            }
                        gameScore.Score += tempScore;
                        levelUp();
                        tempScore = 0;
                       

                        if (gameLevel.Level == 99)
                        {
                            this.timer.Enabled = false;
                            bool newgame = false;
                            if (bSound)
                                playSound.playSoundGameWin();
                            int rank = Constant.saver.saveRecords(gameScore.Score);
                            if (rank > 0)
                            {
                                if (MessageBox.Show("New High Score: " + gameScore.Score + "\nRank: " + rank + "\nDo you want to play again?",
                                    "You Win!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    newgame = true;
                                }
                            }

                            else
                                if (MessageBox.Show("Your score: " + gameScore.Score + "\nDo you want to play again?",
                                    "You Win!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    newgame = true;
                                }
                            if (newgame)
                            {
                                gameControl.setGhostNull();
                                gameControl.resetGame();

                                gameScore.Score = 0;
                                gameLevel.Level = 1;
                                gameLine.Line = 0;
                                gamePiece.Piece = 0;
                                gameControl.gameInitObj(out  shapeNext, out  colorNext, out  rotaterNext);
                                gameControl.setShape(shapeNext, colorNext, rotaterNext);
                                nextShape.drawNextShape(shapeNext, colorNext, rotaterNext);
                                gamePiece.Piece++;

                                menuItem2.Enabled = true;
                                menuItem2.Text = "Pause";
                                if (bSound)
                                    playSound.playSoundTheme();
                                changeMode(ModeGame.Playing);
                                timer.Enabled = true;
                            }
                            else
                            {
                                changeMode(ModeGame.Ready);
                                playSound.stopSoundTheme();
                                timer.Enabled = true;
                                return;
                            }
                        }
                        else
                        {

                            gameControl.gameInitObj(out  shapeNext, out  colorNext, out  rotaterNext);
                            gameControl.setShape(shapeNext, colorNext, rotaterNext);
                            nextShape.drawNextShape(shapeNext, colorNext, rotaterNext);
                            gameControl.drawGhostShape(bGhost);
                            gamePiece.Piece++;
                        }

                    }
                    break;

                case ModeGame.Paused:
                    break;

                case ModeGame.Ready:
                    break;

                case ModeGame.MenuFocus:
                    break;
            }   
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            if (bSound)
            {
                playSound.stopSoundTheme();
                bSound = false;
            }
            else
            {
                playSound.playSoundTheme();
                bSound = true;
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            changeMode(ModeGame.Paused);

            if (MessageBox.Show("Are you sure you want to exit?",
                "Exit confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Constant.saver.saveRecords(gameScore.Score);
                gameControl.destroy();
                gameLevel.destroy();
                gameLine.destroy();
                gamePiece.destroy();
                gameScore.destroy();
                nextShape.destroy();
                playSound.Dispose();
                Application.Exit();
            }
            else
                if (stt == 0)
                    changeMode(ModeGame.Ready);
                else
                    if (stt == 1)
                        changeMode(ModeGame.Playing);
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            if (bGhost && gameControl.check() && modeGame == ModeGame.Playing)
            {
                gameControl.eraserGhostShape();
                bGhost = false;
            }
            else
            {
                gameControl.drawGhostShape();
                bGhost = true;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int temp = 0;
            if (modeGame == ModeGame.Playing)
            {
                gameControl.keyDown(e, ref temp, bGhost);
                gameScore.Score += temp;
                levelUp();
            }
        }
        public void levelUp()
        {
            if (gameScore.Score > gameLevel.Level * gameLevel.Level * 999)
            {
                gameLevel.Level++;
                if (bSound)
                    playSound.playSoundLevelUp();
                Constant.speedGame = 1000 - (gameLevel.Level / 10 * 100);
                gameControl.resetGame();
            }
        }
    }
}