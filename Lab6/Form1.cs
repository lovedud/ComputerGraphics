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
            Scale,
            Camera
        }


        Mode cur_mode;
        OrtMode cur_ort_mode;
        State cur_state;
        Edge3D RAL; // прямая, вокруг которой происходит вращение start - точка начала, end - нормализованный вектор, задающий направление
        Polyhedron cur_polyhedron;
        Bitmap bm;
        Graphics g;
        Point3D start_point;
        Projector projector;
        AffinTransformator aff_trans;

        Point3D viewVector = new Point3D(0, 0, 1);
        Point3D default_camera;
        Light light;

        private Color DimeColor(Color c, double sat)
        {
            return Color.FromArgb((int)(c.R * sat), (int)(c.G * sat), (int)(c.B * sat));
        }

        public void Draw(bool drawpoint = true, bool update = true)
        {
            g.Clear(Color.White);
            if (cur_polyhedron is null)
                return;
            projector.UpdatePointOfView(cur_polyhedron.Center());
            cur_polyhedron.Triangulate();
            var rastrs = projector.Project(cur_mode, cur_polyhedron, viewVector, light);

            //DrawAxis(start_point); убрал, так как сломались ( становятся не по центру объекта)
            foreach (var rastr in rastrs)
            {
                if (rastr.X < 0 || rastr.Y < 0 || rastr.X >= pictureBox1.Width || rastr.Y >= pictureBox1.Height )
                    continue;
                var rastr_color = DimeColor(Color.Aqua, rastr.H);
                bm.SetPixel(rastr.X, rastr.Y, rastr_color);
            }
            
            pictureBox1.Image = bm;
            if (update)
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
            cur_polyhedron = CreateCube(start_point, 100);
            projector.UpdateCamera(default_camera);
            pictureBox1.Image = bm;
        }

        public int AngleBetweenPoints(Point p1, Point p2)
        {
            Point p1v = new Point(200, 0);
            Point p2v = new Point(p2.X - p1.X, p1.Y - p2.Y);
            double a = Math.Sqrt(p1v.X * p1v.X + p1v.Y * p1v.Y) * Math.Sqrt(p2v.X * p2v.X + p2v.Y * p2v.Y); // lol*cheb
            double b = p1v.X * p2v.X + p1v.Y * p2v.Y; //kek
            double c = b / a;
            double d = Math.Acos(c) * 180 / Math.PI;
            if (p2.Y > p1.Y)
                d = 360 - d;
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
                Ortxyz.Checked = false;
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
                Ortxyz.Checked = false;
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
                Ortxyz.Checked = false;
                cur_ort_mode = OrtMode.YZ;
                projector.Update(cur_ort_mode);
                //ort_button.Enabled = true;
                Draw();
            } else OrtButtonAvailability();
        }
        private void Ortxyz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortxyz.Checked)
            {
                Ortxz.Checked = false;
                Ortxy.Checked = false;
                Ortyz.Checked = false;
                //ort_button.Enabled = true;
                Draw();
            }
            else OrtButtonAvailability();

        }
        private void Ort_Button_Click(object sender, EventArgs e)
        {
            cur_mode = Mode.Orthographic;
            viewVector = new Point3D(0, 0, 1);
            SwitchModeButtons(true);
            ort_button.Enabled = false;
            Draw(false);
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
            move_button.Enabled = false;

            cur_polyhedron = CreateCube(new Point3D(0, 0, 0), 100);
            s_x.Text = (pictureBox1.Width / 2).ToString();
            s_y.Text = (pictureBox1.Height / 2).ToString();
            s_z.Text = (pictureBox1.Width / 2).ToString();

            e_x.Text = (1).ToString();
            e_y.Text = (0).ToString();
            e_z.Text = (0).ToString();
            default_camera = new Point3D(pictureBox1.Width / 2 - 250, pictureBox1.Height / 2 - 250, 300);

            projector = new Projector(start_point);
            projector.UpdateCamera(default_camera);
            light = new Light(default_camera.X, default_camera.Y, default_camera.Z);

            aff_trans = new AffinTransformator(start_point);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            Draw();
        }

        private void Rota_Click(object sender, EventArgs e)
        {
            cur_state = State.RotateAroundLine;
            OX.Enabled = false;
            OY.Enabled = true;
            OZ.Enabled = true;
            Custom.Enabled = true;
            SwitchStateButtons(true);
            rotateAroundLine.Enabled = false;
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

        Point point_angle = new Point(0, 0);
        bool m_down = false;
        Edge3D RAL_toDraw;

        int prev_angle = 0;

        Point3D prevMouseMove;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            point_angle.X = e.X - 50;
            point_angle.Y = e.Y;
            var x = Math.Sin(90);
            m_down = true;
            if (cur_state == State.RotateAroundLine)
            {
                RAL_toDraw = new Edge3D(new Point3D(RAL.start.X - RAL.end.X * 500, RAL.start.Y - RAL.end.Y * 500, RAL.start.Z - RAL.end.Z * 500),
                                        new Point3D(RAL.start.X + RAL.end.X * 500, RAL.start.Y + RAL.end.Y * 500, RAL.start.Z + RAL.end.Z * 500));
            }
            prev_angle = 0;


            prevMouseMove = new Point3D(e.X, e.Y, 0);
        }
        private void RotatingLine(Point e)
        {
            int angle = AngleBetweenPoints(point_angle, new Point(e.X, e.Y));
            aff_trans.Rotate(ref cur_polyhedron, RAL.end, prev_angle - angle);
            prev_angle = angle;
            Draw(false, false);
            DrawPoint(ref bm, new PointF(point_angle.X, point_angle.Y), Color.Orange);

            //var edge = projector.Project(cur_mode, RAL_toDraw);
            //g.DrawLine(new Pen(Color.Orange, 1), edge.start, edge.end); //Не работает из-за нового центрирования

            pictureBox1.Image = bm;

        }
        private void MovingPoly(Point e)
        {
            Point3D mouseMove = prevMouseMove;
            if (Ortxy.Checked)
            {
                mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);
            }
            else if (Ortxz.Checked)
            {
                mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);
            }
            else if (Ortyz.Checked)
            {
                mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);
            }
            aff_trans.Move(ref cur_polyhedron, mouseMove);
            prevMouseMove.X = e.X;
            prevMouseMove.Y = e.Y;
            Draw(false);
        }
        private void ScalingPoly(Point e)
        {
            Point3D mouseMove = prevMouseMove;
            double ky = 0;
            if (Ortxyz.Checked)
            {
                mouseMove = new Point3D(e.X - prevMouseMove.X, e.X - prevMouseMove.X, e.X - prevMouseMove.X);
                ky = 1 - mouseMove.X * 0.01;
            }
            else if (Ortxy.Checked)
            {
                mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);
            }
            else if (Ortxz.Checked)
            {
                mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);

            }
            else if (Ortyz.Checked)
            {
                mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);
            }
            ky = ky == 0 ? 1 + mouseMove.Y * 0.01 : ky;
            aff_trans.Scale(ref cur_polyhedron, 1 - mouseMove.X * 0.01, ky, 1 - mouseMove.Z * 0.01);
            prevMouseMove.X = e.X;
            prevMouseMove.Y = e.Y;

            Draw(false);
        }
            private void CameraMoving(Point e)
            {
                Point3D mouseMove = prevMouseMove;
                if (Ortxy.Checked)
                {
                    mouseMove = new Point3D(e.X - prevMouseMove.X, e.Y - prevMouseMove.Y, 0);
                }
                else if (Ortxz.Checked)
                {
                    mouseMove = new Point3D(e.X - prevMouseMove.X, 0, e.Y - prevMouseMove.Y);
                }
                else if (Ortyz.Checked)
                {
                    mouseMove = new Point3D(0, e.X - prevMouseMove.X, e.Y - prevMouseMove.Y);
                }
                projector.MoveCamera(mouseMove);
                view_x.Text = projector.camera.X.ToString();
                view_y.Text = projector.camera.Y.ToString();
                view_z.Text = projector.camera.Z.ToString();
                projector.UpdatePointOfView(cur_polyhedron.Center());
                prevMouseMove.X = e.X;
                prevMouseMove.Y = e.Y;
                Draw(false);
            }
            private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
            {
                if (cur_polyhedron is null || !m_down)
                    return;

                aff_trans.center = cur_polyhedron.Center();
                switch (cur_state)
                {
                    case State.MoveP:
                        MovingPoly(e.Location);
                        break;
                    case State.RotateAroundLine:
                        RotatingLine(e.Location);
                        break;
                    case State.Scale:
                        ScalingPoly(e.Location);
                        break;
                    case State.Camera:
                        CameraMoving(e.Location);
                        break;

                }

            }

            private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
            {
                m_down = false;
                Draw(false);
            }
            private void iso_button_Click(object sender, EventArgs e)
            {
                cur_mode = Mode.Isometric;
                SwitchModeButtons(true);
                iso_button.Enabled = false;
                Draw(false);
            }
            private void button3_Click(object sender, EventArgs e)
            {
                cur_state = State.MoveP;
                rotateAroundLine.Enabled = true;
                OX.Enabled = false;
                OY.Enabled = false;
                OZ.Enabled = false;
                Custom.Enabled = false;
                move_button.Enabled = false;
                scaleButton.Enabled = true;
            }
            private void Cub_Button_Click(object sender, EventArgs e)
            {
                cur_polyhedron = CreateCube(new Point3D(0, 0, 0), 100);
                Draw(false);
            }


            private void perspective_button_Click(object sender, EventArgs e)
            {
                cur_mode = Mode.Perspective;
                SwitchModeButtons(true);
                perspective_button.Enabled = false;
                viewVector = new Point3D(0, 0, 1);
                Draw();
            }

            private void ScaleButton_Click(object sender, EventArgs e)
            {
                cur_state = State.Scale;
                OX.Enabled = false;
                OY.Enabled = false;
                OZ.Enabled = false;
                Custom.Enabled = false;
                SwitchStateButtons(true);
                scaleButton.Enabled = false;

            }

            private void CheckForCustomLine(object sender, EventArgs e)
            {
                if ((s_x.Text != e_x.Text || s_y.Text != e_y.Text || s_z.Text != e_z.Text)
                    && float.TryParse(s_x.Text, out float x1) && float.TryParse(s_y.Text, out float y1)
                    && float.TryParse(s_z.Text, out float z1) && float.TryParse(e_x.Text, out float x2)
                    && float.TryParse(e_y.Text, out float y2) && float.TryParse(e_z.Text, out float z2)
                    && (x2 != 0 || y2 != 0 || z2 != 0))
                {
                    RAL = new Edge3D(new Point3D(x1, y1, z1), NormalizedVector(new Edge3D(new Point3D(0, 0, 0), new Point3D(x2, y2, z2))));
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
                        aff_trans.Scale(ref cur_polyhedron, 1 / 50.0, 1 / 50.0, 1 / 50.0);
                        Draw();
                    }
                }
            }

            double Func(double x, double y)
            {
                if (comboBox1.SelectedIndex == 0)
                    return Math.Sin(x) * Math.Cos(y);
                else if (comboBox1.SelectedIndex == 1)
                    return Math.Sin(x) + Math.Sin(y);
                else return 0;
            }

            private void Graph_Click(object sender, EventArgs e)
            {
                if (double.TryParse(grx1.Text, out double x1) &&
                    double.TryParse(grx2.Text, out double x2) &&
                    double.TryParse(gry1.Text, out double y1) &&
                    double.TryParse(gry2.Text, out double y2) &&
                    int.TryParse(X_step.Text, out int amountx) &&
                    int.TryParse(Y_step.Text, out int amounty))
                {
                    double stepx = (x2 - x1) / amountx;
                    double stepy = (y2 - y1) / amounty;
                    List<Point3D> buf = new List<Point3D>();
                    Point3D p = new Point3D((float)x1, (float)y1, (float)Func(x1, y1));
                    Point3D p_prev = new Point3D(0, 0, 0);
                    cur_polyhedron = new Polyhedron();
                    buf.Add(p);
                    int k;
                    for (double j = y1 + stepy; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                    {
                        p = new Point3D((float)x1, (float)j, (float)Func(x1, j));
                        buf.Add(p);
                    }

                    for (double i = x1 + stepx; i < x2 || Math.Abs(i - x2) < 0.001; i += stepx)
                    {
                        k = 0;
                        for (double j = y1; j < y2 || Math.Abs(j - y2) < 0.001; j += stepy)
                        {
                            p = new Point3D((float)i, (float)j, (float)Func(i, j));
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
                            if (Math.Abs(j - y2) < 0.001 && Math.Abs(i - x2) > 0.001)
                            {
                                buf[--k] = new Point3D(p_prev.X, p_prev.Y, p_prev.Z);
                            }
                        }
                    }
                    //aff_trans.Move(ref cur_polyhedron, new Point3D(pictureBox1.Width / 4, pictureBox1.Height / 4, 0));
                    Point3D cent = cur_polyhedron.Center();
                    double scale = (x2 - x1) < (y2 - y1) ? (x2 - x1) / pictureBox1.Width * 1.5 : (y2 - y1) / pictureBox1.Height * 1.25;
                    //aff_trans.Scale(cur_polyhedron, scale, scale, scale);
                    Draw(false, false);
                }
            }
        
            private void button4_Click(object sender, EventArgs e)
            {
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
                    aff_trans.Rotate(ref rotated_pol, axis, angle);
                    
                    for (int j = 0; j < buf.Count; ++j)
                    {
                        //if (rotated_pol.points[j] != buf[j])
                        //    cur_polyhedron.AddPoints(new List<Point3D> { 
                        //                                                new Point3D(buf[j].X, buf[j].Y, buf[j].Z), 
                        //                                                new Point3D(buf[(j + 1) % buf.Count].X, buf[(j + 1) % buf.Count].Y, buf[(j + 1) % buf.Count].Z), 
                        //                                                new Point3D(rotated_pol.points[j].X, rotated_pol.points[j].Y, rotated_pol.points[j].Z) 
                        //});
                        //if (rotated_pol.points[(j + 1) % buf.Count] != buf[(j + 1) % buf.Count])
                        //    cur_polyhedron.AddPoints(new List<Point3D> {
                        //                                                    new Point3D(rotated_pol.points[j].X, rotated_pol.points[j].Y, rotated_pol.points[j].Z),
                        //                                                    new Point3D(rotated_pol.points[(j + 1) % buf.Count].X, rotated_pol.points[(j + 1) % buf.Count].Y, rotated_pol.points[(j + 1) % buf.Count].Z),
                        //                                                    new Point3D(buf[(j + 1) % buf.Count].X, buf[(j + 1) % buf.Count].Y, buf[(j + 1) % buf.Count].Z)
                        //});
                        if (rotated_pol.points[j] != buf[j])
                            cur_polyhedron.AddPoints(new List<Point3D> { new Point3D(buf[j].X, buf[j].Y, buf[j].Z),
                                                                     new Point3D(rotated_pol.points[j].X, rotated_pol.points[j].Y, rotated_pol.points[j].Z),
                                                                     new Point3D(buf[(j + 1) % buf.Count].X, buf[(j + 1) % buf.Count].Y, buf[(j + 1) % buf.Count].Z)});

                        if (rotated_pol.points[(j + 1) % buf.Count] != buf[(j + 1) % buf.Count])
                            cur_polyhedron.AddPoints(new List<Point3D> { new Point3D(rotated_pol.points[j].X, rotated_pol.points[j].Y, rotated_pol.points[j].Z),
                                                                     new Point3D(rotated_pol.points[(j + 1) % buf.Count].X, rotated_pol.points[(j + 1) % buf.Count].Y, rotated_pol.points[(j + 1) % buf.Count].Z),
                                                                     new Point3D(buf[(j + 1) % buf.Count].X, buf[(j + 1) % buf.Count].Y, buf[(j + 1) % buf.Count].Z)});
                    }

                    for (int k = 0; k < buf.Count; k++)
                    {
                        buf[k] = new Point3D(rotated_pol.points[k].X, rotated_pol.points[k].Y, rotated_pol.points[k].Z);
                    }

                }
                aff_trans.Move(ref cur_polyhedron, new Point3D(pictureBox1.Width / 2, pictureBox1.Height / 2, 0));
                aff_trans.Rotate(ref cur_polyhedron, new Point3D(0, 1, 0), 90);
                Draw();
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
            private void button6_Click(object sender, EventArgs e)
            {
                cur_polyhedron = CreateTetrahedron(new Point3D(0, 0, 0), 100);
                Draw();
            }

            private void button7_Click(object sender, EventArgs e)
            {
                cur_polyhedron = CreateTestFigure(new Point3D(0, 0, 0), 100);
                Draw();
            }

            private void ChangeViewVector(object sender, EventArgs e)
            {
                //float x, y, z;
                //if (float.TryParse(view_x.Text, out x) && float.TryParse(view_y.Text, out y) && float.TryParse(view_z.Text, out z)
                //    && (x != 0 || y != 0 || z != 0))
                //{
                //    viewVector.X = x;
                //    viewVector.Y = y;
                //    viewVector.Z = z;
                //    Draw(false);
                //}
            }
            public void SwitchStateButtons(bool on)
            {
                rotateAroundLine.Enabled = on;
                move_button.Enabled = on;
                scaleButton.Enabled = on;
                camera_button.Enabled = on;

            }
            public void SwitchModeButtons(bool on)
            {
                iso_button.Enabled = on;
                ort_button.Enabled = on;
                perspective_button.Enabled = on;
                camera_button.Enabled = on;

            }
            private void Camera_Click(object sender, EventArgs e)
            {
                cur_mode = Mode.Camera;
                cur_state = State.Camera;
                SwitchModeButtons(true);
                SwitchStateButtons(true);
                camera_button.Enabled = false;
                Draw(false);
            }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    save_obj_click(sender, e);
                }
                else if (e.Control && e.KeyCode == Keys.O)
                {
                    load_obj_click(sender, e);
                }
            }

        private void light_create_button_Click(object sender, EventArgs e)
        {
            
            float.TryParse(x_light_box.Text, out float x);
            float.TryParse(y_light_box.Text, out float y);
            float.TryParse(z_light_box.Text, out float z);
            float.TryParse(luminosity_box.Text, out float h);
            light = new Light(x, y, z, h);
        }
    }
}
