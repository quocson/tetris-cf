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
    public partial class TopScores : Form
    {
        private MainForm mainForm;

        public TopScores(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connecting = new Connecting(listView1);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}