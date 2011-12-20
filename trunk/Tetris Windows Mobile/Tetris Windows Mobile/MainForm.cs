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
    public enum ModeGame {Ready, Playing, MenuFocus, Paused, Over, Win };

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
            stt = 0;
            tempScore = 0;
            full = new Stack<int>();
            playSound = new PlaySound();
            changeMode(ModeGame.Ready);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            changeMode(ModeGame.Paused);

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
                            changeMode(ModeGame.Over);
                            return;
                        }
                        int val, i = 0;
                        bool isfull;
                        isfull = ((full = gameControl.fullLine()).Count > 0);

                       
                        tempScore += (full.Count / 4) * 100;
                        gameLine.Line += full.Count;
                        int c = 0;
                        while (full.Count > 0)
                        {
                            c++;
                            val = full.Pop() + i;
                            tempScore += c * (Constant.yMax / Constant.d) * (24 - val);
                            gameControl.deleteLine(val);
                            Constant.updateMap(val, ref i);
                        }
                        gameScore.Score += tempScore;
                        tempScore = 0;
                        if (gameScore.Score > gameLevel.Level * gameLevel.Level * 999)
                        {
                            gameLevel.Level++;
                            Constant.speedGame = 1000 - (gameLevel.Level / 10 * 100);
                            gameControl.resetGame();
                        }
                        

                        if (gameLevel.Level == 99)
                           changeMode(ModeGame.Win);
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

                case ModeGame.Over:
                    break;

                case ModeGame.Win:
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
            //playSound.stopSoundTheme();
            //playSound.stopSoundPlayer();
            //connecting.updateScore(gameScore.Score);
            //save game (if dang choi).
            gameControl.destroy();
            gameLevel.destroy();
            gameLine.destroy();
            gamePiece.destroy();
            gameScore.destroy();
            nextShape.destroy();
            playSound.Dispose();
            Application.Exit();
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
                gameControl.keyDown(e, playSound, bSound, ref temp, bGhost);
                gameScore.Score += temp;
            }
        }
    }
}