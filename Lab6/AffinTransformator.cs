using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    class AffinTransformator
    {
        private double[,] move_matr = new double[4, 4]
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                };
        private double[,] back_move_matr = new double[4, 4]
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                };
        private double[,] scale_matr = new double[4, 4]
                {
                    { 1 ,0, 0, 0 },
                    { 0, 1 ,0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                };
        private double[,] rotate_matr;
        private double[,] perspective_matr = new double[4, 4]
                {
                    { 1 ,0, 0, 0 },
                    { 0, 1 ,0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                };
        public Point3D center;
        public AffinTransformator(Point3D c)
        {
            center = c;
        }

        private void BackMove(ref Point3D p)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, back_move_matr);
            p = VectorToPoint3D(new_vector_p);
        }
        private void Move(ref Point3D p)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, move_matr);
            p = VectorToPoint3D(new_vector_p);
        }
        private Point3D Move(Point3D p)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, move_matr);
            return VectorToPoint3D(new_vector_p);  
        }
        private void Scale(ref Point3D p)
        {
            BackMove(ref p);
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, scale_matr);
            p = VectorToPoint3D(new_vector_p);
            Move(ref p);
        }
        private Point3D Scale(Point3D p)
        {
            BackMove(ref p);
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, scale_matr);
            p = VectorToPoint3D(new_vector_p);
            Move(ref p);
            return p;
        }
        private void Rotate(ref Point3D p)
        {
            BackMove(ref p);
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, rotate_matr);
            p = VectorToPoint3D(new_vector_p);
            Move(ref p);
        }
        private Point3D Rotate(Point3D p)
        {
            Rotate(ref p);
            return p;
        }
        private Point3D Perspect(Point3D p)
        {
            var vector_p = PointToVector(p);
            var new_vector_p = MatrixMultiplication(vector_p, perspective_matr);
            p = VectorToPoint3D(new_vector_p);
            return p;
        }
        private void Move(ref Edge3D e)
        {
            Move(ref e.start);
            Move(ref e.end);
        }
        private void Scale(ref Edge3D e)
        {
            Scale(ref e.start);
            Scale(ref e.end);
        }
        private void Rotate(ref Edge3D e)
        {
            Rotate(ref e.start);
            Rotate(ref e.end);
        }

        public Polyhedron Move(Polyhedron poly, Point3D p)
        {
            UpdateMoveMatr(p.X, p.Y, p.Z);
            poly.points = poly.points.Select((x) => Move(x)).ToList();
            return poly;
        }
        public void Move(ref Polyhedron poly, Point3D p)
        {
            UpdateMoveMatr(p.X, p.Y, p.Z);
            poly.points = poly.points.Select((x) => Move(x)).ToList();
        }

        public Polyhedron Scale(Polyhedron poly, double kx, double ky, double kz)
        {
            Polyhedron copy_poly = new Polyhedron(poly);
            UpdateMoveMatr(center.X, center.Y, center.Z);
            UpdateBackMoveMatr();
            UpdateScaleMatr(kx, ky, kz);
            copy_poly.points.ForEach((x) => Scale(ref x));
            return copy_poly;
        }
        public void Scale(ref Polyhedron poly, double kx, double ky, double kz)
        {
            UpdateMoveMatr(center.X, center.Y, center.Z);
            UpdateBackMoveMatr();
            UpdateScaleMatr(kx, ky, kz);
            poly.points = poly.points.Select((x) => Scale(x)).ToList();
        }

        public void Rotate(ref Polyhedron poly, Point3D vector, double angle)
        {
            angle = angle * (Math.PI / 180.0);
            Rotate(ref poly, vector, Math.Sin(angle), Math.Cos(angle));
        }
        public Polyhedron Rotate(Polyhedron poly, Point3D vector, double sin, double cos)
        {
            Rotate(ref poly, vector, sin, cos);
            return poly;
        }
        public void Rotate(ref Polyhedron poly, Point3D vector, double sin, double cos)
        {
            UpdateMoveMatr(center.X, center.Y, center.Z);
            UpdateBackMoveMatr();
            UpdateRotateMatr(vector, sin, cos);
            poly.points = poly.points.Select((x) => Rotate(x)).ToList();
        }
        public void Perspective(ref Polyhedron poly, double c)
        {
            UpdatePerspectiveMatr(c);
            poly.points = poly.points.Select((x) => Perspect(x)).ToList();
        }
        private void UpdateMoveMatr(float x, float y, float z)
        {
            move_matr[3, 0] = x;
            move_matr[3, 1] = y;
            move_matr[3, 2] = z;
        }
        private void UpdateBackMoveMatr()
        {
            back_move_matr[3, 0] = -move_matr[3, 0];
            back_move_matr[3, 1] = -move_matr[3, 1];
            back_move_matr[3, 2] = -move_matr[3, 2];
        }
        private void UpdateScaleMatr(double kx, double ky, double kz)
        {
            scale_matr[0, 0] = 1 / kx;
            scale_matr[1, 1] = 1 / ky;
            scale_matr[2, 2] = 1 / kz;
        }
        private void UpdateRotateMatr(Point3D vector, double sin, double cos)
        {
            double l = vector.X;
            double m = vector.Y;
            double n = vector.Z;

            double l_2 = l * l;
            double m_2 = m * m;
            double n_2 = n * n;

            rotate_matr = new double[4, 4]
            { { l_2 + cos*(1-l_2),       l*(1 - cos)*m + n * sin, l*(1 - cos)*n - m * sin, 0},
              { l * (1 - cos)*m - n*sin, m_2 + cos*(1 - m_2),     m*(1-cos)*n + l*sin,     0 },
              {l*(1-cos)*n+m*sin,        m*(1-cos)*n-l*sin,       n_2+cos*(1-n_2),         0 },
              { 0,                       0,                       0,                       1 } };

        }
        private void UpdatePerspectiveMatr(double c)
        {
            perspective_matr[3, 2] = 1 / c;
        }

    }
}
