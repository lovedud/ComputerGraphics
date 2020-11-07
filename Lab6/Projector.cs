using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    class Projector
    {
        private double[,] ortographics_matr;
        OrtMode ort_mode;

        private double[,] isometric_matr;
        private double[,] perspective_matr;
        int z_length;

        public Projector(int c, OrtMode om = OrtMode.XY)
        {
            z_length = c;
            ort_mode = om;
            FormOrtoghraphicsMatr(ort_mode);
            FormIsometricMatr();
            FormPerspectiveMatr(z_length);
        }

        public PointF Project(Mode m, Point3D p)
        {
            
            switch(m)
            {
                case Mode.Orthographic:
                    return ToOrtographics(p, ort_mode);
                case Mode.Isometric:
                    return ToIsometric(p);
                case Mode.Perspective:
                    return ToPerspective(p, z_length);
                default:
                    return new PointF(-1, -1);
            }
        }
        public Edge Project(Mode m, Edge3D e)
        {
            switch (m)
            {
                case Mode.Orthographic:
                    return ToOrtographics(e);
                case Mode.Isometric:
                    return ToIsometric(e);
                case Mode.Perspective:
                    return ToPerspective(e);
                default:
                    return new Edge(new PointF(-1, -1), new PointF(-1, -1));
            }
            
        }
        public List<Edge> Project(Mode m, Polyhedron p)
        {
            switch (m)
            {
                case Mode.Orthographic:
                    return ToOrtographics(p);
                case Mode.Isometric:
                    return ToIsometric(p);
                case Mode.Perspective:
                    return ToPerspective(p);
                default:
                    return new List<Edge>();

            }
        }

        public List<Edge> ProjectFHA(Polyhedron p)
        {
            return ToOrtographicsFHA(p);
        }

        private Edge ToOrtographics(Edge3D e)
        {
            var new_start = VectorToPoint(MatrixMultiplication(PointToVector(e.start), ortographics_matr));
            var new_end = VectorToPoint(MatrixMultiplication(PointToVector(e.end), ortographics_matr));
            return new Edge(new_start, new_end);
        }
        private Edge ToIsometric(Edge3D e)
        {
            var new_start = VectorToPoint(MatrixMultiplication(PointToVector(e.start), isometric_matr));
            var new_end = VectorToPoint(MatrixMultiplication(PointToVector(e.end), isometric_matr));
            return new Edge(new_start, new_end);
        }
        private Edge ToPerspective(Edge3D e)
        {
            var new_start = VectorToPoint(MatrixMultiplication(PointToVector(e.start), perspective_matr));
            var new_end = VectorToPoint(MatrixMultiplication(PointToVector(e.end), perspective_matr));
            return new Edge(new_start, new_end);
        }

        private void FormOrtoghraphicsMatr(OrtMode m)
        {
            ortographics_matr = new double[4, 4]
            {{ 1, 0, 0, 0 },
             { 0, 1, 0, 0 },
             { 0, 0, 1, 0 },
             { 0, 0, 0, 1 }};
            switch (m)
            {
                case OrtMode.XY:
                    ortographics_matr[2, 2] = 0;
                    break;
                case OrtMode.XZ:
                    ortographics_matr[1, 1] = 0;
                    break;
                case OrtMode.YZ:
                    ortographics_matr[0, 0] = 0;
                    break;
            }
           
        }
        private void FormIsometricMatr()
        {
            double cos = Math.Cos(60 * Math.PI / 180);
            double sin = Math.Sin(60 * Math.PI / 180);
            isometric_matr =  new double[4, 4]
            {{ cos, sin * sin,  0, 0 },
             { 0,   cos,        0, 0 },
             { sin, -sin * cos, 0, 0 },
             { 0,   0,          0, 1 } };
        }
        private void FormPerspectiveMatr(int c)
        {
            perspective_matr = new double[4, 4]
             {{ 1, 0, 0, 0      },
             { 0, 1, 0, 0       },
             { 0, 0, 0, 1.0 / c },
             { 0, 0, 0, 1       } };
        }

        private PointF ToIsometric(Point3D p)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, isometric_matr);
            return VectorToPoint(new_vector_p);
        }
        private PointF ToPerspective(Point3D p, int c)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, perspective_matr);
            return VectorToPoint(new_vector_p);
        }
        private PointF ToOrtographics(Point3D p, OrtMode om)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, ortographics_matr);
            return VectorToPoint(new_vector_p);
        }

        private List<Edge> ToIsometric(Polyhedron ph)
        {
            List<Edge> res = new List<Edge>();
            var edges_3d = ph.PreparePrint();
            foreach (var edge in edges_3d)
            {
                res.Add(Project(Mode.Isometric, edge));
            }
            return res;
        }
        private List<Edge> ToPerspective(Polyhedron ph)
        {
            List<Edge> res = new List<Edge>();
            var edges_3d = ph.PreparePrint();
            foreach (var edge in edges_3d)
            {
                res.Add(Project(Mode.Perspective, edge));
            }
            return res;
        }
        private List<Edge> ToOrtographics(Polyhedron ph)
        {
            List<Edge> edges = new List<Edge>();
            var edges_3d = ph.PreparePrint();
            foreach (var edge in edges_3d)
            {
                edges.Add(Project(Mode.Orthographic, edge));
            }
            return edges;
        }

        private List<Edge> ToOrtographicsFHA(Polyhedron ph)
        {
            List<Edge> edges = new List<Edge>();
            var edges_3d = ph.PreparePrintFHA();
            foreach (var edge in edges_3d)
            {
                edges.Add(Project(Mode.Orthographic, edge));
            }
            return edges;
        }

        public void Update(OrtMode om)
        {
            ort_mode = om;
        }
    }
}
