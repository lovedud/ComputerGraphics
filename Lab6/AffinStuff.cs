using Affin3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public enum Position { Left, Right };
    

    static class AffinStuff
    {
        public enum Mode
        {
            XY,
            XZ,
            YZ
        }

        public class Point3D
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public float eps;
            public Point3D(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
                eps = 0.01F;
            }
            public static bool operator !=(Point3D e1, Point3D e2)
            {
                return !(e1 == e2);
            }
            public static bool operator ==(Point3D e1, Point3D e2)
            {
                return (e1.X - e2.X) < e1.eps && (e1.Y - e2.Y) < e1.eps && (e1.Z - e1.Z) < e1.eps;
            }
        }
 
        public class Polyhedron
        {
            List<Point3D> points;
            Dictionary<int, List<int>> connections; // Key - index of point in list, value - indices of points in list
            // which are connected with key point

            public Polyhedron(List<Point3D> p, Dictionary<int, List<int>> conn)
            {
                points = p;
                connections = conn;
            }
            public Polyhedron(Point3D start_point)
            {
                points = new List<Point3D>();
                connections = new Dictionary<int, List<int>>();
                connections[0] = new List<int>();
                points.Add(start_point);
            }
            private int PointInd(Point3D p)
            {
                int point_ind = points.IndexOf(p);
                if (-1 == point_ind)
                {
                    points.Add(p);
                    point_ind = points.Count() - 1;
                    connections[point_ind] = new List<int>();
                }
                return point_ind;
            }
            public void AddPoint(Point3D p, Point3D conn)
            {
                int point_ind = PointInd(p);
                int c_ind = PointInd(conn);
                connections[point_ind].Add(c_ind);
                connections[c_ind].Add(point_ind);
                
            }

            public List<Edge3D> PreparePrint()
            {
                //TODO: add check for repeat of edge
                //TODO: using type of polyhedron and points to calculate amount of edge and finish later
                List<Edge3D> res = new List<Edge3D>();
                foreach(var c in connections)
                {
                    Point3D point = points[c.Key];
                    foreach(var conn in c.Value)
                    {
                        res.Add(new Edge3D(point, points[conn]));
                    }
                }
                return res;
            }
            
            public void RotateAroundLine(Edge3D line, double angle)
            {
                Point3D lvector = new Point3D(line.end.X - line.start.X, line.end.Y - line.start.Y, line.end.Z - line.start.Z);
                angle = angle * (Math.PI / 180.0);
                //нормализуем вектор, заданный линией
                double len = Math.Sqrt(lvector.X* lvector.X + lvector.Y * lvector.Y + lvector.Z * lvector.Z);
                double l = (lvector.X / len); // l
                double m = (lvector.Y / len); // m
                double n = (lvector.Z / len); // n

                double l_2 = l*l;
                double m_2 = m*m;
                double n_2 = n*n;

                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);

                double[,] matrMoveToZero = new double[4, 4] { { 1,0,0,0 },
                                                        { 0,1,0,0 },
                                                        { 0,0,1,0 },
                                                        { - line.start.X, - line.start.Y, - line.start.Z, 1 } };

                double[,] matrMoveBack = new double[4, 4] { { 1,0,0,0 },
                                                        { 0,1,0,0 },
                                                        { 0,0,1,0 },
                                                        { line.start.X, line.start.Y, line.start.Z, 1 } };

                double[,] matr = new double[4, 4] { { l_2 + cos*(1-l_2), l*(1 - cos)*m + n * sin, l*(1 - cos)*n - m * sin, 0},
                                                    { l * (1 - cos)*m - n*sin, m_2 + cos*(1 - m_2), m*(1-cos)*n + l*sin, 0 },
                                                    {l*(1-cos)*n+m*sin, m*(1-cos)*n-l*sin, n_2+cos*(1-n_2),0 },
                                                    { 0, 0, 0, 1 } };

                foreach(var x in points)
                {
                    double[,] vec = new double[1,4] { { x.X, x.Y, x.Z, 1 } };
                    var res = MatrixMultiplication(MatrixMultiplication(MatrixMultiplication(vec, matrMoveToZero), matr), matrMoveBack);
                    x.X = (float)res[0, 0];
                    x.Y = (float)res[0, 1];
                    x.Z = (float)res[0, 2];
                }
            }

            public Point3D center()
            {
                int counter = 0;
                double xs = 0;
                double ys = 0;
                double zs = 0;
                foreach (var x in points)
                {
                    xs += x.X;
                    ys += x.Y;
                    zs += x.Z;
                    ++counter;
                }
                xs /= counter;
                ys /= counter;
                zs /= counter;
                return new Point3D((float)xs, (float)ys, (float)zs);
            }
        }

            public void getMoved(Point3D p)
            {
                var moveMatrix = new double[4, 4]
                {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { p.X, p.Y, p.Z, 1 }
                };
                for (int i = 0; i < points.Count; i++)
                {
                    var pointMatr = new double[1, 4] { { points[i].X, points[i].Y, points[i].Z, 1 } };
                    var resMatrix = MatrixMultiplication(pointMatr, moveMatrix);
                    points[i] = new Point3D((float)resMatrix[0, 0], (float)resMatrix[0, 1], (float)resMatrix[0, 2]);
                }
            }
        }

        static public Polyhedron CreateCube(Point3D start, float a)
        {
            Polyhedron res = new Polyhedron(start);
            Point3D start_z = new Point3D(start.X, start.Y, start.Z + a);
            Point3D start_x = new Point3D(start.X + a, start.Y, start.Z);
            Point3D start_y = new Point3D(start.X, start.Y + a, start.Z);
            Point3D start_xyz = new Point3D(start.X + a, start.Y + a, start.Z + a);
            Point3D start_xz = new Point3D(start_x.X, start.Y, start_z.Z);
            Point3D start_xy = new Point3D(start_x.X, start_y.Y, start.Z);
            Point3D start_yz = new Point3D(start.X, start_y.Y, start_z.Z);            
            res.AddPoint(start, start_x);
            res.AddPoint(start, start_y);
            res.AddPoint(start, start_z);

            res.AddPoint(start_x, start_xy);
            res.AddPoint(start_x, start_xz);

            res.AddPoint(start_y, start_xy);
            res.AddPoint(start_y, start_yz);

            res.AddPoint(start_z, start_yz);
            res.AddPoint(start_z, start_xz );

            res.AddPoint(start_xyz, start_yz);
            res.AddPoint(start_xyz, start_xz);
            res.AddPoint(start_xyz, start_xy);
            return res;
        }

        static public List<Edge> ToOrtographics(Polyhedron ph, Mode plain)
        {
            List<Edge> edges = new List<Edge>();
            var edges_3d = ph.PreparePrint();
            //var ort_matr = FormMatrByMode(plain);
            foreach(var edge in edges_3d)
            {
                PointF s = new PointF();
                PointF e = new PointF();
                switch(plain)
                {
                    case Mode.XY:
                        s.X = edge.start.X;
                        s.Y = edge.start.Y;
                        e.X = edge.end.X;
                        e.Y = edge.end.Y;
                        break;
                    case Mode.XZ:
                        s.X = edge.start.X;
                        s.Y = edge.start.Z;
                        e.X = edge.end.X;
                        e.Y = edge.end.Z;
                        break;
                    case Mode.YZ:
                        s.X = edge.start.Y;
                        s.Y = edge.start.Z;
                        e.X = edge.end.Y;
                        e.Y = edge.end.Z;
                        break;
                }
                edges.Add(new Edge(s, e));
            }
            return edges;
            
        }
        static public float[,] FormMatrByMode(Mode m)
        {
            float[,] matr = new float[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    if (i == j)
                        matr[i, j] = 1;
                    else matr[i, j] = 0;
                }
            }
            switch(m)
            {
                case Mode.XY:
                    matr[2, 2] = 0;
                    break;
                case Mode.XZ:
                    matr[1, 1] = 0;
                    break;
                case Mode.YZ:
                    matr[0, 0] = 0;
                    break;
            }
            return matr;
        }
        static public double[,] FormIsometricMatr()
        {
            double cos = Math.Cos(2.094395); //120 degree
            double sin = Math.Sin(2.094395);
            return new double[4, 4]
            {{ cos, sin * sin,  0, 0 },
             {0,    cos,        0, 0 },
             {sin,  -sin * cos, 0, 0 },
             {0,    0,          0, 1 } };
        }

        static public List<Edge> ToIsometric(Polyhedron ph)
        {
            List<Edge> res = new List<Edge>();
            var edges_3d = ph.PreparePrint();
            var matr = FormIsometricMatr();
            foreach(var edge in edges_3d)
            {
                var new_start = MatrixMultiplication(PointToVector(edge.start), matr);
                var new_end = MatrixMultiplication(PointToVector(edge.end), matr);
                res.Add(new Edge(VectorToPoint(new_start), VectorToPoint(new_end)));
            }
            return res;
        }

        static public double[,] PointToVector(Point3D p)
        {
            return new double[1, 4] { { p.X, p.Y, p.Z, 1 } };
        }
        static public PointF VectorToPoint(double [,] vec)
        {
            return new PointF((float)vec[0, 0], (float)vec[0, 1]);
        }

        public class Edge3D
        {
            public Edge3D(Point3D s, Point3D e)
            {
                start = s;
                end = e;
            }
            public Edge3D()
            {
                start = end = new Point3D(-1, -1, -1);
            }
            public bool IsFake()
            {
                return start == end;
            }
            public static bool operator== (Edge3D e1, Edge3D e2)
            {
                return e1.start == e2.start && e1.end == e2.end;
            }
            public static bool operator!= (Edge3D e1, Edge3D e2)
            {
                return e1.start != e2.start || e1.end != e2.end;
            }

            public Point3D start;

            public Point3D end;

        }

        public class Edge
        {
            public Edge(PointF s, PointF e)
            {
                start = s;
                end = e;
            }

            public Edge()
            {
                start = end = new PointF(-1, -1);
            }
            public bool IsFake()
            {
                return start == end;
            }
            public static bool operator ==(Edge e1, Edge e2)
            {
                return e1.start == e2.start && e1.end == e2.end;
            }
            public static bool operator !=(Edge e1, Edge e2)
            {
                return e1.start != e2.start || e1.end != e2.end;
            }

            public PointF start;

            public PointF end;

            public Position WherePointF(PointF p)
            {
                float vector_x = end.X - start.X;
                float vector_y = end.Y - start.Y;
                float PointF_vector_x = p.X - start.X;
                float PointF_vector_y = p.Y - start.Y;
                if ((vector_y * PointF_vector_x - vector_x * PointF_vector_y) > 0)
                    return Position.Left;
                else return Position.Right;
            }
        }

        //Класс полигона
        public class Polygon
        {
            public Polygon(List<PointF> ps)
            {
                PointFs = ps;
                convex = IsConvex();
            }

            public Polygon(PointF start_PointF)
            {
                PointFs = new List<PointF>();
                PointFs.Add(start_PointF);
            }

            public void AddEdge(Edge e)
            {
                if (!PointFs.Contains(e.end))
                    PointFs.Add(e.end);
            }
            public void Draw(ref Graphics g)
            {
                Pen p = new Pen(Color.Black, 1);
                g.DrawPolygon(p, PointFs.ToArray());
            }
            

            public List<PointF> PointFs;
            bool convex;

            private bool IsConvex()
            {
                var arrPointF = PointFs.ToArray();
                for (var i = 0; i < arrPointF.Length; i++)
                {
                    Edge edge;
                    if (i == arrPointF.Length - 1)
                    {
                        edge = new Edge(arrPointF[i], arrPointF[0]);
                    }
                    else
                    {
                        edge = new Edge(arrPointF[i], arrPointF[i + 1]);
                    }
                    Position pos = Position.Left; ;
                    for (int j = 0; j < arrPointF.Length; j++)
                    {
                        if (arrPointF[j] != edge.start && arrPointF[j] != edge.end)
                        {

                            if (j == 0)
                                pos = edge.WherePointF(arrPointF[j]);
                            else if (pos != edge.WherePointF(arrPointF[j]))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            public bool IsPointInside(PointF p)
            {
                Edge ray = new Edge(p, new PointF(p.X + 1000, p.Y)); // TODO: come up with somth better, than 1000
                if (EdgeIntersectWithPoly(ray) % 2 == 0)
                    return false;
                else return true;
                    
            }
            private int EdgeIntersectWithPoly(Edge e)
            {
                var arr_Point = PointFs.ToArray();
                int intersect_counter = 0;
                for (int i = 0; i < arr_Point.Length; i++)
                {
                    Edge edge;
                    PointF p = new PointF();
                    if (i == arr_Point.Length - 1)
                        edge = new Edge(arr_Point[i], arr_Point[0]);
                    else
                        edge = new Edge(arr_Point[i], arr_Point[i + 1]);

                    if (CheckEdgesForIntersection(edge, e, ref p)) // TODO: add clause if e intersect Poly's PointF
                        intersect_counter++;
                }
                return intersect_counter;
            }
        }

        static public double MultVectors(PointF v1, PointF v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        static public bool CheckEdgesForIntersection(Edge e1, Edge e2, ref PointF res)
        {
            float x1 = e1.start.X;
            float y1 = e1.start.Y;

            float x2 = e1.end.X;
            float y2 = e1.end.Y;

            float x3 = e2.start.X;
            float y3 = e2.start.Y;

            float x4 = e2.end.X;
            float y4 = e2.end.Y;

            PointF v_e2se2e = new PointF(x4 - x3, y4 - y3);
            PointF v_e2se1s = new PointF(x1 - x3, y1 - y3);
            PointF v_e2se1e = new PointF(x2 - x3, y2 - y3);
            PointF v_e1se1e = new PointF(x2 - x1, y2 - y1);
            PointF v_e1se2s = new PointF(x3 - x1, y3 - y1);
            PointF v_e1se2e = new PointF(x4 - x1, y4 - y1);

            double v1 = MultVectors(v_e2se2e, v_e2se1s);
            double v2 = MultVectors(v_e2se2e, v_e2se1e);
            double v3 = MultVectors(v_e1se1e, v_e1se2s);
            double v4 = MultVectors(v_e1se1e, v_e1se2e);

            double mult1 = v1 * v2;
            double mult2 = v3 * v4;

            if (mult1 < 0 && mult2 < 0)
            {
                double a1 = y2 - y1;
                double b1 = x1 - x2;
                double c1 = x1 * (y1 - y2) + y1 * (x2 - x1);

                double a2 = y4 - y3;
                double b2 = x3 - x4;
                double c2 = x3 * (y3 - y4) + y3 * (x4 - x3);
                double det = a1 * b2 - a2 * b1;
                double detx = c2 * b1 - c1 * b2;
                double dety = c1 * a2 - a1 * c2;
                res = new PointF((int)(detx / det), (int)(dety / det));
                return true;
            }

            return false;

        }

        //Перемножение матриц
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
        static public float[,] MatrixMultiplication(float[,] matrixA, float[,] matrixB)
        {

            var matrixC = new float[matrixA.RowsCount(), matrixB.ColumnsCount()];

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

        static public bool SamePointF(PointF p1, PointF p2)
        {
            if (Math.Abs(p1.X - p2.X) <= 3 && Math.Abs(p1.Y - p2.Y) <= 3)
                return true;
            return false;
        }

        static public bool EdgeHasPointF(PointF p, Edge e)
        {
            double squared_sdx = (e.start.X - p.X) * (e.start.X - p.X);
            double squared_sdy = (e.start.Y - p.Y) * (e.start.Y - p.Y);
            double squared_edx = (e.end.X - p.X) * (e.end.X - p.X);
            double squared_edy = (e.end.Y - p.Y) * (e.end.Y - p.Y);
            double squared_edge_dist_x = (e.end.X - e.start.X) * (e.end.X - e.start.X);
            double squared_edge_dist_y = (e.end.Y - e.start.Y) * (e.end.Y - e.start.Y);

            double kek = Math.Sqrt(squared_sdx + squared_sdy) + Math.Sqrt(squared_edx + squared_edy)
                - Math.Sqrt(squared_edge_dist_x + squared_edge_dist_y);
            if (Math.Abs(kek) < 1)
                return true;
            return false;
        }

        static public void RotateEdge(ref Edge e, double angle)
        {
            angle = angle * (Math.PI / 180.0);
            PointF center = new PointF(e.start.X + (e.end.X - e.start.X) / 2, e.start.Y + (e.end.Y - e.start.Y) / 2);

            var a = center.X;
            var b = center.Y;
            var p = e.start;
            var matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            var matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            var res = MatrixMultiplication(matr1, matr2);
            var newstart = new PointF((int)res[0, 0], (int)res[0, 1]);
            p = e.end;
            matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            res = MatrixMultiplication(matr1, matr2);
            var newend = new PointF((int)res[0, 0], (int)res[0, 1]);

            e = new Edge(newstart, newend);
        }

        static public void DrawPoint(ref Bitmap bitmap, PointF e, Color color)
        {
            if (e.X > 0 && e.X < bitmap.Width && e.Y > 0 && e.Y < bitmap.Height)
            {
                bitmap.SetPixel((int)e.X + 1, (int)e.Y, color);
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

        static public void DrawEdge(ref Graphics g, ref Bitmap bitmap, Edge e)
        {
            Pen p = new Pen(Color.Black, 1);
            g.DrawLine(p, e.start, e.end);
            DrawPoint(ref bitmap, e.end, Color.Red);
        }

        // метод расширения для получения количества строк матрицы
        public static int RowsCount<T>(this T[,] matrix)
        {
            return matrix.GetUpperBound(0) + 1;
        }

        // метод расширения для получения количества столбцов матрицы
        public static int ColumnsCount<T>(this T[,] matrix)
        {
            return matrix.GetUpperBound(1) + 1;
        }
    }
}
