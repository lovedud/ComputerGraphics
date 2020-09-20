using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task3
{
    public partial class Form1 : Form
    {
        bool begin;
        Point point1, point2;
        int penwidth = 1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            begin = false;
            pictureBox1.BackColor = Color.White;
        }

        private Tuple<int, int> Direction(Point a, Point b)
        {
            int x = 0;
            int y = 0;

            if (a.X > b.X)
                x = -1;
            else if (a.X < b.X)
                x = 1;

            if (a.Y > b.Y)
                y = -1;
            else if (a.Y < b.Y)
                y = 1;

            return new Tuple<int, int>(x, y);
        }

        private void Bretzenheim(Graphics graphics, Pen p, Point a, Point b)
        {
            Bitmap bm = new Bitmap(1, 1);

            double dx = Math.Abs(b.X - a.X);
            double dy = Math.Abs(b.Y - a.Y);
            double grad = dy/dx;
            var txy = Direction(a, b);
            int dirx = txy.Item1;
            int diry = txy.Item2;
            int x = a.X;
            int y = a.Y;
            if (grad <= 1)
            {
                double d = 2 * dy - dx;
                while (x != b.X || y != b.Y)
                {
                    
                    if (d < 0)
                    {
                        d = d + 2 * dy;
                    }
                    else
                    {
                        y += diry;
                        d = d + 2 * (dy - dx);
                    }
                    x += dirx;
                    bm.SetPixel(0, 0, Color.Red);
                    graphics.DrawImageUnscaled(bm, x, y);
                }
            }
            else 
            {
                double d = 2 * dx - dy;
                while (x != b.X || y != b.Y)
                {

                    if (d < 0)
                    {
                        d = d + 2 * dx;
                    }
                    else
                    {
                        x += dirx;
                        d = d + 2 * (dx - dy);
                    }
                    y += diry;
                    bm.SetPixel(0, 0, Color.Red);
                    graphics.DrawImageUnscaled(bm, x, y);
                }
            }
        }

        private void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        private void DrawPoint(Graphics graphics , bool steep, int x, int y, double grade)
        {
            grade = 1 - grade;
            Bitmap bm = new Bitmap(1, 1);
            bm.SetPixel(0, 0, Color.FromArgb((int)(255 * grade), (int)(255 * grade), (int)(255 * grade)));
            if (steep)
                graphics.DrawImageUnscaled(bm, y, x);
            else
                graphics.DrawImageUnscaled(bm, x, y);
        }

        private void Wu(Graphics graphics, Pen p, int x0, int y0, int x1, int y1)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            DrawPoint(graphics, steep, x0, y0, 1);
            DrawPoint(graphics, steep, x1, y1, 1);
            float dx = x1 - x0;
            float dy = y1 - y0;
            float gradient = dy / dx;
            float y = y0 + gradient;
            for (var x = x0 + 1; x <= x1 - 1; x++)
            {
                DrawPoint(graphics, steep, x, (int)y, 1 - (y - (int)y));
                DrawPoint(graphics, steep, x, (int)y + 1, y - (int)y);
                y += gradient;
            }
        }

        private void MyPaint(Graphics graphics, Point a, Point b)
        {
            Pen p = new Pen(Color.Black, penwidth);
            if (checkBox2.Checked)
                Bretzenheim(graphics, p, a, b);
            else
                Wu(graphics, p, a.X,a.Y,b.X,b.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            penwidth = (int)numericUpDown1.Value;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox2.Checked = false;
            else
                checkBox2.Checked = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (begin)
            {
                Graphics g = pictureBox1.CreateGraphics();
                point2 = new Point(e.X, e.Y);
                begin = false;
                MyPaint(g, point1, point2);
            }
            else
            {
                begin = true;
                point1 = new Point(e.X, e.Y);
            }
        }
    }
}
