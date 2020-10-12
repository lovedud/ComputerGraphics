using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {

        public enum Mode { Add = 0, AddOnLine, Remove, Move}

        public Mode current_mode;

        public List<PointF> points;
        public Bitmap bitmap;

        //вспомогательные для движения

        public bool moveisbeingpressed = false;
        public bool addisbeingpressed = false;
        public bool addonlineisbeingpressed = false;

        public int movingPoint_num = -1;
        public int addonlinePoint_num = -1;

        public Form1()
        {
            InitializeComponent();
        }

        public void SwitchOffAllButtons()
        {
            foreach (var x in Controls)
            {
                if (x is Button)
                    ((Button)x).Enabled = false;
            }
            Clear.Enabled = true;
        }

        public void SwitchOnAllButtonsExcept(Button button)
        {
            foreach (var x in Controls)
            {
                if (x is Button && ((Button)x).Text != button.Text)
                    ((Button)x).Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            points = new List<PointF>();
            bitmap = new Bitmap(pbox.Width, pbox.Height);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            SwitchOffAllButtons();
            SwitchOnAllButtonsExcept(Add);
            current_mode = Mode.Add;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            SwitchOffAllButtons();
            SwitchOnAllButtonsExcept(Remove);
            current_mode = Mode.Remove;
        }

        private void Move_Click(object sender, EventArgs e)
        {
            SwitchOffAllButtons();
            SwitchOnAllButtonsExcept(Move);
            current_mode = Mode.Move;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pbox.Width, pbox.Height);
            pbox.Image = bitmap;
            points.Clear();
        }

        private void pbox_Click(object sender, EventArgs e)
        {

        }

        private bool LineHasPoint(PointF l1, PointF l2, float px, float py)
        {
            if (Math.Abs(Math.Sqrt((l1.X - px) * (l1.X - px) + (l1.Y - py) * (l1.Y - py)) + Math.Sqrt((l2.X - px) * (l2.X - px) + (l2.Y - py) * (l2.Y - py)) - Math.Sqrt((l2.X - l1.X) * (l2.X - l1.X) + (l2.Y - l1.Y) * (l2.Y - l1.Y))) < 0.1)
                return true;
            return false;
        }

        private void DrawPoint(ref Bitmap b, PointF e, Color color)
        {
            if (e.X > 1 && e.X < b.Width-1 && e.Y > 1 && e.Y < b.Height-1)
            {
                bitmap.SetPixel((int)(e.X + 1), (int)e.Y, color);
                bitmap.SetPixel((int)e.X - 1, (int)e.Y, color);
                bitmap.SetPixel((int)e.X + 1, (int)e.Y + 1, color);
                bitmap.SetPixel((int)e.X - 1, (int)e.Y + 1, color);
                bitmap.SetPixel((int)e.X + 1, (int)e.Y - 1, color);
                bitmap.SetPixel((int)e.X - 1, (int)e.Y - 1, color);
                bitmap.SetPixel((int)e.X, (int)e.Y + 1, color);
                bitmap.SetPixel((int)e.X, (int)e.Y - 1, color);

                bitmap.SetPixel((int)e.X, (int)e.Y, color);
            }
        }

        private void DrawPoint(ref Bitmap b, double x, double y, Color color)
        {
            if (x > 1 && x < b.Width-1 && y > 1 && y < b.Height-1)
            {
                bitmap.SetPixel((int)(x + 1), (int)y, color);
                bitmap.SetPixel((int)x - 1, (int)y, color);
                bitmap.SetPixel((int)x + 1, (int)y + 1, color);
                bitmap.SetPixel((int)x - 1, (int)y + 1, color);
                bitmap.SetPixel((int)x + 1, (int)y - 1, color);
                bitmap.SetPixel((int)x - 1, (int)y - 1, color);
                bitmap.SetPixel((int)x, (int)y + 1, color);
                bitmap.SetPixel((int)x, (int)y - 1, color);

                bitmap.SetPixel((int)x, (int)y, color);
            }
        }

        static public double[,] MatrixMultiplication(double[,] matrixA, double[,] matrixB)
        {
            if (matrixA.ColumnsCount() != matrixB.RowsCount())
            {
                throw new Exception("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            }

            var matrixC = new double[matrixA.RowsCount(), matrixB.ColumnsCount()];

            for (var i = 0; i < matrixA.RowsCount(); i++)
            {
                for (var j = 0; j < matrixB.ColumnsCount(); j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < matrixA.ColumnsCount(); k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return matrixC;
        }

        private bool SamePoint(PointF p1, PointF p2)
        {
            if (Math.Abs(p1.X - p2.X) <= 3 && Math.Abs(p1.Y - p2.Y) <= 3)
                return true;
            return false;
        }

        private bool SamePoint(PointF p1, float x2, float y2)
        {
            if (Math.Abs(p1.X - x2) <= 3 && Math.Abs(p1.Y - y2) <= 3)
                return true;
            return false;
        }

        public void RedrawPoints(ref Graphics g)
        {
            for (int i = 0; i < points.Count; ++i)
                DrawPoint(ref bitmap, points[i], Color.Black);
            pbox.Image = bitmap;
        }

        public void DrawUsualCurve(ref Graphics g)
        {
            Pen p = new Pen(Color.LightGray, 1);
            for (int i = 0; i < points.Count-1; ++i)
                g.DrawLine(p, points[i], points[i + 1]);
            pbox.Image = bitmap;
        }

        public void DrawBezierCurve(ref Graphics g, List<PointF> cur_points)
        {
            Pen pen = new Pen(Color.Red, 1);

            double upperbound = 1.0;

            var p1 = cur_points[0];

            PointF prevpoint = new PointF(p1.X, p1.Y);

            while (cur_points.Count > 3)
            {

                p1 = cur_points[0];
                var p2 = cur_points[1];
                var p3 = cur_points[2];
                var p4 = cur_points[3];
                if (cur_points.Count != 4)
                {
                    cur_points.Insert(3, new PointF((float)(p3.X + (p4.X - p3.X) / 2.5), (float)(p3.Y + (p4.Y - p3.Y) / 2.5)));
                    p4 = cur_points[3];
                }

                var xs = new double[1, 4] { { p1.X, p2.X, p3.X, p4.X } };
                var ys = new double[1, 4] { { p1.Y, p2.Y, p3.Y, p4.Y } };

                var matr = new double[4, 4] { {1,-3, 3, -1},
                                              {0, 3,-6, 3 },
                                              {0, 0, 3,-3 },
                                              {0, 0, 0, 1 } };

                var ts = new double[1, 4];
                PointF curpoint;
                for (double t = 0.0; t <= upperbound; t += 0.04)
                {
                    ts = new double[4, 1] { { 1 }, { t }, { t * t }, { t * t * t } };
                    curpoint = new PointF((int)MatrixMultiplication(MatrixMultiplication(xs, matr), ts)[0, 0],
                                          (int)MatrixMultiplication(MatrixMultiplication(ys, matr), ts)[0, 0]);
                    g.DrawLine(pen, prevpoint, curpoint);
                    prevpoint = curpoint;
                    pbox.Image = bitmap;
                }
                if (cur_points.Count == 4)
                {
                    g.DrawLine(pen, prevpoint, p4);
                    upperbound = 1.0;
                }
                cur_points = cur_points.Skip(3).ToList();
            }

            pbox.Image = bitmap;
        }

        private void RedrawAll(ref Graphics g)
        {
            RedrawPoints(ref g);
            DrawUsualCurve(ref g);
            if (points.Count > 3)
                if (points.Count % 2 == 0)
                {
                    var temp_points = new List<PointF>();
                    for (int i = 0; i < points.Count; i++)
                        temp_points.Add(points[i]);
                    DrawBezierCurve(ref g, temp_points);
                }
                else
                {
                    var last_point = points[points.Count - 1];
                    var prelast_point = points[points.Count - 2];
                    var distx = last_point.X - prelast_point.X;
                    var disty = last_point.Y - prelast_point.Y;
                    var temp_points = new List<PointF>();
                    for (int i = 0; i < points.Count; i++)
                        temp_points.Add(points[i]);
                    //temp_points.Insert(temp_points.Count - 2, new PointF((float)(prelast_point.X + distx / 32.0), (float)(prelast_point.Y + disty / 32.0)));
                    temp_points.Insert(temp_points.Count - 2, new PointF((float)(prelast_point.X), (float)(prelast_point.Y)));
                    DrawBezierCurve(ref g, temp_points);
                }
        }

        private void pbox_MouseDown(object sender, MouseEventArgs e)
        {
            bitmap = new Bitmap(pbox.Width, pbox.Height);
            var g = Graphics.FromImage(bitmap);
            if (current_mode == Mode.Add)
            {
                points.Add(new PointF(e.X, e.Y));
                addisbeingpressed = true;
            }
            else if (current_mode == Mode.Remove)
            {
                foreach (var x in points)
                {
                    if (SamePoint(x, e.X, e.Y))
                    {
                        points.Remove(x);
                        break;
                    }
                }
                RedrawAll(ref g);
            }
            else if (current_mode == Mode.Move)
            {
                moveisbeingpressed = true;
                for (int i = 0; i < points.Count; i++)
                {
                    if (SamePoint(points[i], e.X, e.Y))
                    {
                        movingPoint_num = i;
                        break;
                    }
                }
            }
            else if (current_mode == Mode.AddOnLine)
            {
                addonlineisbeingpressed = true;
                for (int i = 0; i < points.Count - 1; i++)
                {
                    if (LineHasPoint(points[i], points[i + 1], e.X, e.Y))
                    {
                        addonlinePoint_num = i + 1;
                        points.Insert(addonlinePoint_num, new PointF(e.X, e.Y));
                        break;
                    }
                }
            }
        }

        private void pbox_MouseUp(object sender, MouseEventArgs e)
        {
            bitmap = new Bitmap(pbox.Width, pbox.Height);
            var g = Graphics.FromImage(bitmap);

            moveisbeingpressed = false;
            movingPoint_num = -1;
            addisbeingpressed = false;
            addonlineisbeingpressed = false;
            addonlinePoint_num = -1;

            RedrawAll(ref g);
        }

        private void pbox_MouseMove(object sender, MouseEventArgs e)
        {
            int current_point_num = -1;
            if (current_mode == Mode.Move && movingPoint_num != -1)
                current_point_num = movingPoint_num;
            else if (current_mode == Mode.Add && addisbeingpressed)
                current_point_num = points.Count - 1;
            else if (current_mode == Mode.AddOnLine && addonlinePoint_num != -1)
                current_point_num = addonlinePoint_num;

            if (current_point_num >= 0)
            {
                bitmap = new Bitmap(pbox.Width, pbox.Height);
                var g = Graphics.FromImage(bitmap);
                points[current_point_num] = new PointF(e.X, e.Y);
                RedrawAll(ref g);
            }
        }

        private void AddOnLine_Click(object sender, EventArgs e)
        {
            SwitchOffAllButtons();
            SwitchOnAllButtonsExcept(AddOnLine);
            current_mode = Mode.AddOnLine;
        }
    }

    static class MatrixExt
    {
        // метод расширения для получения количества строк матрицы
        public static int RowsCount(this double[,] matrix)
        {
            return matrix.GetUpperBound(0) + 1;
        }

        // метод расширения для получения количества столбцов матрицы
        public static int ColumnsCount(this double[,] matrix)
        {
            return matrix.GetUpperBound(1) + 1;
        }
    }
}
