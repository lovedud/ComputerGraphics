using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bm;
            g = Graphics.FromImage(bm);
        }

        public enum State
        {
            RotateAroundLine,
            MoveP,
            Scale
        }
        

        Mode cur_mode;
        OrtMode cur_ort_mode;
        State cur_state;
        Edge3D RAL; // прямая, вокруг которой происходит вращение start - точка начала, end - нормализованный вектор, задающий направление
        Polyhedron cur_polyhedron;
        Bitmap bm;
        Graphics g;
        Point3D start_point;
        int c = 1600;
        Projector projector;
        Polyhedron view_vector;
        Point3D begview;
        bool FHAflag = false;

        public void Draw(bool drawpoint = true, bool update = true)
        {
            g.Clear(Color.White);
            if (cur_polyhedron is null)
                return;
            List<Edge> edges = projector.Project(cur_mode, cur_polyhedron);
            
            //DrawAxis(start_point); убрал, так как сломались ( становятся не по центру объекта)
            foreach (var edge in edges)
            {
                DrawEdge(ref g, ref bm, edge, drawpoint);
            }
            pictureBox1.Image = bm;
            if (update)
                pictureBox1.Update();
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
        private List<int> cur_points(Point a, Point b)
        {

            double dx = Math.Abs(b.X - a.X);
            double dy = Math.Abs(b.Y - a.Y);
            double grad = dy / dx;
            var txy = Direction(a, b);
            int dirx = txy.Item1;
            int diry = txy.Item2;
            int x = a.X;
            int y = a.Y;
            List<int> coords = new List<int>(bm.Width);
            for (int i = 0; i < bm.Width; i++)
            {
                coords.Add(-1);
            }
            coords[x] = y;
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
                    coords[x] = y;
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
                    coords[x] = y;
                }
            }
            return coords;
        }

        private double PointDistance(Point3D p1, Point3D p2)
        {
            double x = p1.X - p2.X;
            double y = p1.Y - p2.Y;
            double z = p1.Z - p2.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public void DrawFHA(int amountx, int amounty)
        {
            g.Clear(Color.White);
            if (cur_polyhedron is null)
                return;
            List<Edge> edges = projector.ProjectFHA(cur_polyhedron);
            List<int> upperbound = new List<int>(bm.Width);
            List<int> lowerbound = new List<int>(bm.Width);
            for (int i = 0; i < bm.Width; i++)
            {
                upperbound.Add(-1);
                lowerbound.Add(bm.Height + 5);
            }
            List<int> cur;
            int x = Math.Abs(anglex) % 360;
            int y = Math.Abs(angley) % 360;
            int z = Math.Abs(anglez) % 360;
            if (x <= 180) //x < 180
            {
                if (z <= 45 || z >= 315)///////////xxxxxxxxxxxxxxxxxx
                {
                    for (int i = 0; i < amountx; i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }

                        }
                    }
                    for (int i = amountx; i < amountx * (amounty + 1); i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }
                        }
                    }
                }
                else if (z >= 135 && z <= 225)    ///////////////////xxxxxxxxxxxxxxx
                {
                    for (int i = amountx * (amounty + 1) - 1; i > amountx * (amounty + 1) - amountx; i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }

                        }
                    }
                    for (int i = amountx * (amounty + 1) - amountx; i > -1; i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }
                        }
                    }
                }
                else if (z > 45 && z < 135)///////////////yyyyyyyyyyy
                {
                    for (int i = amountx * (amounty + 1); i < amountx * (amounty + 1) + amounty; i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }

                        }
                    }
                    for (int i = amountx * (amounty + 1) + amounty; i < edges.Count; i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }
                        }
                    }
                }
                else if (z > 225 && z < 315)//////////yyyyyyyyyyyyyyyyy
                {
                    for (int i = edges.Count - 1; i > edges.Count - 1 - amounty; i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }

                        }
                    }
                    for (int i = edges.Count - 1 - amounty; i >= amountx * (amounty + 1); i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                            }
                        }
                    }
                }
            }
            else //x > 180
            {
                if (z <= 45 || z >= 315)///////////xxxxxxxxxxxxxxxxxx
                {
                    for (int i = amountx * (amounty + 1) - 1; i > amountx * (amounty + 1) - amountx; i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }

                        }
                    }
                    for (int i = amountx * (amounty + 1) - amountx; i > -1; i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }
                        }
                    }
                }
                else if (z >= 135 && z <= 225)    ///////////////////xxxxxxxxxxxxxxx
                {
                    for (int i = 0; i < amountx; i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }

                        }
                    }
                    for (int i = amountx; i < amountx * (amounty + 1); i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }
                        }
                    }
                }
                else if (z > 45 && z < 135)///////////////yyyyyyyyyyy
                {
                    for (int i = edges.Count - 1; i > edges.Count - 1 - amounty; i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }

                        }
                    }
                    for (int i = edges.Count - 1 - amounty; i >= amountx * (amounty + 1); i--)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)), new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }
                        }
                    }
                }
                else if (z > 225 && z < 315)//////////yyyyyyyyyyyyyyyyy
                {
                    for (int i = amountx * (amounty + 1); i < amountx * (amounty + 1) + amounty; i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }

                        }
                    }
                    for (int i = amountx * (amounty + 1) + amounty; i < edges.Count; i++)
                    {
                        cur = cur_points(new Point((int)Math.Round(edges[i].start.X), (int)Math.Round(edges[i].start.Y)), new Point((int)Math.Round(edges[i].end.X), (int)Math.Round(edges[i].end.Y)));
                        for (int k = 0; k < cur.Count; ++k)
                        {
                            if (cur[k] > 0 && cur[k] < bm.Height)
                            {
                                if (upperbound[k] < cur[k])
                                {
                                    upperbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Black);
                                }
                                if (lowerbound[k] > cur[k])
                                {
                                    lowerbound[k] = cur[k];
                                    bm.SetPixel(k, cur[k], Color.Blue);
                                }
                            }
                        }
                    }
                }
            }
            pictureBox1.Image = bm;
            pictureBox1.Update();

        }
        private void DrawAxis(Point3D center)
        {
            var new_axis = GetAxis(center, cur_mode, projector);
            g.DrawLine(new Pen(Color.Red), new_axis[0].start, new_axis[0].end);
            g.DrawLine(new Pen(Color.Blue), new_axis[1].start, new_axis[1].end);
            g.DrawLine(new Pen(Color.Green), new_axis[2].start, new_axis[2].end);
        }

        public void Clear()
        {
            g.Clear(Color.White);

            pictureBox1.Image = bm;
        }

        public int AngleBetweenPoints(Point p1, Point p2)
        {
            Point p1v = new Point(200, 0);
            Point p2v = new Point(p2.X-p1.X, p1.Y - p2.Y);
            double a = Math.Sqrt(p1v.X * p1v.X + p1v.Y * p1v.Y) * Math.Sqrt(p2v.X * p2v.X + p2v.Y * p2v.Y);
            double b = p1v.X * p2v.X + p1v.Y * p2v.Y;
            double c = b / a;
            double d = Math.Acos(c)*180/Math.PI;
            if (p2.Y > p1.Y)
                d = 360-d;
            return (int)d;
        }

        private int AngleBetweenVectorsXY(Point3D v1, Point3D n2)
        {
            //Point3D n1 = NormalizedVector(new Edge3D(new Point3D(0, 0, 0), new Point3D(v1.X, v1.Y, 0)));
            Point3D v2 = NormalizedVector(new Edge3D(new Point3D(0, 0, 0), new Point3D(n2.X, n2.Y, 0)));
            double a = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y) * Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y);
            double b = v1.X * v2.X + v1.Y * v2.Y;
            double c = b / a;
            double d = Math.Acos(c) * 180 / Math.PI;
            //if (v2.Y > v1.Y)
            //    d = 360 - d;
            return (int)d;
        }

        private int AngleBetweenVectors3D(Point3D v1, Point3D v2)
        {
            double a = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z) * Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z);
            double b = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            double c = b / a;
            double d = Math.Acos(c) * 180 / Math.PI;
            //if (v2.Y > v1.Y)
            //    d = 360 - d;
            return (int)d;
        }

        private void OrtButtonAvailability()
        {
            if (!Ortxy.Checked && !Ortxz.Checked && !Ortyz.Checked)
            {
                ort_button.Enabled = false;
            }
        }

        private void Ortxy_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortxy.Checked)
            {
                Ortxz.Checked = false;
                Ortyz.Checked = false;
                cur_ort_mode = OrtMode.XY;
                projector.Update(cur_ort_mode);
                //ort_button.Enabled = true;
                Draw();
            }
            else OrtButtonAvailability();

        }

        private void Ortxz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortxz.Checked)
            {
                Ortxy.Checked = false;
                Ortyz.Checked = false;
                cur_ort_mode = OrtMode.XZ;
                projector.Update(cur_ort_mode);
                //ort_button.Enabled = true;
                Draw();
            } else OrtButtonAvailability();
        }

        private void Ortyz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortyz.Checked)
            {
                Ortxz.Checked = false;
                Ortxy.Checked = false;
                cur_ort_mode = OrtMode.YZ;
                projector.Update(cur_ort_mode);
                //ort_button.Enabled = true;
                Draw();
            } else OrtButtonAvailability();
        }

        private void Ort_Button_Click(object sender, EventArgs e)
        {
            cur_mode = Mode.Orthographic;
            ort_button.Enabled = false;
            iso_button.Enabled = true;
            perspective_button.Enabled = true;
            Draw();
        }


        private void ClearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cur_mode = Mode.Orthographic;
            ort_button.Enabled = false;
            
            start_point = new Point3D(pictureBox1.Width / 2 - 50, pictureBox1.Height / 2 - 50, 300);

            cur_state = State.MoveP;
            button3.Enabled = false;

            cur_polyhedron = CreateCube(new Point3D(0 , 0, 0), 100);
            s_x.Text = (pictureBox1.Width / 2).ToString();
            s_y.Text = (pictureBox1.Height / 2).ToString();
            s_z.Text = (pictureBox1.Width / 2).ToString();

            e_x.Text = (1).ToString();
            e_y.Text = (0).ToString();
            e_z.Text = (0).ToString();
            projector = new Projector(c);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            view_vector = new Polyhedron();
            begview = new Point3D(1, 0, 0);
            view_vector.AddPoints(new List<Point3D> { new Point3D(begview.X, begview.Y, begview.Z) });
            Draw();
        }

        private void Rota_Click(object sender, EventArgs e)
        {
            cur_state = State.RotateAroundLine;
            rotateAroundLine.Enabled = false;
            OX.Enabled = false;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = true;
            button3.Enabled = true;
            scaleButton.Enabled = true;
            Point3D center = cur_polyhedron.Center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(1, 0, 0));
        }

        private void OX_Click(object sender, EventArgs e)
        {
            OX.Enabled = false;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = true;
            Point3D center = cur_polyhedron.Center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(1, 0, 0));
        }

        private void OY_Click(object sender, EventArgs e)
        {
            OX.Enabled = true;
            OY.Enabled = false;
            OZ.Enabled = true;
            Custom.Enabled = true;
            Point3D center = cur_polyhedron.Center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(0, 1, 0));
        }

        private void OZ_Click(object sender, EventArgs e)
        {
            OX.Enabled = true;
            OY.Enabled = true;
            OZ.Enabled = false;
            Custom.Enabled = true;
            Point3D center = cur_polyhedron.Center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(0, 0, 1));
        }

        private void Custom_Click(object sender, EventArgs e)
        {
            OX.Enabled = true;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = false;
            CheckForCustomLine(sender, e);
        }

        Point point_angle = new Point(0,0);
        bool m_down = false;
        Edge3D RAL_toDraw;

        int prev_angle = 0;
        
        Point3D prevMouseMove;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            point_angle.X = e.X-50;
            point_angle.Y = e.Y;
            m_down = true;
            if (cur_state == State.RotateAroundLine)
            {
                RAL_toDraw = new Edge3D(new Point3D(RAL.start.X - RAL.end.X * 500, RAL.start.Y - RAL.end.Y * 500, RAL.start.Z - RAL.end.Z * 500),
                                        new Point3D(RAL.start.X + RAL.end.X * 500, RAL.start.Y + RAL.end.Y * 500, RAL.start.Z + RAL.end.Z * 500));
            }
            prev_angle = 0;


            prevMouseMove = new Point3D(e.X, e.Y, 0);
        }

        int anglex = 0;
        int angley = 0;
        int anglez = 0;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_down && cur_state == State.RotateAroundLine && !(cur_polyhedron is null))
            {
                int angle = AngleBetweenPoints(point_angle, new Point(e.X, e.Y));
                cur_polyhedron.RotateAroundLine(RAL.start, RAL.end, prev_angle - angle);
                view_vector.RotateAroundLine(new Point3D(0,0,0), RAL.end, prev_angle - angle);
                if ((int)RAL.end.X == 1)
                    anglex += prev_angle - angle;
                else if ((int)RAL.end.Y == 1)
                    angley += prev_angle - angle;
                else
                    anglez += prev_angle - angle;

                prev_angle = angle;
                if (FHAflag)
                    DrawFHA(int.Parse(X_step.Text), int.Parse(Y_step.Text));
                else
                    Draw(false,false);
                DrawPoint(ref bm, new PointF(point_angle.X, point_angle.Y), Color.Orange);

                //var edge = projector.Project(cur_mode, RAL_toDraw);
                //g.DrawLine(new Pen(Color.Orange, 1), edge.start, edge.end); //Не работает из-за нового центрирования
                
                pictureBox1.Image = bm;

                pictureBox1.Update();
            }
            if (m_down && cur_state == State.MoveP && !(cur_polyhedron is null))
            {
                if (Ortxy.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);

                    cur_polyhedron.getMoved(mouseMove);
                    if (FHAflag)
                        DrawFHA(int.Parse(X_step.Text), int.Parse(Y_step.Text));
                    else
                        Draw(false);
                }
                else if (Ortxz.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);

                    cur_polyhedron.getMoved(mouseMove);
                    if (FHAflag)
                        DrawFHA(int.Parse(X_step.Text), int.Parse(Y_step.Text));
                    else
                        Draw(false);
                }
                else if (Ortyz.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);

                    cur_polyhedron.getMoved(mouseMove);
                    if (FHAflag)
                        DrawFHA(int.Parse(X_step.Text), int.Parse(Y_step.Text));
                    else
                        Draw(false);
                    
                }

                prevMouseMove.X = e.X;
                prevMouseMove.Y = e.Y;

                pictureBox1.Image = bm;
            }
            if (m_down && cur_state == State.Scale && !(cur_polyhedron is null))
            {
                if (Ortxyz.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, e.X - prevMouseMove.X, e.X - prevMouseMove.X);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 - mouseMove.X * 0.01, 1 - mouseMove.X * 0.01);
                }
                else if (Ortxy.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 + mouseMove.Y * 0.01, 1 - mouseMove.Z * 0.01);
                }
                else if (Ortxz.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 + mouseMove.Y * 0.01, 1 - mouseMove.Z * 0.01);
                }
                else if (Ortyz.Checked)
                {
                    Point3D center = cur_polyhedron.Center();
                    Point3D mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 + mouseMove.Y * 0.01, 1 - mouseMove.Z * 0.01);
                }
                

                prevMouseMove.X = e.X;
                prevMouseMove.Y = e.Y;
                if (FHAflag)
                    DrawFHA(int.Parse(X_step.Text), int.Parse(Y_step.Text));
                else
                    Draw(false);
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m_down = false;
            if (FHAflag)
                DrawFHA(int.Parse(X_step.Text), int.Parse(Y_step.Text));
            else
                Draw(false);
        }
        private void iso_button_Click(object sender, EventArgs e)
        {
            cur_mode = Mode.Isometric;
            ort_button.Enabled = true;
            iso_button.Enabled = false;
            perspective_button.Enabled = true;
            Draw();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cur_state = State.MoveP;
            rotateAroundLine.Enabled = true;
            OX.Enabled = false;
            OY.Enabled = false;
            OZ.Enabled = false;
            Custom.Enabled = false;
            button3.Enabled = false;
            scaleButton.Enabled = true;
        }
        private void Cub_Button_Click(object sender, EventArgs e)
        {
            FHAflag = false;
            cur_polyhedron = CreateCube(new Point3D(0, 0, 0), 100);
            Draw();
        }
       

        private void perspective_button_Click(object sender, EventArgs e)
        {
            cur_mode = Mode.Perspective;
            ort_button.Enabled = true;
            iso_button.Enabled = true;
            perspective_button.Enabled = false;
            Draw();
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            cur_state = State.Scale;
            rotateAroundLine.Enabled = true;
            OX.Enabled = false;
            OY.Enabled = false;
            OZ.Enabled = false;
            Custom.Enabled = false;
            button3.Enabled = true;
            scaleButton.Enabled = false;
        }

        private void CheckForCustomLine(object sender, EventArgs e)
        {
            float x1, y1, z1, x2, y2, z2;
            if ((s_x.Text != e_x.Text || s_y.Text != e_y.Text || s_z.Text != e_z.Text)
                && float.TryParse(s_x.Text, out x1) && float.TryParse(s_y.Text, out y1)
                && float.TryParse(s_z.Text, out z1) && float.TryParse(e_x.Text, out x2)
                && float.TryParse(e_y.Text, out y2) && float.TryParse(e_z.Text, out z2)
                && (x2 != 0 || y2 != 0 || z2 != 0))
            { 
                RAL = new Edge3D(new Point3D(x1, y1, z1), NormalizedVector(new Edge3D(new Point3D(0,0,0), new Point3D(x2, y2, z2))));
                RAL_toDraw = new Edge3D(new Point3D(RAL.start.X - RAL.end.X * 500, RAL.start.Y - RAL.end.Y * 500, RAL.start.Z - RAL.end.Z * 500),
                                        new Point3D(RAL.start.X + RAL.end.X * 500, RAL.start.Y + RAL.end.Y * 500, RAL.start.Z + RAL.end.Z * 500));
            }
        }

        private void load_obj_click(object sender, EventArgs e)
        {
            var objects = new List<Polyhedron>();
            OpenFileDialog openfileD = new OpenFileDialog
            {
                Filter = "Obj Files(*.obj)|*.obj"
            };
            if (openfileD.ShowDialog() == DialogResult.OK)
            {
                Parcer p = new Parcer();
                objects = p.ParceFromFile(openfileD.FileName);
                foreach (var o in objects)
                {
                    cur_polyhedron = o;
                    cur_polyhedron.scale(cur_polyhedron.Center(), 1 / 50.0, 1 / 50.0, 1 / 50.0);
                    Draw();
                }
            }
        }

        double Func(double x, double y)
        {
            if (comboBox1.SelectedIndex == 0)
                return Math.Sin(x) * Math.Cos(y);
            else if (comboBox1.SelectedIndex == 1)
                //return Math.Sin(x) + Math.Sin(y);
                return Math.Sin(x) + Math.Cos(y);
            else if (comboBox1.SelectedIndex == 2)
            {
                double r = x * x + y * y + 1;
                return 5 * (Math.Cos(r) / r + 0.1);
            }
            else return 0;
        }

        private void Graph_Click(object sender, EventArgs e)
        {
            FHAflag = false;
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 0;
            int amountx = 0;
            int amounty = 0;
            if (double.TryParse(grx1.Text, out x1) &&
                double.TryParse(grx2.Text, out x2) &&
                double.TryParse(gry1.Text, out y1) &&
                double.TryParse(gry2.Text, out y2) &&
                int.TryParse(X_step.Text, out amountx) &&
                int.TryParse(Y_step.Text, out amounty))
            {
                double stepx = (x2 - x1) / amountx;
                double stepy = (y2 - y1) / amounty;
                List<Point3D> buf = new List<Point3D>();
                Point3D p = new Point3D((float)x1, (float)Func(x1, y1), (float)y1);
                Point3D p_prev = new Point3D(0, 0, 0);
                cur_polyhedron = new Polyhedron();
                buf.Add(p);
                int k;
                for (double j = y1 + stepy; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                {
                    p = new Point3D((float)x1, (float)Func(x1, j),(float)j);
                    buf.Add(p);
                }

                for (double i = x1 + stepx; i < x2 || Math.Abs(i - x2) < 0.001; i += stepx)
                {
                    k = 0;
                    for (double j = y1; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                    {
                        p = new Point3D((float)i, (float)Func(i, j), (float)j );
                        if (k != 0)
                        {
                            cur_polyhedron.AddPoints(new List<Point3D> { new Point3D(buf[k - 1].X, buf[k - 1].Y, buf[k - 1].Z),
                                                                         new Point3D(p_prev.X, p_prev.Y, p_prev.Z),
                                                                         new Point3D(p.X,p.Y,p.Z)});
                            cur_polyhedron.AddPoints(new List<Point3D> { new Point3D(buf[k - 1].X, buf[k - 1].Y, buf[k - 1].Z),
                                                                         new Point3D(p.X,p.Y,p.Z),
                                                                         new Point3D(buf[k].X, buf[k].Y, buf[k].Z)});
                            buf[k - 1] = new Point3D(p_prev.X, p_prev.Y, p_prev.Z);
                        }
                        ++k;
                        p_prev = new Point3D(p.X, p.Y, p.Z);
                        if (Math.Abs(j - y2)<0.001 && Math.Abs(i - x2) > 0.001)
                        {
                            buf[--k] = new Point3D(p_prev.X, p_prev.Y, p_prev.Z);
                        }
                    }
                }
                cur_polyhedron.getMoved(new Point3D(pictureBox1.Width / 4, pictureBox1.Height / 4, 0));
                Point3D cent = cur_polyhedron.Center();
                double scale = (x2 - x1) < (y2 - y1) ? (x2 - x1) / pictureBox1.Width * 1.5 : (y2 - y1) / pictureBox1.Height * 1.25;
                cur_polyhedron.scale(cent, scale, scale, scale);
                Draw(false);
            }
        }

        private void gry1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RotateFigure(Edge3D RAL, int count)
        {
            float angle = 360f / count;

            for (int i = 0; i < count; ++i)
            {
                cur_polyhedron.RotateAroundLine(RAL.start, RAL.end, angle);
                Draw();
            }
        }

        private Point3D findCenterRotationFigure(List<Point3D> points, int axis)
        {
            double sum = 0;
            foreach (var p in points)
            {
                switch (axis)
                {
                    case 1: sum += p.X; break;
                    case 2: sum += p.Y; break;
                    case 3: sum += p.Z; break;
                }
            }
            switch (axis)
            {
                case 1: return new Point3D((float)(sum / points.Count), 0, 0);
                case 2: return new Point3D(0, (float)(sum / points.Count), 0);
                case 3: return new Point3D(0, 0, (float)(sum / points.Count));
            }
            return new Point3D();
        }

        private List<Point3D> Copy(List<Point3D> points)
        {
            var l = new List<Point3D>();
            foreach (var p in points)
                l.Add(new Point3D(p.X, p.Y, p.Z));
            return l;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FHAflag = false;
            List<Point3D> buf = new List<Point3D>();

            var lines = textBox1.Text.Split('\n');
            int count = (int)numericUpDown1.Value;
            double angle = 360.0 / count;
            Point3D axis = new Point3D(1, 0, 0);
            Polyhedron rotated_pol = new Polyhedron();
            
            
            cur_polyhedron = new Polyhedron();

            switch (comboBox2.SelectedItem.ToString())
            {
                case "OX":
                    axis = new Point3D(1, 0, 0);
                    break;
                case "OY":
                    axis = new Point3D(0, 1, 0);
                    break;
                case "OZ":
                    axis = new Point3D(0, 0, 1);
                    break;
            }


            foreach (var p in lines)
            {
                var arr = ((string)p).Split(',');
                buf.Add(new Point3D(float.Parse(arr[0]), float.Parse(arr[1]), float.Parse(arr[2])));
            }

            rotated_pol.AddPoints(buf);

            for (int i = 0; i <= count; ++i)
            {
                rotated_pol.RotateAroundLine(new Point3D(0, 0, 0), axis, angle);

                for (int j = 0; j < buf.Count; ++j)
                {
                    if (rotated_pol.points[j] != buf[j])
                        cur_polyhedron.AddPoints(new List<Point3D> { 
                                                                    new Point3D(buf[j].X, buf[j].Y, buf[j].Z), 
                                                                    new Point3D(buf[(j + 1) % buf.Count].X, buf[(j + 1) % buf.Count].Y, buf[(j + 1) % buf.Count].Z), 
                                                                    new Point3D(rotated_pol.points[j].X, rotated_pol.points[j].Y, rotated_pol.points[j].Z) 
                    });
                    if (rotated_pol.points[(j + 1) % buf.Count] != buf[(j + 1) % buf.Count])
                        cur_polyhedron.AddPoints(new List<Point3D> {
                                                                        new Point3D(rotated_pol.points[j].X, rotated_pol.points[j].Y, rotated_pol.points[j].Z),
                                                                        new Point3D(rotated_pol.points[(j + 1) % buf.Count].X, rotated_pol.points[(j + 1) % buf.Count].Y, rotated_pol.points[(j + 1) % buf.Count].Z),
                                                                        new Point3D(buf[(j + 1) % buf.Count].X, buf[(j + 1) % buf.Count].Y, buf[(j + 1) % buf.Count].Z)
                    });
                }

                for (int k = 0; k < buf.Count; k++)
                {
                    buf[k] = new Point3D(rotated_pol.points[k].X, rotated_pol.points[k].Y, rotated_pol.points[k].Z);
                }
                
            }

            Draw();
        }

        private void Ortxyz_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void save_obj_click(object sender, EventArgs e)
        {
            var objects = new List<Polyhedron>();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Obj Files(*.obj)|*.obj";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Parcer p = new Parcer();
                p.SaveToFile(cur_polyhedron, sfd.OpenFile());
            }
        }

        private void FHA_Click(object sender, EventArgs e)
        {
            anglex = 0;
            angley = 0;
            anglez = 0;
            view_vector.points[0] = new Point3D(begview.X, begview.Y, begview.Z);
            FHAflag = true;
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 0;
            int amountx = 0;
            int amounty = 0;
            if (double.TryParse(grx1.Text, out x1) &&
                double.TryParse(grx2.Text, out x2) &&
                double.TryParse(gry1.Text, out y1) &&
                double.TryParse(gry2.Text, out y2) &&
                int.TryParse(X_step.Text, out amountx) &&
                int.TryParse(Y_step.Text, out amounty))
            {
                double stepx = (x2 - x1) / amountx;
                double stepy = (y2 - y1) / amounty;
                Point3D p;
                cur_polyhedron = new Polyhedron();
                List<Point3D> pointsList = new List<Point3D>();
                double angle = AngleBetweenVectorsXY(begview, view_vector.points[0]);
                for (double j = y1; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                {
                    pointsList.Clear();
                    for (double i = x1; i < x2 || Math.Abs(i - x2) < 0.001; i += stepx)
                    {
                        p = new Point3D((float)i, (float)Func(i, j), (float)j);
                        pointsList.Add(p);
                    }
                    cur_polyhedron.AddPoints(pointsList);
                }
                for (double i = x1; i < x2 || Math.Abs(i - x2) < 0.001; i += stepx)
                {
                    pointsList.Clear();
                    for (double j = y1; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                    {
                        p = new Point3D((float)i, (float)Func(i, j), (float)j);
                        pointsList.Add(p);
                    }
                    cur_polyhedron.AddPoints(pointsList);
                }
                Point3D cent = cur_polyhedron.Center();
                double scale = (x2 - x1) < (y2 - y1) ? (x2 - x1) / pictureBox1.Width * 1.5 : (y2 - y1) / pictureBox1.Height * 1.25;
                cur_polyhedron.scale(cent, scale, scale, scale);
                cur_polyhedron.RotateAroundLine(cent, new Point3D(1, 0, 0), 90);
                cur_polyhedron.getMoved(new Point3D(pictureBox1.Width / 8, pictureBox1.Height / 4, 0));
                DrawFHA(amountx, amounty);
            }
        }

        private void RebuildFHA()
        {
            
            view_vector.points[0] = new Point3D(begview.X, begview.Y, begview.Z);
            FHAflag = true;
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 0;
            int amountx = 0;
            int amounty = 0;
            if (double.TryParse(grx1.Text, out x1) &&
                double.TryParse(grx2.Text, out x2) &&
                double.TryParse(gry1.Text, out y1) &&
                double.TryParse(gry2.Text, out y2) &&
                int.TryParse(X_step.Text, out amountx) &&
                int.TryParse(Y_step.Text, out amounty))
            {
                double stepx = (x2 - x1) / amountx;
                double stepy = (y2 - y1) / amounty;
                Point3D p;
                cur_polyhedron = new Polyhedron();
                List<Point3D> pointsList = new List<Point3D>();
                double angle = AngleBetweenVectorsXY(begview, view_vector.points[0]);
                if (Math.Abs(angle) <= 45 || Math.Abs(angle) > 135)
                {
                    for (double j = y1; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                    {
                        pointsList.Clear();
                        for (double i = x1; i < x2 || Math.Abs(i - x2) < 0.001; i += stepx)
                        {
                            p = new Point3D((float)i, (float)Func(i, j), (float)j);
                            pointsList.Add(p);
                        }
                        cur_polyhedron.AddPoints(pointsList);
                    }
                }
                else
                {
                    for (double i = x1; i < x2 || Math.Abs(i - x2) < 0.001; i += stepx)
                    {
                        pointsList.Clear();
                        for (double j = y1; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                        {
                            p = new Point3D((float)i, (float)Func(i, j), (float)j);
                            pointsList.Add(p);
                        }
                        cur_polyhedron.AddPoints(pointsList);
                    }
                }
                //повороты к позиции
                Point3D cent = cur_polyhedron.Center();
                double scale = (x2 - x1) < (y2 - y1) ? (x2 - x1) / pictureBox1.Width * 1.5 : (y2 - y1) / pictureBox1.Height * 1.25;
                cur_polyhedron.scale(cent, scale, scale, scale);
                cur_polyhedron.RotateAroundLine(cent, new Point3D(1, 0, 0), 90);
                cur_polyhedron.getMoved(new Point3D(pictureBox1.Width / 8, pictureBox1.Height / 4, 0));
                DrawFHA(amountx, amounty);
            }
        }

        private void X_step_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
