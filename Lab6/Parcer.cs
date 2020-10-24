using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Affin3D.AffinStuff;

namespace Affin3D
{
    class Parcer
    {
        public List<Polyhedron> ParceFromFile(string fname)
        {
            List<Polyhedron> res = new List<Polyhedron>();
            bool constructing_polyhedr = false;
            using (StreamReader sr = new StreamReader(fname))
            {
                string line = sr.ReadLine();
                List<Point3D> cur_points = new List<Point3D>();
                List<List<int>> cur_polygons = new List<List<int>>();
                while(line != null)
                {  
                    char id = line == "" ? '#' : line[0];
                    if (id == 'v')
                    {
                        if (constructing_polyhedr)
                        {
                            constructing_polyhedr = false;
                            res.Add(new Polyhedron(cur_points, cur_polygons));
                            cur_points = new List<Point3D>();
                            cur_polygons = new List<List<int>>();
                        }
                        cur_points.Add(ParcePoint(line));
                    }
                    else if (id == 'f')
                    {
                        constructing_polyhedr = true;
                        cur_polygons.Add(ParcePolygon(line));
                    }
                    line = sr.ReadLine();
                }
            }
            return res;
        }
        private Point3D ParcePoint(string line)
        {
            Point3D res = new Point3D();
            var tokens = line.Split(' ');
            if (tokens.Length < 4)
                throw new InvalidCastException("Point parse fail");
            res.X = float.Parse(tokens[1].Trim(' '));
            res.Y = float.Parse(tokens[2].Trim(' '));
            res.Z = float.Parse(tokens[3].Trim(' '));
            return res;
        }
        private List<int> ParcePolygon(string line)
        {
            List<int> res = new List<int>();
            var tokens = line.Split(' ');
            for(var i = 1; i < tokens.Length - 1; i++)
            {
                var vertex_ind = int.Parse(tokens[i].Split('/')[0]);
                res.Add(vertex_ind);
            }
            return res;
        }
    }
}
