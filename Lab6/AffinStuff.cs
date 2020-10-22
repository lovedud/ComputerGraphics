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
            Perspective
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

            public Polyhedron() { }
            public Polyhedron(List<Point3D> p, Dictionary<int, List<int>> conn)
            {
                points = p;
                connections = conn;
            }

            public Polyhedron Clone()
            {
                Polyhedron cl = new Polyhedron();
                //List<Point3D> npoints = new List<Point3D>();
                //Dictionary<int, List<int>> nconnections = new Dictionary<int, List<int>>();
                cl.points = new List<Point3D>();
                cl.connections = new Dictionary<int, List<int>>();
                for (int i = 0; i < points.Count; ++i)
                {
                    cl.points.Add(new Point3D(points[i].X, points[i].Y, points[i].Z));
                }
                for (int i = 0; i < connections.Count; i++)
                {
                    cl.connections[i] = new List<int>();
                    for (int j = 0; j < connections[i].Count; j++)
                    {
                        cl.connections[i].Add(connections[i][j]);
                    }
                }
                return cl;
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
                List<Edge3D> res = new List<Edge3D>();
                foreach (var c in connections)
                {
                    Point3D point = points[c.Key];
                    foreach (var conn in c.Value)
                    {
                        res.Add(new Edge3D(point, points[conn]));
                    }
                }
                return res;
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

        static public Polyhedron CreateTetrahedron(Point3D start, float a)
        {
            Polyhedron res = new Polyhedron(start);
            Point3D start_x = new Point3D(start.X + a, start.Y, start.Z);
            Point3D start_y = new Point3D(start.X, start.Y + a, start.Z);
            Point3D start_z = new Point3D(start.X, start.Y, start.Z + a);
            Point3D start_xy = new Point3D(start_x.X, start_y.Y, start.Z);
            Point3D start_xz = new Point3D(start_x.X, start.Y, start_z.Z);
            Point3D start_yz = new Point3D(start.X, start_y.Y, start_z.Z);

            res.AddPoint(start, start_xy);
            res.AddPoint(start, start_xz);
            res.AddPoint(start, start_yz);
            
            res.AddPoint(start_xz, start_yz);
            res.AddPoint(start_xz, start_xy);

            res.AddPoint(start_yz, start_xy);

            return res;
        }

        static public Polyhedron CreateCube(Point3D start, float a)
        {
            Polyhedron res = new Polyhedron(start);
            Point3D start_x = new Point3D(start.X + a, start.Y, start.Z);
            Point3D start_y = new Point3D(start.X, start.Y + a, start.Z);
            Point3D start_z = new Point3D(start.X, start.Y, start.Z + a);
            Point3D start_xy = new Point3D(start_x.X, start_y.Y, start.Z);
            Point3D start_xz = new Point3D(start_x.X, start.Y, start_z.Z);
            Point3D start_yz = new Point3D(start.X, start_y.Y, start_z.Z);
            Point3D start_xyz = new Point3D(start.X + a, start.Y + a, start.Z + a);
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

        static public Polyhedron CreateOctahedron(Point3D start, float a)
        {
            a = (float)(a * 1.5);

            Point3D start_xy_2 = new Point3D(start.X + a/2, start.Y+a/2, start.Z);
            Polyhedron res = new Polyhedron(start_xy_2);

            Point3D start_yz_2 = new Point3D(start.X, start.Y + a/2, start.Z+a/2);
            Point3D start_xz_2 = new Point3D(start.X+a/2, start.Y, start.Z + a/2);
            Point3D start_x_yz_2 = new Point3D(start.X+a, start.Y + a/2, start.Z + a/2);
            Point3D start_y_xz_2 = new Point3D(start.X + a/2, start.Y + a, start.Z + a / 2);
            Point3D start_z_xy_2 = new Point3D(start.X + a/2, start.Y + a / 2, start.Z + a);


            res.AddPoint(start_xy_2, start_yz_2);
            res.AddPoint(start_xy_2, start_x_yz_2);
            res.AddPoint(start_xy_2, start_y_xz_2);
            res.AddPoint(start_xy_2, start_xz_2);

            res.AddPoint(start_z_xy_2, start_yz_2);
            res.AddPoint(start_z_xy_2, start_x_yz_2);
            res.AddPoint(start_z_xy_2, start_y_xz_2);
            res.AddPoint(start_z_xy_2, start_xz_2);

            res.AddPoint(start_yz_2, start_y_xz_2);
            res.AddPoint(start_yz_2, start_xz_2);

            res.AddPoint(start_x_yz_2, start_y_xz_2);
            res.AddPoint(start_x_yz_2, start_xz_2);

            return res;
        }

        static public Polyhedron CreateIcosahedron(Point3D start, float a)
        {
            float half_side = (float)((Math.Sqrt(5) - 1) / 4)*a;
            float n = a / 2 - half_side;
            Point3D start_xn_y2 = new Point3D(start.X + n, start.Y + a / 2, start.Z);
            Polyhedron res = new Polyhedron(start_xn_y2);

            Point3D start_xnn_y2 = new Point3D(start.X + a - n, start.Y + a / 2, start.Z);
            Point3D start_x2_zn = new Point3D(start.X + a / 2, start.Y, start.Z + n);
            Point3D start_x2_y_zn = new Point3D(start.X + a / 2, start.Y + a, start.Z + n);
            Point3D start_yn_z2 = new Point3D(start.X, start.Y + n, start.Z + a / 2);
            Point3D start_ynn_z2 = new Point3D(start.X, start.Y + a - n, start.Z + a / 2);
            Point3D start_x_yn_z2 = new Point3D(start.X + a, start.Y + n, start.Z + a / 2);
            Point3D start_x_ynn_z2 = new Point3D(start.X + a, start.Y + a - n, start.Z + a / 2);
            Point3D start_x2_znn = new Point3D(start.X + a / 2, start.Y, start.Z + a - n);
            Point3D start_x2_y_znn = new Point3D(start.X + a / 2, start.Y + a, start.Z + a - n);
            Point3D start_xn_y2_z = new Point3D(start.X + n, start.Y + a / 2, start.Z + a);
            Point3D start_xnn_y2_z = new Point3D(start.X + a - n, start.Y + a / 2, start.Z + a);

            res.AddPoint(start_xn_y2, start_xnn_y2);
            res.AddPoint(start_xn_y2, start_x2_zn);
            res.AddPoint(start_xn_y2, start_yn_z2);
            res.AddPoint(start_xn_y2, start_ynn_z2);
            res.AddPoint(start_xn_y2, start_x2_y_zn);

            res.AddPoint(start_xnn_y2_z, start_x_yn_z2);
            res.AddPoint(start_xnn_y2_z, start_x_ynn_z2);
            res.AddPoint(start_xnn_y2_z, start_x2_y_znn);
            res.AddPoint(start_xnn_y2_z, start_xn_y2_z);
            res.AddPoint(start_xnn_y2_z, start_x2_znn);

            res.AddPoint(start_x2_zn, start_yn_z2);
            res.AddPoint(start_x2_zn, start_x2_znn);
            res.AddPoint(start_x2_zn, start_x_yn_z2);
            res.AddPoint(start_x2_zn, start_xnn_y2);

            res.AddPoint(start_x2_y_znn, start_xn_y2_z);
            res.AddPoint(start_x2_y_znn, start_ynn_z2);
            res.AddPoint(start_x2_y_znn, start_x2_y_zn);
            res.AddPoint(start_x2_y_znn, start_x_ynn_z2);

            res.AddPoint(start_yn_z2, start_x2_znn);
            res.AddPoint(start_yn_z2, start_xn_y2_z);
            res.AddPoint(start_yn_z2, start_ynn_z2);

            res.AddPoint(start_xnn_y2, start_x2_y_zn);
            res.AddPoint(start_xnn_y2, start_x_ynn_z2);
            res.AddPoint(start_xnn_y2, start_x_yn_z2);

            res.AddPoint(start_x2_znn, start_x_yn_z2);
            res.AddPoint(start_x2_znn, start_xn_y2_z);

            res.AddPoint(start_ynn_z2, start_xn_y2_z);
            res.AddPoint(start_ynn_z2, start_x2_y_zn);

            res.AddPoint(start_x_ynn_z2, start_x_yn_z2);
            res.AddPoint(start_x_ynn_z2, start_x2_y_zn);

            return res;
        }

        public static Point3D center(Point3D p1, Point3D p2, Point3D p3)
        {
            float xs = (p1.X + p2.X + p3.X)/3;
            float ys = (p1.Y + p2.Y + p3.Y) / 3;
            float zs = (p1.Z + p2.Z + p3.Z) / 3;
            return new Point3D(xs, ys, zs);
        }

        static public Polyhedron CreateDodecahedron(Point3D start, float a)
        {
            float half_side = (float)((Math.Sqrt(5) - 1) / 4) * a;
            float n = a / 2 - half_side;
            Point3D p0 = new Point3D(start.X + n, start.Y + a / 2, start.Z);
            Point3D p1 = new Point3D(start.X + a - n, start.Y + a / 2, start.Z);
            Point3D p2 = new Point3D(start.X + a / 2, start.Y, start.Z + n);
            Point3D p3 = new Point3D(start.X + a / 2, start.Y + a, start.Z + n);
            Point3D p4 = new Point3D(start.X, start.Y + n, start.Z + a / 2);
            Point3D p5 = new Point3D(start.X, start.Y + a - n, start.Z + a / 2);
            Point3D p6 = new Point3D(start.X + a, start.Y + n, start.Z + a / 2);
            Point3D p7 = new Point3D(start.X + a, start.Y + a - n, start.Z + a / 2);
            Point3D p8 = new Point3D(start.X + a / 2, start.Y, start.Z + a - n);
            Point3D p9 = new Point3D(start.X + a / 2, start.Y + a, start.Z + a - n);
            Point3D p10 = new Point3D(start.X + n, start.Y + a / 2, start.Z + a);
            Point3D p11 = new Point3D(start.X + a - n, start.Y + a / 2, start.Z + a);

            Point3D n0 = center(p0, p2, p4);
            Point3D n1 = center(p0, p1, p2);
            Point3D n2 = center(p0, p1, p3);
            Point3D n3 = center(p0, p3, p5);
            Point3D n4 = center(p0, p4, p5);
            Point3D n5 = center(p6,p8,p11);
            Point3D n6 = center(p6, p7, p11);
            Point3D n7 = center(p7, p9, p11);
            Point3D n8 = center(p9, p10, p11);
            Point3D n9 = center(p8, p10, p11);
            Point3D n10 = center(p2, p6, p8);
            Point3D n11 = center(p1, p2, p6);
            Point3D n12 = center(p1, p6, p7);
            Point3D n13 = center(p1, p3, p7);
            Point3D n14 = center(p3, p7, p9);
            Point3D n15 = center(p3, p5, p9);
            Point3D n16 = center(p5, p9, p10);
            Point3D n17 = center(p4, p5, p10);
            Point3D n18 = center(p4, p8, p10);
            Point3D n19 = center(p2, p4, p8);

            Polyhedron res = new Polyhedron(p0);

            res.AddPoint(n0, n1);
            res.AddPoint(n0, n4);
            res.AddPoint(n0, n19);

            res.AddPoint(n2, n1);
            res.AddPoint(n2, n3);
            res.AddPoint(n2, n13);

            res.AddPoint(n11, n1);
            res.AddPoint(n11, n10);
            res.AddPoint(n11, n12);

            res.AddPoint(n17, n4);
            res.AddPoint(n17, n16);
            res.AddPoint(n17, n18);

            res.AddPoint(n15, n3);
            res.AddPoint(n15, n14);
            res.AddPoint(n15, n16);

            res.AddPoint(n3, n4);

            res.AddPoint(n8, n7);
            res.AddPoint(n8, n9);
            res.AddPoint(n8, n16);

            res.AddPoint(n6, n5);
            res.AddPoint(n6, n7);
            res.AddPoint(n6, n12);

            res.AddPoint(n13, n12);
            res.AddPoint(n13, n14);

            res.AddPoint(n7, n14);

            res.AddPoint(n5, n9);
            res.AddPoint(n5, n10);

            res.AddPoint(n19, n10);
            res.AddPoint(n19, n18);

            res.AddPoint(n9, n18);

            return res;
        }

        static public List<Edge> GetAxis(Point3D center, Mode projection, Projector pr)
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
