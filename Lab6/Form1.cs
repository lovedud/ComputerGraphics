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
        State cur_state;
        Edge3D RAL; // прямая, вокруг которой происходит вращение start - точка начала, end - нормализованный вектор, задающий направление
        Polyhedron cur_polyhedron;
        Bitmap bm;
        Graphics g;

        public void Draw()
        {
            g.Clear(Color.White);
            if (cur_polyhedron is null)
                return;
            var edges = ToOrtographics(cur_polyhedron, cur_mode);
            foreach (var edge in edges)
            {
                DrawEdge(ref g, ref bm, edge);
            }
            pictureBox1.Image = bm;
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

        public void SwitchOnAllButtonsExcept(Button button)
        {
            rotateAroundLine.Enabled = true;
            button.Enabled = false;
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
                cur_mode = Mode.XY;
                ort_button.Enabled = true;
            }
            else OrtButtonAvailability();

        }

        private void Ortxz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortxz.Checked)
            {
                Ortxy.Checked = false;
                Ortyz.Checked = false;
                cur_mode = Mode.XZ;
                ort_button.Enabled = true;
            } else OrtButtonAvailability();
        }

        private void Ortyz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortyz.Checked)
            {
                Ortxz.Checked = false;
                Ortxy.Checked = false;
                cur_mode = Mode.YZ;
                ort_button.Enabled = true;
            } else OrtButtonAvailability();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cur_polyhedron is null)
                return;
            var edges = ToOrtographics(cur_polyhedron, cur_mode);
            foreach(var edge in edges)
            {
                DrawEdge(ref g, ref bm, edge);
            }
            pictureBox1.Image = bm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateCube(new Point3D(200, 200, 200), 100);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Rota_Click(object sender, EventArgs e)
        {
            cur_state = State.RotateAroundLine;
            SwitchOnAllButtonsExcept(rotateAroundLine);
            OX.Enabled = false;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = true;
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


        Point3D prevMouseMove;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            point_angle.X = e.X-50;
            point_angle.Y = e.Y;
            m_down = true;
            if(cur_state == State.RotateAroundLine)
            {
                RAL_toDraw = new Edge3D(new Point3D(RAL.start.X - RAL.end.X*500, RAL.start.Y - RAL.end.Y * 500, RAL.start.Z - RAL.end.Z * 500),
                                        new Point3D(RAL.start.X + RAL.end.X * 500, RAL.start.Y + RAL.end.Y * 500, RAL.start.Z + RAL.end.Z * 500));
            }
            prev_angle = 0;

            prevMouseMove = new Point3D(e.X, e.Y, 1);
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
                Point3D center = cur_polyhedron.center();
                Point3D mouseMove = new Point3D(e.X - center.X, e.Y - center.Y, 0);

                cur_polyhedron.getMoved(mouseMove);
                Draw();

                pictureBox1.Image = bm;
            }
            if (m_down && cur_state == State.Scale && !(cur_polyhedron is null))
            {
                Point3D center = cur_polyhedron.center();
                Point3D mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 10);
                //if (mouseMove.X != 0 && mouseMove.Y != 0 && mouseMove.Z != 0 )
                cur_polyhedron.scale(center, mouseMove.X * 0.01, mouseMove.Y * 0.01, mouseMove.Z * 0.01);

                Draw();

                prevMouseMove.X = mouseMove.X;
                prevMouseMove.Y = mouseMove.Y;
                prevMouseMove.Z = mouseMove.Z;


                pictureBox1.Image = bm;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m_down = false;
            Draw();
            //int angle = AngleBetweenPoints(point_angle, new Point(e.X, e.Y));
            //testbox.Text = angle.ToString();
            //cur_polyhedron.RotateAroundLine(RAL, angle);
            //Draw();
        }
        private void iso_button_Click(object sender, EventArgs e)
        {
            if (cur_polyhedron is null)
                return;
            var edges = ToIsometric(cur_polyhedron);
            foreach (var edge in edges)
            {
                DrawEdge(ref g, ref bm, edge);
            }
            pictureBox1.Image = bm;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cur_state = State.MoveP;
            //var dialogX = new InputForCoordinates("Введите смещение по X");
            //var dialogY = new InputForCoordinates("Введите смещение по Y");
            //var dialogZ = new InputForCoordinates("Введите смещение по Z");
            //if (dialogX.ShowDialog() == DialogResult.OK && dialogY.ShowDialog() == DialogResult.OK && dialogZ.ShowDialog() == DialogResult.OK) { 
            //    var x = new Point3D(int.Parse(dialogX.ResultText), int.Parse(dialogY.ResultText), int.Parse(dialogZ.ResultText));
            //    cur_polyhedron.getMoved(x);

            //    g.Clear(Color.White);
            //    pictureBox1.Image = bm;
            //    g = Graphics.FromImage(bm);

            //    var edges = ToOrtographics(cur_polyhedron, cur_mode);
            //    foreach (var edge in edges)
            //    {
            //        DrawEdge(ref g, ref bm, edge);
            //    }
            //    pictureBox1.Image = bm;
                //cur_polyhedron = AffinStuff.getMoved(
                //    cur_polyhedron,
                //    int.Parse(dialogX.ResultText),
                //    int.Parse(dialogY.ResultText),
                //    int.Parse(dialogZ.ResultText)
                //);
            //}
        }

        private void Tetrahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateTetrahedron(new Point3D(200, 200, 200), 100);
        }

        private void Octahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateOctahedron(new Point3D(200, 200, 200), 100);
        }

        private void Icosahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateIcosahedron(new Point3D(200, 200, 200), 100);
        }

        private void dodecahedron_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateDodecahedron(new Point3D(200, 200, 200), 100);
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            cur_state = State.Scale;
        }
    }
}
