using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin3D
{
    public enum Position { Left, Right };
    
    static class AffinStuff
    {
        public enum OrtMode
        {
            XY,
            XZ,
            YZ
        }
        public enum Mode
        {
            Isometric,
            Orthographic,
            Perspective,
            Camera
        }

        public class Point3D : IEquatable<Point3D>
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public float eps;
            public Point3D()
            {
                X = -1;
                Y = -1;
                Z = -1;
            }
            public Point3D(Point3D p)
            {
                X = p.X;
                Y = p.Y;
                Z = p.Z;
            }
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
                return Math.Abs(e1.X - e2.X) < e1.eps && Math.Abs(e1.Y - e2.Y) < e1.eps && Math.Abs(e1.Z - e2.Z) < e1.eps;
            }

            public bool Equals(Point3D other)
            {
                return other == this;
            }

            public bool ObtuseAngle(Point3D v)
            {
                double a = Math.Sqrt(X * X + Y * Y + Z * Z) * Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
                double b = X * v.X + Y * v.Y + Z * v.Z;
                double c = b / a;
                return c < 0 ? true : false;
                // d = Math.Acos(c) * 180 / Math.PI;
                //return ((int)Math.Abs(d)) >= 90;
            }

        }


        public class Polyhedron
        {
            public List<Point3D> points;
            public List<List<int>> polygons;
            public List<Point3D> normals;

            
            public Polyhedron()
            {
                points = new List<Point3D>();
                polygons = new List<List<int>>();
                normals = new List<Point3D>();
            }
            public Polyhedron(List<Point3D> p, List<List<int>> conn, List<Point3D> norm)
            {
                points = p;
                polygons = conn;
                normals = norm;
            }
            public Polyhedron(Polyhedron p)
            {
                points = new List<Point3D>(p.points);
                polygons = new List<List<int>>(p.polygons);
                normals = new List<Point3D>(p.normals);
            }
            private int PointInd(Point3D p)
            {
                int point_ind = points.IndexOf(p);
                if (-1 == point_ind)
                {
                    points.Add(p);
                    point_ind = points.Count() - 1;
                }
                return point_ind;
            }
            
            public void AddPoints(List<Point3D> polygon)
            {
                polygons.Add(new List<int>());
                int polygon_ind = polygons.Count - 1;
                foreach(var p in polygon)
                {
                    int point_ind = PointInd(p);
                    polygons[polygon_ind].Add(point_ind);
                }
                AddNormal(polygon);
            }
            public List<int> PolyClipping(Point3D viewVector)
            {
                List<int> visible_poly = new List<int>();
                for(var i = 0; i < polygons.Count(); i++)
                {
                    if ((viewVector.X != 0 || viewVector.Y != 0 || viewVector.Z != 0) && !normals[i].ObtuseAngle(viewVector))
                    {
                        visible_poly.Add(i);
                    }
                }
                return visible_poly;
            }
            public HashSet<Edge3D> PreparePrint(List<int> visible_polys)
            {
                HashSet<Edge3D> res = new HashSet<Edge3D>();
                for(var poly = 0; poly < polygons.Count(); poly++)
                {
                    if (polygons[poly].Count < 3 || !visible_polys.Contains(poly))
                        continue;
                    Point3D prev = points[polygons[poly][0]];
                    for (var i = 1; i < polygons[poly].Count; i++)
                    {
                        Point3D cur = points[polygons[poly][i]];
                        res.Add(new Edge3D(prev, cur));
                        prev = cur;
                    }
                    Edge3D last = new Edge3D(prev, points[polygons[poly][0]]);
                    res.Add(last);
                }
                return res;
            }

            public void AddNormal(List<Point3D> polygon)
            {
                normals.Add(CreateNormal(polygon));
            }

            public Point3D CreateNormal(List<Point3D> polygon)
            {
                Point3D v1 = new Point3D(polygon[0].X - polygon[1].X, polygon[0].Y - polygon[1].Y, polygon[0].Z - polygon[1].Z);
                Point3D v2 = new Point3D(polygon[2].X - polygon[1].X, polygon[2].Y - polygon[1].Y, polygon[2].Z - polygon[1].Z);
                //Point3D normalv = new Point3D(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X); //если против часовой
                Point3D normalv = new Point3D(v1.Z * v2.Y - v1.Y * v2.Z, v1.X * v2.Z - v1.Z * v2.X, v1.Y * v2.X - v1.X * v2.Y); //если по часовой
                return NormalizedVector(new Edge3D(new Point3D(0, 0, 0), normalv));
            }


            public void RotateAroundLine(Point3D start, Point3D vector, double angle)
            {
                angle = angle * (Math.PI / 180.0);

                double l = vector.X;
                double m = vector.Y;
                double n = vector.Z;

                double l_2 = l * l;
                double m_2 = m * m;
                double n_2 = n * n;

                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);

                double[,] matrMoveToZero = new double[4, 4] { { 1,0,0,0 },
                                                        { 0,1,0,0 },
                                                        { 0,0,1,0 },
                                                        { - start.X, - start.Y, - start.Z, 1 } };

                double[,] matrMoveBack = new double[4, 4] { { 1,0,0,0 },
                                                        { 0,1,0,0 },
                                                        { 0,0,1,0 },
                                                        { start.X, start.Y, start.Z, 1 } };

                double[,] matr = new double[4, 4] { { l_2 + cos*(1-l_2), l*(1 - cos)*m + n * sin, l*(1 - cos)*n - m * sin, 0},
                                                    { l * (1 - cos)*m - n*sin, m_2 + cos*(1 - m_2), m*(1-cos)*n + l*sin, 0 },
                                                    {l*(1-cos)*n+m*sin, m*(1-cos)*n-l*sin, n_2+cos*(1-n_2),0 },
                                                    { 0, 0, 0, 1 } };

                foreach (var x in points)
                {
                    double[,] vec = new double[1, 4] { { x.X, x.Y, x.Z, 1 } };
                    var res = MatrixMultiplication(MatrixMultiplication(MatrixMultiplication(vec, matrMoveToZero), matr), matrMoveBack);
                    x.X = (float)res[0, 0];
                    x.Y = (float)res[0, 1];
                    x.Z = (float)res[0, 2];
                }

                foreach (var x in normals)
                {
                    double[,] vec = new double[1, 4] { { x.X, x.Y, x.Z, 1 } };
                    var res = MatrixMultiplication(vec, matr);
                    x.X = (float)res[0, 0];
                    x.Y = (float)res[0, 1];
                    x.Z = (float)res[0, 2];
                }
                //int polycount = 0;
                //foreach(var x in polygons)
                //{
                //    List<Point3D> polys = new List<Point3D>();
                //    foreach(var y in x)
                //    {
                //        polys.Add(points[y]);
                //    }
                //    normals[polycount] = CreateNormal(polys);
                //    polycount++;
                //}
            }

            public Point3D Center()
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

                //for (int i = 0; i < normals.Count; i++)
                //{
                //    var pointMatr = new double[1, 4] { { normals[i].X, normals[i].Y, normals[i].Z, 1 } };
                //    var resMatrix = MatrixMultiplication(pointMatr, moveMatrix);
                //    normals[i] = new Point3D((float)resMatrix[0, 0], (float)resMatrix[0, 1], (float)resMatrix[0, 2]);
                //}
            }
            

            public void scale(Point3D p, double kx, double ky, double kz)
            {
                var moveMatr = new double[4, 4] 
                { 
                    { 1, 0, 0, 0}, 
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { p.X, p.Y, p.Z, 1 } 
                };

                var moveMatrToZero = new double[4, 4]
                {
                    { 1, 0, 0, 0},
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { -p.X, -p.Y, -p.Z, 1 } 
                };

                var scaleMatr = new double[4, 4] 
                { 
                    { 1 / kx, 0, 0, 0 }, 
                    { 0, 1 / ky, 0, 0 },
                    { 0, 0, 1 / kz, 0 },
                    { 0, 0, 0, 1 } 
                };

                for (int i = 0; i < points.Count; i++)
                {
                    var pointMatr = new double[1, 4] { { points[i].X, points[i].Y, points[i].Z, 1 } };
                    var resMatrix = MatrixMultiplication(pointMatr, moveMatrToZero);
                    resMatrix = MatrixMultiplication(resMatrix, scaleMatr);
                    resMatrix = MatrixMultiplication(resMatrix, moveMatr);
                    points[i] = new Point3D((float)resMatrix[0, 0], (float)resMatrix[0, 1], (float)resMatrix[0, 2]);
                }

            }
        }
        
        public static Point3D NormalizedVector(Edge3D line)
        {
            Point3D lvector = new Point3D(line.end.X - line.start.X, line.end.Y - line.start.Y, line.end.Z - line.start.Z);

            //нормализуем вектор, заданный линией
            double len = Math.Sqrt(lvector.X * lvector.X + lvector.Y * lvector.Y + lvector.Z * lvector.Z);
            double l = (lvector.X / len);
            double m = (lvector.Y / len); 
            double n = (lvector.Z / len); 
            return new Point3D((float)l, (float)m, (float)n);
        }


        static public Polyhedron CreateCube(Point3D start, float a)
        {
            Polyhedron res = new Polyhedron();
            Point3D p1 = new Point3D(start.X + a, start.Y, start.Z);
            Point3D p2 = new Point3D(start.X, start.Y + a, start.Z);
            Point3D p3 = new Point3D(start.X, start.Y, start.Z + a);
            Point3D p4 = new Point3D(p1.X, p2.Y, start.Z);
            Point3D p6 = new Point3D(p1.X, start.Y, p3.Z);
            Point3D p5 = new Point3D(start.X, p2.Y, p3.Z);
            Point3D p7 = new Point3D(start.X + a, start.Y + a, start.Z + a);

            List<Point3D> face1 = new List<Point3D>() {start, p2, p4, p1 };
            List<Point3D> face2 = new List<Point3D>() { start, p3, p5, p2 };
            List<Point3D> face3 = new List<Point3D>() { p2, p5, p7, p4 };
            List<Point3D> face4 = new List<Point3D>() { p1, p4, p7, p6 };
            List<Point3D> face5 = new List<Point3D>() { start, p1, p6, p3 };
            List<Point3D> face6 = new List<Point3D>() { p3, p6, p7, p5 };
            res.AddPoints(face1);
            res.AddPoints(face2);
            res.AddPoints(face3);
            res.AddPoints(face4);
            res.AddPoints(face5);
            res.AddPoints(face6);
            return res;
        }

        static public Polyhedron CreateTetrahedron(Point3D start, float a)
        {
            Polyhedron res = new Polyhedron();
            Point3D p1 = new Point3D(start.X, start.Y + a, start.Z + a);
            Point3D p2 = new Point3D(start.X+a, start.Y + a, start.Z);
            Point3D p3 = new Point3D(start.X+a, start.Y, start.Z + a);

            List<Point3D> face1 = new List<Point3D>() { start, p1, p2 };
            List<Point3D> face2 = new List<Point3D>() { start, p2, p3 };
            List<Point3D> face3 = new List<Point3D>() { start, p3, p1 };
            List<Point3D> face4 = new List<Point3D>() { p3, p2, p1 };

            res.AddPoints(face1);
            res.AddPoints(face2);
            res.AddPoints(face3);
            res.AddPoints(face4);

            return res;
        }

        static public Polyhedron CreateTestFigure(Point3D start, float a)
        {

            Polyhedron res = new Polyhedron();
            Point3D p1 = new Point3D(start.X + a, start.Y, start.Z);
            Point3D p2 = new Point3D(start.X, start.Y + a, start.Z-a/2);
            Point3D p3 = new Point3D(start.X, start.Y, start.Z + a);
            Point3D p4 = new Point3D(start.X + a + a/2, start.Y + a, start.Z-a/2);
            Point3D p5 = new Point3D(start.X, start.Y + a, start.Z + a+a/2);
            Point3D p6 = new Point3D(start.X + a, start.Y, start.Z + a);
            Point3D p7 = new Point3D(start.X + a + a/2, start.Y + a, start.Z + a + a/2);

            List<Point3D> face1 = new List<Point3D>() { start, p2, p4, p1 };
            List<Point3D> face2 = new List<Point3D>() { start, p3, p5, p2 };
            List<Point3D> face3 = new List<Point3D>() { p2, p5, p7, p4 };
            List<Point3D> face4 = new List<Point3D>() { p1, p4, p7, p6 };
            List<Point3D> face5 = new List<Point3D>() { start, p1, p6, p3 };
            List<Point3D> face6 = new List<Point3D>() { p3, p6, p7, p5 };
            res.AddPoints(face1);
            res.AddPoints(face2);
            res.AddPoints(face3);
            res.AddPoints(face4);
            res.AddPoints(face5);
            res.AddPoints(face6);
            return res;
        }

        public static Point3D center(Point3D p1, Point3D p2, Point3D p3)
        {
            float xs = (p1.X + p2.X + p3.X)/3;
            float ys = (p1.Y + p2.Y + p3.Y) / 3;
            float zs = (p1.Z + p2.Z + p3.Z) / 3;
            return new Point3D(xs, ys, zs);
        }

        public static List<Edge> GetAxis(Point3D center, Mode projection, Projector pr)
        {
            List<Edge> res = new List<Edge>();
            res.Add(pr.Project(projection, new Edge3D(center, new Point3D(center.X + 200, center.Y, center.Z))));
            res.Add(pr.Project(projection, new Edge3D(center, new Point3D(center.X, center.Y - 200, center.Z))));
            res.Add(pr.Project(projection, new Edge3D(center, new Point3D(center.X, center.Y, center.Z + 200))));
            return res;
        }

        static public double[,] PointToVector(Point3D p)
        {
            return new double[1, 4] { { p.X, p.Y, p.Z, 1 } };
        }
        static public Point3D VectorToPoint3D(double[,] vec)
        {
            var w = vec[0, 3] == 0 ? 1 : vec[0, 3];
            return new Point3D((float)(vec[0, 0] / w), (float)(vec[0, 1] / w), (float)(vec[0, 2] / w));
        }
        static public PointF VectorToPoint(double[,] vec)
        {
            var w = vec[0, 3] == 0 ? 1 : vec[0, 3];
            if (vec[0, 0] == 0)
            {
                return new PointF((float)(vec[0, 1] / w), (float)(vec[0, 2] / w));
            }
            else if (vec[0,1] == 0)
            {
                return new PointF((float)(vec[0, 0] /w), (float)(vec[0, 2] / w));
            }
            return new PointF((float)(vec[0, 0] / w), (float)(vec[0, 1] / w));
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
                Edge ray = new Edge(p, new PointF(p.X + 1000, p.Y));
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

                    if (CheckEdgesForIntersection(edge, e, ref p))
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
        static public double SinBetweenVectorPlain(Point3D plain, Point3D vector)
        {
            double kek = Math.Abs(plain.X * vector.X + plain.Y * vector.Y + plain.Z * vector.Z);
            double lol = Math.Sqrt(plain.X * plain.X + plain.Y * plain.Y + plain.Z * plain.Z);
            double cheburek = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            return Math.Sqrt(1 - kek / (lol * cheburek) * kek / (lol * cheburek));
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
            if (e.X > 1 && e.X < bitmap.Width-1 && e.Y > 1 && e.Y < bitmap.Height-1)
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

        static public void DrawEdge(ref Graphics g, ref Bitmap bitmap, Edge e, bool drawpoint = true)
        {
            Pen p = new Pen(Color.Black, 1);
            g.DrawLine(p, e.start, e.end);
            if (drawpoint)
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
