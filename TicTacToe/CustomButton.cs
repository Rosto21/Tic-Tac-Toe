/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TicTacToe
{
    public class CustomButton : Button
    {
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.Beige;

        public CustomButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(200,20);
            this.BackColor = Color.Green;
            this.ForeColor = Color.DarkGreen;
        }

        private GraphicsPath GetPath(RectangleF rect, float rad)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, rad, rad, 180, 90);
            path.AddArc(rect.Width-rad, rect.Y, rad, rad, 270, 90);
            path.AddArc(rect.Width-rad, rect.Y, rad, rad, 180, 90);
        }

    }
}
*/