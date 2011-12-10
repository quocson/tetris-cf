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
    public partial class MainForm : Form
    {
        private int score;
        private int level;
        private int line;
        private int piece;
        static private Bitmap imgBackLoad;
        private Bitmap backBuffer;
        static MainForm()
        {
            imgBackLoad = new Bitmap(Tetris_Windows_Mobile.Properties.Resources.background);
        }
        public MainForm()
        {

            backBuffer = new Bitmap(240, 294);
            InitializeComponent();
        }

        public void pauseResumeGame()
        {
        }

        private void updateGame()
        {
        }

        private void keyPress (char key)
        {
        }

        private void newGame()
        {
            score = level = line = piece = 0;
        }

        private void continueGame()
        {
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            continueGame();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            pauseResumeGame();
            Help help = new Help(this);
            help.ShowDialog();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            pauseResumeGame();
            TopScores highScores = new TopScores(this);
            highScores.ShowDialog();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            pauseResumeGame();
            About about = new About(this);
            about.ShowDialog();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            pauseResumeGame();
        }

        private void MainForm_LostFocus(object sender, EventArgs e)
        {
            pauseResumeGame();
        }

        private void MainForm_GotFocus(object sender, EventArgs e)
        {
            pauseResumeGame();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress(e.KeyChar);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics.FromImage(backBuffer).DrawImage(imgBackLoad,0,0);
            e.Graphics.DrawImage(backBuffer, 0, 0);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            updateGame();
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