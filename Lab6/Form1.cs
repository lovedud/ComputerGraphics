using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        Point3D start_point = new Point3D(100, 100, 300);
        int c = 1600;

        public void Draw()
        {
            g.Clear(Color.White);
            if (cur_polyhedron is null)
                return;
            List<Edge> edges = new List<Edge>();
            switch(cur_mode)
            {
                case Mode.Isometric:
                    edges = ToIsometric(cur_polyhedron);
                    break;
                case Mode.Orthographic:
                    edges = ToOrtographics(cur_polyhedron, cur_ort_mode);
                    break;
                case Mode.Perspective:
                    edges = ToPerspective(cur_polyhedron, 1600);
                    break;
            }
            DrawAxis(cur_polyhedron.center());
            foreach (var edge in edges)
            {
                DrawEdge(ref g, ref bm, edge);
            }
            pictureBox1.Image = bm;
        }
        private void DrawAxis(Point3D center)
        {
            var new_axis = GetAxis(center, cur_mode, cur_ort_mode, c);
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
                ort_button.Enabled = true;
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
                ort_button.Enabled = true;
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
                ort_button.Enabled = true;
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

            cur_state = State.MoveP;
            button3.Enabled = false;
            testbox.Visible = false;
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
            Point3D center = cur_polyhedron.center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(1, 0, 0));
        }

        private void OX_Click(object sender, EventArgs e)
        {
            OX.Enabled = false;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = true;
            Point3D center = cur_polyhedron.center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(1, 0, 0));
        }

        private void OY_Click(object sender, EventArgs e)
        {
            OX.Enabled = true;
            OY.Enabled = false;
            OZ.Enabled = true;
            Custom.Enabled = true;
            Point3D center = cur_polyhedron.center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(0, 1, 0));
        }

        private void OZ_Click(object sender, EventArgs e)
        {
            OX.Enabled = true;
            OY.Enabled = true;
            OZ.Enabled = false;
            Custom.Enabled = true;
            Point3D center = cur_polyhedron.center();
            RAL = new Edge3D(new Point3D(center.X, center.Y, center.Z), new Point3D(0, 0, 1));
        }

        private void Custom_Click(object sender, EventArgs e)
        {
            OX.Enabled = true;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = false;
            float x1 = 0;
            float y1 = 0;
            float z1 = 0;
            float x2 = 0;
            float y2 = 0;
            float z2 = 0;
            if (float.TryParse(s_x.Text, out x1) && float.TryParse(s_y.Text, out y1) && float.TryParse(s_z.Text, out z1) && float.TryParse(e_x.Text, out x2) && float.TryParse(e_y.Text, out y2) && float.TryParse(e_z.Text, out z2))
                RAL = new Edge3D(new Point3D(x1, y1, z1), NormalizedVector(new Edge3D(new Point3D(x1, y1, z1), new Point3D(x2,y2,z2))));

        }

        Point point_angle = new Point(0,0);
        bool m_down = false;
        Edge3D RAL_toDraw;

        int prev_angle = 0;
        Polyhedron prev_polyhedron;

        Point3D prevMouseMove;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            point_angle.X = e.X-50;
            point_angle.Y = e.Y;
            m_down = true;
            //if(cur_state == State.RotateAroundLine)
            //{
            //    RAL_toDraw = new Edge3D(new Point3D(RAL.start.X - RAL.end.X*500, RAL.start.Y - RAL.end.Y * 500, RAL.start.Z - RAL.end.Z * 500),
            //                            new Point3D(RAL.start.X + RAL.end.X * 500, RAL.start.Y + RAL.end.Y * 500, RAL.start.Z + RAL.end.Z * 500));
            //}
            prev_angle = 0;


            prevMouseMove = new Point3D(e.X, e.Y, 0);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_down && cur_state == State.RotateAroundLine && !(cur_polyhedron is null))
            {
                int angle = AngleBetweenPoints(point_angle, new Point(e.X, e.Y));
                testbox.Text = angle.ToString();
                cur_polyhedron.RotateAroundLine(RAL.start, RAL.end, prev_angle - angle);
                var aa = cur_polyhedron.center();
                prev_angle = angle;
                Draw();
                DrawPoint(ref bm, new PointF(aa.X, aa.Y), Color.Red);
                DrawPoint(ref bm, new PointF(point_angle.X, point_angle.Y), Color.Orange);

                //TODO отрисовать линию, вокруг которой нужно вращать фигуру

                pictureBox1.Image = bm;
            }
            if (m_down && cur_state == State.MoveP && !(cur_polyhedron is null))
            {
                if (Ortxy.Checked)
                {
                    Point3D center = cur_polyhedron.center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);

                    cur_polyhedron.getMoved(mouseMove);
                    Draw();
                }
                else if (Ortxz.Checked)
                {
                    Point3D center = cur_polyhedron.center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);

                    cur_polyhedron.getMoved(mouseMove);
                    Draw();
                }
                else if (Ortyz.Checked)
                {
                    Point3D center = cur_polyhedron.center();
                    Point3D mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);

                    cur_polyhedron.getMoved(mouseMove);
                    Draw();
                }

                prevMouseMove.X = e.X;
                prevMouseMove.Y = e.Y;

                pictureBox1.Image = bm;
            }
            if (m_down && cur_state == State.Scale && !(cur_polyhedron is null))
            {

                if (Ortxy.Checked)
                {
                    Point3D center = cur_polyhedron.center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 + mouseMove.Y * 0.01, 1 - mouseMove.Z * 0.01);
                    Draw();
                }
                else if (Ortxz.Checked)
                {
                    Point3D center = cur_polyhedron.center();
                    Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 + mouseMove.Y * 0.01, 1 - mouseMove.Z * 0.01);
                    Draw();

                }
                else if (Ortyz.Checked)
                {
                    Point3D center = cur_polyhedron.center();
                    Point3D mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);
                    cur_polyhedron.scale(center, 1 - mouseMove.X * 0.01, 1 + mouseMove.Y * 0.01, 1 - mouseMove.Z * 0.01);
                    Draw();

                }

                Draw();

                prevMouseMove.X = e.X;
                prevMouseMove.Y = e.Y;

                pictureBox1.Image = bm;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m_down = false;
            Draw();
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
            cur_polyhedron = CreateCube(start_point, 100);
            Draw();
        }
        private void Tetrahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateTetrahedron(start_point, 100);
            Draw();
        }

        private void Octahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateOctahedron(start_point, 100);
            Draw();
        }

        private void Icosahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateIcosahedron(start_point, 100);
            Draw();
        }

        private void dodecahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateDodecahedron(start_point, 100);
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
    }
}
