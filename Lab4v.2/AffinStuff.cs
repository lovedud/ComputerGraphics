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
            public static bool operator== (Edge e1, Edge e2)
            {
                return e1.start == e2.start && e1.end == e2.end;
            }
            public static bool operator!= (Edge e1, Edge e2)
            {
                return e1.start != e2.start || e1.end != e2.end;
            }

            public PointF start;

            public PointF end;

            public Position WherePoint(PointF p)
            {
                double vector_x = end.X - start.X;
                double vector_y = end.Y - start.Y;
                double Point_vector_x = p.X - start.X;
                double Point_vector_y = p.Y - start.Y;
                if ((vector_y * Point_vector_x - vector_x * Point_vector_y) > 0)
                    return Position.Left;
                else return Position.Right;
            }
        }

        //Класс полигона
        public class Polygon
        {
            public Polygon(List<PointF> ps)
            {
                for (int i = 0; i < ps.Count; ++i)
                {
                    origPoints[i] = ps[i];
                    changedPoints[i] = ps[i];
                }
                convex = IsConvex();
            }

            public Polygon(PointF start_Point)
            {
                origPoints = new List<PointF>();
                changedPoints = new List<PointF>();
                origPoints.Add(start_Point);
                changedPoints.Add(start_Point);
            }

            public void AddEdge(Edge e)
            {
                if (!origPoints.Contains(e.end))
                {
                    origPoints.Add(e.end);
                    changedPoints.Add(e.end);
                }
            }
            public void Draw(ref Graphics g)
            {
                Pen p = new Pen(Color.Black, 1);
                g.DrawPolygon(p, changedPoints.ToArray());
            }
            

            public List<PointF> origPoints;
            public List<PointF> changedPoints;


            public void MoveTo(PointF p)
            {
                var moveMatr = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 },  { p.X, p.Y, 1 } };

                for (int i = 0; i < changedPoints.Count; i++)
                {
                    var pointMatr = new double[1, 3] { { changedPoints[i].X, changedPoints[i].Y, 1 } };
                    var resMatrix = MatrixMultiplication(pointMatr, moveMatr);
                    changedPoints[i] = new PointF((float)resMatrix[0, 0], (float)resMatrix[0,1]);
                }
            }

            public void Rotate(PointF p, int deg)
            { 

                var moveMatr = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { p.X, p.Y, 1 } };
                var moveMatrToZero = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { -p.X, -p.Y, 1 } };

                double angle = deg * Math.PI / 180.0;

                double sin = Math.Sin(angle), cos = Math.Cos(angle);

                var rotateMatrix = new double[3, 3] { { cos, sin, 0 }, { -sin, cos, 0 }, { 0, 0, 1 } };



                for (int i = 0; i < changedPoints.Count; i++)
                {
                    var pointMatr = new double[1, 3] { { changedPoints[i].X, changedPoints[i].Y, 1 } };
                    var resMatrix = MatrixMultiplication(pointMatr, moveMatrToZero);
                    resMatrix = MatrixMultiplication(resMatrix, rotateMatrix);
                    resMatrix = MatrixMultiplication(resMatrix, moveMatr);
                    changedPoints[i] = new PointF((float)resMatrix[0, 0], (float)resMatrix[0, 1]);
                }
            }

            public void Scale(PointF p, double kx, double ky)
            {
                var moveMatr = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { p.X, p.Y, 1 } };

                var moveMatrToZero = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { -p.X, -p.Y, 1 } };

                var scaleMatr = new double[3, 3] { { 1 / kx , 0, 0 }, { 0, 1 / ky, 0 }, { 0, 0, 1 } };

                for (int i = 0; i < changedPoints.Count; i++)
                {
                    var pointMatr = new double[1, 3] { { changedPoints[i].X, changedPoints[i].Y, 1 } };
                    var resMatrix = MatrixMultiplication(pointMatr, moveMatrToZero);
                    resMatrix = MatrixMultiplication(resMatrix, scaleMatr);
                    resMatrix = MatrixMultiplication(resMatrix, moveMatr);
                    changedPoints[i] = new PointF((float)resMatrix[0, 0], (float)resMatrix[0, 1]);
                }
            }

            public bool convex;

            private bool IsConvex()
            {
                var arrPoint = origPoints.ToArray();
                for (var i = 0; i < arrPoint.Length; i++)
                {
                    Edge edge;
                    if (i == arrPoint.Length - 1)
                    {
                        edge = new Edge(arrPoint[i], arrPoint[0]);
                    }
                    else
                    {
                        edge = new Edge(arrPoint[i], arrPoint[i + 1]);
                    }
                    Position pos = Position.Left; ;
                    for (int j = 0; j < arrPoint.Length; j++)
                    {
                        if (arrPoint[j] != edge.start && arrPoint[j] != edge.end)
                        {

                            if (j == 0)
                                pos = edge.WherePoint(arrPoint[j]);
                            else if (pos != edge.WherePoint(arrPoint[j]))
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
                var arr_PointF = origPoints.ToArray();
                int intersect_counter = 0;
                for (int i = 0; i < arr_PointF.Length; i++)
                {
                    Edge edge;
                    PointF p = new PointF();
                    if (i == arr_PointF.Length - 1)
                        edge = new Edge(arr_PointF[i], arr_PointF[0]);
                    else
                        edge = new Edge(arr_PointF[i], arr_PointF[i + 1]);

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

        static public bool SamePoint(PointF p1, PointF p2)
        {
            if (Math.Abs(p1.X - p2.X) <= 10 && Math.Abs(p1.Y - p2.Y) <= 10)
                return true;
            return false;
        }

        static public bool EdgeHasPoint(PointF p, Edge e)
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
            var newstart = new PointF((float)res[0, 0], (float)res[0, 1]);
            p = e.end;
            matr1 = new double[1, 3] { { p.X, p.Y, 1 } };
            matr2 = new double[3, 3] { { Math.Cos(angle), Math.Sin(angle), 0 }, { -Math.Sin(angle), Math.Cos(angle), 0 }, { -a * Math.Cos(angle) + b * Math.Sin(angle) + a, -a * Math.Sin(angle) - b * Math.Cos(angle) + b, 1 } };
            res = MatrixMultiplication(matr1, matr2);
            var newend = new PointF((float)res[0, 0], (float)res[0, 1]);

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
