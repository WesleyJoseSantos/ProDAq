using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProDAq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            serialText1.Begin();
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            serialText1.End();
        }

        private void trend_MouseClick(object sender, MouseEventArgs e)
        {
            propertyGrid.SelectedObject = trend;
        }

        private void btTrendStart_Click(object sender, EventArgs e)
        {
            trend.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trend.Stop();
        }
    }
}
