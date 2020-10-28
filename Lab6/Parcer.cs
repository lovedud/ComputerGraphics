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
            List<Point3D> cur_points = new List<Point3D>();
            List<List<int>> cur_polygons = new List<List<int>>();
            using (StreamReader sr = new StreamReader(fname))
            {
                string line = sr.ReadLine();
                while(line != null)
                {  
                    char id = line == "" ? '#' : line[0];
                    
                    if (id == 'v')
                    {
                        char NextChar = line[1];
                        if (NextChar != 't' && NextChar != 'n' && NextChar != 'p')
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
                        
                    }
                    else if (id == 'f')
                    {
                        constructing_polyhedr = true;
                        cur_polygons.Add(ParcePolygon(line));
                    }
                    line = sr.ReadLine();
                }
            }
            constructing_polyhedr = false;
            res.Add(new Polyhedron(cur_points, cur_polygons));
            return res;
        }
        public void SaveToFile(Polyhedron poly, Stream s)
        {
            using(StreamWriter sm = new StreamWriter(s))
            {
                for(var i = 0; i < poly.points.Count; i++)
                {
                    sm.WriteLine($"v {poly.points[i].X} {poly.points[i].Y} {poly.points[i].Z} ");
                }
                for (var i = 0; i < poly.polygons.Count; i++)
                {
                    string line = "f ";
                    for(var j = 0; j < poly.polygons[i].Count; j++)
                    {
                        line += (poly.polygons[i][j] + 1).ToString() + " ";
                    }
                    sm.WriteLine(line);
                }
            }
        }
        private Point3D ParcePoint(string line)
        {
            Point3D res = new Point3D();
            var tokens = line.Substring(1).Trim().Split(' ').SkipWhile((x) => x == "").ToArray(); 
            if (tokens.Length < 2)
                throw new InvalidCastException("Point parse fail");
            res.X = float.Parse(tokens[0].Replace('.',','));
            res.Y = float.Parse(tokens[1].Replace('.', ','));
            res.Z = float.Parse(tokens[2].Replace('.', ','));
            return res;
        }
        private List<int> ParcePolygon(string line)
        {
            List<int> res = new List<int>();
            var tokens = line.Substring(1).Trim().Split(' ').SkipWhile((x) => x == "").ToArray();
            for (var i = 0; i < tokens.Length; i++)
            {
                var vertex_ind = int.Parse(tokens[i].Split('/')[0]);
                res.Add(vertex_ind - 1);
            }
            return res;
        }
        
    }
}
