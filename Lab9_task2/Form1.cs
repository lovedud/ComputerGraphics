using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Drawing2D;
//using System.Runtime.Serialization;

namespace AffinTransform3D
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Matrixes matr = new Matrixes();
        Pen pen_shape = new Pen(Color.Red); // для фигуры
        int centerX, centerY; // центр pictureBox
        List<face> shape = new List<face>(); // фигура - список граней
        List<my_point> points = new List<my_point>(); // список точек
        List<Tuple<int, int, int, int>> check = new List<Tuple<int, int, int, int>>();
        bool not_redraw = false; // перерисовывать или нет текущее положение
        List<my_point> initial_points = new List<my_point>();
        Dictionary<int, List<int>> relationships = new Dictionary<int, List<int>>();
        //ObjectIDGenerator linker;

        Color[,] color_buffer; //соответсвие между пикселем и цветом
        double size_xx = 0, size_yy = 0;
        double size_diff_x = 0, size_diff_y = 0;

        bool is_texturing = false;

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            centerX = pictureBox.Width / 2;
            centerY = pictureBox.Height / 2;
            pictureBox.Image = bmp;
            g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.White);
            pictureBox.Invalidate();
            color_buffer = new Color[pictureBox.Width, pictureBox.Height];
            openFileDialog_texture.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";

        }
        //Deprecated
        private void build_points()
        {
            points.Clear();
            foreach (face sh in shape)
                for (int i = 0; i < sh.points.Count; i++)
                    if (!points.Contains(sh.points[i]))
                        points.Add(sh.points[i]);
        }

        private void draw_point(my_point p) // рисуем точку
        {
            g.FillEllipse(new SolidBrush(Color.Green), (int)Math.Round(p.X + centerX - 3), (int)Math.Round(-p.Y + centerY - 3), 6, 6);
        }

        private List<my_point> Copy(List<my_point> l)
        {
            List<my_point> res = new List<my_point>(l.Count);
            for (int i = 0; i < l.Count; ++i)
                res.Add(new my_point(l[i].X, l[i].Y, l[i].Z));
            return res;
        }

        private void draw_face(face f) // рисуем грань
        {
            List<my_point> points_to_draw = new List<my_point>(f.points.Count());
                List<my_point> points_to_draw1 = matr.get_transformed_my_points(matr.matrix_projection_yz(), Copy(f.points));
                for (int i = 0; i < points_to_draw1.Count(); i++)
                {
                    points_to_draw.Add(new my_point(points_to_draw1[i].Y, points_to_draw1[i].Z, points_to_draw1[i].Z));
                }

            for (int i = 0; i < points_to_draw.Count(); i++)
            {
                int x1 = (int)Math.Round(points_to_draw[i].X + centerX);
                int x2 = (int)Math.Round(points_to_draw[(i + 1) % points_to_draw.Count()].X + centerX);
                int y1 = (int)Math.Round(-points_to_draw[i].Y + centerY);
                int y2 = (int)Math.Round(-points_to_draw[(i + 1) % points_to_draw.Count()].Y + centerY);
                g.DrawLine(pen_shape, x1, y1, x2, y2);
            }
        }

        Bitmap bmp2;
        bool flag = true;

        private void draw_pic_by_pixels()
        {
            build_pixels_to_draw();
            for (int i = 0; i < pictureBox.Width; ++i)
                for (int j = 0; j < pictureBox.Height; ++j)
                    ((Bitmap)pictureBox.Image).SetPixel(i, j, color_buffer[i, j]);
            pictureBox.Invalidate();
        }

        private void redraw_image() // перерисовываем картинку
        {
            if (not_redraw)
            {
                g.Save();
                if (flag)
                {
                    bmp2 = new Bitmap(bmp);
                    flag = false;
                }
                g.Clear(Color.White);
                bmp = new Bitmap(bmp2);
                g = Graphics.FromImage(bmp);
            }
            else
            {
                flag = true;
                g.Clear(Color.White);
            }

            if ( is_texturing)
                draw_pic_by_pixels();
            else
            {
                foreach (face f in shape)
                {
                    draw_face(f);
                }
                pictureBox.Image = bmp;
            }
        }

        private void build_tetrahedron()
        {
            double h = Math.Sqrt(3) * 50;
            double h_big = 25 * Math.Sqrt(13);
            points.Clear();
            my_point p1 = new my_point(-50, -h/3, 0);
            my_point p2 = new my_point(50, -h/3, 0);
            my_point p3 = new my_point(0, 2*h/3, 0);
            my_point p4 = new my_point(0, 0, h_big);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2 });
            relationships.Add(1, new List<int>() { 0, 3, 1 });
            relationships.Add(2, new List<int>() { 3, 1, 2 });
            relationships.Add(3, new List<int>() { 0, 3, 2 });
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); shape.Add(f1);
            face f2 = new face(); f2.add(p1); f2.add(p4); f2.add(p2); shape.Add(f2);
            face f3 = new face(); f3.add(p4); f3.add(p2); f3.add(p3); shape.Add(f3);
            face f4 = new face(); f4.add(p1); f4.add(p4); f4.add(p3); shape.Add(f4);
            
        }

        private void build_hexahedron()
        {
            points.Clear();
            my_point p1 = new my_point(-50, -50, -50);
            my_point p2 = new my_point(-50, 50, -50);
            my_point p3 = new my_point(50, 50, -50);
            my_point p4 = new my_point(50, -50, -50);
            my_point p5 = new my_point(-50, -50, 50);
            my_point p6 = new my_point(-50, 50, 50);
            my_point p7 = new my_point(50, 50, 50);
            my_point p8 = new my_point(50, -50, 50);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2, 3 });
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            relationships.Add(1, new List<int>() { 0, 4, 5,1 });
            face f2 = new face(); f2.add(p1); f2.add(p2); f2.add(p6); f2.add(p5); shape.Add(f2);
            relationships.Add(2, new List<int>() { 4,6,7,5 });
            face f3 = new face(); f3.add(p5); f3.add(p6); f3.add(p7); f3.add(p8); shape.Add(f3);
            relationships.Add(3, new List<int>() { 2, 6, 7, 3 });
            face f4 = new face(); f4.add(p4); f4.add(p3); f4.add(p7); f4.add(p8); shape.Add(f4);
            relationships.Add(4, new List<int>() { 1, 5, 6,2 });
            face f5 = new face(); f5.add(p2); f5.add(p6); f5.add(p7); f5.add(p3); shape.Add(f5);
            relationships.Add(5, new List<int>() { 3, 7, 4,0 });
            face f6 = new face(); f6.add(p1); f6.add(p5); f6.add(p8); f6.add(p4); shape.Add(f6);
        }

        private void build_octahedron()
        {
            double a = Math.Sqrt(3) / 2 * 100;
            double p = (a + a + 100) / 2;
            double h = 2 * Math.Sqrt(p * (p - 100) * (p - a) * (p - a)) / 100;
            points.Clear();
            my_point p1 = new my_point(0, -h, 0);
            my_point p2 = new my_point(-50, 0, -50);
            my_point p3 = new my_point(0, h, 0);
            my_point p4 = new my_point(50, 0, -50);
            my_point p5 = new my_point(-50, 0, 50);
            my_point p6 = new my_point(50, 0, 50);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 1, 2, 3 });
            face f1 = new face(); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            relationships.Add(1, new List<int>() { 1, 0, 3 });
            face f2 = new face(); f2.add(p2); f2.add(p1); f2.add(p4); shape.Add(f2);
            relationships.Add(2, new List<int>() { 1, 2, 4 });
            face f3 = new face(); f3.add(p2); f3.add(p3); f3.add(p5); shape.Add(f3);
            relationships.Add(3, new List<int>() { 1, 0, 4 });
            face f4 = new face(); f4.add(p2); f4.add(p1); f4.add(p5); shape.Add(f4);
            relationships.Add(4, new List<int>() { 3, 2, 5 });
            face f5 = new face(); f5.add(p4); f5.add(p3); f5.add(p6); shape.Add(f5);
            relationships.Add(5, new List<int>() { 3, 0, 5 });
            face f6 = new face(); f6.add(p4); f6.add(p1); f6.add(p6); shape.Add(f6);
            relationships.Add(6, new List<int>() { 4, 2, 5 });
            face f7 = new face(); f7.add(p5); f7.add(p3); f7.add(p6); shape.Add(f7);
            relationships.Add(7, new List<int>() { 4, 0, 5 });
            face f8 = new face(); f8.add(p5); f8.add(p1); f8.add(p6); shape.Add(f8);
        }

 
        private void rotate_button_Click(object sender, EventArgs e) // поворот
        {
            double x_angle = ((double)x_rotate.Value * Math.PI) / 180;
            double y_angle = ((double)y_rotate.Value * Math.PI) / 180;
            double z_angle = ((double)z_rotate.Value * Math.PI) / 180;
            //rotate(x_angle, y_angle, z_angle);
            matr.get_transformed_my_points(matr.matrix_rotation_x_angular(x_angle), points);
            matr.get_transformed_my_points(matr.matrix_rotation_y_angular(y_angle), points);
            matr.get_transformed_my_points(matr.matrix_rotation_z_angular(z_angle), points);
            redraw_image();
        }

        
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            redraw_image();
        }

        private Tuple<int, int, int, int> find_min_max_XYpoint(face f)
        {
            int ind_max_X = 0;
            int ind_max_Y = 0;
            int ind_min_X = 0;
            int ind_min_Y = 0;

            for (int i = 1; i < f.points.Count(); ++i)
            {
                if(f.points[i].Y < f.points[ind_min_Y].Y)
                    ind_min_Y = i;

                if (f.points[i].X < f.points[ind_min_X].X)
                    ind_min_X = i;

                if (f.points[i].Y > f.points[ind_max_Y].Y)
                    ind_max_Y = i;

                if (f.points[i].X > f.points[ind_max_X].X)
                    ind_max_X = i;
            }
            return new Tuple<int, int, int, int>(ind_min_X, ind_max_X, ind_min_Y, ind_max_Y);
        }

        private double update_point(double t1, double t2, double q, double q1, double q2)
        {
            return t1 + (t2 - t1) * ((q - q1) / (q2 - q1));
        }

        private bool check_if_correctSize(double[,] buffer, int x)
        {
            return x >= 0 && x < pictureBox.Width;
        }

        private void update_pixel_by_texture(int x, int y, Bitmap img)
        {
            if (x - size_xx >= size_diff_x || x <= size_xx)
                return;
            color_buffer[x, y] = img.GetPixel((int)((x - size_xx)/size_diff_x*img.Width), img.Height-1-(int)((-y + size_yy)/ size_diff_y * img.Height));
        }

        private void make_pixel_line(ref double[,] z_buffer, int x1, int x2, int y, double za, double zb, int sign, Color color, Bitmap img)
        {
            for (int cur_X = x1; cur_X * sign <= x2 * sign; cur_X += sign)
            {
                double z = update_point(za, zb, cur_X, x1, x2);
                int x_cur_int = cur_X + centerX;
                if (check_if_correctSize(z_buffer, x_cur_int) && z_buffer[x_cur_int, y] < z && xOK((int)x_cur_int))
                {
                    z_buffer[x_cur_int, y] = z;
                    if (img == null)
                        color_buffer[x_cur_int, y] = color;
                    else
                        update_pixel_by_texture(x_cur_int, y, img);
                }
            }
        }

        private bool xOK(double x)
        {
            return x - size_xx <= size_diff_x && x - size_xx <= size_xx + size_diff_x;
        }

        private void set_pixel(ref double[,] z_buffer, int x, int y, double z)
        {
            if (check_if_correctSize(z_buffer, x) && z_buffer[x, y] <= z+5) //&& xOK((double)x))
            {
                z_buffer[x, y] = z;
                color_buffer[x, y] = Color.Red;
            }
        }

        private void update_indices(ref int up_ind, ref int down_ind, int count, int sign)
        {
            up_ind = down_ind;
            down_ind = (count + down_ind + sign) % count;
        }

        private void update_color_map(face f, double cur_pointXa, double cur_pointXb, int Y, double za, double zb, Color color, ref double[,] z_buffer, Bitmap img)
        {
            int Xa = (int)Math.Round(cur_pointXa);
            int Xb = (int)Math.Round(cur_pointXb);
            int sign = Math.Sign(Xb - Xa);

            //set_pixel(ref z_buffer, Xa, Y, za);
            if (sign != 0)
            {
                make_pixel_line(ref z_buffer, Xa + sign, Xb - sign, Y, za, zb, sign, color, img);
                //set_pixel(ref z_buffer, Xb, Y, zb);
            }
        }

        private void swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        private void draw_line(double x0_d, double y0_d, double x1_d, double y1_d, double z0, double z1, ref double[,] z_buffer)
        {
            int x0 = (int)Math.Round(x0_d);
            int x1 = (int)Math.Round(x1_d);
            int y0 = (int)Math.Round(y0_d);
            int y1 = (int)Math.Round(y1_d);
            if (x0 > x1)
            {
                swap(ref x0, ref x1);
                swap(ref y0, ref y1);
                swap(ref z0, ref z1);
            }
            int sign_x = Math.Sign(x1 - x0);
            int sign_y = Math.Sign(y1 - y0);
            if (sign_x == 0)
            {
                for(int y = y0; y * sign_y <= y1 * sign_y; y += sign_y)
                {
                    double z = update_point(z0, z1, y, y0, y1);
                    set_pixel(ref z_buffer, x0 + centerX, -y + centerY, z);
                }
                return;
            }
            
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            double grad = dy / (double)dx;
            if(grad > 1)
            {
                int di = 2 * dx - dy;
                int x = x0;
                double yd = y0_d;
                for (int y = y0; y * sign_y <= y1 * sign_y; y += sign_y)
                {
                    double z = update_point(z0, z1, y, y0, y1);
                    set_pixel(ref z_buffer, x + centerX, -y + centerY, z);
                    if (di < 0)
                        di += 2 * dx;
                    else
                    {
                        x += sign_x;
                        di += 2 * (dx - dy);
                    }
                    y0_d += sign_y;
                }
            }
            else
            {
                int di = 2 * dy - dx;
                int y = y0;
                for (int x = x0; x * sign_x <= x1 * sign_x; x += sign_x)
                {
                    double z = update_point(z0, z1, x, x0, x1);
                    set_pixel(ref z_buffer, x + centerX, -y + centerY, z);
                    if (di < 0)
                        di += 2 * dy;
                    else
                    {
                        y += sign_y;
                        di += 2 * (dy - dx);
                    }
                }
            }
        }

        private void draw_all_edges(face f, ref double[,] z_buffer)
        {
            for (int i = 0; i < f.points.Count(); i++)
            {
                int ind_next = (i + 1) % f.points.Count();
                int x0 = (int)Math.Round(f.points[i].X);
                int x1 = (int)Math.Round(f.points[ind_next].X);
                int y0 = (int)Math.Round(f.points[i].Y);
                int y1 = (int)Math.Round(f.points[ind_next].Y);
                double by0 = f.points[i].Y;
                double by1 = f.points[ind_next].Y;
                double t0 = f.points[i].Y;
                double t1 = f.points[ind_next].Y;
                if (y0 == y1)
                {
                    by0 = f.points[i].X;
                    by1 = f.points[ind_next].X;
                    t0 = f.points[i].X;
                    t1 = f.points[ind_next].X;
                }
                if(by0 == by1)
                    continue;
                double z0 = update_point(f.points[i].Z, f.points[ind_next].Z, t0, by0, by1);
                double z1 = update_point(f.points[i].Z, f.points[ind_next].Z, t1, by0, by1);
                draw_line(f.points[i].X, f.points[i].Y, f.points[ind_next].X, f.points[ind_next].Y, z0, z1, ref z_buffer);
            }
        }

        //Create map of colors to draw pixels in face
        private void set_texture_with_Zbuffer(face f, ref double[,] z_buffer)
        {
            Tuple<int, int, int, int> min_maxY = find_min_max_XYpoint(f);
            double cur_y = f.points[min_maxY.Item4].Y;
            double cur_pointXa = (int)Math.Round(f.points[min_maxY.Item4].X);
            double cur_pointXb = (int)Math.Round(f.points[min_maxY.Item4].X);

            size_diff_x = f.points[min_maxY.Item2].X - f.points[min_maxY.Item1].X;
            size_diff_y = f.points[min_maxY.Item4].Y - f.points[min_maxY.Item3].Y;

            int size_x = (int)Math.Round(f.points[min_maxY.Item2].X - f.points[min_maxY.Item1].X) + 1;
            int size_y = (int)Math.Round(f.points[min_maxY.Item4].Y - f.points[min_maxY.Item3].Y) + 1;
            size_xx = f.points[min_maxY.Item1].X + centerX;
            size_yy = -f.points[min_maxY.Item3].Y + centerY;
            Bitmap img = null;

            if (is_texturing)
                img = new Bitmap(Image.FromFile(openFileDialog_texture.FileName), size_x, size_y); //probably worth changing

            int up_ind_a = min_maxY.Item4;
            int up_ind_b = min_maxY.Item4;
            int down_ind_a = (f.points.Count() + min_maxY.Item4 - 1)% f.points.Count();
            int down_ind_b = (min_maxY.Item4 + 1) % f.points.Count();

            //While not bottom y
            while (Math.Round(cur_y) >= Math.Round(f.points[min_maxY.Item3].Y))
            {
                int Y = (int)Math.Round(-cur_y) + centerY;
                if (Y < 0 || Y >= pictureBox.Height)
                    break;

                double za = update_point(f.points[up_ind_a].Z, f.points[down_ind_a].Z, (int)Math.Round(cur_y), (int)Math.Round(f.points[up_ind_a].Y), (int)Math.Round(f.points[down_ind_a].Y));
                double zb = update_point(f.points[up_ind_b].Z, f.points[down_ind_b].Z, (int)Math.Round(cur_y), (int)Math.Round(f.points[up_ind_b].Y), (int)Math.Round(f.points[down_ind_b].Y));
                update_color_map(f, cur_pointXa, cur_pointXb, Y, za, zb, Color.White, ref z_buffer, img);

                //update all coordinates
                cur_y--;
                if (cur_y <= f.points[down_ind_a].Y)
                    update_indices(ref up_ind_a, ref down_ind_a, f.points.Count(), -1);

                if (cur_y <= f.points[down_ind_b].Y)
                    update_indices(ref up_ind_b, ref down_ind_b, f.points.Count(), 1);

                cur_pointXa = (int)Math.Round(update_point(f.points[up_ind_a].X, f.points[down_ind_a].X, (int)Math.Round(cur_y), f.points[up_ind_a].Y, f.points[down_ind_a].Y));
                cur_pointXb = (int)Math.Round(update_point(f.points[up_ind_b].X, f.points[down_ind_b].X, (int)Math.Round(cur_y), f.points[up_ind_b].Y, f.points[down_ind_b].Y));
            }
            draw_all_edges(f, ref z_buffer);
        }

        //create color map
        private void build_pixels_to_draw()
        {
            double[,] z_buffer = new double[pictureBox.Width, pictureBox.Height];
            for (int i = 0; i < pictureBox.Width; ++i)
                for (int j = 0; j < pictureBox.Height; ++j)
                {
                    color_buffer[i, j] = Color.White;
                    z_buffer[i, j] = Double.MinValue;
                }

            foreach (face f in shape)
                set_texture_with_Zbuffer(f, ref z_buffer);
        }

      

        //load texture
        private void load_texture_Click(object sender, EventArgs e)
        {
            if(shape.Count() == 0)
            {
                MessageBox.Show("Сначала выберите фигуру для текстурирования");
                return;
            }
            if (openFileDialog_texture.ShowDialog() == DialogResult.OK)
            {
                is_texturing = true;
                redraw_image();
            } 
        }

        //delete texture
        private void delete_texture_Click(object sender, EventArgs e)
        {
            is_texturing = false;
            redraw_image();
        }

        private void shape_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            if ((sender as RadioButton).Checked == false)
                return;
            shape.Clear();
            if (tetrahedron.Checked)
                build_tetrahedron();
            else if (hexahedron.Checked)
                build_hexahedron();
            else if (octahedron.Checked)
                build_octahedron();
            build_points();
            redraw_image();   
        }
    }
}
