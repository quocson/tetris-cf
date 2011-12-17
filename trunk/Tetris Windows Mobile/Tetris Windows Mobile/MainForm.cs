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
    public enum ModeGame { Ready, Loading, New, Playing, CanFall, Paused, Over, Win };

    public partial class MainForm : Form
    {
        private GameControl gameControl;
        private ModeGame modeGame;
        private int shapeNext;
        private int colorNext;
        private int rotaterNext;
        Stack<int> full;

        public MainForm()
        {
            InitializeComponent();
            timer.Interval = Constant.speedGame;
            gameControl = new GameControl();
            Controls.Add(gameControl);
            full = new Stack<int>();
            changeMode(ModeGame.Ready);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            changeMode(ModeGame.Paused);

            gameControl.resetGame();

            gameControl.gameInitObj(out shapeNext, out colorNext, out rotaterNext);
            gameControl.setShape(shapeNext, colorNext, rotaterNext);

            changeMode(ModeGame.Playing);

            timer.Enabled = true;
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            //continue game.
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            Help help = new Help(this);
            help.ShowDialog();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            TopScores highScores = new TopScores(this);
            highScores.ShowDialog();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            About about = new About(this);
            about.ShowDialog();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
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
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch(modeGame)
            {
                case ModeGame.Loading:
                    break;

                case  ModeGame.New:
                    gameControl.setShape(shapeNext, colorNext, rotaterNext);
                    gameControl.gameInitObj(out  shapeNext, out  colorNext, out  rotaterNext);
                    changeMode(ModeGame.Playing);
                    break;

                case  ModeGame.Playing:
                    gameControl.drawPanel();
                    if  (!gameControl.gameObjFall())
                    {
                        gameControl.locked();
                        gameControl.drawPanel();
                        if (gameControl.isEndGame())
                        {
                            changeMode(ModeGame.Over);
                            return;
                        }
                        else
                        {
                            changeMode(ModeGame.New);
                        }
                    }
                    break;

                case ModeGame.Paused:
                    break;

                case ModeGame.Over:
                    break;

                case ModeGame.Win:
                    break;
            }   
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}