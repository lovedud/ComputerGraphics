using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGB_to_gray
{
    public partial class Form1 : Form
    {
        string path;
        Bitmap bm;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileD = new OpenFileDialog();
            openfileD.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*;";
            if (openfileD.ShowDialog() == DialogResult.OK)
            {
                path = openfileD.FileName;
                bm = new Bitmap(path);
                button2.Visible = true;
                label1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
            }
        }

        private Bitmap PictureToGray(Bitmap bm, Func<Color, Color> f_gray)
        {
            Bitmap gray_bm = new Bitmap(bm.Width, bm.Height);
            
            for(int i = 0; i < bm.Width; i++)
                for(int j = 0; j < bm.Height; j++)
                {
                    Color pixel = bm.GetPixel(i, j);
                    gray_bm.SetPixel(i, j, f_gray(pixel));
                }
            return gray_bm;
        }

        private Bitmap ImageSubtraction(Bitmap bm1, Bitmap bm2)
        {
            int w = bm1.Width;
            int h = bm1.Height;
            Bitmap res = new Bitmap(w, h);
            MinMaxSubstraction(bm1, bm2, out int min, out int max);
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color pixel1 = bm1.GetPixel(i, j);
                    Color pixel2 = bm2.GetPixel(i, j);
                    res.SetPixel(i, j, PixelSubtraction(pixel1, pixel2, min, max));
                }
            return res;
        }

        private void MinMaxSubstraction(Bitmap bm1, Bitmap bm2, out int min, out int max)
        {
            int w = bm1.Width;
            int h = bm1.Height;
            min = int.MaxValue;
            max = int.MinValue;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color pixel1 = bm1.GetPixel(i, j);
                    Color pixel2 = bm2.GetPixel(i, j);
                    int substr = pixel1.G - pixel2.G;
                    min = Math.Min(min, substr);
                    max = Math.Max(max, substr);
                }
        }

        private Color PixelSubtraction(Color p1, Color p2, int min, int max)
        {
            double k = 255 / (max - min);
            int sub_value =(int) ((p1.G - p2.G - min) * k);
            return Color.FromArgb(sub_value, sub_value, sub_value);
        }

        private Color ToGray(Color pixel)
        {
            int y =(int)( 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
            return Color.FromArgb(y,y,y);
        }
        private Color ToGrayHuman(Color pixel)
        {
            int y = (int)(0.2126 * pixel.R + 0.7152 * pixel.G + 0.0722 * pixel.B);
            return Color.FromArgb(y, y, y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(PictureToGray(bm, ToGray));
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(PictureToGray(bm, ToGrayHuman));
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(bm);
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var bm1 = PictureToGray(bm, ToGray);
            var bm2 = PictureToGray(bm, ToGrayHuman);
            Form2 f2 = new Form2(ImageSubtraction(bm1, bm2));
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(PictureToGray(bm, ToGray), 1);
            f3.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(PictureToGray(bm, ToGrayHuman), 1);
            f3.Show();
        }
    }
}
