﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    class Z_buffer
    {
        public static Bitmap z_buffer(int width, int height, Polyhedron scene)
        {
            Bitmap newImg = new Bitmap(width, height);

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
                                zbuff[x, y] = point.Z;
                                newImg.SetPixel(x, y, Color.FromArgb(Math.Abs((int)zbuff[x, y] % 256), 0, 0, 0));
                            }
                    }
                }
            return newImg;
        }

        private static List<List<Point3D>> rasterize(Polyhedron polyhedron)
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
