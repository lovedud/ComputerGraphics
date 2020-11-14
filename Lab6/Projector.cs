using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;
using static Affin3D.AffinTransformator;

namespace Affin3D
{
    class Projector
    {
        private double[,] ortographics_matr;
        OrtMode ort_mode;

        private double[,] isometric_matr;
        private double[,] perspective_matr;
        int z_length = 1600;

        public Point3D center;
        Light light;

        public Projector(Point3D coord_center)
        {
            center = coord_center;
            ort_mode = OrtMode.XY;
            FormOrtoghraphicsMatr(ort_mode);
            FormIsometricMatr();
            FillPerspectiveMatr(z_length);
        }

        //public IEnumerable<Edge> Project(Mode m, Polyhedron p, Camera cam)
        //{
        //    AffinMatr view_matrix;
        //    var copy_p = new Polyhedron(p);
        //    ToCenterCoord(ref copy_p);
        //    switch (m)
        //    {
        //        case Mode.Orthographic:
        //            view_matrix = new AffinMatr(ortographics_matr);
        //            break;
        //        case Mode.Isometric:
        //            view_matrix = new AffinMatr(isometric_matr);
        //            break;
        //        case Mode.Perspective:
        //            view_matrix = new AffinMatr(perspective_matr);
        //            break;
        //        case Mode.Camera:
        //            view_matrix = cam.CameraProjection(copy_p.Center());
        //            break;
        //        default:
        //            return new HashSet<Edge>();

        //    }

        //    return copy_p.PreparePrint(new Point3D(0, 0, 1)).Select((edge) => new Edge(ApplyTo(edge.start, view_matrix).To2D(),
        //        ApplyTo(edge.end, view_matrix).To2D()));
        //}

        public IEnumerable<Edge> Project(Mode m, Polyhedron p, Camera cam)
        {
            AffinMatr view_matrix;
            var copy_p = new Polyhedron(p);
            ToCenterCoord(ref copy_p);
            switch (m)
            {
                case Mode.Orthographic:
                    view_matrix = new AffinMatr(ortographics_matr);
                    return copy_p.PreparePrint(new Point3D(0, 0, 1)).Select((edge) => new Edge(ApplyTo(edge.start, view_matrix).To2D(),
                ApplyTo(edge.end, view_matrix).To2D()));
                    break;
                case Mode.Isometric:
                    view_matrix = new AffinMatr(isometric_matr);
                    return copy_p.PreparePrint(new Point3D(-1, 1, 1)).Select((edge) => new Edge(ApplyTo(edge.start, view_matrix).To2D(),
                ApplyTo(edge.end, view_matrix).To2D()));
                    break;
                case Mode.Perspective:
                    view_matrix = new AffinMatr(perspective_matr);
                    return copy_p.PreparePrint(new Camera(new Point3D(0,0,1600), new Point3D(0,0,1))).Select((edge) => new Edge(ApplyTo(edge.start, view_matrix).To2D(),
                ApplyTo(edge.end, view_matrix).To2D()));
                    break;
                case Mode.Camera:
                    view_matrix = cam.CameraProjection(copy_p.Center());
                    return copy_p.PreparePrint(cam).Select((edge) => new Edge(ApplyTo(edge.start, view_matrix).To2D(),
                ApplyTo(edge.end, view_matrix).To2D()));
                    break;
                default:
                    return new HashSet<Edge>();

            }

            
        }
        public IEnumerable<Rastr> Project(Mode m, Polyhedron p, Camera cam, Light l)
        {
            List<Rastr> res = new List<Rastr>();
            var copy_p = new Polyhedron(p);
            ToCenterCoord(ref copy_p);
            IEnumerable<List<Rastr>> points_2D;
            light = l;
            switch (m)
            {
                case Mode.Orthographic:
                    points_2D = PrepareToRastr(ToOrtographics, copy_p, new Point3D(0, 0, 1));
                    break;
                case Mode.Isometric:
                    points_2D = PrepareToRastr(ToIsometric, copy_p, cam.view_vector);
                    break;
                case Mode.Perspective:
                    points_2D = PrepareToRastr(ToPerspective, copy_p, cam.view_vector);
                    break;
                case Mode.Camera:
                    points_2D = ProjectFromCamera(copy_p, cam);
                    break;
                default:
                    return new List<Rastr>();

            }
            foreach (var poly in points_2D)
            {
                if (poly.Count() != 0)
                {
                    res.AddRange(BilinearPolygonInterpolation(poly));
                }
            }
           
            return res;
        }
        public IEnumerable<List<Rastr>> ProjectFromCamera(Polyhedron p, Camera c)
        {
            var copy_p = new Polyhedron(p);
            ApplyTo(ref copy_p, c.CameraProjection(copy_p.Center()));
            return copy_p.PreparePrint(copy_p.PolyClipping(c.view_vector), light)
                .Select((x) => x.Select((pf) => new Rastr(pf.Item1, pf.Item2)).ToList());
        }
        private void ToCenterCoord(ref Polyhedron poly)
        {
            ApplyTo(ref poly, MoveMatr(center));
        }
        private Point3D ToCenterCoord(Point3D p)
        {
            Point3D copy_p = new Point3D(p);
            ApplyTo(ref copy_p, MoveMatr(center));
            return copy_p;
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
        private void FillPerspectiveMatr(int c)
        {
            perspective_matr = new double[4, 4]
            {{ 1, 0, 0, 0      },
             { 0, 1, 0, 0       },
             { 0, 0, 0, -1.0 / c },
             { 0, 0, 0, 1       } };
        }

        private PointF ToIsometric(Point3D p)
        {
            Point3D copy_p = new Point3D(p);
            ApplyTo(ref copy_p, new AffinMatr(isometric_matr));
            return copy_p.To2D();
        }
        private PointF ToPerspective(Point3D p)
        {
            Point3D copy_p = new Point3D(p);
            ApplyTo(ref copy_p, new AffinMatr(perspective_matr));
            return copy_p.To2D();
        }
        private PointF ToOrtographics(Point3D p)
        {
            Point3D copy_p = new Point3D(p);
            ApplyTo(ref copy_p, new AffinMatr(ortographics_matr));
            return copy_p.To2D();
        }


        public void Update(OrtMode om)
        {
            ort_mode = om;
            FormOrtoghraphicsMatr(om);
        }
        
        private IEnumerable<List<Rastr>> PrepareToRastr(Func<Point3D, PointF> projection, Polyhedron poly, Point3D view_vector)
        {
            return poly.PreparePrint(poly.PolyClipping(view_vector), light)
                .Select((x) => x.Select((y) => new Rastr(projection(y.Item1), y.Item2)).ToList());
        }
        
    }
}
