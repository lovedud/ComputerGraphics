using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    static class AffinTransformator
    {
        static public void Move(ref Polyhedron poly, Point3D p)
        {
            var move_matr = MoveMatr(p);
            poly.points = poly.points.Select((point) => ApplyTo(point, move_matr)).ToList();
        }

        static public void Scale(ref Polyhedron poly, Point3D around_p, double kx, double ky, double kz)
        {
            var scale_matr = ScaleMatr(around_p, kx, ky, kz);
            poly.points = poly.points.Select((point) => ApplyTo(point, scale_matr)).ToList();
        }

        static public void Rotate(ref Polyhedron poly, Point3D around_p, Point3D vector, double angle)
        {
            angle = angle * (Math.PI / 180.0);
            Rotate(ref poly, vector, Math.Sin(angle), Math.Cos(angle), around_p);
        }
        static public void Rotate(ref Polyhedron poly, Point3D vector, double sin, double cos, Point3D around_p)
        {
            ApplyTo(ref poly, RotateMatr(around_p, vector, sin, cos));
            var rotate_normals_matr = FormRotateMatr(vector, sin, cos);
            poly.normals = poly.normals.Select((point) => ApplyTo(point, rotate_normals_matr)).ToList();
            poly.points_normals = poly.points_normals.Select((point) => ApplyTo(point, rotate_normals_matr)).ToList();
        }
        static public void Perspective(ref Polyhedron poly, double z_dist)
        {
            ApplyTo(ref poly, PerspectiveMatr(z_dist));
        }

        static public AffinMatr FormRotateMatr(Point3D vector, double sin, double cos)
        {
            double l = vector.X;
            double m = vector.Y;
            double n = vector.Z;

            double l_2 = l * l;
            double m_2 = m * m;
            double n_2 = n * n;

            return new AffinMatr(new double[4, 4]
            { { l_2 + cos*(1-l_2),       l*(1 - cos)*m + n * sin, l*(1 - cos)*n - m * sin, 0},
              { l * (1 - cos)*m - n*sin, m_2 + cos*(1 - m_2),     m*(1-cos)*n + l*sin,     0 },
              {l*(1-cos)*n+m*sin,        m*(1-cos)*n-l*sin,       n_2+cos*(1-n_2),         0 },
              { 0,                       0,                       0,                       1 } });

        }
        static public AffinMatr FormMoveMatr(float x, float y, float z)
        {
            return new AffinMatr(new double[4, 4]
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { x, y, z, 1 }
                });
        }
        static public AffinMatr FormScaleMatr(double kx, double ky, double kz)
        {

            return new AffinMatr(new double[4, 4]
                {
                    { 1 / kx, 0,      0,      0 },
                    { 0,      1 / ky, 0,      0 },
                    { 0,      0,      1 / kz, 0 },
                    { 0,      0,      0,      1 }
                });
        }
        static public AffinMatr FormPerspectiveMatr(double z_dist)
        {
            return new AffinMatr(new double[4, 4]
                {
                    { 1 ,0, 0, 0         },
                    { 0, 1 ,0, 0         },
                    { 0, 0, 0, -1 / z_dist },
                    { 0, 0, 0, 1         }
                });
        }
        static public AffinMatr MoveMatr(Point3D d)
        {
            return FormMoveMatr(d.X, d.Y, d.Z);
        }

        static public AffinMatr RotateMatr(Point3D around_p, Point3D vector, double sin, double cos)
        {
            return MatrixMultiplication(
                FormMoveMatr(-around_p.X, -around_p.Y, -around_p.Z),
                FormRotateMatr(vector, sin, cos),
                FormMoveMatr(around_p.X, around_p.Y, around_p.Z)
                );
        }
        static public AffinMatr ScaleMatr(Point3D around_p, double kx, double ky, double kz)
        {
            return MatrixMultiplication(
                FormMoveMatr(-around_p.X, -around_p.Y, -around_p.Z),
                FormScaleMatr(kx, ky, kz),
                FormMoveMatr(around_p.X, around_p.Y, around_p.Z)
                );
        }
        static public AffinMatr PerspectiveMatr(double z_dist)
        {
            return FormPerspectiveMatr(z_dist);
        }
        static public Point3D ApplyTo(Point3D p, params AffinMatr[] matrices)
        {
            return VectorToPoint3D(MatrixMultiplication(PointToVector(p), matrices));
        }
        static public void ApplyTo(ref Point3D p, params AffinMatr[] matrices)
        {
            p = VectorToPoint3D(MatrixMultiplication(PointToVector(p), matrices));
        }
        static public void ApplyTo(ref Polyhedron poly, params AffinMatr[] matrices)
        {
            poly.points = poly.points.Select((point) => ApplyTo(point, matrices)).ToList();
        }

    }
}
