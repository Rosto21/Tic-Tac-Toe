﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form2 : Form
    {
        public Form2(Tuple <byte, byte> tuple)
        {
            InitializeComponent();
            label1.Text = Convert.ToString(tuple);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
