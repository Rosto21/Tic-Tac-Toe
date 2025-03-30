using System;
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
    public partial class Form3 : Form
    {
        public Form3(char victoryCase)
        {
            InitializeComponent();
            label1.AutoSize = true; 
            this.AutoSize = true;   
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            if (victoryCase != 'D')
                label1.Text = victoryCase + " won the game!";
            else
                label1.Text = "Draw!";
        }

        private int margin = 20;

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.AutoSize = true; 
            label1.Padding = new Padding(10);

            AdjustFormSize();
        }

        private void AdjustFormSize()
        {
            this.ClientSize = new Size(label1.Width + (margin * 2), label1.Height + (margin * 2));

            label1.Left = margin;
            label1.Top = margin;
        }
    }
}
