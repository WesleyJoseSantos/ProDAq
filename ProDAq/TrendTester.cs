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
    public partial class TrendTester : Form
    {
        public TrendTester()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            opcServerConnection1.Connect();
            trend1.Start();
        }
    }
}
