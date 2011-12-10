using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tetris_Windows_Mobile
{
    public partial class About : Form
    {
        private MainForm mainForm;

        public About(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
            mainForm.pauseResumeGame();
        }
    }
}