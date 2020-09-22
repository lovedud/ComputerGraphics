using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BorderSelection
{
    enum Direction
    {
        D0,
        D45,
        D90,
        D135,
        D180,
        D225,
        D270,
        D315
    }
    public partial class Form1 : Form
    {
        int max_x = 0;
        int max_y = 0;
        int prev_x = 0;
        int prev_y = 0;
        bool moving = false;
        Graphics g;
        Bitmap bm;
        Pen pen;
        Color borderColor;
        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            pen.Width = 1;
            borderColor = pen.Color;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            prev_x = e.X;
            prev_y = e.Y;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                g.DrawLine(pen, prev_x, prev_y, e.X, e.Y);
                pictureBox1.Refresh();
                prev_x = e.X;
                prev_y = e.Y;
                if (e.X >= max_x)
                {
                    if (e.X == max_x && e.Y < max_y)
                        max_y = e.Y;
                    if (e.X > max_x)
                    {
                        max_x = e.X;
                        max_y = e.Y;
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }
        private Direction RotateN(Direction dir, int n)// if n > 0 - по часовой, else 
        {
            int res = (int)dir - n;
            if (res < 0)
            {
                res = res + 8;
            }
            else if(res > 7)
            {
                res = res - 8;
            }
            return (Direction)res;
        }
        private Point PointOnDirection(Point center_p, Direction dir)
        {
            switch(dir)
            {
                case Direction.D0:
                    return new Point(center_p.X + 1, center_p.Y);
                case Direction.D45:
                    return new Point(center_p.X + 1, center_p.Y - 1);
                case Direction.D90:
                    return new Point(center_p.X, center_p.Y - 1);
                case Direction.D135:
                    return new Point(center_p.X - 1, center_p.Y - 1);
                case Direction.D180:
                    return new Point(center_p.X - 1, center_p.Y);
                case Direction.D225:
                    return new Point(center_p.X - 1, center_p.Y + 1);
                case Direction.D270:
                    return new Point(center_p.X, center_p.Y + 1);
                case Direction.D315:
                    return new Point(center_p.X + 1, center_p.Y + 1);

            }
            return new Point(0, 0);
                
        }
        private Point FindNextBorderPoint(Point p, ref Direction dir)
        {
            Direction init_dir = dir;
            for(int i = 0; i < 7; i++)
            {
                dir = RotateN(init_dir, -i);
                Point cur_p = PointOnDirection(p, dir);
                Color pixel =  bm.GetPixel(cur_p.X, cur_p.Y);
                if (EqualColors(pixel, borderColor))
                    return cur_p;
            }
            return new Point(-1, -1);
        }
        private bool EqualColors(Color c1, Color c2)
        {
            return c1.A == c2.A && c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        private List<Point> SelectBorder()
        {
            List<Point> border = new List<Point>();
            Point cur_p = new Point(max_x, max_y);
            Direction dir = Direction.D270; // down
            while (!border.Contains(cur_p) && cur_p.X != -1)
            {
                border.Add(cur_p);
                cur_p = FindNextBorderPoint(cur_p, ref dir);
                dir = RotateN(dir, 2);
            }
            border.Sort((p1, p2) => (p1.Y == p2.Y ? p1.X.CompareTo(p2.X) : p1.Y.CompareTo(p2.Y)));
            return border;
        }
        private void DrawBorder(List<Point> border)
        {
            foreach(var p in border)
            {
                bm.SetPixel(p.X, p.Y, Color.Red);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Point> border = SelectBorder();
            DrawBorder(border);
            pictureBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bm;
            g = Graphics.FromImage(bm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bm;
            g = Graphics.FromImage(bm);
        }
    }
}
