using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;
using static Affin3D.AffinTransformator;

namespace Affin3D
{
    class Camera
    {
        public Point3D camera;
        public Point3D prev_view_vector;
        public Point3D view_vector;
        
        public Camera(Point3D camera_pos, Point3D vector)
        {
            camera = camera_pos;
            view_vector = vector;
            prev_view_vector = vector;
        }
        public void Rotate(Point3D around_p, Point3D around_axis, double angle)
        {
            double rad_angle = angle * (Math.PI / 180.0);
            ApplyTo(ref camera, RotateMatr(around_p, around_axis, Math.Sin(rad_angle), Math.Cos(rad_angle)));
            ApplyTo(ref view_vector, RotateMatr(new Point3D(0, 0, 0), around_axis, Math.Sin(rad_angle), Math.Cos(rad_angle)));
        }

        public void Move(Point3D d)
        {
            //Point3D d_move = new Point3D(camera.X + d.X, camera.Y + d.Y, camera.Z + d.Z);
            ApplyTo(ref camera, MoveMatr(d));
        }
        public AffinMatr CameraProjection(Point3D center)
        {
            var cos_x = CosBetween(new Point3D(1, 0, 0), view_vector);
            var cos_y = CosBetween(new Point3D(0, 1, 0), view_vector);
            var cos_z = CosBetween(new Point3D(0, 0, 1), view_vector);

            return MatrixMultiplication(
                MoveMatr(center.Neg()),
                FormRotateMatr(new Point3D(1, 0, 0), Math.Sqrt(1 - cos_x * cos_x), cos_x),
                FormRotateMatr(new Point3D(0, 1, 0), Math.Sqrt(1 - cos_y * cos_y), cos_y),
                FormRotateMatr(new Point3D(0, 0, 1), Math.Sqrt(1 - cos_z * cos_z), cos_z),
                MoveMatr(camera.Neg()),
                MoveMatr(center),
                FormPerspectiveMatr(camera.Z));
        }
        public void Reset()
        {
            camera = new Point3D(500, 300, 100);
            view_vector = new Point3D(0, 1, 1);
        }
    }
}
