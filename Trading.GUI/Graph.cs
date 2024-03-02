
using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Trading.GUI
{
    public partial class Graph : Form
    {
        public Graph()
        {
            InitializeComponent();
            DoubleBuffered = true;

        }
        Pen currentPen = new Pen(Color.Black);
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Point a = new Point(20, 30);
            Point b = new Point(400, 500);
            gr.DrawLine(currentPen, a, b);
        }
    }
}
