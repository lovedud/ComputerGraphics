using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        //Перемножение матриц
        static double[,] MatrixMultiplication(double[,] matrixA, double[,] matrixB)
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

        //Класс ребра
        public class Edge
        {
            public Edge(Point s, Point e)
            {
                start = s;
                end = e;
            }

            public Point start;

            public Point end;
        }

        //Класс полигона
        public class Polygon
        {
            public Polygon(List<Point> ps)
            {
                points = ps;
            }

            public List<Point> points;
        }

        Bitmap bitmap;  //Битмап, на котором рисуем

        List<Point> PointsList = new List<Point>(); // Список всех точек на экране
        List<Edge> EdgeList = new List<Edge>();  // Список всех ребер на экране
        List<Polygon> PolygonList = new List<Polygon>(); // Список всех полигонов на экране

        //Логика проверки режима
        bool AddPoint = true; // Создание точки
        bool AddEdge = true; // Создание ребра
        bool AddPoly = true; // Создание полигона
        bool rotateEdge = true; // Поворот ребра
        bool EdgeIntersection = true; //Пересечение ребер

        bool WasEdge = false; //Проверка того, что на экране есть ребро

        void Switch_On_All()
        {
            AddPointButton.Enabled = true;
            AddEdgeButton.Enabled = true;
            AddPolygonButton.Enabled = true;
            RotateEdgeButton.Enabled = true;
            ClearButton.Enabled = true;
            EdgeIntesectionButton.Enabled = true;
            EdgeIntersection = true;
            AddPoint = true;
            AddEdge = true;
            AddPoly = true;
            rotateEdge = true;
        }

        void Switch_Off_All()
        {
            AddPointButton.Enabled = false;
            AddEdgeButton.Enabled = false;
            AddPolygonButton.Enabled = false;
            RotateEdgeButton.Enabled = false;
            EdgeIntesectionButton.Enabled = false;
            ClearButton.Enabled = false;
        }

        void Redraw(ref Graphics graphics)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            foreach(var x in PointsList)
            {
                DrawPoint(ref bitmap, x, Color.Black);
            }

            foreach (var x in EdgeList)
            {
                DrawPoint(ref bitmap, x.start, Color.Black);
                DrawEdge(ref graphics, x);
            }

            foreach (var x in PolygonList)
            {
                var start = x.points.First();
                var s = start;
                foreach(var p in x.points)
                {
                    if (s.X != p.X && s.Y != p.Y)
                        graphics.DrawLine(new Pen(Color.Black, 1), s, p);
                    s = p;
                }
                graphics.DrawLine(new Pen(Color.Black, 1), s, start);
            }

            pictureBox1.Image = bitmap;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            PointsList.Clear();
            EdgeList.Clear();
            PolygonList.Clear();
        }

        private void AddPoint_Click(object sender, EventArgs e) 
        {
            AddPointButton.Enabled = false;
            AddEdgeButton.Enabled = true;
            AddPolygonButton.Enabled = true;
            RotateEdgeButton.Enabled = true;

            AddPoint = false;
            AddEdge = true;
            AddPoly = true;
            rotateEdge = true;
        }

        private void AddEdge_Click(object sender, EventArgs e)
        {
            AddPointButton.Enabled = true;
            AddEdgeButton.Enabled = false;
            AddPolygonButton.Enabled = true;
            RotateEdgeButton.Enabled = true;

            AddPoint = true;
            AddEdge = false;
            AddPoly = true;
            rotateEdge = true;
        }

        private void AddPoly_Click(object sender, EventArgs e)
        {
            AddPointButton.Enabled = true;
            AddEdgeButton.Enabled = true;
            AddPolygonButton.Enabled = false;
            RotateEdgeButton.Enabled = true;

            AddPoint = true;
            AddEdge = true;
            AddPoly = false;
            rotateEdge = true;
        }

        private void DrawPoint(ref Bitmap b, Point e, Color color)
        {
            if (e.X >= 0 && e.X <= b.Width && e.Y >= 0 && e.Y <= b.Height)
            {
                bitmap.SetPixel(e.X + 1, e.Y, color);
                bitmap.SetPixel(e.X - 1, e.Y, color);
                bitmap.SetPixel(e.X + 1, e.Y + 1, color);
                bitmap.SetPixel(e.X - 1, e.Y + 1, color);
                bitmap.SetPixel(e.X + 1, e.Y - 1, color);
                bitmap.SetPixel(e.X - 1, e.Y - 1, color);
                bitmap.SetPixel(e.X, e.Y + 1, color);
                bitmap.SetPixel(e.X, e.Y - 1, color);

                bitmap.SetPixel(e.X, e.Y, color);
            }
        }

        private void DrawEdge(ref Graphics g, Edge e)
        {
            Pen p = new Pen(Color.Black, 1);
            g.DrawLine(p, e.start, e.end);
            DrawPoint(ref bitmap, e.end, Color.Red);
        }

        private bool SamePoint(ref Point p1, ref Point p2)
        {
            if (Math.Abs(p1.X - p2.X) <= 3 && Math.Abs(p1.Y - p2.Y) <= 3)
                return true;
            return false;
        }

        private bool EdgeHasPoint(Point p, Edge e)
        {
            if (Math.Abs(Math.Sqrt((e.start.X - p.X)* (e.start.X - p.X) + (e.start.Y - p.Y)* (e.start.Y - p.Y)) + Math.Sqrt((e.end.X - p.X)* (e.end.X - p.X) + (e.end.Y - p.Y)* (e.end.Y - p.Y)) - Math.Sqrt((e.end.X - e.start.X)* (e.end.X - e.start.X) + (e.end.Y - e.start.Y) * (e.end.Y - e.start.Y))) < 1)
                return true;
            return false;
        }

        private void RotateEdge(ref Edge e, double angle)
        {
            angle = angle * (Math.PI / 180.0);
            Point center = new Point(e.start.X + (e.end.X - e.start.X)/2, e.start.Y + (e.end.Y - e.start.Y) / 2);

            var a = center.X;
            var b = center.Y;
            var p = e.start;
            var matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            var matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            var res = MatrixMultiplication(matr1, matr2);
            var newstart = new Point((int)res[0, 0], (int)res[0,1]);
            p = e.end;
            matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            res = MatrixMultiplication(matr1, matr2);
            var newend = new Point((int)res[0, 0], (int)res[0, 1]);

            e = new Edge(newstart, newend);
        }


        //для постройки ребра
        bool EdgeBegin = false;
        Point EdgeStart;

        //для простойки полигона
        bool PolyBegin = false;
        Point PolyStart;
        List<Point> PolyPoints = new List<Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X,e.Y);
            Graphics g = Graphics.FromImage(bitmap);
            if (!AddPoint)
            {
                PointsList.Add(p);
                DrawPoint(ref bitmap, p, Color.Black);
                pictureBox1.Image = bitmap;
            }
            else if (!AddEdge)
            {
                if (EdgeBegin)
                {
                    EdgeBegin = false;
                    Edge edge = new Edge(EdgeStart, p);
                    EdgeList.Add(edge);
                    DrawEdge(ref g, edge);
                    pictureBox1.Image = bitmap;
                    Switch_On_All();
                    AddEdgeButton.Enabled = false;
                    AddEdge = false;
                }
                else
                {
                    EdgeBegin = true;
                    Switch_Off_All();
                    EdgeStart = new Point(e.X, e.Y);
                    DrawPoint(ref bitmap, EdgeStart, Color.Black);
                }
            }
            else if (!AddPoly)
            {
                if (PolyBegin)
                {
                    if (SamePoint(ref p, ref PolyStart))
                    {
                        if (PolyPoints.Count >= 3)
                        {
                            PolyBegin = false;
                            g.DrawLine(new Pen(Color.Black, 1), PolyPoints.Last(), PolyStart);
                            PolygonList.Add(new Polygon(PolyPoints));
                            Switch_On_All();
                            AddPolygonButton.Enabled = false;
                            AddPoly = false;
                        }
                    }
                    else
                    {
                        g.DrawLine(new Pen(Color.Black, 1), PolyPoints.Last(), p);
                        PolyPoints.Add(new Point(e.X, e.Y));
                    }
                    
                    pictureBox1.Image = bitmap;
                }
                else
                {
                    AddPointButton.Enabled = false;
                    AddEdgeButton.Enabled = false;
                    ClearButton.Enabled = false;
                    RotateEdgeButton.Enabled = false;
                    EdgeIntesectionButton.Enabled = false;
                    PolyBegin = true;
                    PolyPoints.Clear();
                    PolyStart = new Point(e.X, e.Y);
                    PolyPoints.Add(PolyStart);
                }
            }
            else if (!rotateEdge)
            {
                bool f = false;
                Edge curEdge = new Edge(p,p);
                foreach(var ed in EdgeList)
                {
                    if (EdgeHasPoint(p, ed))
                    {
                        curEdge = ed;
                        f = true;
                        break;
                    }
                }
                if (f)
                {
                    EdgeList.Remove(curEdge);
                    var angle = 90.0;
                    RotateEdge(ref curEdge, angle);

                    EdgeList.Add(curEdge);

                    Redraw(ref g);
                }

            }
            else if (!EdgeIntersection)
            {
                
            }
        }

        private void RotateEdge_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            RotateEdgeButton.Enabled = false;
            rotateEdge = false;
        }

        private void EdgeIntesectionButton_Click(object sender, EventArgs e)
        {      
            Switch_On_All();
            EdgeIntersection = false;
            EdgeIntesectionButton.Enabled = false;
        }

    }

    //Матрица
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
