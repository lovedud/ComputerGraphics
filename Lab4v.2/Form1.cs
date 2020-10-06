using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab4.AffinStuff;



namespace Lab4
{
    
    public partial class Form1 : Form
    {
        Bitmap bitmap;  //Битмап, на котором рисуем

        List<PointF> PointsList = new List<PointF>(); // Список всех точек на экране
        List<Edge> EdgeList = new List<Edge>();  // Список всех ребер на экране
        List<Polygon> PolygonList = new List<Polygon>(); // Список всех полигонов на экране

        //current primitives
        Edge cur_edge = new Edge();
        PointF cur_point;
        Polygon cur_poly;

        //current chosen mode
        Mode cur_mode = Mode.None;

        //background mode
        Mode backgr_mode = Mode.None;

        readonly Color default_color = Color.Black;

        //для постройки ребра
        bool EdgeBegin = false;

        //для простойки полигона
        bool PolyBegin = false;

        public enum Mode
        {
            AddPoint,
            AddEdge,
            AddPoly,
            rotateEdge,
            EdgeIntersection,
            PointPosition,
            PointInside,
            MovingPoly,
            None
        }

        

        void Switch_On_All()
        {
            AddPointButton.Enabled = true;
            AddEdgeButton.Enabled = true;
            AddPolygonButton.Enabled = true;
            RotateEdgeButton.Enabled = true;
            ClearButton.Enabled = true;
        }

        void Switch_Off_All()
        {
            AddPointButton.Enabled = false;
            AddEdgeButton.Enabled = false;
            AddPolygonButton.Enabled = false;
            RotateEdgeButton.Enabled = false;
            ClearButton.Enabled = false;
            
        }

        void Redraw(ref Graphics graphics)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            foreach(var x in PointsList)
            {
                DrawPoint(ref bitmap, x, Color.Black);
            }

            foreach (var x in EdgeList)
            {
                DrawPoint(ref bitmap, x.start, Color.Black);
                DrawEdge(ref graphics, ref bitmap, x);
            }

            foreach (var x in PolygonList)
            {
                x.Draw(ref graphics);
            }

            pictureBox1.Image = bitmap;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
        }


        private void InteractWithButton(Mode m, bool enable)
        {
            switch (m)
            {
                case Mode.AddPoint:
                    if (enable)
                        AddPointButton.Enabled = true;
                    else AddPointButton.Enabled = false;
                    break;
                case Mode.AddEdge:
                    if (enable)
                        AddEdgeButton.Enabled = true;
                    else AddEdgeButton.Enabled = false;
                    break;
                case Mode.AddPoly:
                    if (enable)
                        AddPolygonButton.Enabled = true;
                    else AddPolygonButton.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void AddingPoint(PointF p, Graphics g, bool should_draw = true)
        {
            PointsList.Add(p);
            if (should_draw)
                DrawPoint(ref bitmap, p, Color.Black);
            pictureBox1.Image = bitmap;
            cur_point = p;
        }
        private void AddingEdge(PointF p, Graphics g, bool addingToList = true)
        {
            if (EdgeBegin)
            {
                EdgeBegin = false;
                var prev_point = cur_point;
                AddingPoint(p, g, false);
                Edge edge = new Edge(prev_point, cur_point);
                if (addingToList)
                    EdgeList.Add(edge);
                DrawEdge(ref g, ref bitmap, edge);

                pictureBox1.Image = bitmap;
                cur_edge = edge;
                
            }
            else
            {
                EdgeBegin = true;
                AddingPoint(p, g);
            }
        }

        private void AddingPoly(PointF p, Graphics g)
        {
            if (!(cur_poly is null) && SamePoint(p,cur_poly.points.First()))
                AddingEdge(cur_poly.points.First(), g, false);
            else
                AddingEdge(p, g, false);
            if (PolyBegin)
            {
                cur_poly.AddEdge(cur_edge);
                if (SamePoint(cur_point, cur_poly.points.First()))
                {
                    if (cur_poly.points.Count >= 3)
                    {
                        PolyBegin = false;
                        PolygonList.Add(cur_poly);
                        EdgeBegin = false;
                    }
                }
                EdgeBegin = true;
            }
            else
            {
                PolyBegin = true;
                cur_poly = new Polygon(p);
                EdgeBegin = true;

            }
        }

        private bool FindEdge(PointF p, out Edge e)
        {
            foreach (var ed in EdgeList)
            {
                if (EdgeHasPoint(p, ed))
                {
                    e = ed;
                    return true;
                }
            }
            e = new Edge();
            return false;
        }
        private void RotatingEdge(PointF p, Graphics g)
        {
            if (FindEdge(p, out Edge chosen_edge))
            {
                EdgeList.Remove(chosen_edge);
                PointsList.Remove(chosen_edge.start);
                PointsList.Remove(chosen_edge.end);
                var angle = 90.0;
                RotateEdge(ref chosen_edge, angle);

                EdgeList.Add(chosen_edge);
                
                Redraw(ref g); 
            }
        }

        private void EdgeIntersecting(PointF p)
        {
            if (!EdgeBegin)
            {
                PointF intersec = new PointF();
                foreach (var x in EdgeList)
                {
                    if (cur_edge == x)
                        continue;
                    if (CheckEdgesForIntersection(cur_edge, x, ref intersec))
                    {
                        DrawPoint(ref bitmap, intersec, Color.Blue);
                    }
                }
                pictureBox1.Image = bitmap;
            }
        }

        //using cur_edge and cur_point define the position of cur_point
        private void PointPos()
        {
            string text;
            if (cur_edge is null)
                text = "The edge wasn't selected. Select the edge first, than point";
            else if (cur_point == cur_edge.start || cur_point == cur_edge.end) //TODO: delete it cause it won't happen
                text = "Probably you didn't choose a point, which you want to inspect. Choose point first";
            else if (cur_edge.WherePoint(cur_point) == Position.Left)
                text = "The point is located LEFT from line";
            else text = "The point is located RIGHT from line";
            ShowInInfoBox(text);
        }
        private void ShowInInfoBox(string text)
        {
            InfoTextBox.Text = text;
        }

        private void PointInsidePoly()
        {
            string text;
            if (cur_poly == null)
                text = "Polygon wasn't selected. Create or select polygon first";
            else if (cur_poly.IsPointInside(cur_point))
                text = "Point IS inside polygon";
            else text = "Point IS NOT inside polygon";
            ShowInInfoBox(text);
        }

        private bool SelectedPolygon(PointF p, ref Polygon polygon)
        {
            foreach(var poly in PolygonList)
            {
                if (poly.IsPointInside(p))
                {
                    polygon = poly;
                    return true;
                }
            }
            return false;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PointF p = new PointF(e.X,e.Y);
            Graphics g = Graphics.FromImage(bitmap);
            
            if (Mode.AddPoint == cur_mode)
            {
                AddingPoint(p, g);
                if (backgr_mode == Mode.PointPosition)
                    PointPos();
                else if (backgr_mode == Mode.PointInside)
                    PointInsidePoly();
                Switch_On_All();
                InteractWithButton(Mode.AddPoint, false);
            }
            else if (Mode.AddEdge == cur_mode)
            {
                AddingEdge(p, g);
                if (backgr_mode == Mode.EdgeIntersection)
                    EdgeIntersecting(p);
                if (EdgeBegin)
                {
                    Switch_Off_All();   
                }
                else
                {
                    Switch_On_All();
                    InteractWithButton(Mode.AddEdge, false);
                }
                
            }
            else if (Mode.AddPoly == cur_mode)
            {
                AddingPoly(p,g);
                if (PolyBegin)
                {
                    Switch_Off_All();
                }
                else
                {
                    Switch_On_All();
                    InteractWithButton(Mode.AddPoly, false);
                    EdgeBegin = false;
                }
                
            }
            else if (Mode.rotateEdge == cur_mode)
            {
                RotatingEdge(p, g);
            }
            else if (Mode.MovingPoly == cur_mode)
            {
                
                if (cur_poly == null)// if we didn't select poly
                {
                    if (!SelectedPolygon(p, ref cur_poly))// if we couldn't find poly
                    {
                        string text = "There is no polygon, where point located inside. Try again";
                        ShowInInfoBox(text);
                    }
                    
                }
                else
                {
                    if (cur_poly.IsPointInside(p))
                    {
                        cur_point = p;
                    }
                    else
                    {
                        cur_poly = null;
                        ShowInInfoBox("Polygon was de-selected because you clicked on point outside of polygon");
                    }
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            PointsList.Clear();
            EdgeList.Clear();
            PolygonList.Clear();
            cur_edge = null;
            cur_point = new PointF();
            cur_poly = null;
            EdgeBegin = false;
        }

        private void AddPoint_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            cur_mode = Mode.AddPoint;
            InteractWithButton(cur_mode, false);

        }

        private void AddEdge_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            cur_mode = Mode.AddEdge;
            InteractWithButton(cur_mode, false);
        }

        private void AddPoly_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            cur_mode = Mode.AddPoly;
            InteractWithButton(cur_mode, false);
        }

        private void RotateEdge_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            RotateEdgeButton.Enabled = false;
            cur_mode = Mode.rotateEdge;
        }

        private void EdgeIntesectionButton_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            if (EdgeIntesectionButton.Checked)
                backgr_mode = Mode.EdgeIntersection;
            else backgr_mode = Mode.None;

        }

        private void PointPositionbutton_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            if (PointPositionButton.Checked)
                backgr_mode = Mode.PointPosition;
            else backgr_mode = Mode.None;
        }

        private void PointInsideButton_Click(object sender, EventArgs e)
        {
            Switch_On_All();
            if (backgr_mode == Mode.None)
                backgr_mode = Mode.PointInside;
            else if (backgr_mode == Mode.PointInside)
                backgr_mode = Mode.None;
        }

        private void InfoTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Moving_Click(object sender, EventArgs e)
        {
            string text = "Click inside polygon to start moving it";
            Switch_Off_All();
            cur_mode = Mode.MovingPoly;
            cur_point = new PointF();
            cur_poly = null;
            InteractWithButton(cur_mode, false);
            ShowInInfoBox(text);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mode.MovingPoly != cur_mode || cur_poly != null)
                return;
            Graphics g = Graphics.FromImage(bitmap);
            PointF p = new PointF(e.X, e.Y);
            float dx = cur_point.X - p.X;
            float dy = cur_point.Y - p.Y;
            Polygon poly = new Polygon(p);//TODO: вызов функции перемещения по dx и dy, возвращает полигон
            PolygonList.Add(poly);
            Redraw(ref g);
        }
    }
}
