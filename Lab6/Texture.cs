using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    class Texture
    {
        static Color[,] color_buffer; //соответсвие между пикселем и цветом
        static double size_xx = 0, size_yy = 0;
        static double size_diff_x = 0, size_diff_y = 0;
        static int centerX, centerY, size_x, size_y;
        static Bitmap img;




        private void update_pixel_by_texture(int x, int y, Bitmap img)
        {
            if (x - size_xx >= size_diff_x || x <= size_xx)
                return;
            color_buffer[x, y] = img.GetPixel((int)((x - size_xx) / size_diff_x * img.Width), img.Height - 1 - (int)((-y + size_yy) / size_diff_y * img.Height));
        }

        public static Tuple<int, int, int, int> find_min_max_XYpoint(List<int> f, List<Point3D> points)
        {
            int ind_max_X = 0;
            int ind_max_Y = 0;
            int ind_min_X = 0;
            int ind_min_Y = 0;

            for (int i = 1; i < f.Count(); ++i)
            {
                if (points[f[i]].Y < points[f[ind_min_Y]].Y)
                    ind_min_Y = i;

                if (points[f[i]].X < points[f[ind_min_X]].X)
                    ind_min_X = i;

                if (points[f[i]].Y > points[f[ind_max_Y]].Y)
                    ind_max_Y = i;

                if (points[f[i]].X > points[f[ind_max_X]].X)
                    ind_max_X = i;
            }
            return new Tuple<int, int, int, int>(ind_min_X, ind_max_X, ind_min_Y, ind_max_Y);
        }



        public static Bitmap z_buffer(int width, int height, Polyhedron scene)
        {
            Bitmap newImg = new Bitmap(width, height);
            //List<Point3D> points = scene.points;

            //Tuple<int, int, int, int> min_maxY = find_min_max_XYpoint(f, points);
            //size_diff_x = f.points[min_maxY.Item2].X - f.points[min_maxY.Item1].X;
            //size_diff_y = f.points[min_maxY.Item4].Y - f.points[min_maxY.Item3].Y;

            //int size_x = (int)Math.Round(f.points[min_maxY.Item2].X - f.points[min_maxY.Item1].X) + 1;
            //int size_y = (int)Math.Round(f.points[min_maxY.Item4].Y - f.points[min_maxY.Item3].Y) + 1;
            //size_xx = f.points[min_maxY.Item1].X + centerX;
            //size_yy = -f.points[min_maxY.Item3].Y + centerY;

            //img = new Bitmap(Image.FromFile("D:\\MEXMAT\\course4_1\\КГ\\GIT\\ComputerGraphics\\Lab6\\texture1.jpg"), size_x, size_y);

            double[,] zbuff = new double[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    zbuff[i, j] = Double.MinValue;

            List<List<List<Point3D>>> rasterizedScene = new List<List<List<Point3D>>>();
            rasterizedScene.Add(rasterize(scene));


            for (int i = 0; i < rasterizedScene.Count; i++)
                for (int j = 0; j < rasterizedScene[i].Count; j++)
                {
                    List<Point3D> curr = rasterizedScene[i][j];


                    foreach (Point3D point in curr)
                    {
                        int x = (int)(point.X);
                        int y = (int)(point.Y);

                        if (x < width && y < height && x > 0 && y > 0)
                            if (point.Z > zbuff[x, y])
                            {
                                color_buffer[x, y] = img.GetPixel((int)((x - size_xx) / size_diff_x * img.Width), img.Height - 1 - (int)((-y + size_yy) / size_diff_y * img.Height));
                                newImg.SetPixel(x, y, color_buffer[x, y]);
                            }
                    }
                }
            return newImg;
        }

        public static List<List<Point3D>> rasterize(Polyhedron polyhedron)
        {
            List<List<Point3D>> rasterized = new List<List<Point3D>>();
            foreach (var facet in polyhedron.polygons)
            {
                List<Point3D> currentFac = new List<Point3D>();
                List<Point3D> facetPoints = new List<Point3D>();
                List<Point3D> vertices = polyhedron.points;
                for (int i = 0; i < facet.Count; i++)
                {
                    facetPoints.Add(vertices[facet[i]]);
                }
                currentFac.AddRange(rasterizeShape(facetPoints));
                rasterized.Add(currentFac);

                List<Point3D> points = polyhedron.points;

                centerX = 1083 / 2;
                centerY = 788 / 2;

                Tuple<int, int, int, int> min_maxY = find_min_max_XYpoint(facet, points);
                size_diff_x = points[facet[min_maxY.Item2]].X - points[facet[min_maxY.Item1]].X;
                size_diff_y = points[facet[min_maxY.Item4]].Y - points[facet[min_maxY.Item3]].Y;

                size_x = (int)Math.Round(points[facet[min_maxY.Item2]].X - points[facet[min_maxY.Item1]].X) + 1;
                size_y = (int)Math.Round(points[facet[min_maxY.Item4]].Y - points[facet[min_maxY.Item3]].Y) + 1;
                size_xx = points[facet[min_maxY.Item1]].X + centerX;
                size_yy = -points[facet[min_maxY.Item3]].Y + centerY;

                img = new Bitmap(Image.FromFile("D:\\MEXMAT\\course4_1\\КГ\\GIT\\ComputerGraphics\\Lab6\\texture1.jpg"), size_x, size_y);

            }
            return rasterized;
        }

        private static List<Point3D> rasterizeShape(List<Point3D> points)
        {
            List<Point3D> res = new List<Point3D>();
            List<List<Point3D>> triangles = triangulate(points);
            foreach (var triangle in triangles)
                res.AddRange(rasterizeTriangle(triangle));
            return res;
        }

        private static List<Point3D> rasterizeTriangle(List<Point3D> points)
        {
            List<Point3D> res = new List<Point3D>();

            points.Sort((point1, point2) => point1.Y.CompareTo(point2.Y));

            var interX1 = interpolate((int)Math.Round(points[0].Y), (int)Math.Round(points[0].X), (int)Math.Round(points[1].Y), (int)Math.Round(points[1].X));
            var interX2 = interpolate((int)Math.Round(points[1].Y), (int)Math.Round(points[1].X), (int)Math.Round(points[2].Y), (int)Math.Round(points[2].X));
            var interX3 = interpolate((int)Math.Round(points[0].Y), (int)Math.Round(points[0].X), (int)Math.Round(points[2].Y), (int)Math.Round(points[2].X));

            var interZ1 = interpolate((int)Math.Round(points[0].Y), (int)Math.Round(points[0].Z), (int)Math.Round(points[1].Y), (int)Math.Round(points[1].Z));
            var interZ2 = interpolate((int)Math.Round(points[1].Y), (int)Math.Round(points[1].Z), (int)Math.Round(points[2].Y), (int)Math.Round(points[2].Z));
            var interZ3 = interpolate((int)Math.Round(points[0].Y), (int)Math.Round(points[0].Z), (int)Math.Round(points[2].Y), (int)Math.Round(points[2].Z));

            interX1.RemoveAt(interX1.Count - 1);
            List<int> unitedX = interX1.Concat(interX2).ToList();

            interZ1.RemoveAt(interZ1.Count - 1);
            List<int> unitedZ = interZ1.Concat(interZ2).ToList();

            int middle = unitedX.Count / 2;
            List<int> leftX, rightX, leftZ, rightZ;
            if (interX3[middle] < unitedX[middle])
            {
                leftX = interX3;
                rightX = unitedX;

                leftZ = interZ3;
                rightZ = unitedZ;
            }
            else
            {
                leftX = unitedX;
                rightX = interX3;

                leftZ = unitedZ;
                rightZ = interZ3;
            }

            int y0 = (int)Math.Round(points[0].Y);
            int y2 = (int)Math.Round(points[2].Y);
            while (y2 - y0 > leftX.Count || y2 - y0 > rightX.Count || y2 - y0 > rightZ.Count || y2 - y0 > leftZ.Count)
                y2--;
            for (int ind = 0; ind < y2 - y0; ind++)
            {
                int XL = leftX[ind];
                int XR = rightX[ind];

                List<int> intCurrZ = interpolate(XL, leftZ[ind], XR, rightZ[ind]);

                for (int x = XL; x < XR; x++)
                    res.Add(new Point3D(x, y0 + ind, intCurrZ[x - XL]));
            }
            return res;
        }

        private static List<List<Point3D>> triangulate(List<Point3D> points)
        {
            List<List<Point3D>> res = new List<List<Point3D>>();
            if (points.Count == 3)
                return new List<List<Point3D>> { points };

            for (int i = 2; i < points.Count; i++)
                res.Add(new List<Point3D> { points[0], points[i - 1], points[i] });

            return res;
        }

        private static List<int> interpolate(int i0, int d0, int i1, int d1)
        {
            if (i0 == i1)
                return new List<int> { d0 };
            List<int> res = new List<int>();

            double step = (d1 - d0) * 1.0f / (i1 - i0);
            double value = d0;
            for (int i = i0; i <= i1; i++)
            {
                res.Add((int)value);
                value += step;
            }
            return res;
        }

    }
}
