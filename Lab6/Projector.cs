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
        int z_length = 1600;

        public Point3D camera = null;
        Point3D camera_view_pos = null;
        Point3D center;
        
        public Projector(Point3D coord_center)
        {
            center = coord_center;
            ort_mode = OrtMode.XY;
            FormOrtoghraphicsMatr(ort_mode);
            FormIsometricMatr();
            FormPerspectiveMatr(z_length);
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
        public List<Edge> Project(Mode m, Polyhedron p, Point3D viewVector)
        {
            var copy_p = new Polyhedron(p);
            ToCenterCoord(ref copy_p);
            switch (m)
            {
                case Mode.Orthographic:
                    return ToOrtographics(copy_p, viewVector);
                case Mode.Isometric:
                    return ToIsometric(copy_p, viewVector);
                case Mode.Perspective:
                    return ToPerspective(copy_p, viewVector);
                case Mode.Camera:
                    return ProjectFromCamera(copy_p);
                default:
                    return new List<Edge>();

            }
        }
        public List<Edge> ProjectFromCamera(Polyhedron p)
        {
            AffinTransformator affin_transformer = new AffinTransformator(p.Center());

            var view_vector = new Point3D(0, 0, 1);
            var sin_angle_x = SinBetweenVectorPlain(new Point3D(1, 0, 0), view_vector);
            var sin_angle_y = SinBetweenVectorPlain(new Point3D(0, 1, 0), view_vector);
            var sin_angle_z = SinBetweenVectorPlain(new Point3D(0, 0, 1), view_vector);
            var copy_p = new Polyhedron(p);
            var visible_polys = copy_p.PolyClipping(view_vector);
            affin_transformer.Rotate(ref copy_p, view_vector, sin_angle_x, Math.Sqrt(1 - sin_angle_x * sin_angle_x));
            affin_transformer.Rotate(ref copy_p, view_vector, sin_angle_y, Math.Sqrt(1 - sin_angle_y * sin_angle_y));
            affin_transformer.Rotate(ref copy_p, view_vector, sin_angle_z, Math.Sqrt(1 - sin_angle_z * sin_angle_z));
            affin_transformer.Move(ref copy_p, new Point3D(-camera.X, -camera.Y, -camera.Z));
            affin_transformer.Perspective(ref copy_p,  camera.Z);
            return 
                copy_p
                .PreparePrint(visible_polys)
                .Select((edge3d) => 
                new Edge(new PointF(edge3d.start.X, edge3d.start.Y), 
                            new PointF(edge3d.end.X, edge3d.end.Y)))
                .ToList();
        }
        private void ToCenterCoord(ref Polyhedron poly)
        {
            AffinTransformator affin_transformer = new AffinTransformator(center);
            affin_transformer.Move(ref poly, center);
        }
        private Point3D ToCenterCoord(Point3D p)
        {
            AffinTransformator affin_transformer = new AffinTransformator(center);
            affin_transformer.UpdateMoveMatr(center.X, center.Y, center.Z);
            return affin_transformer.Move(p);
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
        private PointF ToPerspective(Point3D p)
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

        private List<Edge> ToIsometric(Polyhedron ph, Point3D viewVector)
        {
            List<Edge> res = new List<Edge>();
            var edges_3d = ph.PreparePrint(ph.PolyClipping(viewVector));
            foreach (var edge in edges_3d)
            {
                res.Add(Project(Mode.Isometric, edge));
            }
            return res;
        }
        private List<Edge> ToPerspective(Polyhedron ph, Point3D viewVector)
        {
            List<Edge> res = new List<Edge>();
            //var viewVector_4 = MatrixMultiplication(PointToVector(viewVector), perspective_matr);
            //viewVector.X = (float)viewVector_4[0, 0];
            //viewVector.Y = (float)viewVector_4[0, 1];
            //viewVector.Z = (float)viewVector_4[0, 3];
            var edges_3d = ph.PreparePrint(ph.PolyClipping(viewVector));
            foreach (var edge in edges_3d)
            {
                res.Add(Project(Mode.Perspective, edge));
            }
            return res;
        }
        private List<Edge> ToOrtographics(Polyhedron ph, Point3D viewVector)
        {
            List<Edge> edges = new List<Edge>();
            var edges_3d = ph.PreparePrint(ph.PolyClipping(viewVector));
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
        public void UpdateCamera(Point3D cam_pos, Point3D view_pos)
        {
            camera = cam_pos;
            camera_view_pos = view_pos;
        }
        public void UpdateCamera(Point3D cam_pos)
        {
            camera = cam_pos;
        }
        public void UpdatePointOfView(Point3D view_pos)
        {
            camera_view_pos = view_pos;
        }
        public void MoveCamera(Point3D d)
        {
            camera.X += d.X;
            camera.Y += d.Y;
            camera.Z += d.Z;
        }
    }
}
